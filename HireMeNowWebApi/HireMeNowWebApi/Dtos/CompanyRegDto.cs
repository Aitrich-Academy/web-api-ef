using System.ComponentModel.DataAnnotations;

namespace HireMeNowWebApi.Dtos
{
    public class CompanyRegDto
    {
      
        public Guid? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Website { get; set; }
        [Required]
        public string? Phone { get; set; }

        public byte[]? Logo { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? About { get; set; }
        public string? Vision { get; set; }
        public string? Mission { get; set; }
        [Required]
        public string? Location { get; set; }
        public string? Address { get; set; }

        public CompanyRegDto()
        {

        }
        public CompanyRegDto(string? name, string? email, string? website, string? phone, byte[]? image, string? about, string? vision, string? mission, string? location, string? address, Guid? id = null)
        {
            Id = id ?? new Guid();
            Name = name;
            Email = email;
            Website = website;
            Phone = phone;
            Logo = image;
            About = about;
            Vision = vision;
            Mission = mission;
            Location = location;
            Address = address;
        }
    }
}
