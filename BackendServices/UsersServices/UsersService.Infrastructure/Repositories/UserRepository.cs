using Dapper;
using UsersService.Core.Entities;
using UsersService.Core.RepositoriesContracts;
using UsersService.Infrastructure.DbContext;

namespace UsersService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DapperDbContext _dbContext;

    public UserRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser> AddUser(ApplicationUser user)
    {
        user.UserId = Guid.NewGuid();
        string query = @"
            INSERT INTO public.""Users""(""UserId"",""Email"",""Password"",""PersonName"",""Gender"")
              VALUES(@UserId,@Email,@Password,@PersonName,@Gender)";

        var rowsAffected = await _dbContext.Connection.ExecuteAsync(query, user);

        if (rowsAffected > 0)
        {
            return user;
        }
        return null;
    }

    public async Task<ApplicationUser> GetUserByEmailAndPAssword(string email, string password)
    {
        string query = " SELECT * FROM public.\"Users\" WHERE \"Email\"=@Email AND \"Password\"=@Password ";

        var parameters = new { Email = email, Password = password };

        var user = await _dbContext.Connection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);

        return user;
    }
}
