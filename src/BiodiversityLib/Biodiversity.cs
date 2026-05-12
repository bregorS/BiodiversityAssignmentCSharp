using System.Globalization;
using System.Security;

public static class Biodiversity
{
	// CalculateDistance returns the distance in kilometres between two lat/lon locations
	// using the Haversine formula.
	public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
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

	// LineToList takes a string and returns a list of strings, splitting on tab characters.
	// Trailing newline characters are removed before splitting.
	public static List<string> LineToList(string str)
	{
		return str.TrimEnd().Split('\t').ToList();
	}

	// LocationCount reads animal sighting records from a tab-delimited file (name, lat, lon)
	// and returns the count of sightings within the given distance (km) of the specified location.
	public static int LocationCount(string fileName, double distanceKm, double lat, double lon)
	{
		int count = 0;
		foreach (string line in File.ReadLines(fileName))
		{
			if (string.IsNullOrWhiteSpace(line))
				continue;

			List<string> fields = LineToList(line);
			if (fields.Count < 3)
				continue;

			if (!double.TryParse(fields[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double animalLat) ||
				!double.TryParse(fields[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double animalLon))
				continue;

			if (CalculateDistance(lat, lon, animalLat, animalLon) <= distanceKm)
				count++;
		}
		return count;
	}

	// BiodiversityCount reads animal sighting records from a tab-delimited file (name, lat, lon)
	// and returns the number of unique species found within distanceKm of the given location.
	public static int BiodiversityCount(string fileName, double distanceKm, double lat, double lon)
	{
		var species = new HashSet<string>();

		foreach (string line in File.ReadLines(fileName))
		{
			if (string.IsNullOrWhiteSpace(line))
				continue;

			List<string> fields = LineToList(line);
			if (fields.Count < 3)
				continue;

			if (!double.TryParse(fields[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double animalLat) ||
				!double.TryParse(fields[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double animalLon))
				continue;

			if (CalculateDistance(lat, lon, animalLat, animalLon) <= distanceKm)
				species.Add(fields[0]);
		}

		return species.Count;
	}

	// PrintLocation reads animal sighting records from a tab-delimited file (name, lat, lon)
	// and writes all sightings within distanceKm of the given location to "output.kml".
	// KML coordinates are in lon,lat,altitude order.
	public static void PrintLocation(string fileName, double distanceKm, double lat, double lon)
	{
		var placemarks = new System.Text.StringBuilder();

		foreach (string line in File.ReadLines(fileName))
		{
			if (string.IsNullOrWhiteSpace(line))
				continue;

			List<string> fields = LineToList(line);
			if (fields.Count < 3)
				continue;

			if (!double.TryParse(fields[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double animalLat) ||
				!double.TryParse(fields[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double animalLon))
				continue;

			if (CalculateDistance(lat, lon, animalLat, animalLon) <= distanceKm)
			{
				string name = SecurityElement.Escape(fields[0]);
				placemarks.AppendLine(
					$"""
					    <Placemark>
					      <description>{name}</description>
					      <Point>
					        <coordinates>{animalLon.ToString(CultureInfo.InvariantCulture)},{animalLat.ToString(CultureInfo.InvariantCulture)},0</coordinates>
					      </Point>
					    </Placemark>
					""");
			}
		}

		string kml =
			$"""
			<?xml version="1.0" encoding="UTF-8"?>
			<kml xmlns="http://www.opengis.net/kml/2.2">
			  <Document>
			{placemarks.ToString().TrimEnd()}
			  </Document>
			</kml>
			""";

		File.WriteAllText("output.kml", kml);
	}
}
