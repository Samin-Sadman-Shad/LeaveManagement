﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Responses.Common
{
    public class BaseCommandResponse : BaseResponse
    {
        public string Message { get; set; }
        public List<string>? Errors { get; set; }
    }
}
