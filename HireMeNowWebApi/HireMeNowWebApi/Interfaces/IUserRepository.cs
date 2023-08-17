using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Interfaces
{
    public interface IUserRepository
    {
        User getById(Guid userId);
        User GetUserByEmail(string email);
        Task<User> registerAsync(User user);
		User getuser();
		Task<User> Update(User user);
		Task<User> memberRegister(User user);
        List<User> memberListing(Guid companyId);
        byte[] ConvertImageToByteArray(IFormFile image);
        List<User> getAllUsers();
		//byte[] ConvertImageToByteArray(IFormFile image);
		void memberDeleteById(Guid id);
		bool IsUserExist(string email);
        void AddSkill(Skill skill);
        Task<List<Skill>> getSkills(Guid userId);
        Task AddExperience(Experience experience);
        Task AddQualification(Qualification qualification);
         Task DisconnectUserAsync(Guid userId);
    }
}
