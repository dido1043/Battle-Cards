namespace Program.Services
{

    public interface IUserSevices
    {
        void CreateUser(string username, string email, string password);
        bool IsUserValid(string username, string password);

        bool IsUsernameValid(string username);
        bool IsEmailValid(string email);
    }
}
