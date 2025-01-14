﻿namespace ClothingStore.Controllers
{
    public class SearchController : Controller
    {
        public int PageSize = 4;
        private readonly IProductRepository _productRepository;        
        public SearchController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
       
        public async Task<IActionResult> SearchList(string name, int page=1)
        {
            if(string.IsNullOrEmpty(name))
                return RedirectToAction("ProductsNotFound");
            
            var products = await _productRepository.GetProductsByNameOnPageAsync(name, page, PageSize);
            if (products == null || products.Count == 0)
                return RedirectToAction("ProductsNotFound");
            var model = new SearchListViewModel
            {
                SearchKey = name,
                SearchProducts = products,                
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _productRepository.GetProductsByNameAsync(name)
                                                   .Result.Count
                }
            };
            return View(model);
        }

        public IActionResult ProductsNotFound() => View();
    }
}
