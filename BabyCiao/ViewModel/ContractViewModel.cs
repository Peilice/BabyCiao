using BabyCiao.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BabyCiao.ViewModel;

public partial class ContractViewModel
{
    [Display(Name = "合約編號")]
    public int ContractId { get; set; }

    [Display(Name = "保母帳號")]
    public string NannyAccountUserAccount { get; set; } = null!;

    [Display(Name = "保母簽名檔")]
    public bool NannySignature { get; set; }

    [Display(Name = "家長帳號")]
    public string AccountUserAccount { get; set; } = null!;

    [Display(Name = "家長簽名檔")]
    public bool UserSignature { get; set; }

    [Display(Name = "合約開始日")]
    public DateOnly ContractStartTime { get; set; }

    [Display(Name = "合約結束日")]
    public DateOnly ContractFinishTime { get; set; }

    [Display(Name = "合約檔案")]
    public string ContractFile { get; set; } = null!;

    public string Statement { get; set; } = null!;

    [Display(Name = "修改時間")]
    public DateTime ModifiedTime { get; set; }

    [Display(Name = "建立時間")]
    public DateTime BuiledTime { get; set; }

    [Display(Name = "顯示")]
    public bool Display { get; set; }

    [Display(Name = "家長帳號")]
    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;

    [Display(Name = "保母帳號")]
    public virtual UserAccount NannyAccountUserAccountNavigation { get; set; } = null!;

    // 新增簽名檔路徑
    public string NannySignatureFile { get; set; }
    public string UserSignatureFile { get; set; }




    // 新增 IFormFile 
    public IFormFile? ContractFile1 { get; set; }
    public IFormFile? UploadedNannySignatureFile1 { get; set; }
    public IFormFile? UploadedUserSignatureFile1 { get; set; }

    
    public string? ContractFilePath { get; set; }
    public string? NannySignatureFilePath { get; set; }
    public string? UserSignatureFilePath { get; set; }
}
