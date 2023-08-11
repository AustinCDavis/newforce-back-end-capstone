using BackEndCapstone.Models;

namespace BackEndCapstone.Repositories
{
    public interface IExerciseRepository
    {
        void Add(Exercise exercise);
        void Delete(int id);
        List<Exercise> GetAll();
        Exercise GetById(int id);
        List<Exercise> GetExercisesByPatientId(int id);
        List<Exercise> GetExercisesByRegimenId(int id);
        void Update(Exercise exercise);
    }
}