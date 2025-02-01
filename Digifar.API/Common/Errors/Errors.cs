namespace Digifar.API.Common.Errors
{
    public static class Errors
    {

        public const string DuplicateEmail = "User with this Email already exists.";
        public const string DuplicateUsername = "User with this Username already exists.";
        public const string WrongUsername = "The username entered doesn't exists.";
        public const string IncorrectPassword = "The password you entered is incorrect.";
        public const string SignInFailure = "An error occured, failed to Sign In.";


        public const string DuplicateBlogName = "Blog with this name already exists.";
    }
}
