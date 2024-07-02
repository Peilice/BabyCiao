namespace BabyCiao.GlobarVal
{
    public static class OnlineUsers
    {
        public static List<string> OnlineUser_Names=new List<string>();
        //public static string tempUser;
        
       


        public static void AddOnlineUser(string username) {

            OnlineUser_Names.Add(username);
        }
        public static void RemoveOnlineUser(string username)
        {
            OnlineUser_Names.Remove(username);
        }

        public static string GetOnlineUser(string username) {
            if (OnlineUser_Names.Contains(username))
            {
                return username;
            }
            else
            {
                return null;
            }
        }
        
    }
}
