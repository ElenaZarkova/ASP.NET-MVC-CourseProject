using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Services.Contracts
{
    public interface IUsersService
    {
        bool CheckIfUserExists(string username);
    }
}
