using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace CalendarApp
{
    public class Sql
    {

        public void SqlAdd(Person person)
        {
            using (SqlConnection con = new SqlConnection("Data Source = 192.168.23.113, 1433; Initial Catalog = test; Persist Security Info = True; User ID = Admin; Password = Passw0rd"))
            {
                con.Open();

                using (SqlCommand sql_cmnd = new SqlCommand("AddPerson", con))
                {
                    sql_cmnd.CommandType = CommandType.StoredProcedure; 
                    sql_cmnd.Parameters.AddWithValue("@FName", person.FName);
                    sql_cmnd.Parameters.AddWithValue("@LName", person.Lname);
                    sql_cmnd.Parameters.AddWithValue("@Birthday", person.Birthday);

                    sql_cmnd.ExecuteNonQuery();
                }
            }

        }
        public void SqlDel(Person person)
        {
            using (SqlConnection con = new SqlConnection("Data Source = 192.168.23.113, 1433; Initial Catalog = test; Persist Security Info = True; User ID = Admin; Password = Passw0rd"))
            {
                con.Open();

                using (SqlCommand sql_cmnd = new SqlCommand("DelPerson", con))
                {
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Id", person.Id);
                    sql_cmnd.ExecuteNonQuery();
                }
            }

        }
        public static async Task<List<Person>> SqlSelect()
        
            
        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=192.168.23.113,1433;Initial Catalog=test;Persist Security Info=True;User ID = Admin; Password=Passw0rd";
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectList", con);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Person> Birthdays = new List<Person>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Person person = new Person();
                    try { person.Id = reader.GetInt32(0); } catch { person.Id = 0; }
                    try { person.FName = reader.GetString(1); } catch { person.FName = "null"; }
                    try { person.Lname = reader.GetString(2); } catch { person.Lname = "null"; }
                          person.Birthday = reader.GetDateTime(3);


                    Birthdays.Add(person);
                }
            }
            else
            {
                Console.WriteLine("There's no rows");
            }
            reader.Close();
            return Birthdays;
            
            


        }
        public void SqlUpdate(Person person)
        {
            using (SqlConnection con = new SqlConnection("Data Source = 192.168.23.113, 1433; Initial Catalog = test; Persist Security Info = True; User ID = Admin; Password = Passw0rd"))
            {
                con.Open();

                using (SqlCommand sql_cmnd = new SqlCommand("UpdatePerson", con))
                {
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Id", person.Id);
                    sql_cmnd.Parameters.AddWithValue("@FName", person.FName);
                    sql_cmnd.Parameters.AddWithValue("@LName", person.Lname);
                    sql_cmnd.Parameters.AddWithValue("@Birthday", person.Birthday);

                    sql_cmnd.ExecuteNonQuery();
                }
            }

        }
    }
}
