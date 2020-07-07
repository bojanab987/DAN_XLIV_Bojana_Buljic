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
            OrdersList = orderService.GetOrders();           

        }

        //private tblOrder order;
        //public tblOrder Order
        //{
        //    get { return order; }
        //    set
        //    {
        //        order = value;
        //        OnPropertyChanged("Order");
        //    }
        //}

        private List<tblOrderPizza> ordersList;
        public List<tblOrderPizza> OrdersList
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

        //private string status="On hold";
        //public string Status
        //{
        //    get { return status; }
        //    set
        //    {
        //        status = value;
        //        OnPropertyChanged("Status");
        //    }
        //}

        //private bool isChangedOrder;
        //public bool IsChangedOrder
        //{
        //    get { return isChangedOrder; }
        //    set
        //    {
        //        isChangedOrder = value;
        //    }


        #region Commands

        //private ICommand save;
        //public ICommand Save
        //{
        //    get
        //    {
        //        if (save == null)
        //        {
        //            save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
        //        }
        //        return save;
        //    }
        //}

        //private void SaveExecute()
        //{
        //    try
        //    {
        //        if(Status=="On hold")
        //        {
        //            Order.OrderStatus = "On hold";
        //        }
        //        else if(Status=="Approved")
        //        {
        //            Order.OrderStatus = "approved";
        //        }
        //        else
        //        {
        //            Order.OrderStatus = "Denied";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}
        #endregion
    }
}
