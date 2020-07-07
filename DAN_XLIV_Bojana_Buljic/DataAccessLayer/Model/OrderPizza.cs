using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class OrderPizza
    {
        public int ID { get; set; }
        public Pizza Pizza { get; set; }
        public int Amount { get; set; }
    }
}
