using ManageStuffDBApp.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStuffDBApp.Model
{
    public static class DataWorker
    {
        // Получить все отделы
        public static List<Department> GetAllDepartments()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Departments.ToList();
                return result;
            }
        }

        // Получить все должностм
        public static List<Position> GetAllPositions()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Positions.ToList();
                return result;
            }
        }

        // Получить всех сотрудников
        public static List<Employee> GetAllEmployees()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Employees.ToList();
                return result;
            }
        }

        // создать отдел 
        public static string CreateDepartment(string name)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                // проверяем существует ли уже отдел
                bool checkDepartmentExist = db.Departments.Any(el => el.Name == name);
                if (!checkDepartmentExist)
                { 
                    Department newDepartment = new Department { Name = name };
                    db.Departments.Add(newDepartment);
                    db.SaveChanges();
                    result = "Сделано!";
                }
            }
            return result;
        }

        // создать должность 
        public static string CreatePosition(string name, decimal salary, int maxNumber, Department department)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                // проверяем существует ли уже должность
                bool checkPositionExist = db.Positions.Any(el => el.Name == name && el.Salary == salary);
                if (!checkPositionExist)
                {
                    Position newPosition = new Position { 
                        Name = name,
                        Salary = salary,
                        MaxNumber = maxNumber,
                        DepartmentId = department.Id
                    };
                    db.Positions.Add(newPosition);
                    db.SaveChanges();
                    result = "Сделано!";
                }
            }
            return result;
        }

        // создать сотрудника
        public static string CreateEmployee(string name, string surname, string phone, Position position)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                // проверяем существует ли уже такой
                bool checkEmployeeExist = db.Employees.Any(
                    el => el.Name == name
                    && el.Surname == surname
                    && el.Position == position);

                if (!checkEmployeeExist)
                {
                    Employee newEmployee = new Employee
                    {
                        Name = name,
                        Surname = surname,
                        Phone = phone,
                        PositionId = position.Id
                    };
                    db.Employees.Add(newEmployee);
                    db.SaveChanges();
                    result = "Сделано!";
                }
            }
            return result;
        }

        // удаление отдела 
        public static string DeleteDepartment(Department department)
        {
            string result = "Не существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                result = $"Удален отдел {department.Name}";
            }
            return result;
        }

        // удаление должности 
        public static string DeletePosition(Position position)
        {
            string result = "Не существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Positions.Remove(position);
                db.SaveChanges();
                result = $"Удалена должность {position.Name}";
            }
            return result;
        }

        // удаление сотрудника
        public static string DeleteEmployee(Employee employee)
        {
            string result = "Не существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
                result = $"Удален сотрудник {employee.Name} {employee.Surname}";
            }
            return result;
        }

        // редактирование отдела 
        public static string EditDepartment(Department oldDepartment, string newName)
        {
            string result = "Не существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                Department newDepartment = db.Departments.FirstOrDefault(dept => dept.Id == oldDepartment.Id);
                newDepartment.Name = newName;
                db.SaveChanges();
                result = $"Отдел {oldDepartment} изменен на {newDepartment.Name}";
            }
            return result;
        }


        // редактирование должности 
        public static string EditPosition(Position oldPosition, string newName, decimal newSalary, 
            int newMaxNumber, Department newDepartment)
        {
            string result = "Не существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                Position newPosition = db.Positions.FirstOrDefault(pos => pos.Id == oldPosition.Id);
                newPosition.Name = newName;
                newPosition.Salary = newSalary;
                newPosition.MaxNumber = newMaxNumber;
                newPosition.DepartmentId = newDepartment.Id;
                db.SaveChanges();
                result = $"Изменена должность {newPosition.Name}";
            }
            return result;
        }

        // редактирование сотрудника
        public static string EditEmpoyee(Employee oldEmpoyee, string newName, string newSurname,
            string newPhone, Position newPosition)
        {
            string result = "Не существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                Employee newEmployee = db.Employees.FirstOrDefault(pos => pos.Id == oldEmpoyee.Id);
                if(newEmployee != null)
                {
                    newEmployee.Name = newName;
                    newEmployee.Surname = newSurname;
                    newEmployee.Phone = newPhone;
                    newEmployee.PositionId = newPosition.Id;
                    db.SaveChanges();
                    result = $"Изменен сотрудник {newEmployee.Name} {newEmployee.Surname}";
                }                
            }
            return result;
        }
    }
}
