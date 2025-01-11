# Path of Exile 2 AutoSplitter

A LiveSplit auto splitter component for Path of Exile 2. This component automatically splits your speedrun timer based on zone transitions in POE2.

## Features

- Automatic splits on zone transitions
- Support for both Normal and Cruel difficulty zones
- Real-time log file monitoring
- Integration with LiveSplit's timing system

## Installation

1. TBD

## Requirements

- LiveSplit 1.8.0 or later
- Path of Exile 2
- .NET Framework 4.8.1

## Configuration

The auto splitter will read from your POE2 client.txt log file, which is typically located at:
```
C:\Program Files (x86)\Steam\steamapps\common\Path of Exile 2 Beta\logs\Client.txt
```

## Development

### Building from Source

1. Clone the repository:
```bash
git clone [repository-url]
```

2. Open the solution in Visual Studio
3. Build the solution
4. The component will be automatically copied to your LiveSplit Components directory

### Project Structure

- `POE2.AutoSplitter/`: Main project directory
  - `Component/`: Core component classes
    - `GameClient/`: Client log parsing and event handling
    - `Timer/`: Timer and split management
  - `POE2.AutoSplitter.Tests/`: Unit tests

### Running Tests

Use Visual Studio's Test Explorer or run:
```bash
dotnet test
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## Acknowledgments

- LiveSplit Team for the LiveSplit framework
- Path of Exile 2 community
- brandondong for the original POE autosplitter component as inspiration (https://github.com/brandondong/POE-LiveSplit-Component)
