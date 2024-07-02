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
    }
}
