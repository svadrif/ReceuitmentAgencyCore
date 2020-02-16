using Microsoft.EntityFrameworkCore;
using RecruitmentAgencyCore.Data;
using RecruitmentAgencyCore.Data.Models;
using System;
using System.Threading.Tasks;

namespace ReceuitmentAgencyCore.Security
{
    public class CustomUserStore : IDisposable
    {
        public AppDbContext _db;
        public CustomUserStore(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(t => t.Email == email);
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
