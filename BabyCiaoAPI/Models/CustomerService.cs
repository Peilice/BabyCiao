using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class CustomerService
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Context { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Statement { get; set; } = null!;

    public string AccountUserAccount { get; set; } = null!;

    public DateTime Createddated { get; set; }

    public DateTime ModiifiedDate { get; set; }

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;
}
