using System;

namespace CRUD_Functionality.Models
{
	public class EmployeeDTO
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name field is required")]
		[StringLength(maximumLength: 50, MinimumLength = 2)] 
		public string Name { get; set; }

		public string EmailId { get; set; }

		public string Department { get; set; }
	}
}

