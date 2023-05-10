using FluentValidation;
using Template.Domain.Commands.Resultados;

namespace Template.Service.Validators.LabWare
{
    public class ResultadosValidator : AbstractValidator<AmostrasCommand>
    {
        public ResultadosValidator()
        {
            RuleFor(c => c.AuthToken).NotEmpty();
            RuleFor(c => c.AnalysisDateC).NotEmpty();
            RuleFor(c => c.AnalysisUserC).NotEmpty();
            RuleFor(c => c.SampleNumberC).NotEmpty();
            RuleFor(c => c.AnalysisNameC).NotEmpty();
            RuleFor(c => c.ResultCodeC).NotEmpty();
            RuleFor(c => c.ResultValueC).NotEmpty();
        }
    }
}