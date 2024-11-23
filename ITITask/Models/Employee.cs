using ITITask.Models.Contracts;
using System.ComponentModel.DataAnnotations;

namespace ITITask.Models
{
    public class Employee : ISoftDeleteable
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100), MinLength(3)]
        public string Name { get; set; }

        [Required]
        [DataType("money")]
        public decimal Salary { get; set; }

        [Required]
        public string Address { get; set; }
        public string Phone { get; set; }

        [Required]  
        public int? DeptId { get; set; }
        public virtual Department? Department { get; set; }

        public virtual ICollection<Department> Departments { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DateOfDelete { get; set; } = null;
    }
}
