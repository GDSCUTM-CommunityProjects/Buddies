﻿
using Microsoft.AspNetCore.Identity;

namespace Buddies.API.Entities;

public class User : IdentityUser<int>
{
    public override string Email
    {
        get => base.Email;
        set
        {
            base.Email = value;
            // we are using email in place of username but Identity requires this
            // field be populated regardless
            base.UserName = value;
        }
    }
}