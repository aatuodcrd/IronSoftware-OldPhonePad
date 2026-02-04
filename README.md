# IronSoftware Old Phone Pad Challenge

A C# library implementation of the Old Phone Pad coding challenge.

## Project Structure

- `src/IronSoftware.OldPhonePad`: Core class library containing the logic.
- `src/IronSoftware.OldPhonePad.App`: Console application for interactive testing.
- `tests/IronSoftware.OldPhonePad.Tests`: NUnit test project.

## How to Build

Run the following command in the solution root:

```bash
dotnet build "IronSoftware.CodingChallenge.slnx"
```

## How to Run Tests

Execute the unit tests using:

```bash
dotnet test "IronSoftware.CodingChallenge.slnx"
```

## How to Run the App

Start the interactive console app:

```bash
dotnet run --project src/IronSoftware.OldPhonePad.App
```

## Usage Examples

### Basic Usage

```csharp
using IronSoftware.OldPhonePad;

// Simple decoding with standard keypad layout
string result = PhonePad.Decode("4433555 555666#");
Console.WriteLine(result); // Output: HELLO

// More examples
string greeting = PhonePad.Decode("44 444#");        // Output: HI
string word = PhonePad.Decode("8 88777444666*664#"); // Output: TURING
```

### Advanced Usage - Custom Keypad Layout

```csharp
using IronSoftware.OldPhonePad;

// Create a custom keypad layout
public class CustomKeypadLayout : IKeypadLayout
{
    public IReadOnlyDictionary<char, string> Mapping { get; }
    
    public CustomKeypadLayout()
    {
        Mapping = new Dictionary<char, string>
        {
            { '2', "ABCÅ2" },  // Swedish characters
            { '3', "DEFÄ3" },
            // ... more mappings
        }.ToFrozenDictionary();
    }
}

// Use custom layout
var customLayout = new CustomKeypadLayout();
string result = PhonePad.Decode("22#", customLayout);
```

### Button Mapping

| Button | Characters |
|--------|------------|
| 0 | Space, 0 |
| 1 | 1 |
| 2 | A, B, C, 2 |
| 3 | D, E, F, 3 |
| 4 | G, H, I, 4 |
| 5 | J, K, L, 5 |
| 6 | M, N, O, 6 |
| 7 | P, Q, R, S, 7 |
| 8 | T, U, V, 8 |
| 9 | W, X, Y, Z, 9 |
| * | Backspace |
| # | Send/Submit |
| Space | Pause (commit current character) |


## Features

- ✅ **Modern Data Structures**: Uses FrozenDictionary for immutable, read-only key-value mapping
- ✅ **Immutable State**: Thread-safe design using readonly record structs
- ✅ **Extensible**: Support custom keypad layouts via `IKeypadLayout` interface
- ✅ **Memory Efficient**: Uses `ReadOnlySpan<char>` for input processing
- ✅ **Well-Tested**: 100% test coverage with 7 comprehensive test cases
- ✅ **Production-Ready**: Clean code following SOLID principles

## Implementation Details

**Architecture:**
- Interface-based design (`IKeypadLayout`) for flexibility
- Dependency injection pattern in `PhonePadProcessor`
- Singleton pattern for standard keypad layout

**Code Optimizations Applied:**
- `FrozenDictionary` for read-only dictionary (optimized for lookup operations)
- `ReadOnlySpan<char>` to avoid string enumeration allocations
- `[MethodImpl(AggressiveInlining)]` on frequently-called methods
- Pre-allocated `StringBuilder` capacity (32 characters)
- `readonly record struct` for immutable state management
- `sealed` classes where inheritance is not needed

## AI Output

- [Old Phone Pad Scaffolding](Old%20Phone%20Pad%20Scaffolding.md)
