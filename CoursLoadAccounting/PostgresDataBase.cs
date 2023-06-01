﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace CoursLoadAccounting
{
    public class PostgresDataBase
    {
        private NpgsqlConnection conn;

        private Dictionary<int, string> tablesView = new Dictionary<int, string>()
        {
            [0] = "uchet_fulltable",
            [1] = "OrgStructur",
            [2] = "Facultets",
            [3] = "Members",
            [4] = "Discip",
            [5] = "Spec"
        };

        private Dictionary<int, string> tables = new Dictionary<int, string>()
        {
            [0] = "DisciplineSpeciality",
            [1] = "Kafedra",
            [2] = "Faculty",
            [3] = "departmentmembers",
            [4] = "Discipline",
            [5] = "Speciality"
        };

        private Dictionary<int, string> procedursDelete = new Dictionary<int, string>()
        {
            [0] = "discipline_speciality",
            [1] = "kafedra",
            [2] = "faculty",
            [3] = "departmentmember",
            [4] = "discipline",
            [5] = "speciality"
        };

        private Dictionary<int, string> taskView = new Dictionary<int, string>()
        {
            [0] = "SubQueryFrom",
            [1] = "SubQueryWhere",
            [2] = "SubQueryCorrelated",
            [3] = "QueryHaving",
            [4] = "QueryAny",
            [5] = "QueryAll",
            [6] = "get_department_members_with_email()",
            [7] = "Spec"
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
            return RefreshDB(tablesView[ind]);
        }

        public DataTable SearchTable(int ind, string search)
        {
            
            string cmd = $"SELECT * FROM {tablesView[ind]} WHERE";

            NpgsqlDataReader reader = ExecuteQuery($"SELECT * FROM {tablesView[ind]} LIMIT 1");
            
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

        public void Delete(int tableID, int numberOfString)
        {
            Open();

            int idStr = GetId(tableID, numberOfString);
            
            ExecuteNonQuery($"CALL delete_{procedursDelete[tableID]}({idStr})");

            Close();
        }

        public int GetId(int tableID, int numberOfString)
        {           
            int idStr = -1;

            using (var reader = ExecuteQuery($"SELECT * FROM {tables[tableID]} LIMIT {numberOfString + 1}"))
            {
                int i = 0;

                while (reader.Read())
                {
                    if (i == numberOfString)
                    {
                        idStr = (int)reader["id"];
                    }

                    i++;
                }
            }

            return idStr;
        }

        public DataTable GetTableForTask(int taskID)
        {            
            NpgsqlDataReader reader = ExecuteQuery($"SELECT * FROM {taskView[taskID]}");

            var dataTable = new DataTable();

            dataTable.Load(reader);

            return dataTable;
        }

        public DataTable GetTableForScalarFunc(string phone)
        {
            NpgsqlDataReader reader = ExecuteQuery($"SELECT CASE WHEN CheckMembersPhone(format_phone_number('{phone}')) = true THEN 'Номер занят' ELSE 'Номер свободен' END as \"Состояние\"");

            var dataTable = new DataTable();

            dataTable.Load(reader);

            return dataTable;
        }

        public void CursorSwap()
        {
            ExecuteNonQuery("SELECT swap_lec_pr_hours()");    
        }

        
    }
}
