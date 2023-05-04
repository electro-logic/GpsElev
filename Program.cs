// Extract GPS Altitude from the GPS timestamp and write it as metadata
// exiftool (https://exiftool.org/) executable is required in the same folder of this app
using System.Diagnostics;
var files = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.arw");
foreach (var file in files)
{
    // Extract timestamp (example: "02:04:50")
    string timestamp = ExifTool($"{file} -gpstimestamp -T");
    // Calculate GPS Altitude  Base 60 encoded from Time
    var tokens = timestamp.Split(":");
    if (tokens.Length != 3)
    {
        Console.WriteLine($"{file} skipped");
        continue;
    }
    double altitude = (double.Parse(tokens[0]) * 3600 + double.Parse(tokens[1]) * 60 + double.Parse(tokens[2])) / 10.0;
    // Write exif Altitude (meters), AltitudeRef (above sea level), Remove Timestamp
    ExifTool($"-GPSAltitude={altitude} -GPSAltitudeRef=0 -GPSMeasureMode=3 -GPSTimeStamp= {file}");
    Console.WriteLine($"EXIF Metadata updated for {file}");
}
string ExifTool(string arg)
{
    var psi = new ProcessStartInfo("exiftool.exe", arg)
    {
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        RedirectStandardInput = true
    };
    var proc = Process.Start(psi);
    proc.StandardInput.WriteLine();
    proc.WaitForExit(5000);
    string result = proc.StandardOutput.ReadToEnd();
    return result;
}