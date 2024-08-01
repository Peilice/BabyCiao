﻿using BabyCiao.Models;
using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.Models;

    public class CompetitionDetailDTO
    {
        public int Id { get; set; }
        [Display(Name = "活動名稱")]
        public string CompetitionName { get; set; }

        [Display(Name = "活動開始時間")]
        public DateOnly StartTime { get; set; }

        [Display(Name = "活動結束時間")]
        public DateOnly EndTime { get; set; }

        [Display(Name = "活動介紹")]
        public string Content { get; set; }

        [Display(Name = "活動進行狀態")]
        public string Statement { get; set; }

    //活動參賽者

        [Display(Name = "參賽者")]
        public string AccountUserAccount { get; set; }

        public string? CompetitionPhotos { get; set; }
        [Display(Name = "活動照片")]
        public IFormFile CompetitionPhoto { get; set; }
        public int CompetitionDetailId { get; set; }

    //投票數
        //public List<Response>Number { get; set; }
        public int number {  get; set; }

        public class Response
        {
        }
    
    }
