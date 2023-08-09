using BackEndCapstone.Models;
using BackEndCapstone.Utils;
using Microsoft.Data.SqlClient;

namespace BackEndCapstone.Repositories
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(IConfiguration configuration) : base(configuration) { }

        //Lets froms see who they've messaged
        public List<Message> GetMessagesByUserId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT m.Id, m.FromId, m.ToId, m.Content, m.CreateDateTime, up1.FirstName AS FromFirstName, up1.LastName AS FromLastName, up1.Email AS FromEmail, up1.ImageLocation AS FromImage, up2.FirstName AS ToFirstName, up2.LastName AS ToLastName, up2.Email AS ToEmail, up2.ImageLocation AS ToImage
                        FROM Message m
                            LEFT JOIN UserProfile up1 on up1.Id = m.FromId
                            LEFT JOIN UserProfile up2 on up2.Id = m.ToId
                        WHERE m.FromId = @Id OR m.ToId = @Id
                        ORDER BY m.CreateDateTime";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    var notes = new List<Message>();
                    while (reader.Read())
                    {
                        notes.Add(MessageFormat(reader));
                    }
                    reader.Close();
                    return notes;

                }
            }
        }


        //Lets froms and tos the all messages sent between each other
        public List<Message> GetMessagesByFromIdAndToId(int fromId, int toId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT m.Id, m.FromId, m.ToId, m.Content, m.CreateDateTime, up1.FirstName AS FromFirstName, up1.LastName AS FromLastName, up1.Email AS FromEmail, up1.ImageLocation AS FromImage, up2.FirstName AS ToFirstName, up2.LastName AS ToLastName, up2.Email AS ToEmail, up2.ImageLocation AS ToImage
                        FROM Message m
                            LEFT JOIN UserProfile up1 on up1.Id = m.FromId
                            LEFT JOIN UserProfile up2 on up2.Id = m.ToId
                        WHERE m.ToId = @ToId AND m.FromId = @FromId 
                        ORDER BY m.CreateDateTime";

                    DbUtils.AddParameter(cmd, "@FromId", fromId);
                    DbUtils.AddParameter(cmd, "@ToId", toId);

                    var reader = cmd.ExecuteReader();

                    var notes = new List<Message>();
                    while (reader.Read())
                    {
                        notes.Add(MessageFormat(reader));
                    }
                    reader.Close();
                    return notes;

                }
            }
        }


        public void Add(Message message)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Message (FromId, ToId, Content, CreateDateTime)
                        OUTPUT INSERTED.Id
                        VALUES (@FromId, @ToId, @Content, @CreateDateTime)";

                    DbUtils.AddParameter(cmd, "@FromId", message.FromId);
                    DbUtils.AddParameter(cmd, "@ToId", message.ToId);
                    DbUtils.AddParameter(cmd, "@Content", message.Content);
                    DbUtils.AddParameter(cmd, "@CreateDateTime", message.CreateDateTime);

                    message.Id = (int)cmd.ExecuteScalar();
                }

            }
        }

        //May not include ability to edit messages
        //public void Update(Message message)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                UPDATE Message
        //                SET FromId = @FromId,
        //                    ToId = @ToId,
        //                    Content = @Content,
        //                    CreateDateTime = @CreateDateTime
        //                WHERE Id = @Id";

        //            DbUtils.AddParameter(cmd, "@FromId", message.FromId);
        //            DbUtils.AddParameter(cmd, "@ToId", message.ToId);
        //            DbUtils.AddParameter(cmd, "@Content", message.Content);
        //            DbUtils.AddParameter(cmd, "@CreateDateTime", message.CreateDateTime);
        //            DbUtils.AddParameter(cmd, "@Id", message.Id);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}


        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM Message
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        private Message MessageFormat(SqlDataReader reader)
        {
            return new Message
            {
                Id = DbUtils.GetInt(reader, "Id"),
                FromId = DbUtils.GetInt(reader, "FromId"),
                ToId = DbUtils.GetInt(reader, "ToId"),
                Content = DbUtils.GetString(reader, "Content"),
                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                FromUserProfile = new UserProfile()
                {
                    FirstName = DbUtils.GetString(reader, "FromFirstName"),
                    LastName = DbUtils.GetString(reader, "FromLastName"),
                    Email = DbUtils.GetString(reader, "FromEmail"),
                    ImageLocation = DbUtils.GetString(reader, "FromImage")
                },
                ToUserProfile = new UserProfile()
                {
                    FirstName = DbUtils.GetString(reader, "ToFirstName"),
                    LastName = DbUtils.GetString(reader, "ToLastName"),
                    Email = DbUtils.GetString(reader, "ToEmail"),
                    ImageLocation = DbUtils.GetString(reader, "ToImage")
                }
            };
        }




    }
}
