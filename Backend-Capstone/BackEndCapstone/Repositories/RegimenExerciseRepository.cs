using BackEndCapstone.Models;
using BackEndCapstone.Utils;
using Microsoft.Data.SqlClient;

namespace BackEndCapstone.Repositories
{
    public class RegimenExerciseRepository : BaseRepository, IRegimenExerciseRepository
    {
        public RegimenExerciseRepository(IConfiguration configuration) : base(configuration) { }

        //public RegimenExercise GetRegimenExerciseByRegimenIdAndExerciseId(int regimenId, int exerciseId)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                SELECT re.Id, re.RegimenId, re.ExerciseId
        //                FROM RegimenExercise re
        //                    LEFT JOIN Regimen r on r.Id = re.RegimenId
        //                    LEFT JOIN Exercise e on e.Id = re.ExerciseId
        //                WHERE r.Id = @RegimenId AND e.Id = @ExerciseId";

        //            DbUtils.AddParameter(cmd, "@RegimenId", regimenId);
        //            DbUtils.AddParameter(cmd, "@ExerciseId", exerciseId);

        //            var reader = cmd.ExecuteReader();

        //            RegimenExercise regimenExercise = null;

        //            if (reader.Read())
        //            {
        //                regimenExercise = new RegimenExercise()
        //                {
        //                    Id = DbUtils.GetInt(reader, "Id"),
        //                    RegimenId = DbUtils.GetInt(reader, "RegimenId"),
        //                    ExerciseId = DbUtils.GetInt(reader, "ExerciseId")
        //                };
        //            }

        //            reader.Close();
        //            return regimenExercise;
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
