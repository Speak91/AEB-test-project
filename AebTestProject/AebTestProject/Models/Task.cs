using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AebTestProject.Models
{
    /// <summary>
    /// Модель таблицы задача
    /// </summary>
    public class Task
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(128)]
        public string Title { get; set; }

        [MaxLength(1024)]
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
