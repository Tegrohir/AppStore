using System;
using System.Collections.Generic;

namespace AppstoreWeb.Models
{
    public partial class App
    {
        public App()
        {
            Device = new HashSet<Device>();
        }

        public int AppId { get; set; }
        public string AppName { get; set; }
        public string AppVersion { get; set; }
        public string AppDescription { get; set; }
        public byte[] AppFile { get; set; }

        public ICollection<Device> Device { get; set; }
    }
}
