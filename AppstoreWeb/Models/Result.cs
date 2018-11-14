using System;
using System.Collections.Generic;

namespace AppstoreWeb.Models
{
    public partial class Result
    {
        public int ResultId { get; set; }
        public int DeviceId { get; set; }
        public string ResultJson { get; set; }
        public DateTime Date { get; set; }

        public Device Device { get; set; }
    }
}
