# Copilot Instructions

## Solution Structure

```
BiodiversityAssignmentCSharp.sln
├── src/
│   ├── BiodiversityLib/        # Class library — all domain logic lives here
│   │   └── Biodiversity.cs     # Single public static class: Biodiversity
│   └── BiodiversityConsole/    # Console entry point — calls into BiodiversityLib
│       └── Program.cs
├── tests/
│   └── BiodiversityTests/      # xUnit tests against BiodiversityLib
│       ├── LocationCountTests.cs
│       └── BiodiversityCountTests.cs
└── data/
    ├── Mammal.txt              # Tab-delimited sighting data: name\tlat\tlon
    └── Birds.txt
```

All logic belongs in `BiodiversityLib`. `BiodiversityConsole` is a thin driver only. Tests reference `BiodiversityLib` directly — never `BiodiversityConsole`.

## Build & Test Commands

```bash
# Build entire solution
dotnet build

# Run all tests
dotnet test tests/BiodiversityTests/BiodiversityTests.csproj

# Run a single test by name
dotnet test tests/BiodiversityTests/BiodiversityTests.csproj --filter "FullyQualifiedName~Birds_5km"

# Run the console app (must run from data/ so relative file paths resolve)
cd data
dotnet run --project ../src/BiodiversityConsole/BiodiversityConsole.csproj
```

## Key Conventions

### Data file format
Tab-delimited, one sighting per line: `SpeciesName\tLatitude\tLongitude`  
Parse coordinates with `CultureInfo.InvariantCulture` — the files use `.` as decimal separator regardless of system locale.

### Adding a new function to `Biodiversity`
- Add it as a `public static` method on the `Biodiversity` class in `BiodiversityLib/Biodiversity.cs`
- Reuse `LineToList` for parsing lines and `CalculateDistance` for distance filtering — don't duplicate that logic
- Add corresponding xUnit `[Fact]` tests in `BiodiversityTests`, one test class per function

### Test data files in tests
Data files are linked into the test output directory via `<None Include=... Link="data\...">` in `BiodiversityTests.csproj`. Reference them in tests with:
```csharp
Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "Mammal.txt")
```
To add a new data file to tests, add another `<None Include=...>` entry in `BiodiversityTests.csproj`.

### KML output
`PrintLocation` writes to `output.kml` in the working directory. KML coordinates are `lon,lat,0` order. Species names go in `<description>` (not `<name>`). Use `SecurityElement.Escape` on species names before writing to XML.

### Target framework
All projects target `net10.0` with `ImplicitUsings` and `Nullable` enabled.
