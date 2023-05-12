using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CoursLoadAccounting
{
    public class PostgresDataBase
    {
        private NpgsqlConnection conn;

        private Dictionary<int, string> tables = new Dictionary<int, string>()
        {
            [0] = "uchet_fulltable",
            [1] = "OrgStructur",
            [2] = "Facultets",
            [3] = "Members",
            [4] = "Discip",
            [5] = "Spec"
        };
        public PostgresDataBase(string userId, string password)
        {
            string connString = $"Server=localhost;Database=UniversityWorkload;User Id={userId};Password={password};";
            conn = new NpgsqlConnection(connString);
        }

        public void Open()
        {
            conn.Open();
        }

        public void Close()
        {
            conn.Close();
        }

        public bool IsOpen()
        {
            return conn.State == ConnectionState.Open;
        }

        public object ExecuteScalar(string sqlCommand)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, conn);
            return cmd.ExecuteScalar();
        }

        public void ExecuteNonQuery(string sqlCommand)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public NpgsqlDataReader ExecuteQuery(string sqlCommand)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, conn);
            return cmd.ExecuteReader();
        }

        public DataTable RefreshDB(string nameTable)
        {
            var commandText = $"SELECT * FROM {nameTable}";

            NpgsqlDataReader reader = ExecuteQuery(commandText); //.ExecuteReader
            var dataTable = new DataTable();

            dataTable.Load(reader);

            return dataTable;
        }

        public DataTable GetTable(int ind)
        {
            return RefreshDB(tables[ind]);
        }

        public DataTable SearchTable(int ind, string search)
        {
            
            string cmd = $"SELECT * FROM {tables[ind]} WHERE";

            NpgsqlDataReader reader = ExecuteQuery($"SELECT * FROM {tables[ind]} LIMIT 1");
            
            for (int i = 0; i < reader.FieldCount-1; i++)
            {
                cmd += $" \"{reader.GetName(i)}\" ILIKE '%{search}%' OR";
            }

            cmd += $" \"{reader.GetName(reader.FieldCount - 1)}\" ILIKE '%{search}%'";

            reader.Close();
           
            NpgsqlDataReader rreader = ExecuteQuery(cmd);
               
            var dataTable = new DataTable();

            dataTable.Load(rreader);

            return dataTable;
           
        }

      
    }
}
