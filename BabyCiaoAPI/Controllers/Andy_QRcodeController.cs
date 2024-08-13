using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using BabyCiaoAPI.Models;
using BabyCiaoAPI.DTO;
using System.Drawing;
using QRCoder;
using System.Drawing.Imaging;

namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class Andy_QRcodeController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        public Andy_QRcodeController(BabyciaoContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> getQRcode(int id) {
            var ebook=_context.ContactBooks.Where(c=>c.Id==id).FirstOrDefault();
            byte[] QRcode = createQRcode(ebook.BabyName);
           
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
