using System.ComponentModel.DataAnnotations;
using Hahaton.Core.Enums;

namespace Hahaton.API.Contracts.Mission
{
    public class CreateMissionRequest : IValidatableObject
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int PeriodOfTime { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [EndDateMustBeGreaterThanCreatedAt]
        public DateTime? EndDate { get; set; }

        [Required]
        public Complexity Complexity { get; set; }

        [Required]
        public Priority Priority { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var minEndDate = CreatedAt.AddMinutes((int)Priority);
            
            if (EndDate < minEndDate)
            {
                yield return new ValidationResult(
                    $"EndDate должно быть позже {minEndDate:yyyy-MM-dd HH:mm:ss}.",
                    new[] { nameof(EndDate) });
            }
        }
    }

    public class EndDateMustBeGreaterThanCreatedAtAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var request = (CreateMissionRequest)validationContext.ObjectInstance;

            if (request.EndDate < request.CreatedAt.AddMinutes(request.PeriodOfTime))
            {
                return new ValidationResult($"EndDate must be greater than CreatedAt + PeriodOfTime ({request.PeriodOfTime} minutes).");
            }

            return ValidationResult.Success;
        }
    }

}