using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Kyc.Entities;

namespace Kyc.Validators
{
    public class AadharValidator : AbstractValidator<Aadhar>
    {
        public AadharValidator()
        {
             RuleFor(x => x.Number).Must(x => x != null && x.Length == 12)
             .WithMessage("Aadhar card is not valid.");
        }

    }
}