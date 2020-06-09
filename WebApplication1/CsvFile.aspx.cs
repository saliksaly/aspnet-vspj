using CsvHelper;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

namespace WebApplication1
{
    public partial class CsvFile : System.Web.UI.Page
    {
        private readonly IDbConnection _dbConnection = 
            new MySqlConnection(ConfigurationManager.ConnectionStrings["domkat"].ConnectionString);


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnDownload_OnClick(object sender, EventArgs e)
        {
            // Inspirace:
            //  How to export CSVs with ASP.NET and C#
            //  https://blog.hildenco.com/2018/03/exporting-csv-generated-in-memory-in.html
            //  Dapper ORM
            //  https://github.com/StackExchange/Dapper
            //  CsvHelper
            //  https://joshclose.github.io/CsvHelper/

            string dbTableName = dlDbTables.SelectedValue;
            string fileName = tbFileName.Text;

            // Případ: Všechny sloupce
            var records = QueryDatabase(dbTableName);

            // Případ: Jen vybrané sloupce mapované do DTO
            //var records = QueryDatabase<MessageDto>(dbTableName);

            SendCsvFile(ConvertToCsv(records), fileName);
        }


        private IEnumerable<dynamic> QueryDatabase(string tableName) =>
            _dbConnection.Query(GetSqlQuery(tableName));

        private IEnumerable<TEntity> QueryDatabase<TEntity>(string tableName) =>
            _dbConnection.Query<TEntity>(GetSqlQuery(tableName));

        private static string GetSqlQuery(string tableName) =>
            $"SELECT * FROM {tableName} LIMIT 20";


        private static string ConvertToCsv(IEnumerable records)
        {
            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                var csv = new CsvWriter(sw, CultureInfo.InvariantCulture);

                csv.WriteRecords(records);
            }

            return sb.ToString();
        }

        private void SendCsvFile(string csv, string fileName)
        {
            // Inspirace:
            // https://stackoverflow.com/questions/39500102/csv-file-export-with-encoding-issues
            
            //Response.Clear();
            //Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, Encoding.UTF8) + "");
            Response.Charset = "";
            Response.ContentType = "application/csv";
            Response.ContentEncoding = Encoding.UTF8;
            Response.BinaryWrite(Encoding.UTF8.GetPreamble());
            Response.Write(csv);
            Response.Flush();
            Response.End();
        }
    }

    class MessageDto
    {
        public DateTime Time { get; set; }
        public string Id_VyrobniCislo_RJ { get; set; }
    }
}