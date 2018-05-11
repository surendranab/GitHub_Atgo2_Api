using System;

namespace Atgo2.Api.Entity
{
    public class AppSettings
    {
        public ConnectionInfo connectionInfo { get; set; }
        public Settings settings { get; set; }
        //public NlogSettings NlogSettings { get; set; }
        //public EmailSettings EmailSettings { get; set; }
        //public Events Events { get; set; }
    }
}
