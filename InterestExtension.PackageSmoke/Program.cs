using System.IO.Compression;
using InterestExtension;
using InterestExtension.Enums;

const decimal expected = 102.83908863189753692594868626m;
var actual = 100m.CalculateCompoundInterest(0.004m, 7, InterestPeriod.Monthly);

if (actual != expected)
{
    throw new InvalidOperationException($"Expected {expected}, but the installed package returned {actual}.");
}

if (args.Length != 1)
{
    throw new ArgumentException("Pass the package output directory as the only argument.");
}

var packageDirectory = Path.GetFullPath(args[0]);
var packagePath = Directory.GetFiles(packageDirectory, "InterestExtensions.*.nupkg").Single();
var symbolPackagePath = Path.ChangeExtension(packagePath, ".snupkg");

if (!File.Exists(symbolPackagePath))
{
    throw new InvalidDataException($"Symbol package not found: {symbolPackagePath}");
}

using var package = ZipFile.OpenRead(packagePath);
var entries = package.Entries.Select(entry => entry.FullName).ToHashSet(StringComparer.Ordinal);
var expectedEntries = new[]
{
    "README.md",
    "icon.png",
    "lib/netstandard2.0/InterestExtension.dll",
    "lib/netstandard2.0/InterestExtension.xml",
    "lib/net8.0/InterestExtension.dll",
    "lib/net8.0/InterestExtension.xml"
};

var missingEntries = expectedEntries.Where(entry => !entries.Contains(entry)).ToArray();

if (missingEntries.Length > 0)
{
    throw new InvalidDataException($"Package is missing: {string.Join(", ", missingEntries)}");
}

Console.WriteLine($"Package smoke test passed on {System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription}.");
