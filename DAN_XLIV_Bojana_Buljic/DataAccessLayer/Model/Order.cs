using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime OrderDateTime{ get; set; }
        public string JMBG { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderPizza> PizzaOrders { get; set; }
    }
}
