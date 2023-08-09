using BackEndCapstone.Models;
using BackEndCapstone.Utils;
using Microsoft.Data.SqlClient;

namespace BackEndCapstone.Repositories

{

    public class PatientAssignmentRepository : BaseRepository, IPatientAssignmentRepository
    {
        public PatientAssignmentRepository(IConfiguration configuration) : base(configuration) { }

        public List<PatientAssignment> GetPatientAssignments()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT pa.Id, pa.ProviderProfileId, pa.PatientProfileId, pa.BeginDate, pa.EndDate, up1.FirstName AS ProviderFirstName, up1.LastName AS ProviderLastName, up1.Email AS ProviderEmail, up1.ImageLocation AS ProviderImage, up2.FirstName AS PatientFirstName, up2.LastName AS PatientLastName, up2.Email AS PatientEmail, up2.ImageLocation AS PatientImage
                    FROM PatientAssignment pa
                        LEFT JOIN UserProfile up1 on up1.Id = pa.ProviderProfileId
                        LEFT JOIN UserProfile up2 on up2.Id = pa.PatientProfileId
                    ORDER BY pa.Id";

                    var reader = cmd.ExecuteReader();

                    var patientAssignments = new List<PatientAssignment>();
                    while (reader.Read())
                    {
                        patientAssignments.Add(PatientAssignmentFormat(reader));
                    }
                    conn.Close();

                    return patientAssignments;
                }
            }
        }


        public List<PatientAssignment> GetPatientAssignmentByProviderId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up2.FirstName AS PatientFirstName, up2.LastName AS PatientLastName, up2.Email AS PatientEmail, up2.ImageLocation AS PatientImage
                        FROM PatientAssignment pa
                            LEFT JOIN UserProfile up2 on up2.Id = pa.PatientProfileId
                        WHERE pa.ProviderProfileId = @Id
                        ORDER BY up2.LastName";
                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    var assignments = new List<PatientAssignment>();
                    while (reader.Read())
                    {
                        assignments.Add( 
                            new PatientAssignment()
                            {
                                PatientProfile = new UserProfile()
                                {
                                    FirstName = DbUtils.GetString(reader, "PatientFirstName"),
                                    LastName = DbUtils.GetString(reader, "PatientLastName"),
                                    Email = DbUtils.GetString(reader, "PatientEmail"),
                                    ImageLocation = DbUtils.GetString(reader, "PatientImage")
                                }
                            });
                    }

                    reader.Close();
                    return assignments;
                }
            }
        }


        public void Add(PatientAssignment patientAssignment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO PatientAssignment (ProviderProfileId, PatientProfileId, BeginDate, EndDate)
                        OUTPUT INSERTED.Id
                        VALUES (@ProviderProfileId, @PatientProfileId, @BeginDate, @EndDate)";

                    DbUtils.AddParameter(cmd, "@ProviderProfileId", patientAssignment.ProviderProfileId);
                    DbUtils.AddParameter(cmd, "@PatientProfileId", patientAssignment.PatientProfileId);
                    DbUtils.AddParameter(cmd, "@BeginDate", patientAssignment.BeginDate);
                    DbUtils.AddParameter(cmd, "@EndDate", patientAssignment.EndDate);


                    patientAssignment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }


        public void Update(PatientAssignment patientAssignment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE PatientAssignment
                        SET ProviderProfileId = @ProviderProfileId,
                            PatientProfileId = @PatientProfileId,
                            BeginDate = @BeginDate,
                            EndDate = @EndDate
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@ProviderProfileId", patientAssignment.ProviderProfileId);
                    DbUtils.AddParameter(cmd, "@PatientProfileId", patientAssignment.PatientProfileId);
                    DbUtils.AddParameter(cmd, "@BeginDate", patientAssignment.BeginDate);
                    DbUtils.AddParameter(cmd, "@EndDate", patientAssignment.EndDate);
                    DbUtils.AddParameter(cmd, "@Id", patientAssignment.Id);

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
                        DELETE FROM PatientAssignment
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        private PatientAssignment PatientAssignmentFormat(SqlDataReader reader)
        {
            return new PatientAssignment()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                ProviderProfileId = DbUtils.GetInt(reader, "ProviderProfileId"),
                PatientProfileId = DbUtils.GetInt(reader, "PatientProfileId"),
                BeginDate = DbUtils.GetDateTime(reader, "BeginDate"),
                EndDate = DbUtils.GetDateTime(reader, "EndDate"),
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
