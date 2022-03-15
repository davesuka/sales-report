using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesReport.Models
{
    public class Sales
    {
        [Key]
        public int DealNumber { get; set; }

        public string CustomerName { get; set; }

        public string DealerShipName { get; set; }

        public string Vehicle { get; set; }

        public double Price { get; set; }

        public DateTime Date { get; set; }
    }
}
