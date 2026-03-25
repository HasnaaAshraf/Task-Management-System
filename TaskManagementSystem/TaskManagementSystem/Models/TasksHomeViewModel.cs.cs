using System.Collections.Generic;
using TaskManagement.Data.Models;

namespace TaskManagement.Presentation.Models
{
	public class TasksHomeViewModel
	{
		public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
	}
}
