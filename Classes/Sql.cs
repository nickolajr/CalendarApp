using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using CalendarApp.Classes;
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
        public void WorkLog(Authenticastion.AuthService authService, workLog worklog)
        {
            using (SqlConnection con = new SqlConnection("Data Source = 192.168.23.113, 1433; Initial Catalog = test; Persist Security Info = True; User ID = Admin; Password = Passw0rd"))
            {
                con.Open();

                using (SqlCommand sql_cmnd = new SqlCommand("WorkLogAdd", con))
                {
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@AccId", authService.Id);
                    sql_cmnd.Parameters.AddWithValue("@Title", worklog.Title);
                    sql_cmnd.Parameters.AddWithValue("@Descs", worklog.Descs);
                    sql_cmnd.Parameters.AddWithValue("@Cday", worklog.Cday);
                    sql_cmnd.Parameters.AddWithValue("@IStartTime", worklog.StartTime.TimeOfDay);
                    sql_cmnd.Parameters.AddWithValue("@IEndTime", worklog.EndTime.TimeOfDay);

                    sql_cmnd.ExecuteNonQuery();
                }
            }

        }
        public static async Task<List<workLog>> selectworklog()


        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=192.168.23.113,1433;Initial Catalog=test;Persist Security Info=True;User ID = Admin; Password=Passw0rd";
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectWorkLog", con);
            SqlDataReader reader = cmd.ExecuteReader();
            List<workLog> workLogs = new List<workLog>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    workLog worklog = new workLog();
                    worklog.Accid = reader.GetInt32(0);
                    worklog.FName = reader.GetString(1);
                    worklog.LName = reader.GetString(2);
                    worklog.Title = reader.GetString(3);
                    worklog.Descs = reader.GetString(4);
                    worklog.Cday = reader.GetDateTime(5);
                    worklog.StartTime = reader.GetDateTime(6);
                    worklog.EndTime = reader.GetDateTime(7);


                    workLogs.Add(worklog);
                }
            }
            else
            {
                Console.WriteLine("There's no rows");
            }
            reader.Close();
            return workLogs;




        }
    }
}
