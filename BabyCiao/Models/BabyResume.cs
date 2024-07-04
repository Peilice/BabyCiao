using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BabyCiao.Models;

public partial class BabyResume
{
    public int Id { get; set; }

    [Required]
    public string AccountUserAccount { get; set; } = null!;

    public string? Photo { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string City { get; set; } = null!;
    [Required]
    public string District { get; set; } = null!;
    [DataType(DataType.Date)]
    [Required]
    public DateOnly ApplyDate { get; set; }
    [DataType(DataType.Date)]
    [Required]
    public DateOnly RequireDate { get; set; }
    [Required]
    public string Babyage { get; set; } = null!;
    [Required]
    public string TypeOfDaycare { get; set; } = null!;
    [Required]
    public string TimeSlot { get; set; } = null!;

    public string? Memo { get; set; }
    [Required]
    public bool Display { get; set; }

    public virtual UserAccount? AccountUserAccountNavigation { get; set; } = null!;
}
