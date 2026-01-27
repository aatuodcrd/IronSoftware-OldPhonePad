# IronSoftware Old Phone Pad Challenge

A C# library implementation of the Old Phone Pad coding challenge.

## Project Structure

- `src/IronSoftware.OldPhonePad`: Core class library containing the logic.
- `src/IronSoftware.OldPhonePad.App`: Console application for interactive testing.
- `tests/IronSoftware.OldPhonePad.Tests`: NUnit test project.

## How to Build

Run the following command in the solution root:

```bash
dotnet build
```

## How to Run Tests

Execute the unit tests using:

```bash
dotnet test
```

## How to Run the App

Start the interactive console app:

```bash
dotnet run --project src/IronSoftware.OldPhonePad.App
```

## Usage Example

```csharp
using IronSoftware.OldPhonePad;

string result = PhonePad.OldPhonePad("4433555 555666#");
Console.WriteLine(result); // Output: HELLO
```
