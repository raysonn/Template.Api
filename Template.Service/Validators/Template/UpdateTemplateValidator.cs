using FluentValidation;
using Template.Domain.Commands;

namespace Template.Service.Validators.Template
{
    public class UpdateTemplateValidator : AbstractValidator<TemplateCommand>
    {
        public UpdateTemplateValidator()
        {
            RuleFor(c => c.Property1).NotEmpty();
            RuleFor(c => c.Property2).NotEmpty();
        }
    }
}
