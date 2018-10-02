using System;
using System.Net;

namespace LanguageProcessor.Model
{
    public class User
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public string Location { get; set; }

        public string HostName { get; set; }

        public IPAddress IpAddress { get; set; }
    }
}
