using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireMeNowWebApi.Models
{
    public class MessageGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int newCount { get; set; }
        public bool IsNewMessages { get; set; }
        public IList<User> Users { get; set; }=new List<User>();
        public IList<Message> Messages { get; set; }=new List<Message>();


    }
}
