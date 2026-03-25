using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data.Models;

namespace TaskManagement.Infrastructure.Data
{
	public class TaskDBContext:DbContext
	{

	   public TaskDBContext(DbContextOptions<TaskDBContext> dbContextOptions) : base(dbContextOptions) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<TaskItem>()
						.HasOne(t => t.AssignedUser)
						.WithMany()
						.HasForeignKey(t => t.UserId);
		}

		public DbSet<TaskItem> Tasks { get; set; }
		public DbSet<User> Users { get; set; }

	}
}
