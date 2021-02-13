using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodingChallenge2
{
    /* Scenario: We have a flat file from a client that contains a few address columns in a CSV format:
     * Address Number, Address Direction, Address Street, Address Unit, Address City State, Address Zip.
     * Example data: 11102, North, CENTINELA AVE, 100A, “Los Angeles, CA”, 90403
     * IPS requires that the AVE be in its own field called Suffix and that city and state be placed in their own City and State fields.
     */
    class Program
    {
        static void Main(string[] args)
        {
            string updatedCSVString = ConvertCSV();
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\UpdatedCSVFile\UpdatedCSVFileInsightGlobal.csv");
            // Below a new file is created with 'updatedCSVString' written to a csv file. 
            // The file can be found in the Solution Explore in the'UpdatedCSVFile' folder.
            File.WriteAllText(path, updatedCSVString);
        }
        /// <summary>
        /// Parses a csv file, converts the file into a list and returns the manipulated list of strings.
        /// </summary>
        /// <returns>
        /// List of strings.
        /// </returns>
        public static string ConvertCSV()
        {
            StringBuilder newCSVContent = new StringBuilder();
            // Resource I used to accomplish relative path:
            // https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getcurrentdirectory?redirectedfrom=MSDN&view=net-5.0#System_IO_Directory_GetCurrentDirectory
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestFile\CSVFileInsightGlobal.csv");
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 0)
                {
                    List<string> row1List = ProcessRow(lines[0]);
                    List<string> updatedRow1Lst = UpdateRow1(row1List);
                    foreach (string item in updatedRow1Lst)
                    {
                        newCSVContent.Append(item);
                    }
                    newCSVContent.Append("\n");
                }
                if (i == 1)
                {
                    List<string> row2List = ProcessRow(lines[1]);
                    List<string> updatedRow2Lst = UpdateRow2(row2List);
                    foreach (string item in updatedRow2Lst)
                    {
                        newCSVContent.Append(item);
                    }
                }
            }
            return newCSVContent.ToString();
        }
        /// <summary>
        /// Modifies a list by inserting and removing strings from a list at a given index.
        /// </summary>
        /// <param name="row2List"></param>
        /// <returns>
        /// List of strings.
        /// </returns>
        private static List<string> UpdateRow2(List<string> row2List)
        {
            int commaCnt = 0;
            for (int k = 0; k < row2List.Count; k++)
            {
                if (row2List[k] == "\"")
                {
                    row2List.RemoveAt(k);
                }
                if (row2List[k] == ",")
                {
                    commaCnt++;
                    if (commaCnt == 3)
                    {
                        row2List.RemoveAt(k - 2);
                        row2List.Insert(k - 2, ",");
                    }
                    if (commaCnt == 6)
                    {
                        row2List.RemoveAt(k - 2);
                    }
                }
            }
            return row2List;
        }
        /// <summary>
        /// Modifies a list by inserting strings into a list at a given index.
        /// </summary>
        /// <param name="row1List"></param>
        /// <returns>
        /// List of strings.
        /// </returns>
        private static List<string> UpdateRow1(List<string> row1List)
        {
            string suffix = "Suffix";
            string state = "Address";
            int commaCnt = 0;
            for (int k = 0; k < row1List.Count; k++)
            {
                if (row1List[k] == ",")
                {
                    commaCnt++;
                    if (commaCnt == 3)
                    {
                        row1List.Insert(k + 1, suffix);
                        row1List.Insert(k + 2, ",");
                    }
                }
                else if (row1List[k] == "City")
                {
                    row1List.Insert(k + 1, ",");
                    row1List.Insert(k + 2, state);
                }
            }
            return row1List;
        }
        /// <summary>
        /// Splits apart an array by spaces and punctuation.
        /// </summary>
        /// <param name="rowString"></param>
        /// <returns>
        /// A list of strings.
        /// </returns>
        private static List<string> ProcessRow(string rowString)
        {
            List<string> rowList = new List<string>();
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < rowString.Length; j++)
            {
                if (rowString[j] == ' ' || rowString[j] == ',' || rowString[j] == '"')
                {
                    // This condition prevents an unwanted empty space from being added 
                    // to 'rowList' when 'sb' is empty.
                    if (sb.Length != 0)
                    {
                        rowList.Add(sb.ToString());
                    }
                    rowList.Add(rowString[j].ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(rowString[j]);
                }
            }
            // This condition is needed because the algorithm above relies on ',' '"', and ' ' in order to add
            // the string that was created by the stored characters in 'sb' to 'rowList'.
            // In conclusion, without the code below the last word in 'sb' would not be added to the 'rowList'.
            if (sb.Length != 0)
            {
                rowList.Add(sb.ToString());
                sb.Clear();
            }
            return rowList;
        }
    }
}
