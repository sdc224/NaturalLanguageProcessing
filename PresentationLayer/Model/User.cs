using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageProcessor.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public string Location { get; set; }

        public string HostName { get; set; }

        public string IpAddress { get; set; }
    }
}
