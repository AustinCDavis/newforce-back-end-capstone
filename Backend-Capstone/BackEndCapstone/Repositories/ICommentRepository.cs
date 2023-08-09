using BackEndCapstone.Models;

namespace BackEndCapstone.Repositories
{
    public interface ICommentRepository
    {
        void Add(Comment comment);
        void Delete(int id);
        List<Comment> GetCommentsByExerciseId(int id);
        void Update(Comment comment);
    }
}