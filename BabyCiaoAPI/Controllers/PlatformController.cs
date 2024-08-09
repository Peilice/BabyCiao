﻿using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.DTO;
using Microsoft.AspNetCore.Cors;
using System;
using Microsoft.JSInterop.Infrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        public PlatformController(BabyciaoContext context)
        {
            _context = context;
        }

        //讀取區塊文章
        //Get api/Platform/{type}
        [HttpGet("getArticle/{type}")]
        public async Task<ActionResult<IEnumerable<PlatformDTO>>> getArticle(string type)
        {
            //讀出ID
            var Article= await(from p in _context.Platforms
                               where p.Type==type
                               select new PlatformDTO
                               {
                                   ArticleID=p.Id,
                                   PostAccount=p.AccountUserAccount,
                                   PostTitle=p.Title,
                                   PostType=p.Type,
                                   PostModifiedTime=p.ModifiedTime,
                               }).ToListAsync();
            //將ID單獨進一個List中
            List<int> ids = new List<int>();
            foreach(var i in Article)
            {
                ids.Add(i.ArticleID);
            }
            //遍歷ID讀出第二個表格的筆數，並寫進List
            List<int> count = new List<int>();
            foreach(var n in ids)
            {
                var number = _context.PlatformResponses.Where(c=>c.IdPlatform==n).Count();
                count.Add(number);
            }

            //遍歷筆數List，將資料連同第一個表格欄位一起寫入DTO中
            List<PlatformDTO> pDTOs= new List<PlatformDTO>();
            for(int i=0; i<count.Count(); i++)
            {
                PlatformDTO pDTO = new PlatformDTO()
                {
                    ArticleID = Article[i].ArticleID,
                    PostAccount = Article[i].PostAccount,
                    PostTitle = Article[i].PostTitle,
                    PostType = Article[i].PostType,
                    PostModifiedTime = Article[i].PostModifiedTime,
                    ResponseCount = count[i],
                    
                };
                pDTOs.Add(pDTO);
                
            }
            
            return Ok(pDTOs);
        }


        //創立新文章
        //Post api/Platform/createPost
        [HttpPost("createPost")]
        public async Task<string> createPost ([FromBody] Platform_createDTO createDTO)
        {
            Platform newPost = new Platform()
            {
                AccountUserAccount = createDTO.PostAccount,
                Title = createDTO.PostTitle,
                Content = createDTO.PostContent,
                Type = createDTO.PostType,
            };
            try
            {
                _context.Platforms.Add(newPost);
                _context.SaveChangesAsync();
                return "新增成功";
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }


        //讀取文章+回應
        //Get api/Platform/{id}
        [HttpGet("getResponse/{id}")]
        public async Task<ActionResult<IEnumerable<PlatformResponseDTO>>> getResponse(int id)
        {
            var response = await(from p in _context.Platforms
                                 join pr in _context.PlatformResponses
                                 on p.Id equals pr.IdPlatform
                                 where pr.IdPlatform == id
                                 select new PlatformResponseDTO
                                 {
                                     //文章
                                     ArticleID=p.Id,
                                     PostAccount=p.AccountUserAccount,
                                     PostTitle=p.Title,
                                     PostContent=p.Content,
                                     PostType=p.Type,
                                     PostModifiedTime=p.ModifiedTime,
                                     //回應
                                     ResponseID =pr.Id,
                                     ResponseAccount=pr.AccountUserAccount,
                                     ResponseContent=pr.Content,
                                     ResponseModifiedTime=pr.ModifiedTime,
                                     ResponseModifiedTimeView= pr.ModifiedTime.ToString("yyyy-MM-dd HH:MM"),
                                 }).ToListAsync();
            return Ok(response);
        }


        //收藏文章
        //Post api/Platform/newFavorite
        [HttpPost("newFavorite")]
        public async Task<string> newFavorite([FromBody] Favorite_createDTO createDTO)
        {
            PlatformFavorite Favorite = new PlatformFavorite()
            {
                IdPlatform=createDTO.ArticleID,
                AccountUserAccount=createDTO.favoriteAccount,
            };
            try
            {
                _context.PlatformFavorites.Add(Favorite);
                await _context.SaveChangesAsync();
                return "新增成功";
            }
            catch
            {
                return "新增失敗";
            }
        }


        //創立回應
        //Post api/Platform/
        [HttpPost("newResponse")]
        public async Task<string> newResponse([FromBody] Response_createDTO createDTO)
        {
            PlatformResponse Response = new PlatformResponse()
            {
                AccountUserAccount = createDTO.ResponseAccount,
                IdPlatform = createDTO.ArticleID,
                Content=createDTO.ResponseContent,
            };
            try
            {
                _context.PlatformResponses.Add(Response);
                await _context.SaveChangesAsync();
                return "新增成功";
            }
            catch
            {
                return "新增失敗";
            }
        }


    }
}