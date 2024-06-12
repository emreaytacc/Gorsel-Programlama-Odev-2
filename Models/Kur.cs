using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emre.Models
{
    public class Kur
    {
        public KurDetay USD { get; set; }
        public KurDetay EUR { get; set; }
        public KurDetay GBP { get; set; }
        public KurDetay GA { get; set; }
        public KurDetay C { get; set; }
        public KurDetay GAG { get; set; }
        public KurDetay BTC { get; set; }
        public KurDetay ETH { get; set; }
    }
}
