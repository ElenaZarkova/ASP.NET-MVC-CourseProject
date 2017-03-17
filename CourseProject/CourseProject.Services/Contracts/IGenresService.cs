using System.Collections.Generic;
using CourseProject.Models;

namespace CourseProject.Services.Contracts
{
    public interface IGenresService
    {
        IEnumerable<Genre> GetAllGenres();
    }
}