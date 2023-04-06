using FluentValidation;
using Microsoft.EntityFrameworkCore;
using P229FirstApi.DAL;

namespace P229FirstApi.DTOs.CategroryDTOs
{
    public class CategoryPostDTo
    {
        public string? Name { get; set; }
    }

    public class CategoryPostDToValidate : AbstractValidator<CategoryPostDTo>
    {
        public CategoryPostDToValidate(AppDbContext context)
        {
            RuleFor(r => r.Name)
                .MinimumLength(5).WithMessage("Minimum 5 Simvol")
                .MaximumLength(50).WithMessage("Maksimum 50 Simvol")
                .NotEmpty().WithMessage("Is Required");

            RuleFor(r => r).Custom((obj, ctx) =>
            {
                if (context.Categories.Any(c => c.IsDeleted == false && c.Name.ToLower() == obj.Name.Trim().ToLower()))
                {
                    ctx.AddFailure("Name", $"Daxil etdiyniz Categoriya adi: {obj.Name.Trim()} artiq movcuddur");
                }
            });
        }
    }
}
