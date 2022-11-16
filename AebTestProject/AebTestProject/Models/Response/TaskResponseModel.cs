using System.ComponentModel.DataAnnotations;

namespace AebTestProject.Models.Response
{
    public class TaskResponseModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime CompletionDate { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Выполнить до даты
        /// </summary>
        public DateTime CompleteBeforeDate { get; set; }
        public Status Status { get; set; }
    }
}
