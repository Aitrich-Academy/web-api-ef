using HireMeNowWebApi.Enums;
using HireMeNowWebApi.Exceptions;
using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HireMeNowWebApi.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private List<Company> companies = new List<Company>();
        HireMeNowDbContext context;

		public CompanyRepository(HireMeNowDbContext _context, AutoMapper.IMapper _mapper)
        {
            context= _context;
        }

		public byte[] ConvertImageToByteArray(IFormFile image)
		{
	
				using (var memoryStream = new MemoryStream())
				{
					image.CopyTo(memoryStream);
					return memoryStream.ToArray();
				}
			
		}

		public List<Company> getAllCompanies(string? name)
        {
            if(name == null)    
            return context.Companies.ToList();
            else return context.Companies.Where(e=>e.Name==name).ToList();
        }

        public Company? getById(Guid id)
        {
           return context.Companies.Where(c=>c.Id == id).FirstOrDefault();
        }

        public Company? Register(Company company)
        {
            company.Id=Guid.NewGuid();
           context.Companies.Add(company);
            context.SaveChanges();
            return company;
        }

        public Company Update(Company company)
        {
            var indexToUpdate =context.Companies.Where(item => item.Id == company.Id).FirstOrDefault();
            if (indexToUpdate != null)
            {
				// Modify the properties of the item at the found index
				indexToUpdate.Name = company.Name ?? indexToUpdate.Name;

                indexToUpdate.Email = company.Email ?? indexToUpdate.Email;

				indexToUpdate.Website = company.Website ?? indexToUpdate.Website;
				indexToUpdate.Vision = company.Vision ?? indexToUpdate.Vision;
				indexToUpdate.Mission = company.Mission ?? indexToUpdate.Mission;
				indexToUpdate.Location = company.Location ?? indexToUpdate.Location;
				indexToUpdate.Address = company.Address ?? indexToUpdate.Address;
				indexToUpdate.Logo = company.Logo ?? indexToUpdate.Logo;
				indexToUpdate.Phone = company.Phone==null ? indexToUpdate.Phone : company.Phone;
				context.Companies.Update(indexToUpdate);
				 context.SaveChanges();

			}
            else
            {
                throw new NotFoundException("Company Not Found");
            }

            return indexToUpdate;
        }
        public async Task<PagedList<Company>> GetAllByFilter(CompanyListParams companyListParams)
        {
            var query = context.Companies
               .OrderByDescending(c => c.CreatedDate)
               //.ProjectTo<Job>(_mapper.ConfigurationProvider)
               .AsQueryable();

           
            if (companyListParams.Name != null)
            {
                query = query.Where(c => c.Name.Contains(companyListParams.Name));
            }


            return await PagedList<Company>.CreateAsync(query,
                companyListParams.PageNumber, companyListParams.PageSize);
        }
        public bool IsUserExist(string email)
        {
            return context.Companies.Where(e => e.Email == email).ToList().Count > 0;
        }
    }

}
