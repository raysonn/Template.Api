using FluentValidation;
using Template.Domain.Commands.Amostras;

namespace Template.Service.Validators.LabWare
{
    public class SamplesValidator : AbstractValidator<List<SampleCommand>>
    {
        public SamplesValidator()
        {
            RuleFor(c => c).NotEmpty();

            When(c => c != null, () =>
            {
                RuleForEach(x => x).SetValidator(new SampleValidator());
            });
        }
    }
}