using System;

namespace CRUD_Functionality.Models
{
	public class EmployeeUpdateDTO
	{
		[Required]
		public int Id { get; set; }

		[Required(ErrorMessage = "Name field is required")]
		[StringLength(maximumLength: 50, MinimumLength = 2)] 
		public string Name { get; set; }

        [Required(ErrorMessage = "Email ID field is required")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Department field is required")]
        public string Department { get; set; }
	}
}

