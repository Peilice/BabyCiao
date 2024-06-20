using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class ContactBook
{
    public int Id { get; set; }

    public string ParentsIdUserAccount { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string BabyName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string BloodType { get; set; } = null!;

    public string EmergencyContact { get; set; } = null!;

    public string EmergencyContactPhone1 { get; set; } = null!;

    public string? EmergencyContactPhone2 { get; set; }

    public virtual ICollection<DiaperDetail> DiaperDetails { get; set; } = new List<DiaperDetail>();

    public virtual ICollection<Diary> Diaries { get; set; } = new List<Diary>();

    public virtual ICollection<DietDetail> DietDetails { get; set; } = new List<DietDetail>();

    public virtual ICollection<HealthInformation> HealthInformations { get; set; } = new List<HealthInformation>();

    public virtual ICollection<Memo> Memos { get; set; } = new List<Memo>();

    public virtual UserAccount ParentsIdUserAccountNavigation { get; set; } = null!;

    public virtual ICollection<SleepDetail> SleepDetails { get; set; } = new List<SleepDetail>();
}
