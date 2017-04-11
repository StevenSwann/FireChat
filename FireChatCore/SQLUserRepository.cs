using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FireChatCore
{
    public class SQLUserRepository : IUserRepository
    {
        IDbConnection connection;
        IDbCommand command;
                
        public SQLUserRepository(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
        }

        public void AddUser(User user)
        {
            string sqlStatement = "INSERT INTO users(username, password, email, is_admin) VALUES(@username, @password, @email, @is_admin);";

            command.CommandText = sqlStatement;
            command.Connection = connection;
            command.Parameters.Clear();

            //IDbDataParameter parameter = new SqlParameter("@user_id", SqlDbType.Int);
            //parameter.Value = user.user_id;
            //command.Parameters.Add(parameter);

            IDbDataParameter parameter = new SqlParameter("@username", SqlDbType.VarChar, 25);
            parameter.Value = user.username;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter("@password", SqlDbType.VarChar, 25);
            parameter.Value = user.password;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter("@email", SqlDbType.VarChar, 100);
            parameter.Value = user.email;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter("@is_admin", SqlDbType.Bit, 0);
            parameter.Value = user.is_admin;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();

            }
            catch (SqlException sqlError)
            {
                Console.WriteLine("Error: {0} Inner Exception{1}", sqlError.Message, sqlError.InnerException);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

        }

        public void EditUser(User user)
        {
            string sqlStatement = "UPDATE users SET username = @usernameUp, password = @passwordUp, email = @emailUp, is_admin = @is_adminUp WHERE user_id = @user_idUp;";

            command.CommandText = sqlStatement;
            command.Connection = connection;
            command.Parameters.Clear();

            //IDbDataParameter parameter = new SqlParameter("@user_id", SqlDbType.Int);
            //parameter.Value = user.user_id;
            //command.Parameters.Add(parameter);

            IDbDataParameter parameter = new SqlParameter("@usernameUp", SqlDbType.VarChar, 25);
            parameter.Value = user.username;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter("@passwordUp", SqlDbType.VarChar, 25);
            parameter.Value = user.password;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter("@emailUp", SqlDbType.VarChar, 100);
            parameter.Value = user.email;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter("@is_adminUp", SqlDbType.Bit, 0);
            parameter.Value = user.is_admin;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter("@user_idUp", SqlDbType.Int);
            parameter.Value = user.user_id;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();

            }
            catch (SqlException sqlError)
            {
                Console.WriteLine("Error: {0} Inner Exception{1}", sqlError.Message, sqlError.InnerException);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteUser(User user)
        {
            string sqlStatement = "DELETE FROM users WHERE user_id = @iddel;";

            command.CommandText = sqlStatement;
            command.Connection = connection;
            command.Parameters.Clear();

            IDbDataParameter parameter = new SqlParameter("@iddel", SqlDbType.Int);
            parameter.Value = user.user_id;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();

            }
            catch (SqlException sqlError)
            {
                Console.WriteLine("Error: {0} Inner Exception{1}", sqlError.Message, sqlError.InnerException);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

        }

        public List<User> GetUsers()
        {
                     
           string sqlStatement = "SELECT user_id, username, password, email, is_admin FROM users;";

            List<User> users = new List<User>();

            command.CommandText = sqlStatement;
            command.Connection = connection;
            
           
            try
            {
                connection.Open();
                
                IDataReader sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    users.Add(new User()
                    {
                        user_id = int.Parse(sqlReader["user_id"].ToString()),
                        username = sqlReader["username"].ToString(),
                        password = sqlReader["password"].ToString(),
                        email = sqlReader["email"].ToString(),
                        is_admin = bool.Parse(sqlReader["is_admin"].ToString())
                    }
                       );
                }
            }
            catch (SqlException sqlError)
            {
                Console.WriteLine("Error: {0} Inner Exception {1}", sqlError.Message, sqlError.InnerException);
            }
            finally
            {
                connection.Close();
            }

            return users;
        }
        
        public User GetUserById(int id)
       {
           string sqlStatement = "SELECT user_id, username, password, email, is_admin FROM users WHERE user_id = @id;";
           User returnedUser = new User();
           List<User> users = new List<User>();

           command.CommandText = sqlStatement;
           command.Connection = connection;
           command.Parameters.Clear();

           IDbDataParameter parameter = new SqlParameter("@id", SqlDbType.Int);
           parameter.Value = id;
           command.Parameters.Add(parameter);

           try
           {
               connection.Open();
               IDataReader sqlReader = command.ExecuteReader();
               while (sqlReader.Read())
               {
                   users.Add(new User()
                   {
                       user_id = int.Parse(sqlReader["user_id"].ToString()),
                       username = sqlReader["username"].ToString(),
                       password = sqlReader["password"].ToString(),
                       email = sqlReader["email"].ToString(),
                       is_admin = bool.Parse(sqlReader["is_admin"].ToString())
                   }
                      );
               }
                     
               
          }
           catch (SqlException sqlError)
           {
               Console.WriteLine("Error: {0} Inner Exception {1}", sqlError.Message, sqlError.InnerException);
           }
           finally
           {
               connection.Close();
           }

           foreach (User u in users)
           {
               returnedUser = u;
           }

           return returnedUser;
       }

        public User GetUserByUsername(string username)
        {
            string sqlStatement = "SELECT user_id, username, password, email, is_admin FROM users WHERE username = @username;";
            User returnedUser = new User();
            List<User> users = new List<User>();

            command.CommandText = sqlStatement;
            command.Connection = connection;
            command.Parameters.Clear();

            IDbDataParameter parameter = new SqlParameter("@username", SqlDbType.VarChar);
            parameter.Value = username;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();
                IDataReader sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    users.Add(new User()
                    {
                        user_id = int.Parse(sqlReader["user_id"].ToString()),
                        username = sqlReader["username"].ToString(),
                        password = sqlReader["password"].ToString(),
                        email = sqlReader["email"].ToString(),
                        is_admin = bool.Parse(sqlReader["is_admin"].ToString())
                    }
                       );
                }


            }
            catch (SqlException sqlError)
            {
                Console.WriteLine("Error: {0} Inner Exception {1}", sqlError.Message, sqlError.InnerException);
            }
            finally
            {
                connection.Close();
            }

            foreach (User u in users)
            {
                returnedUser = u;
            }

            return returnedUser;
        }
        
        public bool UserExists(User user)
        {
            string sqlStatement = "SELECT user_id, username, password, email, is_admin FROM users WHERE username = @username;";
            User returnedUser = new User();
            List<User> users = new List<User>();

            command.CommandText = sqlStatement;
            command.Connection = connection;
            command.Parameters.Clear();

            IDbDataParameter parameter = new SqlParameter("@username", SqlDbType.VarChar);
            parameter.Value = user.username;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();
                IDataReader sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    users.Add(new User()
                    {
                        user_id = int.Parse(sqlReader["user_id"].ToString()),
                        username = sqlReader["username"].ToString(),
                        password = sqlReader["password"].ToString(),
                        email = sqlReader["email"].ToString(),
                        is_admin = bool.Parse(sqlReader["is_admin"].ToString())
                    }
                       );
                }


            }
            catch (SqlException sqlError)
            {
                Console.WriteLine("Error: {0} Inner Exception {1}", sqlError.Message, sqlError.InnerException);
            }
            finally
            {
                connection.Close();
            }

            if (users.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EmailExists(User user)
        {
            string sqlStatement = "SELECT user_id, username, password, email, is_admin FROM users WHERE email = @email;";
            User returnedUser = new User();
            List<User> users = new List<User>();

            command.CommandText = sqlStatement;
            command.Connection = connection;
            command.Parameters.Clear();

            IDbDataParameter parameter = new SqlParameter("@email", SqlDbType.VarChar);
            parameter.Value = user.email;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();
                IDataReader sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    users.Add(new User()
                    {
                        user_id = int.Parse(sqlReader["user_id"].ToString()),
                        username = sqlReader["username"].ToString(),
                        password = sqlReader["password"].ToString(),
                        email = sqlReader["email"].ToString(),
                        is_admin = bool.Parse(sqlReader["is_admin"].ToString())
                    }
                       );
                }


            }
            catch (SqlException sqlError)
            {
                Console.WriteLine("Error: {0} Inner Exception {1}", sqlError.Message, sqlError.InnerException);
            }
            finally
            {
                connection.Close();
            }

            if (users.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public bool PasswordMatch(User user)
        {
            string sqlStatement = "SELECT user_id, username, password, email, is_admin FROM users WHERE username = @username AND password = @password COLLATE SQL_Latin1_General_CP1_CS_AS;";
            User returnedUser = new User();
            List<User> users = new List<User>();

            command.CommandText = sqlStatement;
            command.Connection = connection;
            command.Parameters.Clear();

            IDbDataParameter parameter = new SqlParameter("@username", SqlDbType.VarChar);
            parameter.Value = user.username;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter("@password", SqlDbType.VarChar);
            parameter.Value = user.password;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();
                IDataReader sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    users.Add(new User()
                    {
                        user_id = int.Parse(sqlReader["user_id"].ToString()),
                        username = sqlReader["username"].ToString(),
                        password = sqlReader["password"].ToString(),
                        email = sqlReader["email"].ToString(),
                        is_admin = bool.Parse(sqlReader["is_admin"].ToString())
                    }
                       );
                }


            }
            catch (SqlException sqlError)
            {
                Console.WriteLine("Error: {0} Inner Exception {1}", sqlError.Message, sqlError.InnerException);
            }
            finally
            {
                connection.Close();
            }

            if (users.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsAdmin(User user)
        {
            string sqlStatement = "SELECT user_id, username, password, email, is_admin FROM users WHERE is_admin = 1 AND username = @username;";
            User returnedUser = new User();
            List<User> users = new List<User>();

            command.CommandText = sqlStatement;
            command.Connection = connection;
            command.Parameters.Clear();
                   
            IDbDataParameter parameter = new SqlParameter("@username", SqlDbType.VarChar);
            parameter.Value = user.username;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();
                IDataReader sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    users.Add(new User()
                    {
                        user_id = int.Parse(sqlReader["user_id"].ToString()),
                        username = sqlReader["username"].ToString(),
                        password = sqlReader["password"].ToString(),
                        email = sqlReader["email"].ToString(),
                        is_admin = bool.Parse(sqlReader["is_admin"].ToString())
                    }
                       );
                }


            }
            catch (SqlException sqlError)
            {
                Console.WriteLine("Error: {0} Inner Exception {1}", sqlError.Message, sqlError.InnerException);
            }
            finally
            {
                connection.Close();
            }

            if (users.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
