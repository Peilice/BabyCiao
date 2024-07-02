using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class BabyResume
{
    public int Id { get; set; }

    public string AccountUserAccount { get; set; } 

    public string Photo { get; set; }

    public string FirstName { get; set; } 

    public string City { get; set; } 

    public string District { get; set; } 

    public DateOnly ApplyDate { get; set; }

    public DateOnly RequireDate { get; set; }

    public DateOnly BabyBirthday { get; set; }

    public string TypeOfDaycare { get; set; }

    public string TimeSlot { get; set; }

    public string Memo { get; set; }

    public bool Display { get; set; }

    public virtual UserAccount AccountUserAccountNavigation { get; set; } 
}
