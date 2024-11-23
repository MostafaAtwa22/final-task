using ITITask.DTO;
using ITITask.Models;
using ITITask.Reposetory.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITITask.Reposetory
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly ITIContext context;

        public DepartmentRepo(ITIContext context)
        {
            this.context = context;
        }
        public bool Delete(int id)
        {
            var dept = context.Departments.FirstOrDefault(x => x.Id == id);
            if (dept is null)
                return false;
            try
            {
                context.Departments.Remove(dept);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        public List<DepartmentWithEmployeesDTO> GetAll()
        {
            var depts = context.Departments
                .Include(d => d.Employees)
                .ToList();
            List<DepartmentWithEmployeesDTO> deptsDTO = new List<DepartmentWithEmployeesDTO>();
            foreach (var dept in depts)
            {
                List<string> emps = new List<string>();
                foreach (var emp in dept?.Employees)
                {
                    emps.Add(emp.Name);
                }
                DepartmentWithEmployeesDTO deptDTO = new DepartmentWithEmployeesDTO
                {
                    Id = dept.Id,
                    Name = dept.Name,
                    ManagerName = dept?.Employee?.Name ?? "Unkwon",
                    Employees = emps
                };
                deptsDTO.Add(deptDTO);
            }
            return deptsDTO;
        }

        public DepartmentWithEmployeesDTO GetById(int id)
        {
            var dept = context.Departments
            .Include(d => d.Employees)
            .FirstOrDefault(d => d.Id == id);
            if (dept is null)
                return null;

            List<string> emps = new List<string>();
            foreach (var emp in dept?.Employees)
            {
                emps.Add(emp.Name);
            }

            DepartmentWithEmployeesDTO deptDTO = new DepartmentWithEmployeesDTO
            {
                Id = dept.Id,
                Name = dept.Name,
                ManagerName = dept?.Employee?.Name ?? "Unkwon",
                Employees = emps
            };
            return deptDTO;
        }

        public void Insert(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
        }

        public bool Update(int id, Department department)
        {
            var dept = context.Departments.First(x => x.Id == id);
            if (dept is null) 
                return false;
            dept.Name = department.Name;
            dept.ManagerId = department.ManagerId;
            dept.Employees = department.Employees;
            context.SaveChanges();
            return true;
        }
    }
}
