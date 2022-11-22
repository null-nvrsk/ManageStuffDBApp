using ManageStuffDBApp.Model;
using ManageStuffDBApp.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ManageStuffDBApp.View
{
    /// <summary>
    /// Interaction logic for EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        public EditEmployeeWindow(Employee employeeToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedEmployee = employeeToEdit;
            DataManageVM.EmployeeName = employeeToEdit.Name;
            DataManageVM.EmployeeSurname = employeeToEdit.Surname;
            DataManageVM.EmployeePhone = employeeToEdit.Phone;
            //DataManageVM.EmployeePosition = employeeToEdit.EmployeePosition;
        }

        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
