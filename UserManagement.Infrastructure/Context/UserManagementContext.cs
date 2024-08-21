using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;

namespace UserManagement.Infrastructure.Context
{
    public class UserManagementContext : IdentityDbContext<User>
    {
        //public DbSet<User> Users { get; set; }
        public UserManagementContext(DbContextOptions<UserManagementContext> options) : base(options)
        {
        }
    }
}
