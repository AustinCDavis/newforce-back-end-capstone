using BackEndCapstone.Models;

namespace BackEndCapstone.Repositories
{
    public interface INoteRepository
    {
        void Add(Note note);
        void Delete(int id);
        List<Note> GetNotesByPatientId(int id);
        List<Note> GetNotesByProviderId(int id);
        void Update(Note note);
    }
}