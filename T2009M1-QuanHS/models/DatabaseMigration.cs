using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2009M1_QuanHS.entities;
using Windows.Storage;

namespace T2009M1_QuanHS.models
{
    public class DatabaseMigration
    {
        public static string _databasePath;
        private static string _databaseName = "mynote.db";
        private static string _createNoteTable = "CREATE TABLE IF NOT EXISTS contacts " +
            "(Name NVARCHAR(255) NOT NULL," +
            "Phone NVARCHAR(255) NOT NULL UNIQUE,)";

        internal static void UpdateDatabase()
        {
            throw new NotImplementedException();
        }

        public async static void UpdateDabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(_databaseName, CreationCollisionOption.OpenIfExists);
            _databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, _databaseName);
            using (SqliteConnection db = new SqliteConnection($"Filename={_databasePath}"))
            {
                db.Open();
                SqliteCommand createTableNote = new SqliteCommand(_createNoteTable, db);
                createTableNote.ExecuteNonQuery();
            }
            await GenerateData();
        }

        public async static Task GenerateData()
        {
            ContactModel noteModel = new ContactModel();
            noteModel.ClearData();
            noteModel.Save(new Contact()
            {
                Name = "Quân",
                Phone = "0345686899",
            });
            noteModel.Save(new Contact()
            {
                Name = "Dũng",
                Phone = "0548396945",
            });
            noteModel.Save(new Contact()
            {
                Name = "Huy",
                Phone = "0548693645",
            });
        }
    }
}
