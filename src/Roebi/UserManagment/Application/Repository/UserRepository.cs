using Roebi.Common.Context;
using Roebi.Common.Repository;
using Roebi.UserManagment.Domain;

namespace Roebi.UserManagment.Application.Repository
{
    class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(RoebiContext context) : base(context) { }
    }
}
