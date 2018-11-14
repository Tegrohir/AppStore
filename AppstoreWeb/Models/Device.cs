using System;
using System.Collections.Generic;

namespace AppstoreWeb.Models
{
    public partial class Device
    {
        public Device()
        {
            Result = new HashSet<Result>();
        }

        public int DeviceId { get; set; }
        public int UserId { get; set; }
        public int AppId { get; set; }

        public App App { get; set; }
        public User User { get; set; }
        public ICollection<Result> Result { get; set; }
    }
}
