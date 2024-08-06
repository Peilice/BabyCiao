using Microsoft.AspNetCore.Mvc;
using BabyCiaoAPI.DTO;
using BabyCiaoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BabyCiaoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInformationController : ControllerBase
    {
        private readonly BabyciaoContext _context;

        public UserInformationController(BabyciaoContext context)
        {
            _context = context;
        }

        // 讀取所有會員資訊
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInformation>>> GetUserInfos()
        {
            var userInfos = await _context.UserInformations.ToListAsync();
            return Ok(userInfos);
        }

        // 讀取單一會員資訊
        [HttpGet("{account}")]
        public async Task<ActionResult<UserInformation>> GetUserInfo(string account)
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

            return Ok(userInfo);
        }

        // 更新會員資訊
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserInfo(int id, [FromBody] UserInformationDTO userInfoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUserInfo = await _context.UserInformations.FirstOrDefaultAsync(u => u.UserinfoId == id);
            if (existingUserInfo == null)
            {
                return NotFound();
            }

            existingUserInfo.AccountUser = userInfoDto.AccountUser;
            existingUserInfo.UserFirstName = userInfoDto.UserFirstName;
            existingUserInfo.UserLastName = userInfoDto.UserLastName;
            existingUserInfo.UserPhoto = userInfoDto.UserPhoto;
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

        // 新增會員資訊
        [HttpPost]
        public async Task<ActionResult<UserInformation>> AddUserInfo([FromBody] UserInformationDTO userInfoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userAccount = await _context.UserAccounts.FirstOrDefaultAsync(u => u.Account == userInfoDto.AccountUser);
            if (userAccount == null)
            {
                return BadRequest("無效的帳號");
            }

            var newUserInfo = new UserInformation
            {
                AccountUser = userInfoDto.AccountUser,
                UserFirstName = userInfoDto.UserFirstName,
                UserLastName = userInfoDto.UserLastName,
                UserPhoto = userInfoDto.UserPhoto,
                Phone = userInfoDto.Phone,
                Address = userInfoDto.Address,
                Gender = userInfoDto.Gender,
                Email = userInfoDto.Email,
                Nickname = userInfoDto.Nickname,
                Birthday = userInfoDto.Birthday,
                ModiifiedDate = DateTime.Now
            };

            _context.UserInformations.Add(newUserInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserInfo), new { account = newUserInfo.AccountUser }, newUserInfo);
        }
    }
}
