using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using CourseProject.Models;

namespace CourseProject.Web.Identity.Contracts
{
    public interface IApplicationSignInManager : IDisposable
    {
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);

        Task SignInAsync(User user, bool isPersistent, bool rememberBrowser);
    }
}
