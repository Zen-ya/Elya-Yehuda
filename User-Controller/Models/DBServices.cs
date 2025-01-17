
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

public class DBServices
 {
     public static string connectionStr = @"workstation id=En_chanter_Karaoke.mssql.somee.com;packet size=4096;user id=Elya_Amram_SQLLogin_5;pwd=qvrs6xc9y2;data source=En_chanter_Karaoke.mssql.somee.com;persist security info=False;initial catalog=En_chanter_Karaoke;TrustServerCertificate=True";
     public static Users Login(string name, string pass)
     {
         Users usr2Ret = null;
         using (SqlConnection con = new SqlConnection(connectionStr))
         {
             string query = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password";
             SqlCommand cmd = new SqlCommand(query, con);
             cmd.Parameters.AddWithValue("@UserName", name);
             cmd.Parameters.AddWithValue("@Password", pass);

             con.Open();
             using (SqlDataReader rdr = cmd.ExecuteReader())
             {
                 if (rdr.Read())
                 {
                     usr2Ret = new Users()
                     {
                         Id = (int)rdr["UserID"],
                         UserName = rdr["UserName"].ToString(),
                         Password = rdr["Password"].ToString(),
                         Email = rdr["Email"].ToString(),
                         Phone = rdr["Phone"].ToString(),
                         Birthday = (DateTime)rdr["Birthday"],
                         avatarUrl = rdr["avatarUrl"].ToString(),
                     };
                 }
             }
         }
         return usr2Ret;
     }

     public static List<Users> GetUsers()
     {
         List<Users> users = new List<Users>();
         string query = "SELECT * FROM Users";
         using (SqlConnection con = new SqlConnection(connectionStr))
         {
             SqlCommand cmd = new SqlCommand(query, con);
             con.Open();
             using (SqlDataReader rdr = cmd.ExecuteReader())
             {
                 while (rdr.Read())
                 {
                     users.Add(new Users()
                     {
                         Id = (int)rdr["UserID"],
                         UserName = rdr["UserName"].ToString(),
                         Email = rdr["Email"].ToString(),
                         Phone = rdr["Phone"].ToString(),
                         Password = rdr["Password"].ToString(),
                         Birthday = (DateTime)rdr["Birthday"]
                     });
                 }
             }
         }
         return users;
     }


     public static bool DeleteUserById(int userId)
     {
         using (SqlConnection con = new SqlConnection(connectionStr))
         {
             string query = "DELETE FROM Users WHERE UserID = @UserId";
             SqlCommand cmd = new SqlCommand(query, con);
             cmd.Parameters.AddWithValue("@UserId", userId);

             con.Open();
             int rowsAffected = cmd.ExecuteNonQuery();
             return rowsAffected > 0; // Returns true if a row was deleted, false otherwise
         }
     }


     public static bool CreateUser(Users newUser)
     {
         using (SqlConnection con = new SqlConnection(connectionStr))
         {
             string query = "INSERT INTO Users (UserName, Email, Phone, Password , Birthday) " +
                            "VALUES (@UserName, @Email, @Phone, @Password ,@birthday)";
             SqlCommand cmd = new SqlCommand(query, con);
             cmd.Parameters.AddWithValue("@UserName", newUser.UserName);
             cmd.Parameters.AddWithValue("@Email", newUser.Email);
             cmd.Parameters.AddWithValue("@Phone", newUser.Phone);
             cmd.Parameters.AddWithValue("@Password", newUser.Password);
             cmd.Parameters.AddWithValue("@birthday", newUser.Birthday.Date);

             con.Open();
             int rowsAffected = cmd.ExecuteNonQuery();
             return rowsAffected > 0;
         }
     }


     public static bool UpdateUser(int id, Users updatedUser)
     {
         using (SqlConnection con = new SqlConnection(connectionStr))
         {
             string query = $"UPDATE Users SET UserName = @UserName, Email = @Email, Phone = @Phone,Password = @Password, Birthday = @Birthday, avatarUrl = @avatarUrl WHERE UserID = {id}";

             SqlCommand cmd = new SqlCommand(query, con);
             cmd.Parameters.AddWithValue("@UserName", updatedUser.UserName);
             cmd.Parameters.AddWithValue("@Email", updatedUser.Email);
             cmd.Parameters.AddWithValue("@Phone", updatedUser.Phone);
             cmd.Parameters.AddWithValue("@Password", updatedUser.Password);
             cmd.Parameters.AddWithValue("@birthday", updatedUser.Birthday);
             cmd.Parameters.AddWithValue("@avatarUrl", updatedUser.avatarUrl);

             con.Open();
             int rowsAffected = cmd.ExecuteNonQuery();
             return rowsAffected > 0;
         }
     }

    public static Users GetUserById(int userId)
    {
        Users user2Ret = null;
        using (SqlConnection con = new SqlConnection(connectionStr))
        {
            string query = "SELECT * FROM Users WHERE UserID = @UserId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", userId);

            con.Open();
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.Read())
                {
                    user2Ret = new Users()
                    {
                        Id = (int)rdr["UserID"],
                        UserName = rdr["UserName"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Phone = rdr["Phone"].ToString(),
                        Password = rdr["Password"].ToString(),
                        Birthday = (DateTime)rdr["Birthday"],
                        avatarUrl = rdr["avatarUrl"].ToString()
                    };
                }
            }
        }
        return user2Ret;
    }

}