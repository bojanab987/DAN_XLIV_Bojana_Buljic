using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Service
{
    class MenuService
    {
        /// <summary>
        /// Method gets all pizzas from menu from database and returns list
        /// </summary>
        /// <returns>List of pizzas</returns>
        public List<tblPizza> GetAllPizzas()
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    List<tblPizza> list = new List<tblPizza>();
                    list = (from x in context.tblPizzas select x).ToList();
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
