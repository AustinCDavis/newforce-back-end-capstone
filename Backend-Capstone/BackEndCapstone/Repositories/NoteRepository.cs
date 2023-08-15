using BackEndCapstone.Models;
using BackEndCapstone.Utils;
using Microsoft.Data.SqlClient;

namespace BackEndCapstone.Repositories
{
    public class NoteRepository : BaseRepository, INoteRepository
    {

        public NoteRepository(IConfiguration configuration) : base(configuration) { }

        public List<Note> GetNotesByProviderId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT n.Id, n.ProviderProfileId, n.PatientProfileId, n.Content, n.CreateDateTime, up1.FirstName AS ProviderFirstName, up1.LastName AS ProviderLastName, up1.Email AS ProviderEmail, up1.ImageLocation AS ProviderImage, up2.FirstName AS PatientFirstName, up2.LastName AS PatientLastName, up2.Email AS PatientEmail, up2.ImageLocation AS PatientImage
                        FROM Note n
                            LEFT JOIN UserProfile up1 on up1.Id = n.ProviderProfileId
                            LEFT JOIN UserProfile up2 on up2.Id = n.PatientProfileId
                        WHERE n.ProviderProfileId = @Id
                        ORDER BY n.CreateDateTime DESC";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    var notes = new List<Note>();
                    while (reader.Read())
                    {
                        notes.Add(NoteFormat(reader));
                    }
                    reader.Close();
                    return notes;

                }
            }
        }


        public List<Note> GetNotesByPatientId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT n.Id, n.ProviderProfileId, n.PatientProfileId, n.Content, n.CreateDateTime, up1.FirstName AS ProviderFirstName, up1.LastName AS ProviderLastName, up1.Email AS ProviderEmail, up1.ImageLocation AS ProviderImage, up2.FirstName AS PatientFirstName, up2.LastName AS PatientLastName, up2.Email AS PatientEmail, up2.ImageLocation AS PatientImage
                        FROM Note n
                            LEFT JOIN UserProfile up1 on up1.Id = n.ProviderProfileId
                            LEFT JOIN UserProfile up2 on up2.Id = n.PatientProfileId
                        WHERE n.PatientProfileId = @Id
                        ORDER BY n.CreateDateTime DESC";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    var notes = new List<Note>();
                    while (reader.Read())
                    {
                        notes.Add(NoteFormat(reader));
                    }
                    reader.Close();
                    return notes;

                }
            }
        }


        public Note GetNoteById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT n.Id, n.ProviderProfileId, n.PatientProfileId, n.Content, n.CreateDateTime, up1.FirstName AS ProviderFirstName, up1.LastName AS ProviderLastName, up1.Email AS ProviderEmail, up1.ImageLocation AS ProviderImage, up2.FirstName AS PatientFirstName, up2.LastName AS PatientLastName, up2.Email AS PatientEmail, up2.ImageLocation AS PatientImage
                        FROM Note n
                            LEFT JOIN UserProfile up1 on up1.Id = n.ProviderProfileId
                            LEFT JOIN UserProfile up2 on up2.Id = n.PatientProfileId
                        WHERE n.Id = @Id
                        ORDER BY n.CreateDateTime DESC";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Note note = null;

                    if (reader.Read())
                    {
                        note = NoteFormat(reader);
                    }

                    reader.Close();
                    return note;
                }
            }
        }


        public void Add(Note note)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Note (ProviderProfileId, PatientProfileId, Content, CreateDateTime)
                        OUTPUT INSERTED.Id
                        VALUES (@ProviderProfileId, @PatientProfileId, @Content, @CreateDateTime)";

                    DbUtils.AddParameter(cmd, "@ProviderProfileId", note.ProviderProfileId);
                    DbUtils.AddParameter(cmd, "@PatientProfileId", note.PatientProfileId);
                    DbUtils.AddParameter(cmd, "@Content", note.Content);
                    DbUtils.AddParameter(cmd, "@CreateDateTime", note.CreateDateTime);

                    note.Id = (int)cmd.ExecuteScalar();
                }

            }
        }


        public void Update(Note note)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Note
                        SET ProviderProfileId = @ProviderProfileId,
                            PatientProfileId = @PatientProfileId,
                            Content = @Content,
                            CreateDateTime = @CreateDateTime
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@ProviderProfileId", note.ProviderProfileId);
                    DbUtils.AddParameter(cmd, "@PatientProfileId", note.PatientProfileId);
                    DbUtils.AddParameter(cmd, "@Content", note.Content);
                    DbUtils.AddParameter(cmd, "@CreateDateTime", note.CreateDateTime);
                    DbUtils.AddParameter(cmd, "@Id", note.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM Note
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        private Note NoteFormat(SqlDataReader reader)
        {
            return new Note
            {
                Id = DbUtils.GetInt(reader, "Id"),
                ProviderProfileId = DbUtils.GetInt(reader, "ProviderProfileId"),
                PatientProfileId = DbUtils.GetInt(reader, "PatientProfileId"),
                Content = DbUtils.GetString(reader, "Content"),
                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                ProviderProfile = new UserProfile()
                {
                    FirstName = DbUtils.GetString(reader, "ProviderFirstName"),
                    LastName = DbUtils.GetString(reader, "ProviderLastName"),
                    Email = DbUtils.GetString(reader, "ProviderEmail"),
                    ImageLocation = DbUtils.GetString(reader, "ProviderImage")
                },
                PatientProfile = new UserProfile()
                {
                    FirstName = DbUtils.GetString(reader, "PatientFirstName"),
                    LastName = DbUtils.GetString(reader, "PatientLastName"),
                    Email = DbUtils.GetString(reader, "PatientEmail"),
                    ImageLocation = DbUtils.GetString(reader, "PatientImage")
                }
            };
        }


    }
}
