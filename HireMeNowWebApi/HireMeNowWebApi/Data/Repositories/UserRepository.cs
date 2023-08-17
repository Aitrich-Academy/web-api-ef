using AutoMapper;
using HireMeNowWebApi.Enums;
using HireMeNowWebApi.Exceptions;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HireMeNowWebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
         static List<User> users = new List<User>();
		private HireMeNowDbContext context;
		private IMapper mapper;

		public UserRepository(HireMeNowDbContext context, IMapper mapper)
		{
			this.context=context;
			this.mapper=mapper;
		}

	
		public User getById(Guid userId)
        {
           User user= context.Users.Where(e =>e.Id==userId)
                .Include(e=>e.Skills)
                .Include(e => e.Experiences)
                .Include(e => e.Qualifications).FirstOrDefault();
            return user;
        }

        public User GetUserByEmail(string email)
        {
			return context.Users.FirstOrDefault(e => e.Email==email);
		}

        public async Task<User> registerAsync(User user)
        {
			user.Id = Guid.NewGuid();
			//user.Role = Roles.CompanyMember;

			if (context.Users.Where(e => e.Email == user.Email).Count() <= 0)
			{
				await context.Users.AddAsync(user);
				context.SaveChanges();
				return user;
			}
			else
			{
				throw new UserAlreadyExistException(user.Email);
			}
			
        }

        public async Task<User> Update(User updatedUser)
        {
            try
            {
                var usertoUpdate = await context.Users.FirstOrDefaultAsync(item => item.Id == updatedUser.Id);
                if (usertoUpdate != null)
                {
                    // Modify the properties of the item at the found index
                    usertoUpdate.About = updatedUser.About ?? usertoUpdate.About;

                    usertoUpdate.Image = updatedUser.Image ?? usertoUpdate.Image;
                    usertoUpdate.FirstName = updatedUser.FirstName??usertoUpdate.FirstName;
                    usertoUpdate.LastName = updatedUser.LastName??usertoUpdate.LastName;
                    usertoUpdate.Location = updatedUser.Location??usertoUpdate.Location;
                    usertoUpdate.Gender = updatedUser.Gender??usertoUpdate.Gender;
                    usertoUpdate.Phone = updatedUser.Phone==null ? usertoUpdate.Phone : updatedUser.Phone;
                    usertoUpdate.Email = updatedUser.Email == null ? usertoUpdate.Email : updatedUser.Email;

                    //for chat application
                    //usertoUpdate.ConnectionId = updatedUser.ConnectionId;
                    //updatedUser.OnlineStatus= updatedUser.OnlineStatus;
                    //updatedUser.LastActive= updatedUser.LastActive;


                    context.Users.Update(usertoUpdate);
                    await context.SaveChangesAsync();

                }
                else
                {
                    throw new NotFoundException("User Not Found");
                }

                return usertoUpdate;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
		
		public async Task<User> memberRegister(User user)
        {
            user.Id = Guid.NewGuid();
            //user.Role = Roles.CompanyMember;

            if (context.Users.Where(e => e.Email == user.Email).Count()<=0)
            {
			  await context.Users.AddAsync(user);
                context.SaveChanges();
                return user;
            }
            else
            {
                throw new UserAlreadyExistException(user.Email);
            }
        }

        public List<User> memberListing(Guid companyId) 
        {
            var memberList = context.Users.Where(e=>e.Role==Roles.COMPANY_MEMBER && e.CompanyId==companyId).ToList();
            return memberList;
        }

		public User getuser()
		{
			return context.Users.FirstOrDefault();
		}
	

		public List<User> getAllUsers()
		{
            return context.Users.ToList();
		}

        public void memberDeleteById(Guid id)
        {
            User user = context.Users.Where(e => e.Id==id).FirstOrDefault();
            if (user!=null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

		public bool IsUserExist(string email)
		{
           return context.Users.Where(e => e.Email==email).ToList().Count>0;
		}

		public byte[] ConvertImageToByteArray(IFormFile image)
		{
			using (var memoryStream = new MemoryStream())
			{
				image.CopyTo(memoryStream);
				return memoryStream.ToArray();
			}
		}

        public void AddSkill(Skill skill)
        {
            context.Skills.Add(skill);
            context.SaveChanges();
        }

        public async Task<List<Skill>> getSkills(Guid userId)
        {
          return await context.Skills.Where(e=>e.UserId==userId).ToListAsync();
        }

        public async Task AddExperience(Experience experience)
        {
             context.Experiences.Add(experience);
            context.SaveChanges();
        }

        public async Task AddQualification(Qualification qualification)
        {
           context.Qualifications.Add(qualification);
           await context.SaveChangesAsync();
        }

        public async Task DisconnectUserAsync(Guid userId)
        {
            try
            {
                var usertoUpdate = await context.Users.FirstOrDefaultAsync(item => item.Id == userId);

                usertoUpdate.OnlineStatus= false;
                usertoUpdate.LastActive= DateTime.Now;
                 context.Users.Update(usertoUpdate);
                 context.SaveChangesAsync();
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
    }

    

}
