using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Site.Models
{
    public class ConsumeRecordModel
    {
        public int Id { get; set; }

        public string ConsumeType { get; set; }

        public decimal Money { get; set; }

        public DateTime CreateTime { get; set; }

        public string User { get; set; }
    }
}
