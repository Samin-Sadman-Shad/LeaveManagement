using HR.LeaveManagement.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Responses
{
    public class BaseQueryListResponse:BaseResponse
    {
        public string Error { get; set; }
        public List<IBaseDto> Records { get; set; }
        public string Message { get; set; }
    }
}
