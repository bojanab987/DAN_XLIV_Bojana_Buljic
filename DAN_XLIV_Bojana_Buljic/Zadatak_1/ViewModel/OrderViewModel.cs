using DataAccessLayer.Model;
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
    class OrderViewModel:ViewModelBase
    {
        OrderView orderView = new OrderView();
        private string JMBG;
        MenuService service;
        OrderService orService;

        #region Constructors        
        
        public OrderViewModel(OrderView orderViewOpen, List<tblOrderPizza> pizzas, int totalPrice, string JMBG)
        {
            orderView = orderViewOpen;
            service = new MenuService();
            orService = new OrderService();
            PizzaList = pizzas;
            this.JMBG = JMBG;

            totalAmount = string.Format("Total order price: {0}", totalPrice);            
        }
        #endregion

        private List<tblOrderPizza> pizzaList;
        public List<tblOrderPizza> PizzaList
        {
            get
            {
                return pizzaList;
            }
            set
            {
                pizzaList = value;
                OnPropertyChanged("PizzaList");
            }
        }

        private bool orderConfirmed;
        public bool OrderConfirmed
        {
            get
            {
                return orderConfirmed;
            }
            set
            {
                orderConfirmed = value;
                OnPropertyChanged("OrderConfirmed");
            }
        }

        private string totalAmount;
        public string TotalAmount
        {
            get
            {
                return totalAmount;
            }
            set
            {
                totalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }

        private ICommand confirmOrder;
        public ICommand ConfirmOrder
        {
            get
            {
                if (confirmOrder == null)
                {
                    confirmOrder = new RelayCommand(param => ConfirmOrderExecute(), param => CanConfirmOrderExecute());
                }
                return confirmOrder;
            }
        }

        private void ConfirmOrderExecute()
        {
            try
            {
                OrderConfirmed = true;
                tblOrder newOrder = new tblOrder();
                newOrder.JMBG = JMBG;
                newOrder.OrderStatus = "On Hold";
                newOrder.OrderDateTime = DateTime.Now;

                newOrder = orService.AddOrder(newOrder);

                foreach (var pizza in PizzaList)
                {
                    tblOrderPizza pizzaOrder = new tblOrderPizza();
                    pizzaOrder.PizzaID = pizza.tblPizza.ID;
                    pizzaOrder.OrderID = newOrder.ID;
                    pizzaOrder.Amount = pizza.Amount;

                    orService.AddPizzaOrder(pizzaOrder);
                }
                orderView.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanConfirmOrderExecute()
        {
            return true;
        }

        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {

                orderView.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }
    }
}
