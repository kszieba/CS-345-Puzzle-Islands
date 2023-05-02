# Puzzle Islands
## CS345 Project 2
## By Oluwademilade Bolatimi, Tobias Dean & Katrina Ziebarth

## Introduction:

Welcome to our Unity project, Puzzle Islands. This is a 2D puzzle-focused exploration game for WebGL in which the player controls a ship exploring a chain of island mazes, collecting gold and raising their score. The player's main objective is to get from the start to the final island with as high of a score as possible. After completing a maze, the player will proceed to the next maze.

## Software Used:

- Unity Version 2021.3.21f1
- C#

## Software Architecture Diagram:

![alt text](Puzzle_islands_SAD.png)

## Instructions on how to test game:

- Clone GitHub repository onto local machine or machine of choice.
- Load folder using Unity Hub (version must be 2021.3.21f1!)
- Go to the 1-1 file in the Assets/Scenes folder and press play with Unity UI.

### Building the game:

- Go to file
- Go to build settings
- Select WebGL on left side of screen
- Hit build 
    - If build option not working hit switch platform, wait for load then hit build.


## Assets Folder Description:

- Audio - folder contains sounds for the game.
    - Gold Pick up - contains sounds for collisions with gold coins.
    - Background music - contains sounds for background music of all the levels of the game.
- Animations - contains gold coin and player animations.
- Collectibles - contains gold coin prefab and destination prefab.
- Scenes - All the different scenes/levels of our game.
- TextMesh Pro - built in with unity.
- Tilemaps - contains all the tile maps that we used to build our mazes.
- UIScrolls - Has all the credits, intro scroll and the original score assets.
- PlayerBehavior.cs - Script used to control player behavior, world intercation and updates the players UI.
- Mid-game Player.prefab - Version of the player used after the first level and contains the score and camera attached to it.
- New Game Player.prefab - Same as Mid Game Player except it prompts player to give username.

## Links to tutorials used:

- [Coins animation](https://www.youtube.com/watch?v=DZ-3g31jk90)
- [Audio added](https://www.youtube.com/watch?v=J77CMuAwVDY)
- [Tilemap implementation](https://www.youtube.com/watch?v=DTp5zi8_u1U&t=77s)




