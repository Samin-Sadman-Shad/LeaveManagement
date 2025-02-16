using HR.LeaveManagement.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Responses.Common
{
    public class BaseQueryResponse : BaseResponse
    {
        private string Error { get; set; }
        public virtual BaseQueryDto Record { get; set; }
        public int RecordId { get; set; }
        private string Message { get; set; }
    }

    public class BaseQueryResponse<T> : BaseResponse where T: BaseQueryDto
    { 
        public T Record { get; set; }
    }
}
