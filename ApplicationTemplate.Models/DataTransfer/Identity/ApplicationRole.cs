using System.Collections.Generic;

namespace ApplicationTemplate.Models.DataTransfer
{
    public class ApplicationRole
    {
        public ApplicationRole()
        {
            Users = new List<ApplicationUserRole>();
        }

        public int Id
        {
            get; set;
        }

        public virtual ICollection<ApplicationUserRole> Users{ get; set; }

        public string Name { get; set; }
    }
}
