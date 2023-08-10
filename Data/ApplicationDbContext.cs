using System;

namespace CRUD_Functionality.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<Models.Employee> Employees { get; set; }
	}
}

