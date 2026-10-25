# Snake Game — C# Windows Forms

A desktop Snake game built with **C# and Windows Forms** as an Object-Oriented Programming (OOP) project. Guide the snake across a grid, collect food to earn points, avoid your own body and obstacles, and try to beat your saved high score.

The game includes configurable movement speed, optional wall collisions, rounded snake segments, multiple food types, and a replay option after game over.

## Features

- **Grid-based movement:** The snake moves in 40-pixel steps using a Windows Forms timer.
- **Smooth visuals:** Rounded snake segments, a small gap between segments, direction-aware eyes, and anti-aliased drawing.
- **Four food types:** Randomly selected food with different colors, sizes, scores, and probabilities.
- **Obstacle avoidance:** Eight randomly generated obstacles per game; touching one ends the round.
- **Collision detection:** Detects collisions with the snake's own body, obstacles, and (when enabled) walls.
- **Optional wall mode:** Keep **Walls** checked to lose on wall contact, or uncheck it to wrap around the board edges.
- **Four speed settings:** Easy, Medium, Hard, and Expert.
- **Score-based levels:** The displayed level increases every 100 points, up to level 12, and the snake's colors change with the level.
- **Pause and resume:** Use the `P` key or the Pause/Resume button.
- **Persistent high score:** The best score is read from and saved to `highscore.txt` in the application's output directory.
- **Restart and replay:** Reset the current run or choose to play again after game over.
- **Main-menu theme control:** Cycle through several muted background colors.

## Controls

| Input | Action |
| --- | --- |
| `↑` or `W` | Move up |
| `↓` or `S` | Move down |
| `←` or `A` | Move left |
| `→` or `D` | Move right |
| `P` | Pause / resume |
| **PAUSE / RESUME** | Pause / resume using the button |
| **RESET** | Start a new round and reset the current score |
| **EXIT** | Leave the game and return to the main menu |
| **Walls** checkbox | Enable or disable wall collisions / edge wrapping |
| **Difficulty** dropdown | Select the snake's movement speed |

The game prevents an immediate 180-degree reversal of the snake's direction.

## Food and scoring

Each new food item is selected using a random number between 0 and 99:

| Food | Appearance probability | Size | Points |
| --- | ---: | ---: | ---: |
| Red | 50% | 20 × 20 px | 10 |
| Orange | 25% | 20 × 20 px | 15 |
| Cyan | 15% | 40 × 40 px | 20 |
| Gold | 10% | 60 × 60 px | 30 |

Eating food increases the score and grows the snake by one segment. Food is positioned on the 40-pixel grid, with checks intended to prevent it from overlapping the snake or obstacles. The game retries placement up to 500 times and ends the round if it cannot find a valid location.

### Levels and colors

The level is calculated as:

```csharp
int level = Math.Min(score / 100, 12);
```

For example, a score of 250 gives level 2. The maximum displayed level is 12. Each level has its own snake body/head color combination.

### Speed settings

| Difficulty | Timer interval |
| --- | ---: |
| Easy | 200 ms |
| Medium | 150 ms |
| Hard | 100 ms |
| Expert | 60 ms |

A smaller timer interval makes the snake move faster. **Speed is chosen using the difficulty dropdown; it does not automatically increase when the level changes.**

## Getting started

### Requirements

- Windows
- Visual Studio with the **.NET desktop development** workload
- The .NET SDK matching the target framework specified in the project's `.csproj` file (the previous project documentation specifies **.NET 10 / `net10.0-windows`**)

No third-party NuGet packages are described in the supplied project source.

### Run the project

1. Clone or download the repository.
2. Open its `.sln` solution file in Visual Studio.
3. Select the `snakegame` project as the startup project, if necessary.
4. Build the solution using **Build → Build Solution**.
5. Press **F5** (or click **Start**) to run the application.
6. Click **START SNAKE** on the main menu.
7. Select a difficulty level and choose whether wall collisions are enabled.

## Project structure

```text
snakegame/
├── Program.cs               # Application entry point
├── mainform.cs              # Main menu, theme selection, high-score persistence
├── mainform.Designer.cs     # Main-menu Windows Forms controls
├── Form1.cs                 # Game loop, keyboard input, collision flow, drawing
├── Form1.Designer.cs        # Game-window controls and event hookups
├── Snake.cs                 # Movement, growth, direction, collision logic
├── Food.cs                  # Food types, random spawning, drawing
├── Obstacle.cs              # Obstacle positions and rendering
├── GameObject.cs            # Abstract base class for drawable game objects
└── README.md
```

*Windows Forms designer filenames may differ in capitalization. Visual Studio may also generate additional project, resource, and configuration files.*

## How the game works

1. `Program.cs` launches `mainform`, which loads the saved high score.
2. **START SNAKE** opens `Form1` and hides the main menu.
3. On game startup, the form generates obstacles and food.
4. Every timer tick, the snake advances one grid cell in its current direction.
5. The game checks wall collisions (if enabled), food collection, self-collision, and obstacle collision.
6. When food is collected, the score increases, the snake grows, and a new food position is generated.
7. When a collision ends the game, a dialog shows the score and offers **Play again?**
8. The game checks and saves a new high score when a round ends, the game form closes, or the player resets the round.

## OOP concepts demonstrated

| Concept | How it is used |
| --- | --- |
| **Classes and objects** | `Snake`, `Food`, `Obstacle`, `Form1`, and `mainform` |
| **Inheritance** | `Food` and `Obstacle` inherit from `GameObject`; the two forms inherit from `Form` |
| **Abstraction** | `GameObject` defines common properties, `Bounds`, and an abstract `Draw(Graphics)` method |
| **Polymorphism** | `Food` and `Obstacle` override `Draw()` and are drawn through `GameObject` references |
| **Encapsulation** | Internal state and methods control movement, scoring, and food behavior |
| **Method overriding** | `Form1` overrides `ProcessCmdKey()` to handle gameplay input |
| **Collections** | `List<Point>` stores snake segments; `List<Obstacle>` stores obstacles |
| **Event-driven programming** | Button clicks, difficulty selection, keyboard input, paint events, and timer ticks |

The project also demonstrates `Random`, `Rectangle` intersection checks, file I/O, expression-bodied methods, tuples, and Windows Forms graphics drawing.

## High-score storage

The highest score is stored in a plain-text file named `highscore.txt` beside the compiled application. The main form loads this file at startup and writes a new value when the player's score exceeds the saved record.

The application's output directory must be writable for high-score saving to succeed.

## Current limitations

- Obstacle generation does not explicitly prevent two obstacles from occupying the same position.
- Random food placement uses a limited number of attempts rather than enumerating every available position.
- The snake starts at a predefined grid-aligned position rather than a position calculated from the current panel size.
- The game is Windows-specific because it uses Windows Forms.

## Possible future improvements

- Difficulty progression tied to level, optionally configurable by the player.
- Non-overlapping obstacle generation and more varied obstacle patterns.
- Food spawning from a precomputed set of available cells.
- Sound effects and a settings menu.
- A responsive game board that adjusts the initial snake position for different window sizes.

## Author

**Uzair Bashir**  
BS Software Engineering — Mirpur University of Science & Technology (MUST)

*Created as an academic Object-Oriented Programming project.*
