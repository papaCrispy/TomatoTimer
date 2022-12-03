using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomatoTimer.Databases
{
    public interface IDatabaseManager
    {
        public SqliteConnection SQLiteConnection { get; set; }

        public void ConnectToTheDatabase();
    }
}
