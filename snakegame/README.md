# Snake Game (C# Windows Forms)

A GUI-based Snake game written in C# for the Object-Oriented Programming (OOP) 2nd semester project.
The snake moves on a grid, eats food of different values, grows, and the game gets faster and changes
colour as the score rises.

**Author:** Uzair Bashir - Mirpur University of Science & Technology (MUST)

## Environment

| Item | Version |
|---|---|
| Visual Studio | [fill in: Help > About Microsoft Visual Studio] |
| .NET | .NET 10 (`net10.0-windows`) |
| UI framework | Windows Forms |
| External libraries / NuGet packages | None |

## How to open and run

1. Extract the submitted ZIP file.
2. Open the `.sln` solution file in Visual Studio (with the **.NET desktop development** workload installed).
3. Make sure `snakegame` is the startup project.
4. Choose **Build > Rebuild Solution** (no errors expected).
5. Press **F5** to run.
6. On the main menu, click **START SNAKE**.

## Controls

| Key / Button | Action |
|---|---|
| Arrow keys | Steer the snake |
| `P` or **PAUSE / RESUME** button | Pause or resume the game |
| **RESET** button | Restart (score 0, level 0, Easy speed) |
| **EXIT** button | Leave the game and return to the main menu |

## Major features

- **Grid-based, timer-driven movement** - one 20 px cell per timer tick; direct reversal is blocked.
- **Edge wrap-around** - leaving one side of the board brings the snake back on the opposite side.
- **Self-collision** ends the game and shows the final score.
- **Four food types** - Red 10, Orange 15, Cyan 20 (2x2 cells), Gold 30 (3x3 cells) points, with different probabilities.
- **Levels and difficulty** - level = score / 100 (max 12). Easy (levels 0-2, 200 ms), Medium (3-5, 150 ms),
  Hard (6-8, 110 ms), Expert (9-12, 75 ms).
- **Colour progression** - 13 body/head colour pairs, one per level; the level label matches the snake colour.
- **Pause / Resume** with an on-board PAUSED message.
- **High score** saved to `highscore.txt` next to the compiled program.
- **Main menu** with a theme button that cycles through neutral colours.

## Bonus features implemented

High-score saving using a file, multiple food types with different scores, improved graphics and themes.

## Project structure

| File | Purpose |
|---|---|
| `Program.cs` | Entry point; starts the main menu |
| `mainform.cs` | Main menu, high score loading/saving, theme button |
| `Form1.cs` / `Form1.Designer.cs` | Game window: timer loop, input, scoring, levels, pause, drawing |
| `Snake.cs` | Snake body, movement, growth, direction, collision checks |
| `Food.cs` | Food type, size, points and random position |

## OOP concepts used

Classes and objects (`Snake`, `Food`, `Form1`, `mainform`), encapsulation (private state, validated methods),
constructors, inheritance (`Form1` and `mainform` inherit from `Form`), and polymorphism (overriding
`ProcessCmdKey`; `Brush` references holding `SolidBrush` objects). See the project report for details and the UML diagram.

## Known limitations

- The snake wraps at the board edges instead of dying on a wall hit.
- Difficulty rises automatically with the level; it is not selectable from a menu.
- Only the arrow keys steer the snake.
- Food can appear on the snake's body.
