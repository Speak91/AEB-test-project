namespace AebTestProject.Services
{
    public interface ITaskRepository
    {
        void CreateTask(Models.Task task);
        void UpdateTask(Models.Task task);
        void DeleteTask(Models.Task task);
        Task<Models.Task> GetTaskAsync(Guid id, CancellationToken cancellationToken = default);
        List<Models.Task> GetTasksList();
        List<Models.Task> GetTasksPage(int pageIndex, int pageSize);
        List<Models.Task> GetFilter(DateTime? completionDate, DateTime? completeBeforeDate, string title);
    }
}
