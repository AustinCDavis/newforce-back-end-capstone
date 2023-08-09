using BackEndCapstone.Models;
using BackEndCapstone.Utils;
using Microsoft.Data.SqlClient;

namespace BackEndCapstone.Repositories
{
    public class RegimenAssignmentRepository : BaseRepository, IRegimenAssignmentRepository
    {
        public RegimenAssignmentRepository(IConfiguration configuration) : base(configuration) { }

        //only used to test functionality for now
        //public List<RegimenAssignment> GetAllregimens()
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                SELECT *
        //                FROM RegimenAssignment
        //                ";

        //            var reader = cmd.ExecuteReader();

        //            var assignments = new List<RegimenAssignment>();
        //            while (reader.Read())
        //            {
        //                assignments.Add(RegimenAssignmentFormat(reader));
        //            }

        //            conn.Close();

        //            return assignments;
        //        }
        //    }
        //}


        public void Add(RegimenAssignment regimenAssignment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO RegimenAssignment (RegimenId, PatientProfileId, AssignmentDate)
                        OUTPUT INSERTED.Id
                        VALUES (@RegimenId, @PatientProfileId, @AssignmentDate)";

                    DbUtils.AddParameter(cmd, "@RegimenId", regimenAssignment.RegimenId);
                    DbUtils.AddParameter(cmd, "@PatientProfileId", regimenAssignment.PatientProfileId);
                    DbUtils.AddParameter(cmd, "@AssignmentDate", regimenAssignment.AssignmentDate);

                    regimenAssignment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }


        public void Update(RegimenAssignment regimenAssignment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE RegimenAssignment
                        SET RegimenId = @RegimenId,
                            PatientProfileId = @PatientProfileId,
                            AssignmentDate = @AssignmentDate
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@RegimenId", regimenAssignment.RegimenId);
                    DbUtils.AddParameter(cmd, "@PatientProfileId", regimenAssignment.PatientProfileId);
                    DbUtils.AddParameter(cmd, "@AssignmentDate", regimenAssignment.AssignmentDate);
                    DbUtils.AddParameter(cmd, "@Id", regimenAssignment.Id);

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
                        DELETE FROM RegimenAssignment
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        private RegimenAssignment RegimenAssignmentFormat(SqlDataReader reader)
        {
            return new RegimenAssignment()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                RegimenId = DbUtils.GetInt(reader, "RegimenId"),
                PatientProfileId = DbUtils.GetInt(reader, "PatientProfileId"),
                AssignmentDate = DbUtils.GetDateTime(reader, "AssignmentDate")
            };
        }

    }
}
