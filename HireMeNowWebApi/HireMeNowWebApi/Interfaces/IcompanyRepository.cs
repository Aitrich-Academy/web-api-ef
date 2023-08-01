using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Interfaces
{
    public interface ICompanyRepository
    {
        List<Company> getAllCompanies(string? name);
        Company? getById(Guid id);
       bool IsUserExist(string email);
        Company? Register(Company company);
        Company Update(Company company);
		byte[] ConvertImageToByteArray(IFormFile image);
        Task<PagedList<Company>> GetAllByFilter(CompanyListParams companyListParams);

    }
}
