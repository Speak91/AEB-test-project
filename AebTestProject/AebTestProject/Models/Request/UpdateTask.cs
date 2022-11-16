using System.ComponentModel.DataAnnotations;

namespace AebTestProject.Models.Request
{
    public class UpdateTask
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime CompletionDate { get; set; }

        /// <summary>
        /// Выполнить до даты
        /// </summary>
        public DateTime CompleteBeforeDate { get; set; }

        public Status Status { get; set; }
    }
}
