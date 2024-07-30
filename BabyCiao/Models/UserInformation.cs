using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class UserInformation
{
    public int UserinfoId { get; set; }

    public string AccountUser { get; set; } 

    public string UserFirstName { get; set; } 

    public string UserLastName { get; set; } 

    public string Phone { get; set; } 

    public string Address { get; set; } 

    public int Gender { get; set; }

    public string Email { get; set; } 

    public DateOnly Birthday { get; set; }

    public virtual UserAccount AccountUserNavigation { get; set; } 
}
