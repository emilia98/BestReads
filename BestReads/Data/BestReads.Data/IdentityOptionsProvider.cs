﻿using Microsoft.AspNetCore.Identity;

namespace BestReads.Data
{
    public static class IdentityOptionsProvider
    {
        public static void GetIdentityOptions(IdentityOptions options)
        {
           // options.Password.RequireDigit = false;
           // options.Password.RequireLowercase = false;
           // options.Password.RequireUppercase = false;
           // options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
        }
    }
}