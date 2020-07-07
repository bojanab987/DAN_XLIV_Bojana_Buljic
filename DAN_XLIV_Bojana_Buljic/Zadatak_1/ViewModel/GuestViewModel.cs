using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class GuestViewModel:ViewModelBase
    {
        GuestView guestView;

        #region Constructor
        public GuestViewModel(GuestView guestViewOpen)
        {
            guestView = guestViewOpen;
        }
        #endregion

        private Visibility viewMenu;
        public Visibility ViewMenu
        {
            get { return viewMenu;}
            set
            {
                viewMenu = value;
                OnPropertyChanged("ViewMenu");
            }
        }

        //private List<Menu> menuList;
        //public List<Menu> MenuList
        //{
        //    get { return menuList; }
        //    set
        //    {
        //        menuList = value;
        //        OnPropertyChanged("MenuList");
        //    }
        //}

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
                //MenuView menuView = new MenuView();

                //menuView.ShowDialog();
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
