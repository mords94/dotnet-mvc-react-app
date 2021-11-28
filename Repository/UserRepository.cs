using System.Threading.Tasks;
using dotnet.Data.Repository;
using dotnet.Models;

namespace dotnet.Repository
{
    public class UserRepository : CrudRepository<User, int>
    {
        public UserRepository(DefaultdbContext context) : base(context)
        {
        }


        public async Task<Optional<User>> findByEmail(string email)
        {
            return await findWhere(u => u.personDetails.Email == email);
        }
    }
}