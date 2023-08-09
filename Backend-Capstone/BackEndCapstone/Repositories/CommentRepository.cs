using BackEndCapstone.Models;
using BackEndCapstone.Utils;
using Microsoft.Data.SqlClient;

namespace BackEndCapstone.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {

        public CommentRepository(IConfiguration configuration) : base(configuration) { }

        public List<Comment> GetCommentsByExerciseId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT c.Id, c.UserId, c.RegimenExerciseId, c.Content, c.CreateDateTime
                        FROM Comment c
                            LEFT JOIN RegimenExercise re on re.Id = c.RegimenExerciseId
                            LEFT JOIN Exercise e on e.Id = re.ExerciseId
                        WHERE e.Id = @Id
                        ORDER BY c.CreateDateTime";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    var comments = new List<Comment>();
                    while (reader.Read())
                    {
                        comments.Add(CommentFormat(reader));
                    }
                    reader.Close();
                    return comments;

                }
            }
        }


        public void Add(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Comment (UserId, RegimenExerciseId, Content, CreateDateTime)
                        OUTPUT INSERTED.Id
                        VALUES (@UserId, @RegimenExerciseId, @Content, @CreateDateTime)";

                    DbUtils.AddParameter(cmd, "@UserId", comment.UserId);
                    DbUtils.AddParameter(cmd, "@RegimenExerciseId", comment.RegimenExerciseId);
                    DbUtils.AddParameter(cmd, "@Content", comment.Content);
                    DbUtils.AddParameter(cmd, "@CreateDateTime", comment.CreateDateTime);

                    comment.Id = (int)cmd.ExecuteScalar();
                }

            }
        }


        public void Update(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Comment
                        SET UserId = @UserId,
                            RegimenExerciseId = @RegimenExerciseId,
                            Content = @Content,
                            CreateDateTime = @CreateDateTime
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@UserId", comment.UserId);
                    DbUtils.AddParameter(cmd, "@RegimenExerciseId", comment.RegimenExerciseId);
                    DbUtils.AddParameter(cmd, "@Content", comment.Content);
                    DbUtils.AddParameter(cmd, "@CreateDateTime", comment.CreateDateTime);
                    DbUtils.AddParameter(cmd, "@Id", comment.Id);

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
                        DELETE FROM Comment
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        private Comment CommentFormat(SqlDataReader reader)
        {
            return new Comment
            {
                Id = DbUtils.GetInt(reader, "Id"),
                UserId = DbUtils.GetInt(reader, "UserId"),
                RegimenExerciseId = DbUtils.GetInt(reader, "RegimenExerciseId"),
                Content = DbUtils.GetString(reader, "Content"),
                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime")
            };
        }


    }
}
