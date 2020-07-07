using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Service
{
    class OrderService
    {
        /// <summary>
        /// Method for getting all orders from database
        /// </summary>
        /// <returns>list of orders</returns>
        public List<tblOrder> GetAllOrders()
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    List<tblOrder> orders = new List<tblOrder>();
                    orders = (from x in context.tblOrders select x).ToList();
                    return orders;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Method for deleting orders from database 
        /// </summary>
        /// <param name="orderID"></param>
        public void DeleteOrder(int orderID)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrder orderToDelete = (from o in context.tblOrders where o.OrderId== orderID select o).First();
                    context.tblOrders.Remove(orderToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Method for creating new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public tblOrder CreateOrder(tblOrder order)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {

                    tblOrder newOrder = new tblOrder();
                    //newOrder.Username=
                    newOrder.ItemId = order.ItemId;
                    newOrder.Amount = order.Amount;                    
                    newOrder.OrderDateTime = DateTime.Now;
                    newOrder.OrderStatus = order.OrderStatus;
                    context.tblOrders.Add(newOrder);
                    context.SaveChanges();
                    order.OrderId = newOrder.OrderId;
                    return order;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblOrder ChangeOrderStatus(tblOrder order)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrder orderTochange = (from o in context.tblOrders where o.OrderId == order.OrderId select o).First();
                    orderTochange.OrderStatus = order.OrderStatus;                 

                    context.SaveChanges();
                    return order;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        private vwOrder FindOrderById(int id)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    vwOrder order = (from x in context.vwOrders where x.OrderId== id select x).FirstOrDefault();
                    return order;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;            }
            
        }
    }
}
