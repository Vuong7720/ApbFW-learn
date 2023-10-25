using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedu_Ecommance.IdentitySettings;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Tedu_Ecommance.Products
{
    public class ProductCodeGenarator : ITransientDependency
    {
        private readonly IRepository<IdentitySetting, string> _identitySetting;

        public ProductCodeGenarator(IRepository<IdentitySetting, string> identitySetting)
        {
            _identitySetting = identitySetting;
        }
        public async Task<string> GenerateAsync()
        {
            string newCode;
            var identitySetting = await _identitySetting.FindAsync(Tedu_EcommanceConsts.ProductIdentitySettingId);
            if (identitySetting == null)
            {
                identitySetting = await _identitySetting.InsertAsync(new IdentitySetting(Tedu_EcommanceConsts.ProductIdentitySettingId, "Sản phẩm", Tedu_EcommanceConsts.ProductIdentitySettingPrefix, 1, 1));
                newCode = identitySetting.Prefix + identitySetting.CurrentNumber;

            }
            else
            {
                identitySetting.CurrentNumber += identitySetting.StepNumber;
                newCode = identitySetting.Prefix + identitySetting.CurrentNumber;

                await _identitySetting.UpdateAsync(identitySetting);
            }
            return newCode;
        }
    }
}
