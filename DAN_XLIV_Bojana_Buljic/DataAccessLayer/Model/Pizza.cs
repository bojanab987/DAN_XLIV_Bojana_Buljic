using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class Pizza
    {
        public int ID { get; set; }
        public string PizzaName { get; set; }
        public decimal Price { get; set; }
    }
}
