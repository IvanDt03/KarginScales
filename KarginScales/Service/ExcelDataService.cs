using KarginScales.Models;
using System.Collections.Generic;
using ClosedXML.Excel;
using System;
using System.Linq;

namespace KarginScales.Service;

public static class ExcelDataService 
{
    public static string ErrorMessage = "";
    public static List<Polymer> LoadDataFromFile(string path)
    {
        try
        {
            var result = new List<Polymer>();
            using XLWorkbook wb = new XLWorkbook(path);

            foreach (var ws in wb.Worksheets)
            {
                var polymer = new Polymer(ws.Name, GetListData(ws.RangeUsed()).ToList());
                result.Add(polymer);
            }

            return result;
        }
        catch(Exception ex)
        {
            ErrorMessage = $"Ошибка при загрузке данных из Excel: {ex.Message}";
            return null;
        }
    }

    private static IEnumerable<DataPoint> GetListData(IXLRange? range)
    {
        for (int row = 2; row <= range?.RowCount(); ++row)
        {
            DataPoint point = new DataPoint
            {
                Temperature = range.Cell(row, 1).GetDouble(),
                Gamma = range.Cell(row, 2).GetDouble()
            };

            yield return point;
        }
    }
}
