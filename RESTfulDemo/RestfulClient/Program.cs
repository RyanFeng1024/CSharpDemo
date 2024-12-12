using RestfulClient;

class Program
{
    static async Task Main(string[] args)
    {
        var settings = new ApiSettings();
        var productsClient = new ProductsApiClient(settings);

        Console.WriteLine("=== 开始调用 Products API ===");

        // 1. 获取所有产品
        Console.WriteLine("\n[GET ALL PRODUCTS]");
        var allProducts = await productsClient.GetAllAsync();
        PrintProducts(allProducts);

        // 2. 新增一个产品
        Console.WriteLine("\n[ADD NEW PRODUCT]");
        var newProduct = await productsClient.AddAsync(new Product { Name = "Grapes", Price = 2.5M });
        if (newProduct != null)
        {
            Console.WriteLine($"新增成功：ID={newProduct.Id}, Name={newProduct.Name}, Price={newProduct.Price}");
        }

        // 3. 根据ID获取刚刚新增的产品
        Console.WriteLine("\n[GET PRODUCT BY ID]");
        if (newProduct != null)
        {
            var productById = await productsClient.GetByIdAsync(newProduct.Id);
            if (productById != null)
            {
                Console.WriteLine($"获取成功：ID={productById.Id}, Name={productById.Name}, Price={productById.Price}");
            }
        }

        // 4. 更新产品信息
        Console.WriteLine("\n[UPDATE PRODUCT]");
        if (newProduct != null)
        {
            newProduct.Name = "Green Grapes";
            newProduct.Price = 3.0M;
            var updatedProduct = await productsClient.UpdateAsync(newProduct);
            if (updatedProduct != null)
            {
                Console.WriteLine($"更新成功：ID={updatedProduct.Id}, Name={updatedProduct.Name}, Price={updatedProduct.Price}");
            }
        }

        // 5. 按名称搜索产品
        Console.WriteLine("\n[SEARCH PRODUCTS BY NAME: 'Green']");
        var searchedProducts = await productsClient.SearchByNameAsync("Green");
        PrintProducts(searchedProducts);

        // 6. 删除产品
        Console.WriteLine("\n[DELETE PRODUCT]");
        if (newProduct != null)
        {
            var deleted = await productsClient.DeleteAsync(newProduct.Id);
            Console.WriteLine(deleted ? "删除成功" : "删除失败");
        }

        // 7. 再次获取所有产品列表
        Console.WriteLine("\n[GET ALL PRODUCTS AFTER DELETE]");
        var finalProducts = await productsClient.GetAllAsync();
        PrintProducts(finalProducts);

        Console.WriteLine("\n=== 调用结束 ===");
    }

    static void PrintProducts(IEnumerable<Product>? products)
    {
        if (products == null)
        {
            Console.WriteLine("没有获取到任何产品数据。");
            return;
        }

        foreach (var p in products)
        {
            Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Price: {p.Price}");
        }
    }
}
