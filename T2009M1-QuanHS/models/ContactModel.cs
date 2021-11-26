using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2009M1_QuanHS.entities;

namespace T2009M1_QuanHS.models
{
    class ContactModel
    {
        private static string _insertStatementTemplate = "INSERT INTO contacts (Name, Phone)" +
            " values (@name, @phone)";
        private static string _selectStatementTemplate = "SELECT * FROM contacts";
        private static string _selectStatementWithConditionTemplate = "SELECT * FROM contacts WHERE Name like @keyword";

        public bool Save(Contact contact)
        {
            try
            {
                using (SqliteConnection db = new SqliteConnection($"Filename={DatabaseMigration._databasePath}"))
                {
                    db.Open();
                    SqliteCommand insertCommand = new SqliteCommand(_insertStatementTemplate, db);
                    insertCommand.Parameters.AddWithValue("@name", contact.Name);
                    insertCommand.Parameters.AddWithValue("@phone", contact.Phone);
                    insertCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        internal void ClearData()
        {
            throw new NotImplementedException();
        }

        public List<Contact> FindAll()
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                // mở kết nối.
                using (SqliteConnection cnn = new SqliteConnection($"FileName={DatabaseMigration._databasePath}"))
                {
                    cnn.Open();
                    // tạo câu lệnh.
                    SqliteCommand cmd = new SqliteCommand(_selectStatementTemplate, cnn);
                    // bắn lệnh vào và lấy dữ liệu.
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var name = Convert.ToString(reader["Name"]);
                        var phone = Convert.ToString(reader["Phone"]);
                        var contact = new Contact()
                        {
                            Name = name,
                            Phone = phone,
                        };
                        contacts.Add(contact);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return contacts;
        }

        public List<Contact> SearchByKeyword(string keyword)
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                // mở kết nối.
                using (SqliteConnection cnn = new SqliteConnection($"FileName={DatabaseMigration._databasePath}"))
                {
                    cnn.Open();
                    // tạo câu lệnh.
                    //var select = $"select * from notes where Title like '%{keyword}%'";
                    //SqliteCommand cmd = new SqliteCommand(select, cnn);
                    SqliteCommand cmd = new SqliteCommand(_selectStatementWithConditionTemplate, cnn);
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    Debug.WriteLine(cmd.CommandText);
                    // bắn lệnh vào và lấy dữ liệu.
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var name = Convert.ToString(reader["Name"]);
                        var phone = Convert.ToString(reader["Phone"]);
                        var contact = new Contact()
                        {
                            Name = name,
                            Phone = phone,
                        };
                        contacts.Add(contact);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return contacts;
        }
    }
}
