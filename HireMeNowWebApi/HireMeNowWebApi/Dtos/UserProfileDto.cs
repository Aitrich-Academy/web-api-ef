using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Dtos
{
	public class UserProfileDto:UserDto
	{
		public string? About { get; set; }
		public List<Skill>? Skills { get; set; } = new List<Skill>();
		public List<Qualification>? Educations { get; set; } = new List<Qualification>();
		public List<Experience>? Experiences { get; set; } = new List<Experience>();
		public string? Designation { get; set; }
		public Guid? companyId { get; set; }
		public string? Image { get; set; }
	}
}
