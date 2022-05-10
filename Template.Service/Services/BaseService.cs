using FluentValidation;

namespace Template.Service.Services
{
    public class BaseService
    {
        internal void Validate<T>(T obj, AbstractValidator<T> validator)
        {
            if (obj == null)
                throw new ArgumentException("Objeto Inválido!");

            validator.ValidateAndThrow(obj);
        }
    }
}
