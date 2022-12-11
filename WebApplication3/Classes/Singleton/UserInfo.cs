namespace WebApplication3.Classes.Singleton
{
    public static class UserInfo
    {
        private static int id;
        private static string login;
        private static string password;
        public static int Id
        {
            get { return id; }
            set { id = value; }
        }
        public static string Login
        {
            get { return login; }
            set { login = value; }
        }
        public static string Password
        {
            get { return password; }
            set { password = value; }
        }


    }
}
