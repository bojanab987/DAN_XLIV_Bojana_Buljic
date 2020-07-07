using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.View;
using Zadatak_1.Service;
using System.Linq;

namespace Zadatak_1.ViewModel
{
    class GuestViewModel:ViewModelBase
    {
        GuestView guestView;
        MenuService mservice;
        OrderService oservice;
        /// <summary>
        /// Total order price
        /// </summary>
        private int totalPrice = 0;
        static string JMBG = " ";

        #region Constructor
        public GuestViewModel(GuestView guestViewOpen)
        {
            guestView = guestViewOpen;
            mservice = new MenuService();
            oservice = new OrderService();
            Order = new tblOrder();
            MenuList = mservice.GetAllPizzas();
            OrederedPizzas = new List<tblOrder>();
            
        }
        #endregion

        #region Properites
        private Visibility viewMenu=Visibility.Collapsed;
        public Visibility ViewMenu
        {
            get { return viewMenu;}
            set
            {
                viewMenu = value;
                OnPropertyChanged("ViewMenu");
            }
        }

        /// <summary>
        /// Menu list with all pizzas from tblMenu
        /// </summary>
        private List<tblMenu> menuList;
        public List<tblMenu> MenuList
        {
            get { return menuList; }
            set
            {
                menuList = value;
                OnPropertyChanged("MenuList");
            }
        }

        /// <summary>
        /// Selected pizza item from menu
        /// </summary>
        private tblMenu selectedPizza;
        public tblMenu SelectedPizza
        {
            get
            { return selectedPizza; }
            set
            {
                selectedPizza = value;
                OnPropertyChanged("SelectedPizza");
            }
        }

        /// <summary>
        /// Value of total price amount to display on view
        /// </summary>
        private string totalPriceAmount = "0";
        public string TotalPriceAmount
        {
            get
            {
                return totalPriceAmount;
            }
            set
            {
                totalPriceAmount = value;
                OnPropertyChanged("TotalPriceAmount");
            }
        }

        ///// <summary>
        ///// Propety of amount of selected pizza/menu item
        ///// </summary>
        //private int amount;
        //public int Amount
        //{
        //    get
        //    { return amount;}
        //    set
        //    { Amount = value;
        //        OnPropertyChanged("Amount");
        //    }
        //}

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

        /// <summary>
        /// LIst of pizzas ordered
        /// </summary>
        private List<tblOrder> orederedPizzas;
        public List<tblOrder> OrederedPizzas
        {
            get
            {
                return orederedPizzas;
            }
            set
            {
                orederedPizzas = value;
                OnPropertyChanged("OrederedPizzas");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command to show menu
        /// </summary>
        private ICommand showMenu;
        public ICommand ShowMenu
        {
            get
            {
                if (showMenu == null)
                {
                    showMenu = new RelayCommand(param => ShowMenuExecute(), param => CanShowMenuExecute());
                }
                return showMenu;
            }
        }

        /// <summary>
        /// Method to execute command and showing the menu
        /// </summary>
        private void ShowMenuExecute()
        {
            try
            {
                ViewMenu = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method confirming command is possible to execute
        /// </summary>
        /// <returns></returns>
        private bool CanShowMenuExecute()
        {
            return true;
        }

        /// <summary>
        /// LogOut Command
        /// </summary>
        private ICommand logOut;
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
        /// Method for logging out guest from app
        /// </summary>  
        private void LogOutExecute()
        {
            try
            {
                //closes guest view and opens empty LogInView
                LogInView loginView = new LogInView();
                guestView.Close();
                loginView.ShowDialog();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method check if logout is possible to be Executed
        /// </summary>
        private bool CanLogOutExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that closes the view
        /// </summary>
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

        /// <summary>
        /// Method for execution of view closing command
        /// </summary>
        private void CloseExecute()
        {
            try
            {
                guestView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method for check if close is possible to be Executed
        /// </summary>
        /// <returns>true </returns>
        private bool CanCloseExecute()
        {
            return true;
        }

        /// <summary>
        /// Command for add item into order
        /// </summary>
        private ICommand addToOrder;
        public ICommand AddToOrder
        {
            get
            {
                if (addToOrder == null)
                {
                    addToOrder = new RelayCommand(param => AddToOrderExecute(), param => CanAddToOrderExecute());
                }
                return addToOrder;
            }
        }

        /// <summary>
        /// Method for executing command, adding selected item into order
        /// </summary>
        private void AddToOrderExecute()
        {
            try
            {                
                Order.ItemId = SelectedPizza.ItemId;
                Order.Username = JMBG; 
                if (Order.Amount <= 0)
                {
                    MessageBox.Show("To order you need to have at least one item in cart.");
                    return;
                }

                Order.OrderStatus = "On hold";
                oservice.CreateOrder(Order);                

                if (Order!= null)
                {
                    MessageBox.Show(Order.Amount.ToString());
                    totalPrice -= (Order.Amount*SelectedPizza.Price);
                    
                }
                totalPrice += (Order.Amount * SelectedPizza.Price);                

                TotalPriceAmount = totalPrice.ToString();
                string outputMessage = string.Format("Your order contain {0} {1}", Order.Amount, SelectedPizza.PizzaName);                
                MessageBox.Show(outputMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method to conifrm executing of command Add to order
        /// </summary>
        /// <returns></returns>
        private bool CanAddToOrderExecute()
        {
            return true;
        }


        private ICommand orderCommand;
        public ICommand OrderCommand
        {
            get
            {
                if (orderCommand == null)
                {
                    orderCommand = new RelayCommand(param => OrderCommandExecute(), param => CanOrderCommandExecute());
                }
                return orderCommand;
            }
        }

        private void OrderCommandExecute()
        {
            try
            {
                OrderView orderView = new OrderView();
                orderView.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CanOrderCommandExecute()
        {
            if (!orederedPizzas.Any())
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
