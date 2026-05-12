internal static class Program
{
	private static void Main()
	{
		double distance = Biodiversity.CalculateDistance(50.7, -3.533333, 54.988056, -1.619444);
		Console.WriteLine($"Distance: {distance:F2} km");

		int count = Biodiversity.LocationCount("Mammal.txt", 10.0, 54.988056, -1.619444);
		Console.WriteLine($"Animals within 10km of Newcastle: {count}");

		Biodiversity.PrintLocation("Mammal.txt", 15.0, 51.452884, -0.973906);
		Console.WriteLine("output.kml written for animals within 15km of Reading.");

		int speciesCount = Biodiversity.BiodiversityCount("Mammal.txt", 25.0, 51.508129, -0.128005);
		Console.WriteLine($"Unique species within 25km of London: {speciesCount}");
	}
}
