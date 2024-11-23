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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepo departmentRepo;

        public DepartmentController(IDepartmentRepo departmentRepo)
        {
            this.departmentRepo = departmentRepo;
        }

        #region Get
        [HttpGet]
        public IActionResult GetAll()
        {
            var depts = departmentRepo.GetAll();
            return Ok(depts);
        }

        [HttpGet("{id:int}", Name = "GetDepartmentRoute")]
        public IActionResult GetById(int id)
        {
            var dpet = departmentRepo.GetById(id);
            if (dpet is null)
                return BadRequest($"This Department with {id} is not exists");
            return Ok(dpet);
        }
        #endregion

        #region Post
        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                departmentRepo.Insert(department);

                string? url = Url.Link("GetDepartmentRoute", new { id = department.Id });
                return Created(url, department);
            }
            return BadRequest(department);
        }
        #endregion

        #region Put
        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(int id, Department department)
        {
            if (ModelState.IsValid)
            {
                bool found = departmentRepo.Update(id, department);
                if (found)
                    return StatusCode(204, "Department Has been Updated");
                return BadRequest("This Department is Not found");
            }
            return BadRequest(department);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:int}")]
        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                bool found = departmentRepo.Delete(id);
                if (!found)
                    return BadRequest("Not Found");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return StatusCode(204, "Recorde Has been Removed");
        }
        #endregion
    }
}
