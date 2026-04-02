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
		return 
			from product in repository.All() 
			where product.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)
			orderby product.Price
			select product;
	}

	public IEnumerable<Product> Similar(Product product, int count)
	{
		string[] parts = product.Name.Split(' ');
		List<Product> similarProducts = new();
		foreach (Product p in _products)
		{
			if (p == product)
				continue;
			foreach (string part in parts)
			{
				if (p.Name.Contains(part))
				{
					similarProducts.Add(p);
					break;
				}
			}

			if (similarProducts.Count == count)
				break;
		}

		return similarProducts;
	}

	public void Load()
	{
		_products = repository.All();
	}

	public async Task LoadAsync()
	{
		_products = await repository.AllAsync();
	}

	public List<string> Tokens()
	{
		return repository.Tokens();
	}

	public async Task<List<string>> TokensAsync()
	{
		return await repository.TokensAsync();
	}
}
