namespace HireMeNowWebApi.Dtos
{
    public class ChatUser
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string connectionId { get; set; }
        public string status { get; set; }
        public string lastseen { get; set; }
        public ChatUser()
        {

        }
    }
}
