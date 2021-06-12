using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Model;

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IRepository<EmployeesDetails> EmployeeRepository;
        readonly IUnitOfWork _unitOfWork;
        public EmployeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            this.EmployeeRepository = _unitOfWork.Repository<EmployeesDetails>();
        }

        [HttpGet]
        [Route("GetAllEmployee")]
        public IActionResult GetAllEmployee()
        {
            try
            {
                 var result = EmployeeRepository.GetAll();
                 return Ok(result);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateEmployee")]
        public IActionResult CreateEmployee([FromBody]EmployeesDetails employees)
        {
            try
            {
                EmployeeRepository.Add(employees);
                _unitOfWork.Commit();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}