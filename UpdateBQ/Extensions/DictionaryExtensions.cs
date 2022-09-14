using Google.Cloud.BigQuery.V2;
using System.Collections.Generic;

namespace UpdateBQ.Extensions
{
    public static class DictionaryExtensions
    {
        public static List<BigQueryInsertRow> ToBigQueryRecord(this IDictionary<string, string> dictionary)
        {
            var rows = new List<BigQueryInsertRow>();
            if (dictionary == null)
                return rows;

            foreach (var property in dictionary)
            {
                var row = new BigQueryInsertRow
                {
                    { "key", property.Key },
                    { "value", property.Value }
                };

                rows.Add(row);
            }

            return rows;
        }
    }
}
