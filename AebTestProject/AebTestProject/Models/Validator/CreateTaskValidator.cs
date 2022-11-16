using AebTestProject.Models.Request;
using FluentValidation;
using System;

namespace AebTestProject.Models.Validator
{
    public class CreateTaskValidator : AbstractValidator<CreateTask>
    {
        public CreateTaskValidator()
        {
            RuleFor(x => x.Title).MaximumLength(128).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(1024).NotEmpty();
            RuleFor(x => x).Must(x => x.CompleteBeforeDate > DateTime.Now).NotEmpty()
            .WithMessage("EndTime must greater than StartTime");
            RuleFor(x => x).Must(x => x.CompletionDate > DateTime.Now).NotEmpty()
           .WithMessage("EndTime must greater than StartTime");
            RuleFor(x => x.Status).NotEmpty();
        }
    }
}
