using System;

internal static class Program
{
	private static void Main()
	{
		double distance = CalculateDistance(50.7, -3.533333, 54.988056, -1.619444);
		Console.WriteLine($"Distance: {distance:F2} km");
	}

	private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
	{
		const double DegreesToRadians = 0.017453293;
		const double EarthRadiusKm = 6372.797;

		double deltaLat = (lat1 - lat2) * DegreesToRadians;
		double deltaLon = (lon1 - lon2) * DegreesToRadians;

		lat1 *= DegreesToRadians;
		lat2 *= DegreesToRadians;

		double a = Math.Pow(Math.Sin(deltaLat / 2), 2)
			+ Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(deltaLon / 2), 2);
		double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

		return EarthRadiusKm * c;
	}
}
