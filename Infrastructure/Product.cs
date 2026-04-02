using System.Text.Json.Serialization;

namespace Infrastructure;

public class Product
{
	[JsonPropertyName("retailer")] 
	public string Retailer { get; set; } = string.Empty;
	
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;
	
	[JsonPropertyName("price")]
	public decimal Price { get; set; } = 0;
	
	[JsonPropertyName("imgUrl")]
	public string ImgUrl { get; set; } = string.Empty;
	
	[JsonPropertyName("itemUrl")]
	public string ItemUrl { get; set; } = string.Empty;
	
	/// <summary>
	/// Returns the result from <c>Name.Split(' ')</c>
	/// </summary>
	/// <returns>Returns a <c>string[]</c> filled with the space
	/// separated substrings of <c>Name</c></returns>
	string[] NameTokens()
	{
		return Name.Split(' ');
	}

	/// <summary>
	/// Convenience function for printing a <c>Product</c> to <c>stdout</c>.
	/// </summary>
	public void PrettyPrint()
	{
		ConsoleColor foreground = Console.ForegroundColor;
		ConsoleColor background = Console.BackgroundColor;

		Console.ForegroundColor = ConsoleColor.DarkMagenta;
		Console.Write($"{Name}: ");
		Console.ForegroundColor = ConsoleColor.DarkGreen;
		Console.WriteLine($"R {Price}");
		Console.ForegroundColor = ConsoleColor.Blue;
		Console.WriteLine(Retailer);
		Console.ForegroundColor = ConsoleColor.DarkYellow;
		Console.WriteLine("------------------------------");
		
		Console.ForegroundColor = foreground;
		Console.BackgroundColor =  background;
	}
}