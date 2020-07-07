using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Service;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class EmployeeViewModel:ViewModelBase
    {
        EmployeeView employeeView;
        OrderService orderService;
        

        public EmployeeViewModel(EmployeeView viewOpen)
        {                        
            employeeView = viewOpen;
            orderService = new OrderService();
            OrdersList = orderService.GetAllOrders();           

        }

        #region properties
        private tblOrder order;
        public tblOrder Order
        {
            get { return order; }
            set
            {
                order = value;
                OnPropertyChanged("Order");
            }
        }

        private List<tblOrder> ordersList;
        public List<tblOrder> OrdersList
        {
            get { return ordersList; }
            set
            {
                ordersList = value;
                OnPropertyChanged("OrdersList");
            }
        }

        private tblOrder selectedOrder;
        public tblOrder SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                OnPropertyChanged("SelectedOrder");
            }
        }

        private Visibility viewOrder = Visibility.Visible;
        public Visibility ViewOrder
        {
            get
            {
                return viewOrder;
            }
            set
            {
                viewOrder = value;
                OnPropertyChanged("ViewOrder");
            }
        }
        #endregion

        #region Commands

        private ICommand deleteOrder;
        /// <summary>
        /// delete order command
        /// </summary>
        public ICommand DeleteOrder
        {
            get
            {
                if (deleteOrder == null)
                {
                    deleteOrder = new RelayCommand(param => DeleteOrderExecute(), param => CanDeleteOrderExecute());
                }
                return deleteOrder;
            }
        }

        /// <summary>
        /// delete order execute
        /// </summary>
        private void DeleteOrderExecute()
        {
            try
            {
                if (Order != null)
                {
                    int orderId = Order.ID;
                    if (Order.OrderStatus == "pending")
                    {
                        orderService.DenyOrder(orderId);
                        MessageBox.Show("Order denied");
                    }
                    else
                    {
                        orderService.DeleteOrder(orderId);
                        MessageBox.Show("Order deleted");
                    }
                    using (PizzeriaEntities context = new PizzeriaEntities())
                    {
                        OrdersList = context.tblOrders.ToList();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Can delete order execute
        /// </summary>
        /// <returns>Can or cannot</returns>
        private bool CanDeleteOrderExecute()
        {
            if (Order == null)
                return false;
            else
                return true;
        }

        private ICommand approveOrder;
        /// <summary>
        /// approve order command
        /// </summary>
        public ICommand ApproveOrder
        {
            get
            {
                if (approveOrder == null)
                {
                    approveOrder = new RelayCommand(param => ApproveOrderExecute(), param => CanApproveOrderExecute());
                }
                return approveOrder;
            }
        }

        /// <summary>
        /// approve execute
        /// </summary>
        private void ApproveOrderExecute()
        {
            try
            {
                if (Order != null)
                {
                    int orderId = Order.ID;
                    if (Order.OrderStatus == "pending")
                    {
                        orderService.ApproveOrder(orderId);
                        MessageBox.Show("Order approved");
                    }
                    else
                    {
                        MessageBox.Show("Order already approved");
                    }
                    using (PizzeriaEntities context = new PizzeriaEntities())
                    {
                        OrdersList = context.tblOrders.ToList();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Can approve order execute
        /// </summary>
        /// <returns>Can or cannot</returns>
        private bool CanApproveOrderExecute()
        {
            if (Order == null)
                return false;
            else
                return true;
        }


        private ICommand logOut;
        /// <summary>
        /// logout command
        /// </summary>
        public ICommand LogOut
        {
            get
            {
                if (logOut == null)
                {
                    logOut = new RelayCommand(param => LogOutExecute(), param => CanLogOutExecute());
                }
                return logOut;
            }
        }

        /// <summary>
        /// logout execute
        /// </summary>
        private void LogOutExecute()
        {
            LogInView log = new LogInView();
            log.Show();
            employeeView.Close();
        }

        /// <summary>
        /// Can logout execute
        /// </summary>
        /// <returns>Can or cannot</returns>
        private bool CanLogOutExecute()
        {
            return true;
        }
        #endregion
    }
}
