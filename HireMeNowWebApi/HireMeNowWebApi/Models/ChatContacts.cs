using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HireMeNowWebApi.Models
{
    public class ChatContacts
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }= Guid.NewGuid();
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public string? ConnectionId { get; set; }
        public string? Status { get; set; }
        public DateTime? LastActive { get; set; }
        public virtual User? User { get; set; }
    }
}
