using System.Collections.Generic;
using System.Threading.Tasks;
using Tedu_Ecommance.Public.Catalogs.ProductCategories;

namespace Tedu_Ecommance.Public.Web.Pages.Home;

public class IndexModel : PublicPageModel
{
    private readonly IProductCategoriesAppService _productCategoriesAppService;
    public List<ProductCategoryInListDto>? Categories { get; set; }
    public IndexModel(IProductCategoriesAppService productCategoriesAppService)
    {
        _productCategoriesAppService = productCategoriesAppService;
    }
    public async Task OnGetAsync()
    {
        Categories = await _productCategoriesAppService.GetListAllAsync();
    }
}
