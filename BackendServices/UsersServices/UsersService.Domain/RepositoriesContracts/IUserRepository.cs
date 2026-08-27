using UsersService.Core.Entities;

namespace UsersService.Core.RepositoriesContracts;

public interface IUserRepository
{
    Task<ApplicationUser> AddUser(ApplicationUser user);
    Task<ApplicationUser> GetUserByEmailAndPAssword(string email,string password);
}
