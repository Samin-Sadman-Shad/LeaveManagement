using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Utils
{
    public static class ValidationUtils
    {
        public static List<string> AddValidationErrorsToResponse(ValidationResult validationResult)
        {
            var errors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                if (error != null)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
            return errors;
        }
    }
}
