using System;
using System.Xml.XPath;
using AutoMapper;
using CRUD_Functionality.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Functionality.Controllers
{
	[Route("api/Employee")]
	[ApiController]
	public class EmployeeAPIController : ControllerBase
	{
		private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public EmployeeAPIController(ApplicationDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		//Get: api/Employee
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
		{
			IEnumerable<Employee> employeeList = await _db.Employees.ToListAsync();
			return Ok(_mapper.Map<IEnumerable<EmployeeDTO>>(employeeList));
		}

		//Get: api/Employee/id
		[HttpGet("{id:int}",Name ="GetEmployee")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
		{
			if (id == 0)
				return BadRequest();
			var employee = await _db.Employees.FirstOrDefaultAsync(u => u.Id == id);

			if (employee == null)
				return NotFound();
			return Ok(employee);
		}

		//Post: api/Employee
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<ActionResult<Employee>> AddEmployee([FromBody] EmployeeAddDTO addEmployeeDTO)
		{
			if(addEmployeeDTO == null)
			{
				return BadRequest(addEmployeeDTO);
			}
			Employee model = _mapper.Map<Employee>(addEmployeeDTO);

			await _db.Employees.AddAsync(model);
			await _db.SaveChangesAsync();

			return CreatedAtRoute("GetEmployee", new { id = model.Id }, model);
		}


		[HttpDelete("{id:int}",Name="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee(int id)
		{
			if (id == 0)
				return BadRequest();
			var employee = await _db.Employees.FirstOrDefaultAsync(u => u.Id == id);

			if (employee == null)
				return NotFound();
			_db.Employees.Remove(employee);
			await _db.SaveChangesAsync();
			return NoContent();
		}

		[HttpPut("{id:int}", Name="UpdateVilla")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] EmployeeUpdateDTO updateEmployeeDTO)
		{
			if (updateEmployeeDTO == null || id != updateEmployeeDTO.Id)
				return BadRequest();

			Employee model = _mapper.Map<Employee>(updateEmployeeDTO);
			_db.Employees.Update(model);
			await _db.SaveChangesAsync();
			return NoContent();
		}

	}
}

