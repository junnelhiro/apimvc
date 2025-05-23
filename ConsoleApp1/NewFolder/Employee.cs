using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.NewFolder
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return  Name;
        }
    }
    public class TimeLog
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime WorkDate { get; set; }
        public int ShiftId { get; set; }
        public string LogType { get; set; }
        public DateTime LogTime { get; set; }
    }
}
