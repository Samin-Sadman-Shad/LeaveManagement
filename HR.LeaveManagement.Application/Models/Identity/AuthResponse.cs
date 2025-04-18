﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Models.Identity
{
    public class AuthResponse:IAuthResponse
    {
        public string? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRegistered { get; set; }
        public string Token { get; set; }

        public string? AuthError { get; set; }
    }
}
