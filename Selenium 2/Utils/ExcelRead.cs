using ExcelDataReader;
using Selenium_2;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_2
{
    public class ExcelRead
    {
        public static List<Datacollection> dataCol = new List<Datacollection>();

        //En caso de tener mas de una hoja en el excel se debera enviar por parametro un string para "Sheet1"
        public static DataTable ExcelToDataTable(string fileName, string sheet)
        {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    //Get all the Tables
                    DataTableCollection table = result.Tables;

                    //Store it in DataTable
                    DataTable resultTable = table[sheet];

                    //return
                    return resultTable;
                }
            }
        }

        public void PopulateInCollection(string fileName, string sheet = "Sheet1")
        {
            DataTable table = ExcelToDataTable(fileName, sheet);

            //itera entre filas y columnas de la tabla
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    Datacollection dtTable = new Datacollection()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row - 1][col].ToString()
                    };
                    //Add all the details for each row
                    //agrega los detalles de cada fila
                    dataCol.Add(dtTable);
                }
            }
        }

        //Se usa 
        public string ReadData(int rowNumber, string columnName)
        {
            try
            {
                //Esto es LinQ
                string data = (from colData in dataCol
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colValue).SingleOrDefault();

                return data.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }

    public class Datacollection
    {
        public int rowNumber { get; set; }
        public string colName { get; set; }
        public string colValue { get; set; }
    }
}
