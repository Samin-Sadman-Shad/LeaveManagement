using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Models
{
    /// <summary>
    /// Uses option pattern to bind the properties to a particular section from congiguration.
    /// It must be a non-abstract with parameterless constructor
    /// </summary>
    public class EmailSettingOptions
    {
        /// <summary>
        /// Fields are not bound, EmailSetting will not be needed to hardcoded
        /// </summary>
        public const string EmailSetting = "EmailSetting";
        //only properties are bound
        public string ApiKey { get; set; }
        public string FromAddress { get; set; }
        public string FromName { get; set; }
    }
}
