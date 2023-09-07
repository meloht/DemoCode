using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsExcel
{
    public enum ExcelExt
    {
        Xls = 0,
        Xlsx = 1
    }
    public class NPOIHelper
    {
        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <param name="oFile"></param>
        /// <returns>返回的DataTable</returns>
        public static DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn, string filePath)
        {
            int rowIndex = isFirstRowColumn ? 0 : 1;

            return ExcelToDataTable(sheetName, rowIndex, filePath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="firstRowColumn">数据开始的行索引，从0开始</param>
        /// <param name="oFile"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string sheetName, int firstRowColumn, string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            if (string.IsNullOrEmpty(sheetName))
            {
                throw new ArgumentNullException(sheetName);
            }
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(fileName);
            }

            using (var fileStream = File.Open(filePath, FileMode.Open))
            {
                var data = new DataTable();
                IWorkbook workbook = null;

                try
                {
                    //fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    if (fileName.IndexOf(".xlsx", StringComparison.Ordinal) > 0)
                    {
                        workbook = new XSSFWorkbook(fileStream);
                    }
                    else if (fileName.IndexOf(".xls", StringComparison.Ordinal) > 0)
                    {
                        workbook = new HSSFWorkbook(fileStream);
                    }

                    ISheet sheet = null;
                    if (workbook != null)
                    {
                        //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                        sheet = workbook.GetSheet(sheetName) ?? workbook.GetSheetAt(0);
                    }
                    if (sheet == null)
                        return data;

                    GetDataTable(data, firstRowColumn, sheet);

                    return data;
                }
                catch (IOException ioex)
                {
                    throw new IOException(ioex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (fileStream != null)
                    {
                        fileStream.Close();
                    }
                    if (workbook != null)
                    {
                        workbook.Close();
                    }
                }
            }

        }

        private static void GetDataTable(DataTable data, int firstRowColumn, ISheet sheet)
        {
            var firstRow = sheet.GetRow(firstRowColumn);
            //一行最后一个cell的编号 即总的列数
            int cellCount = firstRow.LastCellNum;
            int startRow = firstRowColumn + 1;

            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
            {
                var cell = firstRow.GetCell(i);
                cell.SetCellType(CellType.String);
                var cellValue = cell.StringCellValue;
                if (cellValue == null) continue;
                var column = new DataColumn(cellValue);
                data.Columns.Add(column);
            }

            //最后一列的标号
            var rowCount = sheet.LastRowNum;
            for (var i = startRow; i <= rowCount; ++i)
            {
                var row = sheet.GetRow(i);
                //没有数据的行默认是null
                if (row == null) continue;
                var dataRow = data.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; ++j)
                {
                    //同理，没有数据的单元格都默认是null
                    if (row.GetCell(j) != null)
                    {
                        ICell cell = row.GetCell(j);
                        if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                        {
                            dataRow[j] = cell.DateCellValue.ToString().Trim();
                        }
                        else
                        {
                            dataRow[j] = cell.ToString().Trim();
                        }
                    }
                }
                data.Rows.Add(dataRow);
            }
        }


        public static void DataTableToExcel(string sheetName, DataTable dt, ExcelExt excelExt, Stream outStream)
        {
            NPOI.SS.UserModel.IWorkbook book = null;
            try
            {

                if (excelExt == ExcelExt.Xls)
                {
                    book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                }
                else
                {
                    book = new NPOI.XSSF.UserModel.XSSFWorkbook();
                }

                NPOI.SS.UserModel.ISheet sheet = book.CreateSheet(sheetName);

                // 添加表头
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
                int index = 0;
                foreach (DataColumn item in dt.Columns)
                {
                    NPOI.SS.UserModel.ICell cell = row.CreateCell(index);
                    cell.SetCellType(NPOI.SS.UserModel.CellType.String);
                    cell.SetCellValue(item.ColumnName);
                    index++;
                }

                // 添加数据
                int num = dt.Rows.Count;
                for (int i = 0; i < num; i++)
                {
                    index = 0;
                    row = sheet.CreateRow(i + 1);
                    foreach (DataColumn item in dt.Columns)
                    {
                        NPOI.SS.UserModel.ICell cell = row.CreateCell(index);
                        cell.SetCellType(NPOI.SS.UserModel.CellType.String);
                        cell.SetCellValue(dt.Rows[i][item].ToString());
                        index++;
                    }
                }

                book.Write(outStream);
                book = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if(book!=null)
                {
                    book.Close();
                }
            }

        }

    }
}
