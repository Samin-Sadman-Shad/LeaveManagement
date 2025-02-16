using HR.LeaveManagement.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Responses.Common
{
    public class BaseQueryListResponse : BaseResponse
    {
        private string Error { get; set; }
        public virtual List<BaseQueryDto> Records { get; set; }
        private string Message { get; set; }
    }

    public class BaseQueryListResponse<T> : BaseResponse where T : BaseQueryDto
    {
        public List<T> Records { get; set;}
    }
}
