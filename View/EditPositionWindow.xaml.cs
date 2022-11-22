using ManageStuffDBApp.Model;
using ManageStuffDBApp.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ManageStuffDBApp.View
{
    /// <summary>
    /// Interaction logic for EditPositionWindow.xaml
    /// </summary>
    public partial class EditPositionWindow : Window
    {
        public EditPositionWindow(Position positionToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedPosition = positionToEdit;
            DataManageVM.PositionName = positionToEdit.Name;
            DataManageVM.PositionSalary = positionToEdit.Salary;
            DataManageVM.PositionMaxNumber = positionToEdit.MaxNumber;
        }

        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
