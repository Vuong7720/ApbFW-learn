using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tedu_Ecommance.Admin.Catalogs.ProductAttributes
{
    public class CreateUpdateProductAttributeDtoValidator : AbstractValidator<CreateUpdateProductAttributeDto>
    {
        public CreateUpdateProductAttributeDtoValidator()
        {
            RuleFor(x => x.Lable).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.AttributeType).NotNull();

        }
    }
}
