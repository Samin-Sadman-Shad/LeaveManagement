﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Models.Identity
{
    public class RegisterResponse
    {
        public string? UserId { get; set; }
        public string? RegisterError { get; set; }
    }
}
