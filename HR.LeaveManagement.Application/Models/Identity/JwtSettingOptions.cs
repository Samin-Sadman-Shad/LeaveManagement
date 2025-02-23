using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Models.Identity
{
    /// <summary>
    /// Implements option pattern, non-abstract class with a parameterless constructor
    /// </summary>
    public class JwtSettingOptions
    {
        /// <summary>
        /// Fields are not bounded
        /// </summary>
        public const string jwtSettings = "JwtSettings";
        //only properties are bound
        public string Key {  get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
    }
}
