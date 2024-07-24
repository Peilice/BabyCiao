namespace BabyCiao
{
    public class UserInfoServer
    {
        public readonly IHttpContextAccessor _HttpContextAccessor;
        public UserInfoServer(IHttpContextAccessor httpContextAccessor ) {

            _HttpContextAccessor= httpContextAccessor;
        }

        public string GetUserName()
        {
            var varClaim = _HttpContextAccessor.HttpContext.User.Identity.Name;
            return varClaim;
        }
        public bool Islogin() { 
            if(_HttpContextAccessor.HttpContext.User.Identity.Name!=null) {
                return true; 
            }else 
            {
                return false; 
            }
        }
    }
}
