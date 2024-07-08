using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class BabyResume
{
    public int Id { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public string? Photo { get; set; }

    public string FirstName { get; set; } = null!;

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public DateOnly ApplyDate { get; set; }

    public DateOnly RequireDate { get; set; }

    public string Babyage { get; set; } = null!;

    public string TypeOfDaycare { get; set; } = null!;

    public string TimeSlot { get; set; } = null!;

    public string? Memo { get; set; }

    public bool Display { get; set; }

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;
}
