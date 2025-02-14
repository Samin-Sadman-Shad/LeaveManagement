using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.Common
{
    /// <summary>
    /// Allows it's children to access the Id
    /// </summary>
    public class BaseQueryDto:IBaseDto
    {
        public int Id { get; set; }
    }
}
