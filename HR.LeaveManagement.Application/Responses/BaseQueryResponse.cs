using HR.LeaveManagement.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Responses
{
    public class BaseQueryResponse:BaseResponse
    {
        public string Error { get; set; }
        public IBaseDto Record { get; set; }
        public int RecordId { get; set; }
        public string Message { get; set; }
    }
}
