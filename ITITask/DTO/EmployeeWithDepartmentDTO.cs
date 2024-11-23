namespace ITITask.DTO
{
    public class EmployeeWithDepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? DepartmentName { get; set; }
    }
}
