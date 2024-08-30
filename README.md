# Ecia.Barcode.Parser
`Ecia.Barcode.Parser` provides the ability to easily parse ECIA barcode strings.

Disclaimer: This package is not an official ECIA product and is not endorsed or affiliated with ECIA. It is an independent implementation for parsing ECIA barcode strings.

## Install Package

[![NuGet](https://img.shields.io/nuget/v/Ecia.Barcode.Parser)](https://www.nuget.org/packages/Ecia.Barcode.Parser/)

Available on [NuGet](http://www.nuget.org/packages/Ecia.Barcode.Parser).

Install with the dotnet CLI: `dotnet add package Ecia.Barcode.Parser`, or through the NuGet Package Manager in Visual Studio.

## Usage
See the [sample app](./sample/Ecia.Barcode.Parser.Sample/Program.cs) for a basic example!
#### Getting an instance of the Parser

Dependency Injection
```csharp
var appBuilder = WebApplication.CreateBuilder(args);
appBuilder.Services.AddTransient<IEciaBarcodeParser, EciaBarcodeParser>();
```
Without DI
```csharp
var parser = new EciaBarcodeParser();
```

#### Parsing a Barcode string
```csharp
var barcodeString = "some ecia barcode string";
var parser = new EciaBarcodeParser();
ParsedBarcode parsedBarcode = parser.ParseBarcode(barcodeString);
```

#### Basic ECIA compliance validation
```csharp
ParsedBarcode parsedBarcode = parser.ParseBarcode(barcodeString);
// LabelFormat is used to determine the required identifiers. Omit or use LabelFormat.Unkown when the LabelFormat isn't known!
ValidationResult validationResult = parser.ValidateParsedBarcodeCompliance(parsedBarcode, LabelFormat.PackingSlip);
```

## ECIA Documentation
ECIA 2D barcode documentation [EIGP 114.2018](https://www.ecianow.org/assets/docs/GIPC/EIGP-114.2018%20ECIA%20Labeling%20Specification%20for%20Product%20and%20Shipment%20Identification%20in%20the%20Electronics%20Industry%20-%202D%20Barcode.pdf)
