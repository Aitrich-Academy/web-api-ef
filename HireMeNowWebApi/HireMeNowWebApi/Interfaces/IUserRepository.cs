using HireMeNowWebApi.Helpers;
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
        Task<PagedList<User>> GetAllByFilter(UserListParams userListParams);
        void memberDeleteById(Guid id);
		bool IsUserExist(string email);
	}
}
