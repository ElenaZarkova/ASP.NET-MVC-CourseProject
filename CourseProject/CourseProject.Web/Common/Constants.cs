namespace CourseProject.Web.Common
{
    public static class Constants
    {
        public const int BooksPerPage = 3;
        public const string GenresCache = "genres";
        public const int GenresExpirationInMinutes = 30;
        public const string AdminRole = "Admin";
        public const string RegularRole = "Regular";
        public const string ViewModelsAssembly = "CourseProject.ViewModels";

        public const string CoverFileErrorMessage = "Cover photo should be an image file.";
        public const string TitleExistsErrorMessage = "There is already a book with this title added to BetterReads";
        public const string ImagesRelativePath = "~/Content/Images/";
        public const string AddBookSuccessMessage = "Your book was added successfully.";
        public const string AddBookSuccessKey = "Addition";

        public const int TopBooksCount = 8;
        public const string TopBooksCache = "topBooks";
        public const int TopBooksExpirationInMinutes = 10;
    }
}