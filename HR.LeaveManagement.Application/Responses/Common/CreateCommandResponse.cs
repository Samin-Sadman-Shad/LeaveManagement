﻿using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Responses.Common
{
    public class CreateCommandResponse : BaseCommandResponse
    {
        public int RecordId { get; set; }
        public virtual IBaseDto Record { get; set; }
    }

    public class CreateCommandResponse<T>:BaseCommandResponse where T : IBaseDto
    {
        public int RecordId { get; set; }
        public T Record { get; set; }
    }
}
