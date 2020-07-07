using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class OrderViewModel:ViewModelBase
    {
        OrderView orderView = new OrderView();
        private string JMBG;

        #region Constructors
        public OrderViewModel(OrderView orderViewOpen, List<tblMenu> menu, string JMBG)
        {
            orderView = orderViewOpen;
            this.JMBG = JMBG;

            
        }
        #endregion
    }
}
