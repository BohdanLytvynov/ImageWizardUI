

using System.Text.RegularExpressions;

namespace Domain.Utilities
{
    public static class ValidationUtility
    {        
        public static bool ValidateTextIsNotEmpty(string value, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrEmpty(value))
            {
                error = "This field must not be empty!";

                return false;
            }

            return true;
        }

        public static bool ValidateDigits(string value, out string error, Func<string, bool> addChecks = default)
        { 
            error = string.Empty;
            
            if (string.IsNullOrEmpty(value))
            {
                error = "This field must not be empty!";

                return false;
            }

            if (addChecks is not null)
            {
                if (OnlyDigits(value) && addChecks.Invoke(value))
                { 
                    return true;
                }
            }
            else if(OnlyDigits(value))
            {
                return true;
            }            
            
            error = "Incorrect input!";

            return false;
        }
        
        private static bool OnlyDigits(string value)
        {
            foreach (char c in value)
            { 
                if(!Char.IsDigit(c))
                    return false;
            }

            return true;
        }
    }
}
