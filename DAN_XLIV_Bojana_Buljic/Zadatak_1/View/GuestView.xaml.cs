using System.Windows;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for GuestView.xaml
    /// </summary>
    public partial class GuestView : Window
    {
        public GuestView()
        {
            InitializeComponent();
        }

        public GuestView(string JMBG)
        {
            InitializeComponent();
            this.DataContext = new GuestViewModel(this,JMBG);
        }
    }
}
