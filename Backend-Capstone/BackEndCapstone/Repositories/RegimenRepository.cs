using BackEndCapstone.Models;
using BackEndCapstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;

namespace BackEndCapstone.Repositories
{
    public class RegimenRepository : BaseRepository, IRegimenRepository
    {
        public RegimenRepository(IConfiguration configuration) : base(configuration) { }

        public List<Regimen> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT r.Id, r.ProviderProfileId, r.Title, r.Description, r.CreateDateTime, up.FirstName, up.LastName, up.UserTypeId
                        FROM Regimen r
                            LEFT JOIN UserProfile up on up.Id = r.ProviderProfileId
                        ORDER BY r.Id
                        ";
                    var reader = cmd.ExecuteReader();

                    var regimens = new List<Regimen>();
                    while (reader.Read())
                    {
                        regimens.Add(RegimenFormat(reader));
                    }
                    reader.Close();
                    return regimens;
                }
            }
        }


        public Regimen GetRegimenById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT r.Id, r.ProviderProfileId, r.Title, r.Description, r.CreateDateTime, up.FirstName, up.LastName, up.UserTypeId
                        FROM Regimen r
                            LEFT JOIN UserProfile up on up.Id = r.ProviderProfileId
                        WHERE r.Id = @Id
                        ORDER BY r.Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Regimen regimen = null;

                    if (reader.Read())
                    {
                        regimen = RegimenFormat(reader);
                    }

                    reader.Close();
                    return regimen;
                }
            }
        }


        //currently will be used to get providers regimens sorted by id may need to change this later to sort by created date
        public List<Regimen> GetProviderRegimensById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT r.Id, r.ProviderProfileId, r.Title, r.Description, r.CreateDateTime, up.FirstName, up.LastName, up.UserTypeId
                        FROM Regimen r
                            LEFT JOIN UserProfile up on up.Id = r.ProviderProfileId
                        WHERE r.ProviderProfileId = @Id
                        ORDER BY r.Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    var regimens = new List<Regimen>();

                    while (reader.Read())
                    {
                        regimens.Add(RegimenFormat(reader));
                    }

                    reader.Close();
                    return regimens;
                }
            }
        }



        public List<Regimen> GetPatientRegimensById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT r.Id, r.ProviderProfileId, r.Title, r.Description,               r.CreateDateTime, up.FirstName, up.LastName, up.UserTypeId
                        FROM Regimen r
                            LEFT JOIN UserProfile up on up.Id = ra.PatientProfileId
                        WHERE ra.PatientProfileId = @Id
                        ORDER BY r.Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    var regimens = new List<Regimen>();

                    while (reader.Read())
                    {
                        regimens.Add(RegimenFormat(reader));
                    }

                    reader.Close();
                    return regimens;
                }
            }
        }


        public void Add(Regimen regimen)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Regimen (ProviderProfileId, Title, Description, CreateDateTime)
                        OUTPUT INSERTED.ID
                        VALUES (@ProviderProfileId, @Title, @Description, @CreateDateTime);
                        "
                    ;

                    DbUtils.AddParameter(cmd, "@ProviderProfileId", regimen.ProviderProfileId);
                    DbUtils.AddParameter(cmd, "@Title", regimen.Title);
                    DbUtils.AddParameter(cmd, "@Description", regimen.Description);
                    DbUtils.AddParameter(cmd, "@CreateDateTime", regimen.CreateDateTime);

                    regimen.Id = (int)cmd.ExecuteScalar();
                }
            }
        }


        public void Update(Regimen regimen)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Regimen
                        SET ProviderProfileId = @ProviderProfileId, 
                            Title = @Title, 
                            Description = @Description, 
                            CreateDateTime = @CreateDateTime
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@ProviderProfileId", regimen.ProviderProfileId);
                    DbUtils.AddParameter(cmd, "@Title", regimen.Title);
                    DbUtils.AddParameter(cmd, "@Description", regimen.Description);
                    DbUtils.AddParameter(cmd, "@CreateDateTime", regimen.CreateDateTime);
                    DbUtils.AddParameter(cmd, "@Id", regimen.Id);


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
                        DELETE FROM Regimen
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        private Regimen RegimenFormat(SqlDataReader reader)
        {
            return new Regimen()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                ProviderProfileId = DbUtils.GetInt(reader, "ProviderProfileId"),
                Title = DbUtils.GetString(reader, "Title"),
                Description = DbUtils.GetString(reader, "Description"),
                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                UserProfile = new UserProfile()
                {
                    FirstName = DbUtils.GetString(reader, "FirstName"),
                    LastName = DbUtils.GetString(reader, "LastName"),
                    UserTypeId = DbUtils.GetInt(reader, "UserTypeId")
                }
            };
        }



    }
}
