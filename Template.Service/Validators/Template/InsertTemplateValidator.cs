using FluentValidation;
using Template.Domain.Commands;

namespace Template.Service.Validators.Template
{
    public class InsertTemplateValidator : AbstractValidator<TemplateCommand>
    {
        public InsertTemplateValidator()
        {
            RuleFor(c => c.Property1).NotEmpty();
            RuleFor(c => c.Property2).NotEmpty();
        }
    }
}
