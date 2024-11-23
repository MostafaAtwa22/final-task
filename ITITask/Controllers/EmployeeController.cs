using ITITask.DTO;
using ITITask.Models;
using ITITask.Reposetory.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo employeeRepo;

        public EmployeeController(IEmployeeRepo employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }

        #region Get
        [HttpGet] 
        public IActionResult GetAll()
        {
            List<EmployeeWithDepartmentDTO> employees = employeeRepo.GetAll();
            return Ok(employees);
        }

        [HttpGet("/{id:int}", Name = "GetEmployeeRoute")]
        public IActionResult GetById(int id)
        {
            EmployeeWithDepartmentDTO emp = employeeRepo.GetById(id);
            if (emp is not null)
                return Ok(emp);
            return BadRequest("This Employee Is not found");
        }

        [HttpGet("/{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            EmployeeWithDepartmentDTO emp = employeeRepo.GetByName(name);
            if (emp is not null)
                return Ok(emp);
            return BadRequest("This Employee Is not found");
        }
        #endregion

        #region Post
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employeeRepo.Insert(employee);

                // current domain
                string? url = Url.Link("GetEmployeeRoute", new { id = employee.Id });
                return Created(url, employee);
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Put
        [HttpPut("/{id:int}")]
        public IActionResult Update([FromRoute]int id, [FromBody]Employee employee)
        {
            if (ModelState.IsValid)
            {
                bool found = employeeRepo.Update(id, employee);
                if (found)
                    return StatusCode(204, "Employee Has been updated");
                else
                    return BadRequest("This Employee is Not Found");
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Delete
        [HttpDelete("/{id:int}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                bool found = employeeRepo.Delete(id);
                if (!found)
                    return BadRequest("Not Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return StatusCode(204, "Recorde Removed");
        }
        #endregion
    }
}
