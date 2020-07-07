using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class ShowOrderViewModel:ViewModelBase
    {
        ShowOrderView orderView = new ShowOrderView();
        private string JMBG;

        #region Constructors
        public ShowOrderViewModel(ShowOrderView orderViewOpen, List<tblOrderPizza> pizzas, int totalPrice)
        {
            orderView = orderViewOpen;
            PizzaList = pizzas;

            totalAmount = String.Format("Total order price: {0}", totalPrice);

        }

        public ShowOrderViewModel(ShowOrderView orderViewOpen, List<tblOrderPizza> pizzas, int totalPrice, string JMBG)
        {
            orderView = orderViewOpen;
            PizzaList = pizzas;

            totalAmount = String.Format("Total order price: {0}", totalPrice);
            this.JMBG = JMBG;

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
