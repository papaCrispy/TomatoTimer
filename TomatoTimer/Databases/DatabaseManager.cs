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
    public class DatabaseManager
    {
        private SqliteConnection _dbConnection;
        private string workingDirectory;
        
        public DatabaseManager()
        {
            workingDirectory = Environment.CurrentDirectory;
        }

        public void CreateConnection()
        {
            
            try
            {
                _dbConnection = new SqliteConnection(@"Data Source=..\..\..\Databases\app-settings.DB");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "dupa");
            }

        }

        public List<string> ReadDataFromTheBase()
        {
            List<string> returnList = new List<string>();

            _dbConnection.Open();
            
            SqliteCommand commandToExecute = new SqliteCommand("SELECT * FROM [user-settings]", _dbConnection);
            SqliteDataReader databaseReader = commandToExecute.ExecuteReader();

            int counter = 0;

            while(databaseReader.Read())
            {
                returnList.Add(databaseReader.GetString(2)); ;
            }

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
                

            _dbConnection.Open();
            SqliteCommand commandToExecute = new SqliteCommand("UPDATE [user-settings] SET value = " + (value * 60).ToString() + " WHERE id = " + id, _dbConnection);
            commandToExecute.ExecuteNonQuery();        
        }
    }
}
