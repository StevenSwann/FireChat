using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FireChatCore
{
    public class SQLMessageRepository : IMessageRepository
    {
        IDbConnection connection;
        IDbCommand command;

        public SQLMessageRepository(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
        }       
           
        //Interface methods below.
       
        public void AddMessage(Message message)
        {
            string sqlStatement = "INSERT INTO messages(message_user_id, message_chat_id, message_text, message_datetime) VALUES(@message_user_id, @message_chat_id, @message_text, @message_datetime);";

            command.CommandText = sqlStatement;
            command.Connection = connection;
            command.Parameters.Clear();

            //IDbDataParameter parameter = new SqlParameter("@user_id", SqlDbType.Int);
            //parameter.Value = user.user_id;
            //command.Parameters.Add(parameter);

            IDbDataParameter parameter = new SqlParameter("@message_user_id", SqlDbType.Int, 25);
            parameter.Value = message.message_user_id;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter("@message_chat_id", SqlDbType.Int, 25);
            parameter.Value = message.message_chat_id;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter("@message_text", SqlDbType.VarChar, 300);
            parameter.Value = message.message_text;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter("@message_datetime", SqlDbType.DateTime);
            parameter.Value = message.message_datetime;
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

        public void DeleteMessage(Message message)
        {
            string sqlStatement = "DELETE FROM messages WHERE message_id = @iddel;";

            command.CommandText = sqlStatement;
            command.Connection = connection;
            command.Parameters.Clear();

            IDbDataParameter parameter = new SqlParameter("@iddel", SqlDbType.Int);
            parameter.Value = message.message_id;
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

        public List<Message> GetMessages()
        {
            string sqlStatement = "SELECT message_id, message_user_id, message_chat_id, message_text, message_datetime FROM messages;";

            List<Message> messages = new List<Message>();

            command.CommandText = sqlStatement;
            command.Connection = connection;


            try
            {
                connection.Open();

                IDataReader sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    messages.Add(new Message()
                    {
                        message_id = int.Parse(sqlReader["message_id"].ToString()),
                        message_user_id = int.Parse(sqlReader["message_user_id"].ToString()),
                        message_chat_id = int.Parse(sqlReader["message_chat_id"].ToString()),
                        message_text = sqlReader["message_text"].ToString(),
                        message_datetime = DateTime.Parse(sqlReader["message_datetime"].ToString())
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

            return messages;
        }
        
        public Message GetMessageById(int id)
        {
            string sqlStatement = "SELECT message_id, message_user_id, message_chat_id, message_text, message_datetime FROM messages WHERE message_id = @id;";
            Message returnedMessage = new Message();
            List<Message> messages = new List<Message>();

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
                    messages.Add(new Message()
                    {
                        message_id = int.Parse(sqlReader["message_id"].ToString()),
                        message_user_id = int.Parse(sqlReader["message_user_id"].ToString()),
                        message_chat_id = int.Parse(sqlReader["message_chat_id"].ToString()),
                        message_text = sqlReader["message_text"].ToString(),
                        message_datetime = DateTime.Parse(sqlReader["message_datetime"].ToString())
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

            foreach (Message m in messages)
            {
                returnedMessage = m;
            }

            return returnedMessage;
        }

        public List<MessageLog> GenerateMessageLog(DateTime fromdatetime)
        {
            string sqlStatement = "SELECT msg.message_datetime, u.username, msg.message_text FROM messages msg JOIN users u ON u.user_id = msg.message_user_id WHERE msg.message_datetime > @fromdatetime ORDER BY message_datetime DESC;";

            List<MessageLog> log = new List<MessageLog>();

            command.CommandText = sqlStatement;
            command.Connection = connection;
            command.Parameters.Clear();

            IDbDataParameter parameter = new SqlParameter("@fromdatetime", SqlDbType.DateTime);
            parameter.Value = fromdatetime;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                IDataReader sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    log.Add(new MessageLog()
                    {
                        username = sqlReader["username"].ToString(),
                        message_text = sqlReader["message_text"].ToString(),
                        message_datetime = DateTime.Parse(sqlReader["message_datetime"].ToString())
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

            return log;
                        
        }
        
        public Message GetMessageByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Message GetMessageByDate(DateTime? date)
        {
            throw new NotImplementedException();
        }
    }
}
