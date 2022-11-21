using ManageStuffDBApp.Model;
using ManageStuffDBApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManageStuffDBApp.ViewModel
{
    public class DataManageVM : INotifyPropertyChanged
    {
        // все отделы
        private List<Department> allDepartments = DataWorker.GetAllDepartments();
        public List<Department> AllDepartments
        { 
            get { return allDepartments; }
            set 
            {
                allDepartments = value;
                NotifyPropertyChanged("AllDepartments");            
            }
        }

        // все должности
        private List<Position> allPositions = DataWorker.GetAllPositions();
        public List<Position> AllPositions
        {
            get { return allPositions; }
            set
            {
                allPositions = value;
                NotifyPropertyChanged("AllPositions");
            }
        }

        // все сотрудники
        private List<Employee> allEmployees = DataWorker.GetAllEmployees();
        public List<Employee> AllEmployees
        {
            get { return allEmployees; }
            set
            {
                allEmployees = value;
                NotifyPropertyChanged("AllEmployees");
            }
        }

        // свойства для отдела
        public string DepartmentName { get; set; }

        // свойства для должности
        public string PositionName { get; set; }
        public decimal PositionSalary { get; set; }
        public int PositionMaxNumber { get; set; }
        public Department PositionDepartment { get; set; }

        // свойства для сотрудника
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeePhone { get; set; }
        public Position EmployeePosition { get; set; }


        #region ADD COMMANDS TO ADD

        private RelayCommand addNewDepartment;
        public RelayCommand AddNewDepartment
        {
            get
            {
                return addNewDepartment ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if(DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateDepartment(DepartmentName);
                        UpdateDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                ); 
            }
        }

        private RelayCommand addNewPosition;
        public RelayCommand AddNewPosition
        {
            get
            {
                return addNewPosition ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");
                    }
                    if (PositionSalary == 0)
                    {
                        SetRedBlockControl(wnd, "SalaryBlock");
                    }
                    if (PositionMaxNumber == 0)
                    {
                        SetRedBlockControl(wnd, "MaxNumberBlock");
                    }
                    if (PositionDepartment == null)
                    {
                        MessageBox.Show("Укажите отдел");
                    }

                    else
                    {
                        resultStr = DataWorker.CreatePosition(PositionName, PositionSalary,
                            PositionMaxNumber, PositionDepartment);
                        UpdateDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        private RelayCommand addNewEmployee;
        public RelayCommand AddNewEmployee
        {
            get
            {
                return addNewEmployee ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (EmployeeName == null || EmployeeName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");
                    }
                    if (EmployeeSurname == null || EmployeeSurname.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "SurnameBlock");
                    }
                    if (EmployeePhone == null || EmployeePhone.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "PhoneBlock");
                    }
                    if (EmployeePosition == null)
                    {
                        MessageBox.Show("Укажите должность");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateEmployee(EmployeeName, EmployeeSurname, EmployeePhone, EmployeePosition);
                        UpdateDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        #endregion

        #region COMMANDS TO OPEN WINDOWS
        private RelayCommand openAddNewDepartmentWnd;
        public RelayCommand OpenAddNewDepartmentWnd
        {
            get
            {
                return openAddNewDepartmentWnd ?? new RelayCommand(obj =>
                {
                    OpenAddDepartmentWindowMethod();
                }
                );
            }
        }

        private RelayCommand openAddNewPositionWnd;
        public RelayCommand OpenAddNewPositionWnd
        {
            get
            {
                return openAddNewPositionWnd ?? new RelayCommand(obj =>
                {
                    OpenAddPositionWindowMethod();
                }
                );
            }
        }

        private RelayCommand openAddNewEmployeeWnd;
        public RelayCommand OpenAddNewEmployeeWnd
        {
            get
            {
                return openAddNewEmployeeWnd ?? new RelayCommand(obj =>
                {
                    OpenAddEmployeeWindowMethod();
                }
                );
            }
        }

        #endregion

        #region METHODS TO OPEN/EDIT WINDOW
        // Методы открытия окон
        private void OpenAddDepartmentWindowMethod()
        {
           AddNewDepartmentWindow newDepartmentWindow = new AddNewDepartmentWindow();
            SetCenterPositionAndOpen(newDepartmentWindow);
        }

        private void OpenAddPositionWindowMethod()
        {
            AddNewPositionWindow newPositionWindow = new AddNewPositionWindow();
            SetCenterPositionAndOpen(newPositionWindow);
        }

        private void OpenAddEmployeeWindowMethod()
        {
            AddNewEmployeeWindow newEmployeeWindow = new AddNewEmployeeWindow();
            SetCenterPositionAndOpen(newEmployeeWindow);
        }

        // Окна редактирования
        private void OpenEditDepartmentWindowMethod()
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow();
            SetCenterPositionAndOpen(editDepartmentWindow);
        }

        private void OpenEditPositionWindowMethod()
        {
            EditPositionWindow editPositionWindow = new EditPositionWindow();
            SetCenterPositionAndOpen(editPositionWindow);
        }

        private void OpenEditEmployeeWindowMethod()
        {
            EditEmployeeWindow editEmployeeWindow = new EditEmployeeWindow();
            SetCenterPositionAndOpen(editEmployeeWindow);
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

        private void SetRedBlockControl(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        #region UPDATE_VIEWS

        private void SetNullValuesToProperties()
        {
            // для пользователя
            EmployeeName = null;
            EmployeeSurname = null;
            EmployeePhone = null;
            EmployeePosition = null;

            // для должности
            PositionName = null;
            PositionSalary = 0;
            PositionMaxNumber = 0;
            PositionDepartment = null;

            // для отдела
            DepartmentName = null;
        }

        private void UpdateDataView()
        {
            UpdateAllDepartmentsView();
            UpdateAllPositionsView();
            UpdateAllEmployeesView();
        }

        private void UpdateAllDepartmentsView()
        {
            AllDepartments = DataWorker.GetAllDepartments();
            MainWindow.AllDepartmentsView.ItemsSource = null;
            MainWindow.AllDepartmentsView.Items.Clear();
            MainWindow.AllDepartmentsView.ItemsSource = AllDepartments;
            MainWindow.AllDepartmentsView.Items.Refresh();
        }

        private void UpdateAllPositionsView()
        {
            AllPositions = DataWorker.GetAllPositions();
            MainWindow.AllPositionsView.ItemsSource = null;
            MainWindow.AllPositionsView.Items.Clear();
            MainWindow.AllPositionsView.ItemsSource = AllPositions;
            MainWindow.AllPositionsView.Items.Refresh();
        }

        private void UpdateAllEmployeesView()
        {
            AllEmployees = DataWorker.GetAllEmployees();
            MainWindow.AllEmployeesView.ItemsSource = null;
            MainWindow.AllEmployeesView.Items.Clear();
            MainWindow.AllEmployeesView.ItemsSource = AllEmployees;
            MainWindow.AllEmployeesView.Items.Refresh();
        }

        #endregion

        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
