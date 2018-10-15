using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageProcessor.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public long NetworkId { get; set; }

        public DateTime Time { get; set; }

        public string Location { get; set; }

        public string HostName { get; set; }

        public string IpAddress { get; set; }
    }
}
