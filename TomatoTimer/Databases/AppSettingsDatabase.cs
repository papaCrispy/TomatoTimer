using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TomatoTimer.Databases
{
    public class AppSettingsDatabase : IDatabaseManager
    {
        private SqliteConnection _SQLiteConnection;
        public SqliteConnection SQLiteConnection { get => _SQLiteConnection; set => _SQLiteConnection = value; }

        public AppSettingsDatabase()
        {
            try
            {
                SQLiteConnection = new SqliteConnection(@"Data Source=..\..\..\Databases\pomodorotimer.DB");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Exception {ex.Message} appeared during trying to connect to database");
            }
        }

        public void ConnectToTheDatabase()
        {
            SQLiteConnection.Open();
        }


        public List<string> ReadDataFromTheDatabase()
        {
            List<string> returnList = new List<string>();
            ConnectToTheDatabase();

            SqliteCommand commandToExecute = new SqliteCommand("SELECT * FROM [user-settings]", _SQLiteConnection);
            SqliteDataReader databaseReader = commandToExecute.ExecuteReader();

            int counter = 0;

            while (databaseReader.Read())
            {
                returnList.Add(databaseReader.GetString(2)); ;
            }

            _SQLiteConnection.Dispose();
            return returnList;
        }

     

        public void UpdateTheDatabase(string property, int value)
        {
            string id;

            switch(property)
            {
                case "Pomodoro":
                    id = "1";
                    break;
                case "ShortBreak":
                    id = "2";
                    break;
                case "LongBreak":
                    id = "3";
                    break;
                default:
                    return;
            }
                

            _SQLiteConnection.Open();
            SqliteCommand commandToExecute = new SqliteCommand("UPDATE [user-settings] SET value = " + (value * 60).ToString() + " WHERE id = " + id, _SQLiteConnection);
            commandToExecute.ExecuteNonQuery();

            _SQLiteConnection.Dispose();
        }
    }
}
