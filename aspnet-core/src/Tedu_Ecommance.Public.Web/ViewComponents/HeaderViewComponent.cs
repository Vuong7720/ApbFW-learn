using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tedu_Ecommance.Public.Catalogs.ProductCategories;

namespace TeduEcommerce.Public.Web.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IProductCategoriesAppService _productCategoriesAppService;
        public HeaderViewComponent(IProductCategoriesAppService ProductCategoriesAppService)
        {
            _productCategoriesAppService = ProductCategoriesAppService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _productCategoriesAppService.GetListAllAsync();
            return View(model);
        }
    }
}