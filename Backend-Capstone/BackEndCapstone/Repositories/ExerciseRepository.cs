using BackEndCapstone.Models;
using BackEndCapstone.Utils;
using Microsoft.Data.SqlClient;
using System.Xml;

namespace BackEndCapstone.Repositories
{
    public class ExerciseRepository : BaseRepository, IExerciseRepository
    {
        public ExerciseRepository(IConfiguration configuration) : base(configuration) { }
        public List<Exercise> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT e.Id, e.Name, e.Type, e.Muscle, e.Instructions, e.VideoLocation
                        FROM Exercise e
                        ORDER BY e.Muscle
                        ";
                    var reader = cmd.ExecuteReader();

                    var exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        exercises.Add(ExerciseFormat(reader));
                    }
                    reader.Close();

                    return exercises;
                }
            }
        }


        public List<Exercise> GetExercisesByRegimenId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT e.Id, e.Name, e.Type, e.Muscle, e.Instructions, e.VideoLocation, r.ProviderProfileId, r.Title, r.Description, r.CreateDateTime
                        FROM Exercise e
                            LEFT JOIN RegimenExercise re on re.ExerciseId = e.Id
                            LEFT JOIN Regimen r on r.Id = re.RegimenId
                        WHERE r.Id = @Id
                        ORDER BY r.Title";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    var exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        exercises.Add(new Exercise()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Type = DbUtils.GetString(reader, "Type"),
                            Muscle = DbUtils.GetString(reader, "Muscle"),
                            Instructions = DbUtils.GetString(reader, "Instructions"),
                            VideoLocation = DbUtils.GetString(reader, "VideoLocation"),
                            Regimen = new Regimen()
                            {
                                ProviderProfileId = DbUtils.GetInt(reader, "ProviderProfileId"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Description = DbUtils.GetString(reader, "Description"),
                                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime")
                            }
                        });
                    }

                    reader.Close();

                    return exercises;
                }
            }
        }


        public List<Exercise> GetExercisesByPatientId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT e.Id, e.Name, e.Type, e.Muscle, e.Instructions, e.VideoLocation, r.ProviderProfileId, r.Title, r.Description, r.CreateDateTime
                        FROM Exercise e
                            LEFT JOIN RegimenExercise re on re.ExerciseId = e.Id
                            LEFT JOIN Regimen r on r.Id = re.RegimenId
                            LEFT JOIN RegimenAssignment ra on ra.RegimenId = r.Id
                        WHERE ra.PatientProfileId = @Id
                        ORDER BY r.Title";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    var exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        exercises.Add(new Exercise()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Type = DbUtils.GetString(reader, "Type"),
                            Muscle = DbUtils.GetString(reader, "Muscle"),
                            Instructions = DbUtils.GetString(reader, "Instructions"),
                            VideoLocation = DbUtils.GetString(reader, "VideoLocation"),
                            Regimen = new Regimen()
                            {
                                ProviderProfileId = DbUtils.GetInt(reader, "ProviderProfileId"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Description = DbUtils.GetString(reader, "Description"),
                                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime")
                            }
                        });
                    }

                    reader.Close();

                    return exercises;
                }
            }
        }


        public void Add(Exercise exercise)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Exercise (Name, Type, Muscle, Instructions, VideoLocation)
                        OUTPUT INSERTED.Id
                        VALUES (@Name, @Type, @Muscle, @Instructions, @VideoLocation)";

                    DbUtils.AddParameter(cmd, "@Name", exercise.Name);
                    DbUtils.AddParameter(cmd, "@Type", exercise.Type);
                    DbUtils.AddParameter(cmd, "@Muscle", exercise.Muscle);
                    DbUtils.AddParameter(cmd, "@Instructions", exercise.Instructions);
                    DbUtils.AddParameter(cmd, "@VideoLocation", exercise.VideoLocation);

                    exercise.Id = (int)cmd.ExecuteScalar();

                }

            }
        }


        public void Update(Exercise exercise)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Exercise
                        SET Name = @Name,
                            Type = @Type,
                            Muscle = @Muscle,
                            Instructions = @Instructions,
                            VideoLocation = @VideoLocation
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Name", exercise.Name);
                    DbUtils.AddParameter(cmd, "@Type", exercise.Type);
                    DbUtils.AddParameter(cmd, "@Muscle", exercise.Muscle);
                    DbUtils.AddParameter(cmd, "@Instructions", exercise.Instructions);
                    DbUtils.AddParameter(cmd, "@VideoLocation", exercise.VideoLocation);
                    DbUtils.AddParameter(cmd, "@Id", exercise.Id);

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
                        DELETE FROM Exercise
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        private Exercise ExerciseFormat(SqlDataReader reader)
        {
            return new Exercise()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Name = DbUtils.GetString(reader, "Name"),
                Type = DbUtils.GetString(reader, "Type"),
                Muscle = DbUtils.GetString(reader, "Muscle"),
                Instructions = DbUtils.GetString(reader, "Instructions"),
                VideoLocation = DbUtils.GetString(reader, "VideoLocation")
            };
        }


    }
}
