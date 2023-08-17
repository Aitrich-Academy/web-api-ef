using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HireMeNowWebApi.Services
{
    public class ChatService
    {
        IUserRepository _userRepository;
        private HireMeNowDbContext context;
        //private static readonly Dictionary<string, string> Users = new Dictionary<string, string>();
        public ChatService(IUserRepository userRepository, HireMeNowDbContext _context)
        {
            _userRepository=userRepository;
            context=_context;
        }
        //public bool AddUsersToList(string UserId)
        //{
        //    lock (Users)
        //    {
        //        foreach (var user in Users)
        //        {
        //            if (Users.ContainsKey(UserId))
        //            {
        //                return false;

        //            }
        //        }

        //        Users.Add(UserId, null);
        //        return true;
        //    }
        //}

        public async Task AddUserConnectionIdAsync(string user, string ConnectionId)
        {
            //lock (Users)
            //{
            //    if (Users.ContainsKey(user))
            //    {
            //        Users[user] = ConnectionId;
            //    }
            //}
           var userToUpdate= _userRepository.GetUserByEmail(user);
            userToUpdate.ConnectionId=ConnectionId;
            userToUpdate.OnlineStatus=true;
            userToUpdate.LastActive=DateTime.Now;
            context.Users.Update(userToUpdate);
            context.SaveChanges();
            //await _userRepository.Update(userToUpdate);
        }

        public User GetUserByConnectionId(string connectionId)
        {
            //lock (Users)
            //{
            //    return Users.Where(x => x.Value == connectionId).Select(x => x.Key).FirstOrDefault();
            //}
            return context.Users.Where(x => x.ConnectionId==connectionId).FirstOrDefault();
        }

        public User GetConnectionIdByUser(string user)
        {
            //lock (Users)
            //{
            //    return Users.Where(x => x.Key == user).Select(x => x.Value).FirstOrDefault();
            //}
            return context.Users.Where(x => x.Email==user).FirstOrDefault();
        }

        public async void RemoveUserFromList(User user)
        {
            
            user.OnlineStatus = false;
            //user.ConnectionId="";
            user.LastActive=DateTime.Now;
           await _userRepository.DisconnectUserAsync(user.Id);
        }

        public async Task<List<User>> GetOnlineUsers()
        {
            //lock (Users)
            //{
            //    return Users.OrderBy(x => x.Value).Select(x => x.Key).ToArray();
            //}

           return await context.Users.Where(e=>e.OnlineStatus ==true && e.ConnectionId!="").ToListAsync();
        }
    }
}
