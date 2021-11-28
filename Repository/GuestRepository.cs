using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet.Data.Repository;
using dotnet.Models;

namespace dotnet.Repository
{
    public class GuestRepository : PageRepository<Guest, int?>
    {
        public GuestRepository(DefaultdbContext context) : base(context)
        {
        }

        public async Task<Optional<Guest>> findByEmail(string email)
        {
            return await findWhere(u => u.PersonDetails.Email == email);
        }

        public async Task<IEnumerable<Guest>> findAllByEmails(IEnumerable<string> emails)
        {
            return await findAllWhere(u => emails.Contains(u.PersonDetails.Email));
        }
    }
}