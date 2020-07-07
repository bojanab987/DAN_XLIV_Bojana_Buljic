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
    class MenuViewModel : ViewModelBase
    {
        MenuView menu;
        MenuService mservice;        
        /// <summary>
        /// Total order price
        /// </summary>
        private int totalPrice = 0;
        private string JMBG;
        private bool orderConfirmed = false;

        #region Constructor
        public MenuViewModel(MenuView menuOpen)
        {
            menu = menuOpen;
            mservice = new MenuService();            
            PizzaList = mservice.GetAllPizzas();
            OrederedPizzas = new List<tblOrderPizza>();

        }

        public MenuViewModel(MenuView menuOpen, string JMBG)
        {
            menu = menuOpen;
            mservice = new MenuService();
            PizzaList = mservice.GetAllPizzas();
            OrederedPizzas = new List<tblOrderPizza>();
            this.JMBG = JMBG;
        }
        #endregion

        #region Properites       

        /// <summary>
        /// Menu list with all pizzas from tblMenu
        /// </summary>
        private List<tblPizza> pizzaList;
        public List<tblPizza> PizzaList
        {
            get { return pizzaList; }
            set
            {
                pizzaList = value;
                OnPropertyChanged("PizzaList");
            }
        }

        /// <summary>
        /// Selected pizza item from menu
        /// </summary>
        private tblPizza selectedPizza;
        public tblPizza SelectedPizza
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
        private string totalPriceAmount = "Total pirce: 0";
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

        /// <summary>
        /// Propety of current amount of selected pizza/menu item
        /// </summary>
        private int currAmount;
        public int CurrAmount
        {
            get
            { return currAmount; }
            set
            {
                currAmount = value;
                OnPropertyChanged("CurrAmount");
            }
        }

        private Visibility viewMakeOrder = Visibility.Visible;
        public Visibility ViewMakeOrder
        {
            get
            {
                return viewMakeOrder;
            }
            set
            {
                viewMakeOrder = value;
                OnPropertyChanged("ViewMakeOrder");
            }
        }

        private Visibility viewShowOrder = Visibility.Collapsed;
        public Visibility ViewShowOrder
        {
            get
            {
                return viewShowOrder;
            }
            set
            {
                viewShowOrder = value;
                OnPropertyChanged("ViewShowOrder");
            }
        }

        /// <summary>
        /// LIst of pizzas ordered
        /// </summary>
        private List<tblOrderPizza> orederedPizzas;
        public List<tblOrderPizza> OrederedPizzas
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
                menu.Close();
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
                menu.Close();
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
                tblOrderPizza thisPizza = FindPizzaByName(SelectedPizza.PizzaName);

                if (thisPizza != null && currAmount == 0)
                {
                    CurrAmount = (int)thisPizza.Amount;
                }
                if (CurrAmount <= 0)
                {
                    MessageBox.Show("You have to select at least 1 pizza to make order.");
                    return;
                }

                tblOrderPizza newOrder = new tblOrderPizza();
                newOrder.PizzaID = SelectedPizza.ID;
                newOrder.tblPizza = SelectedPizza;                

                newOrder.Amount = CurrAmount;

                if (thisPizza != null)
                {
                    totalPrice -= ((int)thisPizza.Amount * (int)thisPizza.tblPizza.Price);
                    OrederedPizzas.Remove(thisPizza);
                }


                totalPrice += (CurrAmount * SelectedPizza.Price);
                OrederedPizzas.Add(newOrder);

                TotalPriceAmount = string.Format("Total order price {0}", totalPrice);
                string outputMessage = string.Format("Your order contain {0} {1}", CurrAmount, SelectedPizza.PizzaName);
                CurrAmount = 0;
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
            if (orderConfirmed)
            {
                return false;
            }
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
                OrderView orderView = new OrderView(orederedPizzas, totalPrice, JMBG);
                orderView.ShowDialog();

                if ((orderView.DataContext as OrderViewModel).OrderConfirmed == true)
                {
                    ViewMakeOrder = Visibility.Hidden;
                    ViewShowOrder = Visibility.Visible;
                    orderConfirmed = true;
                }

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
            if (!orederedPizzas.Any()||orderConfirmed)
            {
                return false;
            }
            return true;
        }

        private ICommand showOrder;
        public ICommand ShowOrder
        {
            get
            {
                if (showOrder == null)
                {
                    showOrder = new RelayCommand(param => ShowOrderExecute(), param => CanShowOrderExecute());
                }
                return showOrder;
            }
        }

        private void ShowOrderExecute()
        {
            try
            {
                ShowOrderView orderView = new ShowOrderView(orederedPizzas, totalPrice, JMBG);
                orderView.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanShowOrderExecute()
        {
            if (!orederedPizzas.Any())
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Methods
        private tblOrderPizza FindPizzaByName(string name)
        {
            foreach (var pizza in orederedPizzas)
            {
                if (pizza.tblPizza.PizzaName.Equals(name))
                {
                    return pizza;
                }
            }
            return null;
        }
        #endregion
    }
}
