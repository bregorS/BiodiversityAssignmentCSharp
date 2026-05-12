# Biodiversity Assignment (C#)

A C# console application for analysing animal sighting data, calculating distances between locations, and exporting results to KML format.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Project Structure

```
src/BiodiversityLib/        # Domain logic library
src/BiodiversityConsole/    # Console application
tests/BiodiversityTests/    # xUnit test suite
data/                       # Tab-delimited sighting data files
```

## Running the Application

The console app reads data files using relative paths, so it must be run from the `data/` directory.

**bash / zsh**
```bash
cd data
dotnet run --project ../src/BiodiversityConsole/BiodiversityConsole.csproj
```

**PowerShell**
```powershell
Set-Location data
dotnet run --project ..\src\BiodiversityConsole\BiodiversityConsole.csproj
```

### Expected Output

```
Distance: 493.92 km
Animals within 10km of Newcastle: 159
output.kml written for animals within 15km of Reading.
Unique species within 25km of London: 44
```

A file named `output.kml` will be created in the `data/` directory containing animal sightings within 15km of Reading, viewable in Google Earth or any KML-compatible tool.

## Running the Tests

**bash / zsh**
```bash
dotnet test tests/BiodiversityTests/BiodiversityTests.csproj
```

**PowerShell**
```powershell
dotnet test tests\BiodiversityTests\BiodiversityTests.csproj
```

To run a single test:

**bash / zsh**
```bash
dotnet test tests/BiodiversityTests/BiodiversityTests.csproj --filter "FullyQualifiedName~<TestName>"
```

**PowerShell**
```powershell
dotnet test tests\BiodiversityTests\BiodiversityTests.csproj --filter "FullyQualifiedName~<TestName>"
```

For example:

**bash / zsh**
```bash
dotnet test tests/BiodiversityTests/BiodiversityTests.csproj --filter "FullyQualifiedName~Mammal_20km"
```

**PowerShell**
```powershell
dotnet test tests\BiodiversityTests\BiodiversityTests.csproj --filter "FullyQualifiedName~Mammal_20km"
```

## Building the Solution

```powershell
dotnet build
```
