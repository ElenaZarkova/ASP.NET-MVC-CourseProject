using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using CourseProject.Models;

namespace CourseProject.Web.Identity.Contracts
{
    public interface IApplicationUserManager : IDisposable
    {
        Task<IdentityResult> CreateAsync(User user, string password);

        Task<IdentityResult> AddToRoleAsync(string userId, string role);
    }
}