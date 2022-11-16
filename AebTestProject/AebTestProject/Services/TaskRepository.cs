using AebTestProject.Models.Request;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace AebTestProject.Services
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;

        public TaskRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateTask(Models.Task task)
        {
            var creationDate = DateTime.UtcNow;
            task.Id = new Guid();
            task.CreatedOn = creationDate;
            _context.Tasks.Add(task);
            _context.SaveChanges();

        }

        public void DeleteTask(Models.Task task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        public void UpdateTask(Models.Task task)
        {
            task.CreatedOn = DateTime.UtcNow;
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }


        public async Task<Models.Task> GetTaskAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            return task;
        }


        public List<Models.Task> GetTasksList()
        {
            var tasks = _context.Tasks.ToList();
            return tasks;
        }

        public List<Models.Task> GetTasksPage(int pageIndex, int pageSize)
        {
            return GetTasksList()
                .OrderBy(ow => ow.Title)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public List<Models.Task> GetFilter(DateTime? completionDate, DateTime? completeBeforeDate, string title)
        {
            var b = GetTasksList();
            var c = new List<Models.Task>();
            if (completionDate != null)
            {
                 b = b.Where(o => o.CompletionDate.Date == completionDate.Value.Date).ToList();
            }
            if (completeBeforeDate != null)
            {
                 b = b.Where(o => o.CompleteBeforeDate == completeBeforeDate).ToList();
            }
            if (!String.IsNullOrEmpty(title))
            {
                b = b.Where(o => o.Title == title).ToList();
            }
            return b;
        }
    }
}
