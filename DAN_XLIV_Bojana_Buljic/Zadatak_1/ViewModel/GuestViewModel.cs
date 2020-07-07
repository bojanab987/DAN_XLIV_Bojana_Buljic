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
        private string JMBG;

        #region Constructor      

        public GuestViewModel(GuestView guestViewOpen, string JMBG)
        {
            mservice = new MenuService();
            guestView = guestViewOpen;
            this.JMBG = JMBG;
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
                MenuView menuView = new MenuView(JMBG);
                menuView.Show();
                guestView.Close();
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
                
        #endregion
    }
}
