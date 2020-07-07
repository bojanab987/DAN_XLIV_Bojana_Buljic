using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Service
{
    class OrderService
    {
        public tblOrder AddOrder(tblOrder order)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrder newOrder = new tblOrder();
                    newOrder.JMBG = order.JMBG;
                    newOrder.OrderStatus = order.OrderStatus;
                    newOrder.OrderDateTime = order.OrderDateTime;
                    context.tblOrders.Add(newOrder);
                    context.SaveChanges();
                    order.ID = newOrder.ID;
                    return order;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblOrderPizza AddPizzaOrder(tblOrderPizza pizzaOrder)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {

                    tblOrderPizza newPizzaOrder = new tblOrderPizza();
                    newPizzaOrder.Amount = pizzaOrder.Amount;
                    newPizzaOrder.OrderID = pizzaOrder.OrderID;
                    newPizzaOrder.PizzaID = pizzaOrder.PizzaID;

                    context.tblOrderPizzas.Add(newPizzaOrder);
                    context.SaveChanges();
                    pizzaOrder.ID = newPizzaOrder.ID;
                    return pizzaOrder;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void ApproveOrder(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public List<tblOrderPizza> GetOrders()
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    List<tblOrderPizza> list = new List<tblOrderPizza>();
                    list = (from x in context.tblOrderPizzas select x).ToList();
                    foreach (var item in list)
                    {
                        tblOrder order = (from e in context.tblOrders where e.ID == item.OrderID select e).First();
                        tblPizza pizza = (from e in context.tblPizzas where e.ID == item.PizzaID select e).First();

                        item.tblOrder.OrderStatus = order.OrderStatus;
                        item.tblOrder.JMBG = order.JMBG;
                        item.tblOrder.OrderDateTime = order.OrderDateTime;

                        item.tblPizza.PizzaName = pizza.PizzaName;
                        item.tblPizza.Price = pizza.Price;
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblOrderPizza> GetOrdersOfGuest(string JMBG)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    List<tblOrderPizza> list = new List<tblOrderPizza>();
                    list = (from x in context.tblOrderPizzas select x).ToList();
                    foreach (var item in list)
                    {
                        tblOrder order = (from e in context.tblOrders where e.ID == item.OrderID select e).First();
                        tblPizza pizza = (from e in context.tblPizzas where e.ID == item.PizzaID select e).First();

                        item.tblOrder.OrderStatus = order.OrderStatus;
                        item.tblOrder.JMBG = order.JMBG;
                        item.tblOrder.OrderDateTime = order.OrderDateTime;

                        item.tblPizza.PizzaName = pizza.PizzaName;
                        item.tblPizza.Price = pizza.Price;
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        
    }
}
