namespace ApplicationCore;

using Infrastructure;

public class ProductProvider(IProductRepository repository)
{
	private List<Product> _products { get; set; } = new();
	public IEnumerable<Product> All()
	{
		return _products;
	}

	public async Task<List<Product>> AllAsync()
	{
		return await repository.AllAsync();
	}
	
	public IEnumerable<Product> ContainedInName(string name)
	{
		Console.WriteLine(name);
		return 
			from product in repository.All() 
			where product.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)
			orderby product.Price
			select product;
	}

	public void Load()
	{
		throw new NotImplementedException();
	}

	public async Task LoadAsync()
	{
		_products = await repository.AllAsync();
	}
}
