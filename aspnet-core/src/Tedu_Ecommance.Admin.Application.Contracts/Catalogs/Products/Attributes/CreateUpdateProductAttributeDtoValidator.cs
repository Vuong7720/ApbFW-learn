using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tedu_Ecommance.Admin.Catalogs.Products.Attributes
{
    public class CreateUpdateProductAttributeDtoValidator : AbstractValidator<AddUpdateProductAttributeDto>
    {
        public CreateUpdateProductAttributeDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.AttributeId).NotEmpty();

        }
    }
}
