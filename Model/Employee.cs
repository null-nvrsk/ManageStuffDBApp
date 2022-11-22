using System.ComponentModel.DataAnnotations.Schema;

namespace ManageStuffDBApp.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        [NotMapped]
        public Position EmployeePosition
        {
            get
            {
                return DataWorker.GetPositionById(PositionId);
            }
        }

    }
}
