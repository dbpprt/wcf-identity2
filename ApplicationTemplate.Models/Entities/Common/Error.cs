using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationTemplate.Models.Entities.Common
{
    [Table("Errors")]
    public class Error
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid Id { get; set; }

        public string HostName { get; set; }

        public string TypeName { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }

        public string User { get; set; }

        public int StatusCode { get; set; }

        public DateTime TimeWritten { get; set; }

        public string ErrorXml { get; set; }
    }
}
