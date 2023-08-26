using FluentValidation;
namespace ITicket.uz.Application.UseCases.Events.Commands.CreateEvent;
public class CreateEventCommandValidation:AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidation()
    {
        RuleFor(_event=> _event.Description).NotEmpty().MinimumLength(3).MaximumLength(200);
        RuleFor(_event => _event.Name).NotEmpty().MinimumLength(3).MaximumLength(50).
            WithMessage("Ism bo'sh bo'lmasligi va eng kamida 3 eng kopida 50 ta harfdan iborat bo'lishi kerak");
    }
}