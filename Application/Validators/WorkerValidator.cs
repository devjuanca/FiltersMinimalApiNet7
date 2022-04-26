using System.Text.RegularExpressions;
using Domain;
using FluentValidation;

namespace Application.Validators;

public class WorkerValidator : AbstractValidator<Worker>
{
    public WorkerValidator()
    {
        RuleFor(a => a.Id).NotEmpty();

        RuleFor(a => a.DNI).NotEmpty();

        RuleFor(a => a.FullName).NotEmpty();

        RuleFor(a => a.Address).NotEmpty();
    }
}
