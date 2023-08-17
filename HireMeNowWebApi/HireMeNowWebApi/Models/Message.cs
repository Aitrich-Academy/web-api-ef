using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireMeNowWebApi.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }
        //[ForeignKey(nameof(FromUser))]
        public Guid? FromUserId { get; set; }
        //[ForeignKey(nameof(ToUser))]
        public Guid? ToUserId { get; set; }
        [ForeignKey(nameof(MessageGroup))]
        public Guid? MessageGroupId { get; set; }

        public string? From { get; set; }
        public string? To { get; set; }
        [Required]
        public string? Content { get; set; }
        public DateTime SentDate { get; set; } = DateTime.Now;
        public string? ToGroup { get; set; }

        public MessageGroup? MessageGroup { get; set; }
        //public User FromUser { get; set; }
        //public User ToUser { get; set; }
    }
}
