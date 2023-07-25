using HireMeNowWebApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace HireMeNowWebApi.Dtos
{
	public class CompanyMemberDto
	{
		public Guid Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		public string? Gender { get; set; }
		public string? Location { get; set; }
		public long? Phone { get; set; }
		[Required]
		public string Password { get; set; }

		public Roles Role { get; set; }
		[Required]
		public Guid CompanyId { get; set; }


		public string? Token { get; set; }

	
		public CompanyMemberDto(string firstName, string lastName, string email, string gender, string? location, long phone, string password, Roles role, Guid companyId)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Gender = gender;
			Location = location;
			Phone = phone;
			Password = password;
			Role = role;
			CompanyId = companyId;
		}

		public CompanyMemberDto()
		{

		}

	

		public CompanyMemberDto(string firstName, string lastName, string email, string gender, string location, long phone, string password, Roles role, Guid companyId, string token)
		{
		
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Gender = gender;
			Location = location;
			Phone = phone;
			Password = password;
			Role = role;
			CompanyId = companyId;
			Token = token;
		}
	}
}
