public class BiodiversityCountTests
{
	private static string DataFile(string name) =>
		Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", name);

	[Fact]
	public void Mammal_20km_From_50_261667_Neg5_043333_Returns_42()
	{
		int result = Biodiversity.BiodiversityCount(DataFile("Mammal.txt"), 20.0, 50.261667, -5.043333);
		Assert.Equal(42, result);
	}

	[Fact]
	public void Mammal_5km_From_51_75_Neg1_25_Returns_30()
	{
		int result = Biodiversity.BiodiversityCount(DataFile("Mammal.txt"), 5.0, 51.75, -1.25);
		Assert.Equal(30, result);
	}

	[Fact]
	public void Mammal_15km_From_53_8_Neg1_583333_Returns_33()
	{
		int result = Biodiversity.BiodiversityCount(DataFile("Mammal.txt"), 15.0, 53.8, -1.583333);
		Assert.Equal(33, result);
	}

	[Fact]
	public void Birds_7_5km_From_52_966667_Neg1_166667_Returns_38()
	{
		int result = Biodiversity.BiodiversityCount(DataFile("Birds.txt"), 7.5, 52.966667, -1.166667);
		Assert.Equal(38, result);
	}

	[Fact]
	public void Birds_5km_From_51_452884_Neg0_973906_Returns_31()
	{
		int result = Biodiversity.BiodiversityCount(DataFile("Birds.txt"), 5.0, 51.452884, -0.973906);
		Assert.Equal(31, result);
	}
}
