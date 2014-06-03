namespace ApplicationTemplate.Models.DataTransfer
{
    public class ApplicationUserRole
    {
        public virtual int RoleId { get; set; }
        public virtual int UserId { get; set; }

        public ApplicationUserRole() { }
    }
}
