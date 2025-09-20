using library_api.Domain.Entities;
using library_api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace library_api.Insfastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext) { }
    }
}
