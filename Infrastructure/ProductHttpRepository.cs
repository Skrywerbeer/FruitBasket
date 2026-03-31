using System.Net.Http.Json;
using System.Text.Json;
using Infrastructure;

namespace Infrastructure;

public class ProductHttpRepository : IProductRepository
{
	public readonly string[] JsonFiles = [
		"data/Checkers/ApplesPears.json",
		"data/Checkers/BabyVeg.json",
		"data/Checkers/Bananas.json",
		"data/Checkers/BerriesCherries.json",
		"data/Checkers/BroccoliCauliflower.json",
		"data/Checkers/ButternutPumpkinSquash.json",
		"data/Checkers/CabbageBrusselSproutsArtichokes.json",
		"data/Checkers/CarrotsBeetrootTurnips.json",
		"data/Checkers/DatesFigsGuavas.json",
		"data/Checkers/GarlicGingerChilies.json",
		"data/Checkers/Grapes.json",
		"data/Checkers/Herbs.json",
		"data/Checkers/MangoKiwiExoticFruit.json",
		"data/Checkers/MarrowsBrinjals.json",
		"data/Checkers/MelonsWaterMelons.json",
		"data/Checkers/Mushrooms.json",
		"data/Checkers/OnionsCeleryLeeks.json",
		"data/Checkers/OrangeCitrus.json",
		"data/Checkers/PeachesNectarinesApricots.json",
		"data/Checkers/PeasBeansSweetcornAsparargus.json",
		"data/Checkers/Peppers.json",
		"data/Checkers/PlumsPrunes.json",
		"data/Checkers/PotatoesSweetPotatos.json",
		"data/Checkers/PreparedFruit.json",
		"data/Checkers/PreparedVegetable.json",
		"data/Checkers/SaladVegetables.json",
		"data/Checkers/SpinachKale.json",
		"data/Checkers/Tomatoes.json",
		
		"data/Woolworths/BulkVegetablesFruitSalads.json",
		"data/Woolworths/FreshVegetables.json",
		"data/Woolworths/InSeasonFruitVegetablesSalads.json",
		"data/Woolworths/SaladsHerbs.json"
	];

	private Dictionary<string, List<Product>> _Products { get; set; } = new();
	private HttpClient _Client;
	public bool DownloadComplete { get => _DownloadTask?.IsCompleted ?? false; }
	private Task _DownloadTask;

	public ProductHttpRepository(HttpClient client)
	{
		_Client = client;
	}
	
	public List<Product> All()
	{
		List<Product> allProducts = new();
		if (DownloadComplete)
		{
			foreach (string file in JsonFiles)
			{
				allProducts.AddRange(_Products[file]);
			}
		}
		return allProducts;
	}

	public async Task<List<Product>> AllAsync()
	{
		if (_DownloadTask is null)
			_DownloadTask = LoadAsync();
		await _DownloadTask;
		return All();
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
	
	private async Task DownloadProductListAsync(string filename)
	{
		_Products.Add(filename, await _Client.GetFromJsonAsync<List<Product>>(filename));
	}

	public void Load()
	{
		throw new NotImplementedException();
	}

	public async Task LoadAsync()
	{
		List<Task> taskList = new();
		foreach (string filename in JsonFiles)
		{
			taskList.Add(DownloadProductListAsync(filename));
		}
		await Task.WhenAll(taskList);
	}
}