using Infrastructure;

ProductJsonRepository repo = new();

Console.ForegroundColor = ConsoleColor.White;
Console.BackgroundColor = ConsoleColor.Black;

foreach (Product product in repo.All())
{
	product.PrettyPrint();
}
