using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Interfaces
{
    public interface IChatRepository
    {
        public Task<bool> CreateChatGroupAsync(string groupName, Message message);
        public void AddChat(Message message, string groupName);
        public Task<List<Message>> GetMessagesByGroupName(string groupName);
        public Task<List<MessageGroup>> GetAllMessageGroupsByEmail(string email);
        public Task<List<MessageGroup>> GetAllMessageGroupsByUserId(Guid userId);
    }
}
