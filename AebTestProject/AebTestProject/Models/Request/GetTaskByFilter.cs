namespace AebTestProject.Models.Request
{
    public class GetTaskByFilter
    {
        public DateTime? CompletionDate { get; set; } 
        public DateTime? CompleteBeforeDate { get; set; }
        public string? Title { get; set; }
    }
}
