using ITITask.Models.Contracts;

namespace ITITask.Models
{
    public class Department : ISoftDeleteable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? ManagerId { get; set; }
        public virtual Employee? Employee { get; set; }

        public virtual ICollection<Employee>? Employees { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DateOfDelete { get ; set; } = null;
    }
}
