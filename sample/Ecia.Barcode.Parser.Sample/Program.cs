using Ecia.Barcode.Parser;
using System.Text.Json;

var jsonOptions = new JsonSerializerOptions { WriteIndented = true };

var barcodeString = "[)>06K111113S222224S333311K4446D20240823Pcustomer-part1Psupplier-part4K5Q66669D22481TLOT4LUSS777733PBin13Q8 of 92PAAA7Q10 lbs1VmfgECompliant";
var parser = new EciaBarcodeParser();

var parsedBarcode = parser.ParseBarcode(barcodeString);
Console.WriteLine("Parsed Barcode:");
Console.WriteLine(JsonSerializer.Serialize(parsedBarcode, jsonOptions));

var validationResult = parser.ValidateParsedBarcodeCompliance(parsedBarcode, LabelFormat.Product);
Console.WriteLine("Validation Results:");
Console.WriteLine(JsonSerializer.Serialize(validationResult, jsonOptions));