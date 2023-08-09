using BackEndCapstone.Models;
using BackEndCapstone.Utils;
using Microsoft.Data.SqlClient;

namespace BackEndCapstone.Repositories
{
    public class RegimenExerciseRepository : BaseRepository, IRegimenExerciseRepository
    {
        public RegimenExerciseRepository(IConfiguration configuration) : base(configuration) { }

        //I believe I should get the exercises vis the exe4rcise repo I 
        //public List<RegimenExercise> GetExercisesByRegimenId(int id)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                SELECT re.Id, re.RegimenId, re. ExerciseId, r.ProviderProfileId, r.Title, r.Description, r.CreateDateTime, e.Name, e.Type, e.Muscle, e.Instructions, e.VideoLocation
        //                FROM RegimenExercise re
        //                    LEFT JOIN Regimen r on re.RegimenId = r.Id
        //                    LEFT JOIN Exercise e on re.ExerciseId = e.Id
        //                WHERE re.RegimenId = @Id
        //                ORDER BY e.Name";

        //            DbUtils.AddParameter(cmd, "@Id", id);

        //            var reader = cmd.ExecuteReader();

        //            var exercises = new List<RegimenExercise>();

        //            while (reader.Read())
        //            {
        //                exercises.Add(RegimenExerciseFormat(reader));
        //            }

        //            reader.Close();
        //            return exercises;
        //        }
        //    }
        //}


        public void Add(RegimenExercise regimenExercise)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO RegimenExercise (RegimenId, ExerciseId)
                        OUTPUT INSERTED.Id
                        VALUES (@RegimenId, @ExerciseID)";

                    DbUtils.AddParameter(cmd, "@RegimenId", regimenExercise.RegimenId);
                    DbUtils.AddParameter(cmd, "@ExerciseId", regimenExercise.ExerciseId);

                    regimenExercise.Id = (int)cmd.ExecuteScalar();
                }
            }
        }


        public void Update(RegimenExercise regimenExercise)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE RegimenExercise 
                        SET RegimenId = @RegimenId,
                            ExerciseId = @ExerciseId
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@RegimenId", regimenExercise.RegimenId);
                    DbUtils.AddParameter(cmd, "@ExerciseId", regimenExercise.ExerciseId);

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
                        DELETE FROM RegimenExercise
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        private RegimenExercise RegimenExerciseFormat(SqlDataReader reader)
        {
            return new RegimenExercise()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                RegimenId = DbUtils.GetInt(reader, "RegimenId"),
                ExerciseId = DbUtils.GetInt(reader, "ExerciseId"),
                Regimen = new Regimen()
                {
                    ProviderProfileId = DbUtils.GetInt(reader, "ProviderProfileId"),
                    Title = DbUtils.GetString(reader, "Title"),
                    Description = DbUtils.GetString(reader, "Description"),
                    CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime")
                }
            };
        }








    }
}
