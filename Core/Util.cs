﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Core
{
    public static class Util
    {
        public static T ConvertFromDBNull<T>(object value)
        {
            return value == DBNull.Value ? default(T) : (T)Convert.ChangeType(value, typeof(T));
        }

        public static DataTable ConvertToDataTable<T>(IEnumerable<T> data)
        {
            var properties =
               TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

    }
}
