using FluentValidation;
using Template.Domain.Commands.Amostras;

namespace Template.Service.Validators.LabWare
{
    public class SampleValidator : AbstractValidator<SampleCommand>
    {
        public SampleValidator()
        {
            RuleFor(c => c.AnalysisDateC).NotEmpty();
            RuleFor(c => c.AnalysisUserC).NotEmpty();
            RuleFor(c => c.SampleNumberC).NotEmpty();
            RuleFor(c => c.AnalysisNameC).NotEmpty();
            RuleFor(c => c.ResultCodeC).NotEmpty();
            RuleFor(c => c.ResultValueC).NotEmpty();
        }
    }
}