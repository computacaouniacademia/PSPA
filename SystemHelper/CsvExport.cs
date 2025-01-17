﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;

namespace SystemHelper
{
    public class CsvExport : IDisposable
    {
        /// <summary>
        /// To keep the ordered list of column names
        /// </summary>
        List<string> _fields = new List<string>();

        /// <summary>
        /// The list of rows
        /// </summary>
        List<Dictionary<string, object>> _rows = new List<Dictionary<string, object>>();

        /// <summary>
        /// The current row
        /// </summary>
        Dictionary<string, object> _currentRow { get { return _rows[_rows.Count - 1]; } }

        /// <summary>
        /// The string used to separate columns in the output
        /// </summary>
        private readonly string _columnSeparator;

        /// <summary>
        /// Whether to include the preamble that declares which column separator is used in the output
        /// </summary>
        private bool _includeColumnSeparatorDefinitionPreamble;

        /// <summary>
        /// Initializes a new instance of the <see cref="Jitbit.Utils.CsvExport"/> class.
        /// </summary>
        /// <param name="columnSeparator">
        /// The string used to separate columns in the output.
        /// By default this is a comma so that the generated output is a CSV file.
        /// </param>
        /// <param name="includeColumnSeparatorDefinitionPreamble">
        /// Whether to include the preamble that declares which column separator is used in the output.
        /// By default this is <c>true</c> so that Excel can open the generated CSV
        /// without asking the user to specify the delimiter used in the file.
        /// </param>
        public CsvExport(string columnSeparator = ",", bool includeColumnSeparatorDefinitionPreamble = true)
        {
            _columnSeparator = columnSeparator;
            _includeColumnSeparatorDefinitionPreamble = includeColumnSeparatorDefinitionPreamble;
        }

        /// <summary>
        /// Set a value on this column
        /// </summary>
        public object this[string field]
        {
            set
            {
                // Keep track of the field names, because the dictionary loses the ordering
                if (!_fields.Contains(field)) _fields.Add(field);
                _currentRow[field] = value;
            }
        }

        /// <summary>
        /// Call this before setting any fields on a row
        /// </summary>
        public void AddRow()
        {
            _rows.Add(new Dictionary<string, object>());
        }

        /// <summary>
        /// Add a list of typed objects, maps object properties to CsvFields
        /// </summary>
        public void AddRows<T>(IEnumerable<T> list)
        {
            if (list.Any())
            {
                foreach (var obj in list)
                {
                    AddRow();
                    var values = obj.GetType().GetProperties();
                    foreach (var value in values)
                    {
                        this[value.Name] = value.GetValue(obj, null);
                    }
                }
            }
        }

        /// <summary>
        /// Converts a value to how it should output in a csv file
        /// If it has a comma, it needs surrounding with double quotes
        /// Eg Sydney, Australia -> "Sydney, Australia"
        /// Also if it contains any double quotes ("), then they need to be replaced with quad quotes[sic] ("")
        /// Eg "Dangerous Dan" McGrew -> """Dangerous Dan"" McGrew"
        /// </summary>
        /// <param name="columnSeparator">
        /// The string used to separate columns in the output.
        /// By default this is a comma so that the generated output is a CSV document.
        /// </param>
        public static string MakeValueCsvFriendly(object value, string columnSeparator = ",")
        {
            if (value == null) return "";
            if (value is INullable && ((INullable)value).IsNull) return "";
            if (value is DateTime)
            {
                if (((DateTime)value).TimeOfDay.TotalSeconds == 0)
                    return ((DateTime)value).ToString("yyyy-MM-dd");
                return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string output = value.ToString().Trim();

            if (output.Length > 30000) //cropping value for stupid Excel
                output = output.Substring(0, 30000);

            if (output.Contains(columnSeparator) || output.Contains("\"") || output.Contains("\n") || output.Contains("\r"))
                output = '"' + output.Replace("\"", "\"\"") + '"';

            return output;
        }

        /// <summary>
        /// Outputs all rows as a CSV, returning one string at a time
        /// </summary>
        private IEnumerable<string> ExportToLines(bool includeHeader = false)
        {
            if (_includeColumnSeparatorDefinitionPreamble) yield return "sep=" + _columnSeparator;

            // The header
            if (includeHeader)
                yield return string.Join(_columnSeparator, _fields.Select(f => MakeValueCsvFriendly(f, _columnSeparator)));

            // The rows
            foreach (Dictionary<string, object> row in _rows)
            {
                foreach (string k in _fields.Where(f => !row.ContainsKey(f)))
                {
                    row[k] = null;
                }
                yield return string.Join(_columnSeparator, _fields.Select(field => MakeValueCsvFriendly(row[field], _columnSeparator)));
            }
        }

        /// <summary>
        /// Output all rows as a CSV returning a string
        /// </summary>
        public string Export(Boolean includeHeader = false)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string line in ExportToLines(includeHeader))
            {
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Exports to a file
        /// </summary>
        public void ExportToFile(string path, Boolean includeHeader = false)
        {
            var encoding = Encoding.GetEncoding("ISO-8859-1");
            File.AppendAllLines(path, ExportToLines(includeHeader), encoding);
        }

        public void AddLinesToFile(string path, Boolean includeHeader = false)
        {
            this._includeColumnSeparatorDefinitionPreamble = false;
            ExportToFile(path, includeHeader);
        }

        /// <summary>
        /// Exports as raw UTF8 bytes
        /// </summary>
        public byte[] ExportToBytes(Boolean includeHeader = false)
        {
            var encoding = Encoding.GetEncoding("ISO-8859-1");
            var data = encoding.GetBytes(Export(includeHeader));
            return encoding.GetPreamble().Concat(data).ToArray();
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar chamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (this._rows != null)
                        this._rows = null;

                    if (this._fields != null)
                        this._fields = null;

                }

                // TODO: liberar recursos não gerenciados (objetos não gerenciados) e substituir um finalizador abaixo.
                // TODO: definir campos grandes como nulos.

                disposedValue = true;
            }
        }

        // TODO: substituir um finalizador somente se Dispose(bool disposing) acima tiver o código para liberar recursos não gerenciados.
        // ~CsvExport()
        // {
        //   // Não altere este código. Coloque o código de limpeza em Dispose(bool disposing) acima.
        //   Dispose(false);
        // }

        // Código adicionado para implementar corretamente o padrão descartável.
        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza em Dispose(bool disposing) acima.
            Dispose(true);
            // TODO: remover marca de comentário da linha a seguir se o finalizador for substituído acima.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}