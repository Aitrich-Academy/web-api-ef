using HireMeNowWebApi.Data.UnitOfWorks;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Services;
using Microsoft.AspNetCore.SignalR;

namespace HireMeNowWebApi.HubConfig
{
    public class ChatHub : Hub
    {
        ChatService _chatService;
        IChatRepository _chatRepository;
        IUnitOfWork _unitOfWork;
        public ChatHub(ChatService chatService, IChatRepository chatRepository, IUnitOfWork unitOfWork)
        {
            _chatService=chatService;
            _chatRepository=chatRepository;
            _unitOfWork=unitOfWork;
        }
        public override async Task OnConnectedAsync()
        {
            //string username = Context.GetHttpContext().Request.Query["username"];
            await Groups.AddToGroupAsync(Context.ConnectionId, "Come2Chat");
            
            await Clients.Caller.SendAsync("UserConnected");


        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Come2Chat");
            var user = _chatService.GetUserByConnectionId(Context.ConnectionId);
            if (user!=null)
            {
                _unitOfWork.UserRepository.DisconnectUserAsync(user.Id);
                _=_unitOfWork.Complete();
            }
            //_chatService.RemoveUserFromList(user);
            await  DisplayOnlineUsers();
            await base.OnDisconnectedAsync(exception);
        }
        public async Task AddUserConnectionId(string username)
        {
            _chatService.AddUserConnectionIdAsync(username, Context.ConnectionId);
           //await DisplayMyGroups();
            await DisplayOnlineUsers();
        }

        private async Task DisplayOnlineUsers()
        {
            var OnlineUsers =await  _chatService.GetOnlineUsers();
            var onlineusersarray=OnlineUsers.Select(obj => obj.Email).ToArray();
            await Clients.Groups("Come2Chat").SendAsync("OnlineUsers", OnlineUsers);
        }

        private async Task DisplayMyGroups(string grpName="")
        {
            var user = _chatService.GetUserByConnectionId(Context.ConnectionId);
            if (user!=null)
            {
                List<MessageGroup> ActiveGroups = await _chatRepository.GetAllMessageGroupsByEmail(user.Email);
                //var onlineusersarray = OnlineUsers.Select(obj => obj.Email).ToArray();
                //await Clients.Groups("Come2Chat").SendAsync("ActiveGroups", ActiveGroups);
               // await Clients.Groups("Come2Chat").SendAsync("ActiveGroups", ActiveGroups);
                await Clients.Client(Context.ConnectionId).SendAsync("ActiveGroups", ActiveGroups);
                if (grpName!="")
                {
                    await Clients.Group(grpName).SendAsync("ActiveGroups", ActiveGroups);
                }
            }
        }
        private async Task RefreshMyGroups()
        {
            var user = _chatService.GetUserByConnectionId(Context.ConnectionId);
            if (user!=null)
            {
                List<MessageGroup> ActiveGroups =await _chatRepository.GetAllMessageGroupsByEmail(user.Email);
                //var onlineusersarray = OnlineUsers.Select(obj => obj.Email).ToArray();
                await Clients.Groups("Come2Chat").SendAsync("ActiveGroups", ActiveGroups);
            }
        }

        public async Task ReceiveMessage(Message message)
        {
            await Clients.Group("Come2Chat").SendAsync("NewMessage", message);
        }

        public async Task CreatePrivateChat(Message message)
        {
            string privateGroupName = GetPrivateGroupName(message.From, message.To);
            _chatRepository.CreateChatGroupAsync(privateGroupName, message);
            message.ToGroup=privateGroupName;
            await Groups.AddToGroupAsync(Context.ConnectionId, privateGroupName);
            var toConnectioId = _chatService.GetConnectionIdByUser(message.To);
            if (toConnectioId!=null)
            {
                await Groups.AddToGroupAsync(toConnectioId.ConnectionId, privateGroupName);
                await Clients.Client(toConnectioId.ConnectionId).SendAsync("OpenPrivateChat", message);
            }
           await DisplayMyGroups(privateGroupName);
        }
        public async Task ReceivePrivateMessage(Message message)
        {
            if (message.ToGroup!=null)
            {
                _chatRepository.AddChat(message, message.ToGroup);
                await Clients.Group(message.ToGroup).SendAsync("NewPrivateMessage", message);
            }
            else
            {
                string privateGroupName = GetPrivateGroupName(message.From, message.To);
                _chatRepository.AddChat(message, privateGroupName);
                await Clients.Group(privateGroupName).SendAsync("NewPrivateMessage", message);
            }
        }
        public async Task RemovePrivateChat(string from, string to)
        {
            string privateGroupName = GetPrivateGroupName(from, to);
            await Clients.Group(privateGroupName).SendAsync("ClosePrivateChat");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, privateGroupName);

        }
        private string GetPrivateGroupName(string from, string to)
        {
            var stringToCompare = string.CompareOrdinal(from, to)<0;
            return stringToCompare ? $"{from}-{to}" : $"{to}-{from}";

        }



        public async Task GetMyPrivateGroups(String email)
        {

            //await Clients.Group("Come2Chat").SendAsync("NewMessage", message);
        }
    }
}
