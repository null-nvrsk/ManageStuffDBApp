using ManageStuffDBApp.ViewModel;
using System.Windows;

namespace ManageStuffDBApp.View
{
    /// <summary>
    /// Interaction logic for AddNewDepartmentWindow.xaml
    /// </summary>
    public partial class AddNewDepartmentWindow : Window
    {
        public AddNewDepartmentWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
    }
}
