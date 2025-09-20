using library_api.Domain.Entities;
using library_api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

// this is the user repository, which has all the DB related implmentation for User entity
// it inherits from the generic repository
// initiate DB context here and take DBset for User entity
namespace library_api.Insfastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext) { }
    }
}
