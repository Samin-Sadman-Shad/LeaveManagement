using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Responses
{
    public class CreateCommandResponse:BaseCommandResponse
    {
        public int RecordId { get; set; }
        public IBaseDto baseDto { get; set; }
    }
}
