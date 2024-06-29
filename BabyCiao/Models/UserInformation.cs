using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class UserInformation
{
    public int UserinfoId { get; set; }

    public string AccountUser { get; set; } = null!;

    public string UserFirstName { get; set; } = null!;

    public string UserLastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int Gender { get; set; }

    public string Email { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public virtual UserAccount AccountUserNavigation { get; set; } = null!;
}
