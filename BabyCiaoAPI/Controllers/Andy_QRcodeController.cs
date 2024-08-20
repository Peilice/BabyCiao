using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using BabyCiaoAPI.Models;
using BabyCiaoAPI.DTO;
using System.Drawing;
using QRCoder;
using System.Drawing.Imaging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class Andy_QRcodeController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public Andy_QRcodeController(BabyciaoContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpcontextAccessor = httpContextAccessor;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> getQRcode(int id) {
            var user = _httpcontextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);

            if (user == null)
            {
                return null;
            }

            var ebook=_context.ContactBooks.Where(c=>c.Id==id).FirstOrDefault();
            byte[] QRcode = createQRcode($"{ebook.Id},{ebook.BabyName},{user}");
           
            string base64String = Convert.ToBase64String(QRcode);

            return Ok(base64String);
        }

        private byte[] createQRcode(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeImage = qrCode.GetGraphic(20);
                return qrCodeImage;
            }
            
        }

    }
}
