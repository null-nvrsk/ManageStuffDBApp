using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace ManageStuffDBApp.Model
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public decimal Salary { get; set; }    
        public int MaxNumber { get; set; }    
        public List<Employee> employees { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        [NotMapped]
        public Department PositionDepartment
        {
            get
            {
                return DataWorker.GetDepartmentById(DepartmentId);
            }
        }
        [NotMapped]
        public List<Employee> PositionEmployees
        {
            get
            {
                return DataWorker.GetAllEmloyeesByPositionId(Id);
            }
        }
    }
}
