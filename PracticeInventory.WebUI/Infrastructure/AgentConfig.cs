namespace PracticeInventory.WebUI.Infrastructure;

public class AgentConfig
{
    public const string BaseAddress = "ClientBaseAddress:url";
    public static class Account
    {
        public const string BaseController = "Account";
        public const string Login = BaseController + "/login";
        public const string Register = BaseController + "/register";
    }
    public static class Users
    {
        public const string BaseController = "User";
        public const string GetAll = BaseController + "/users";
        public const string GetDetail = BaseController + "/detail-user";
        public const string Add = BaseController + "/add-user";
        public const string Update = BaseController + "/update-user";
        public const string Delete = BaseController + "/delete-user";
    }
    public static class Roles
    {
        public const string BaseController = "Role";
        public const string GetAll = BaseController + "/roles";
    }
}
