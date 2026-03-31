using System.Text.Json;
using Infrastructure;

namespace Infrastructure;

public class ProductJsonRepository : IProductRepository
{
	public readonly string[] JsonDirectorys = [
		"data/Checkers",
		"data/Woolworths",
		"data/PnP"
	];

	private Dictionary<string, List<Product>> _Products { get; set; } = new();

	public ProductJsonRepository()
	{
		Load();
	}
	
	public List<Product> All()
	{
		List<Product> allProducts = new();
		foreach (List<Product> productList in _Products.Values)
		{
			allProducts.AddRange(productList);
		}

		return allProducts;
	}

	public Task<List<Product>> AllAsync()
	{
		throw new NotImplementedException();
	}

	public void Add(Product product)
	{
		throw new NotImplementedException();
	}

	public void Remove(Product product)
	{
		throw new NotImplementedException();
	}

	public Product this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	
	public void Load()
	{
		foreach (string jsonDirectory in JsonDirectorys)
		{
			foreach (string file in Directory.EnumerateFiles(jsonDirectory))
			{
				using (StreamReader stream = new StreamReader(file))
				{
					_Products.Add(Path.GetFullPath(file), JsonSerializer.Deserialize<List<Product>>(stream.ReadToEnd()));
				}
			}
		}
	}

	public Task LoadAsync()
	{
		throw new NotImplementedException();
	}
}