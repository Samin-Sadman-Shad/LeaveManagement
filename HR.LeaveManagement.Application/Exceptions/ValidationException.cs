using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Exceptions
{
    /// <summary>
    /// Returns list of properties sent with the request which are wrong
    /// </summary>
    public class ValidationException: ApplicationException
    {
        public List<string> Errors = new List<string>();

        public ValidationException(ValidationResult validationResult)
        {
            foreach(var error in validationResult.Errors)
            {
                Errors.Add(error.ErrorMessage);
            }
        }
    }
}
