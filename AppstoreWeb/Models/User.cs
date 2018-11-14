using System;
using System.Collections.Generic;

namespace AppstoreWeb.Models
{
    public partial class User
    {
        public User()
        {
            Device = new HashSet<Device>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }

        public ICollection<Device> Device { get; set; }
    }
}
