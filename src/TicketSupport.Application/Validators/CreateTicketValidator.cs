using FluentValidation;
using TicketSupport.Application.DTOs;

namespace TicketSupport.Application.Validators
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketDto>
    {
        public CreateTicketValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.TenantId)
                .NotEmpty().WithMessage("TenantId is required.");

            RuleFor(x => x.CreatedByUserId)
                .NotEmpty().WithMessage("CreatedByUserId is required.");
        }
    }
}
