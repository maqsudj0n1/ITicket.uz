using FluentValidation;
using ITicket.uz.Domain.Entities;

namespace ITicket.uz.Application.UseCases.Seats.Commands.CreateSeat
{
    public class CreateSeatCommandValidator : AbstractValidator<CreateSeatCommand>
    {
        public CreateSeatCommandValidator()
        {
           // RuleFor(seat => seat.RowNumber)
           //.Must((seat, rowNumber) => ValidateRowNumber(rowNumber, seat.Venue));
           //.WithMessage("Row number must not exceed the total number of rows.");
           // RuleFor(seat => seat.SeatsInRow)
           //.Must((seat, seatsInRow) => ValidateSeatsInRow(seatsInRow, seat.Venue))
           //.WithMessage("Number of seats in one row must not exceed the maximum allowed.");

        }
    }
}