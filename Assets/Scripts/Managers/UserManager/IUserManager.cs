namespace Managers
{
    public interface IUserManager
    {
        User CurrentUser { get; }
        void Init(UserData userData);
    }
}