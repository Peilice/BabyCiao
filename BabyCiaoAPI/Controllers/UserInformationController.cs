using Microsoft.AspNetCore.Mvc;
using BabyCiaoAPI.DTO;
using BabyCiaoAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserInformationController : ControllerBase
    {
        private readonly BabyciaoContext _context;

        public UserInformationController(BabyciaoContext context)
        {
            _context = context;
        }

        // 讀取單一會員資訊
        [HttpGet("{account}")]
        public async Task<ActionResult<UserInformationDTO>> GetUserInfo(string account)
        {
            var userAccount = await _context.UserAccounts.FirstOrDefaultAsync(u => u.Account == account);
            if (userAccount == null)
            {
                return NotFound();
            }

            var userInfo = await _context.UserInformations.FirstOrDefaultAsync(u => u.AccountUser == userAccount.Account);
            if (userInfo == null)
            {
                return NotFound();
            }

            // 使用平坦化的 DTO 結構
            var userInfoDto = new UserInformationDTO
            {
                UserinfoId = userInfo.UserinfoId,
                AccountUser = userInfo.AccountUser,
                UserFirstName = userInfo.UserFirstName,
                UserLastName = userInfo.UserLastName,
                UserPhoto = userInfo.UserPhoto,
                Phone = userInfo.Phone,
                Address = userInfo.Address,
                Gender = userInfo.Gender,
                Email = userInfo.Email,
                Nickname = userInfo.Nickname,
                Birthday = userInfo.Birthday,
                CreateddDate = userInfo.CreateddDate,
                ModiifiedDate = userInfo.ModiifiedDate
            };

            return Ok(userInfoDto);
        }

        // 更新會員資訊
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserInfo(int id)
        {
            var form = await Request.ReadFormAsync();

            var userInfoDto = new UserInformationDTO
            {
                UserinfoId = id,
                AccountUser = form["AccountUser"],
                UserFirstName = form["UserFirstName"],
                UserLastName = form["UserLastName"],
                Phone = form["Phone"],
                Address = form["Address"],
                Gender = int.Parse(form["Gender"]),
                Email = form["Email"],
                Nickname = form["Nickname"],
                Birthday = DateOnly.Parse(form["Birthday"]),
                ModiifiedDate = DateTime.Now
            };

            var existingUserInfo = await _context.UserInformations.FirstOrDefaultAsync(u => u.UserinfoId == id);
            if (existingUserInfo == null)
            {
                return NotFound();
            }

            // 更新實體模型的屬性
            existingUserInfo.AccountUser = userInfoDto.AccountUser;
            existingUserInfo.UserFirstName = userInfoDto.UserFirstName;
            existingUserInfo.UserLastName = userInfoDto.UserLastName;

            // 處理上傳的照片
            var userPhoto = form.Files["userPhoto"];
            if (userPhoto != null)
            {
                var uploadsFolder = Path.Combine("wwwroot", "uploads");
                var fileName = Guid.NewGuid().ToString() + "_" + userPhoto.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);

                // 確保目錄存在
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await userPhoto.CopyToAsync(stream);
                }

                existingUserInfo.UserPhoto = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
            }

            existingUserInfo.Phone = userInfoDto.Phone;
            existingUserInfo.Address = userInfoDto.Address;
            existingUserInfo.Gender = userInfoDto.Gender;
            existingUserInfo.Email = userInfoDto.Email;
            existingUserInfo.Nickname = userInfoDto.Nickname;
            existingUserInfo.Birthday = userInfoDto.Birthday;
            existingUserInfo.ModiifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //// 新增會員資訊
        //[HttpPost]
        //public async Task<ActionResult<UserInformationDTO>> AddUserInfo([FromBody] UserInformationDTO userInfoDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var newUserInfo = new UserInformation
        //    {
        //        AccountUser = userInfoDto.AccountUser,
        //        UserFirstName = userInfoDto.UserFirstName,
        //        UserLastName = userInfoDto.UserLastName,
        //        UserPhoto = userInfoDto.UserPhoto,
        //        Phone = userInfoDto.Phone,
        //        Address = userInfoDto.Address,
        //        Gender = userInfoDto.Gender,
        //        Email = userInfoDto.Email,
        //        Nickname = userInfoDto.Nickname,
        //        Birthday = userInfoDto.Birthday,
        //        CreateddDate = DateTime.Now,
        //        ModiifiedDate = DateTime.Now
        //    };

        //    _context.UserInformations.Add(newUserInfo);
        //    await _context.SaveChangesAsync();

        //    var createdUserInfoDto = new UserInformationDTO
        //    {
        //        UserinfoId = newUserInfo.UserinfoId,
        //        AccountUser = newUserInfo.AccountUser,
        //        UserFirstName = newUserInfo.UserFirstName,
        //        UserLastName = newUserInfo.UserLastName,
        //        UserPhoto = newUserInfo.UserPhoto,
        //        Phone = newUserInfo.Phone,
        //        Address = newUserInfo.Address,
        //        Gender = newUserInfo.Gender,
        //        Email = newUserInfo.Email,
        //        Nickname = newUserInfo.Nickname,
        //        Birthday = newUserInfo.Birthday,
        //        CreateddDate = newUserInfo.CreateddDate,
        //        ModiifiedDate = newUserInfo.ModiifiedDate
        //    };

        //    return CreatedAtAction(nameof(GetUserInfo), new { account = createdUserInfoDto.AccountUser }, createdUserInfoDto);
        //}
    }
}
