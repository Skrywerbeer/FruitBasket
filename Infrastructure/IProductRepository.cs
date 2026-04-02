namespace Infrastructure;

public interface IProductRepository
{
	public List<Product> All();
	public Task<List<Product>> AllAsync();
	public void Add(Product product);
	public void Remove(Product product);
	public Product this[int index] { get; set; }
	public void Load();
	public Task LoadAsync();
	public List<string> Tokens();
}