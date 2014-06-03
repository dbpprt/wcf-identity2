using System.Collections.Generic;

namespace ApplicationTemplate.Models.DataTransfer
{
    public class ApplicationIdentityResult
    {
        public IEnumerable<string> Errors
        {
            get;
            set;
        }

        public bool Succeeded
        {
            get;
            set;
        }

        public ApplicationIdentityResult()
        {
            
        }

        public ApplicationIdentityResult(IEnumerable<string> errors, bool succeeded)
        {
            Succeeded = succeeded;
            Errors = errors;
        }
    }
}
