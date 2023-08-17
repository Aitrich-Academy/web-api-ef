using AutoMapper;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HireMeNowWebApi.Data.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private HireMeNowDbContext _context;
        private List<Message> _messages=new List<Message>();
        private Dictionary<string,List<Message>> messagesDic = new Dictionary<string,List<Message>>();
        private static List<MessageGroup> messageGroups = new List<MessageGroup>();
        public ChatRepository(HireMeNowDbContext context, IMapper mapper, IUserRepository userRepository)
        {
            _context = context;
            _mapper = mapper;
            _userRepository=userRepository;
        }
        public async Task<bool> CreateChatGroupAsync( string groupName, Message message)
        {
            MessageGroup group = new MessageGroup();
            group.Name = groupName;
            group.Messages.Add(message);
            group.newCount++;

            User userFrom = new User();
            userFrom.Email=message.From;
            userFrom = _userRepository.GetUserByEmail(message.From);

            User userTo = new User();
            userTo.Email=message.To;
            userTo= _userRepository.GetUserByEmail(message.To);

            group.Users.Add(userFrom);
            group.Users.Add(userTo);

            messageGroups.Add(group);

            if (messagesDic.ContainsKey(groupName))
            {
                return false;

            }
            messagesDic.Add(groupName, new List<Message>() {message });
            var grp= _context.MessageGroups.Where(e=>e.Name==groupName).Count();
            if (grp > 0) { return false; }
            await _context.MessageGroups.AddAsync(group);
            _context.SaveChanges();

            return true;
        }
        public void AddChat(Message message,string groupName)
        {
            //var index = messageGroups.FindIndex(e => e.Name==groupName);
            //if (index != -1)
            //{
            //    messageGroups[index].Messages.Add(message);
            //    messageGroups[index].newCount++;

            //}
            //    if (messagesDic.ContainsKey(groupName))
            //{
            //    messagesDic[groupName].Add(message);
            //}
                //updating to database
                var groupid= _context.MessageGroups.Where(e=>e.Name.Contains(groupName)).Select(e=>e.Id).FirstOrDefault();
            message.MessageGroupId=groupid;
            _context.Messages.Add(message);
            _context.SaveChanges();
            //_context.MessageGroups.Where()
        }
        public async Task<List<Message>> GetMessagesByGroupName(string groupName) {
            //if (messagesDic.ContainsKey(groupName))
            //{
            //   return messagesDic[groupName];
            //}
            //return null;
            var msgs=  _context.MessageGroups.Where(e=>e.Name.Equals(groupName)).SelectMany(x=>x.Messages);
            var res= await msgs.ToListAsync();
            return res;
        }

        public async Task<List<MessageGroup>> GetAllMessageGroupsByEmail(string email)
        {
            //var res= messageGroups.Where(e=>e.Users.Any(e=>e.Email==email)).ToList();
            var res2= await _context.MessageGroups.Where(e=>e.Users.Any(e => e.Email==email)).ToListAsync();
            return res2;
            //return messageGroups;
        }

        public async Task<List<MessageGroup>> GetAllMessageGroupsByUserId(Guid userId)
        {
         
            var res2 = await _context.MessageGroups.Where(e => e.Users.Any(e => e.Id==userId)).ToListAsync();
            return res2;
        }
    }
}
