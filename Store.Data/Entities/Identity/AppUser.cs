﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string displayName { get; set; }
        public Address address { get; set; }
    }
}