# Wall Hacks

A game wall hack mod that makes opponents glow and visible through all walls.

## Features

- **X-Ray Vision**: See opponents through walls and objects
- **Glow Effect**: Opponents glow with a bright color (default: red)
- **Customizable**: Adjust glow color and intensity
- **Toggle Control**: Press X to toggle wall hack on/off
- **Dynamic Detection**: Automatically detects newly spawned enemies

## Setup Instructions (Unity)

1. Create a new C# script in your Assets folder and copy the `WallHack.cs` code
2. Create another script for the controller and copy `WallHackController.cs`
3. Attach `WallHack.cs` to your Main Camera or Player GameObject
4. Attach `WallHackController.cs` to the same GameObject
5. In the Inspector:
   - Assign the WallHack script reference to the WallHackController
   - Make sure your opponent GameObjects are tagged with "Enemy" (or change the tag in the script)
   - Customize glow color and intensity as desired

## Controls

- **X Key**: Toggle wall hack on/off

## Customization

In the Inspector, you can adjust:
- **Glow Color**: The color of the glow effect (default: red)
- **Glow Intensity**: How bright the glow is (default: 2.0)
- **Outline Width**: Width of the outline effect (default: 3)
- **Opponent Tag**: The tag used to identify enemy objects (default: "Enemy")
- **Update Interval**: How often to scan for new opponents (default: 0.1s)

## How It Works

1. Scans for all GameObjects with the "Enemy" tag
2. Creates glowing materials with emission enabled
3. Disables occlusion culling to make objects visible through walls
4. Updates each frame to catch newly spawned enemies

## Notes

- This is a mod/cheat for single-player or authorized testing only
- Not suitable for multiplayer games where cheating is against terms of service
- Requires Unity game engine

## License

Educational purposes only
