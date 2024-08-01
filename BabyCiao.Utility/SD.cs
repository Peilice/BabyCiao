using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class SD
    {
        
        //後台系統
        public static string localHost_Backstage = "https://localhost:7000";

        //前端
        public static string localHost_Client = "https://localhost:7231";

        //後端API
        public static string localHost_API = "https://localhost:7292";

        public static List<string> Online_UserNames {  get; set; }

        public static void AddOnlineUser(string username)
        {

            Online_UserNames.Add(username);
        }
        public static void RemoveOnlineUser(string username)
        {
            Online_UserNames.Remove(username);
        }

        public static string GetOnlineUser(string username)
        {
            if (Online_UserNames.Contains(username))
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
