using BackEndCapstone.Models;

namespace BackEndCapstone.Repositories
{
    public interface IMessageRepository
    {
        void Add(Message message);
        void Delete(int id);
        List<Message> GetMessagesByUserId(int id);
        List<Message> GetMessagesByFromIdAndToId(int fromId, int toId);
    }
}