# Simple RPG battle demo for mobile

This project contains a small RPG demo consisting of 2 scenes. In the selection scene the player can pick 3 heroes to go to battle with. In the battle scene, the player can fight an enemy in turnbased RPG style. 

## Platform
- The game currently works in Unity and on Android devices (tested on a Galaxy A51 device). I have no access to iOS.
- The project uses Unity version 2021.3.18f1.

## How to play in Unity

1. In the Hierarchy in the Unity Editor, under Assets/Scenes, open the HeroSelection scene.
2. Press play to play the game.

## Features
- The player can go to battle by selecting 3 heroes from their collection.
- Simple battle mechanics in which players and enemies can attack, take damage and die. 
- Game progress mechanics (levelling after 5XP, increasing amount of heroes after each 5 battles). Progress is saved to file when leaving the Battle Scene.
- Heroes are picked randomly from a reservoir of 30 heroes.
- Enemies are picked randomly from a reservoir of 3 enemies with different stats.
- Two Unity Editor extensions under "Data/":
  - Choose "Data/Reset Player Data" to remove all player data. The next time the game is started, 3 new random heroes are loaded.
  - Choose "Data/Set Player Heroes" to generate new player data with the chosen number of heroes (3-10). Existing progress is erased.

## Used 3rd Party Frameworks, Dependencies
- None.

## Used 3rd Party Assets
- Free "Pixel FX - Fire" asset pack from the Unity Asset Store for attack effects.
- Free "60+ Free Character Portraits With Modular Background" asset pack from the Unity Asset Store for hero avatars.
- Free Pokemon images for enemy avatars.

## Other remarks 
- I tried to use names in the documentation. For example, Experience instead of XP.
- Currently, the player data only records the Id of their heroes and how much Experience the hero has. With the current game rules there is no reason to record other attributes in the saved data, as they are all solely based on the hero's Experience.
- I intended to create functionality to easily test battles against enemies with different strengths, using scriptable objects or an editor extension. Unfortunately I did not have the time to implement this. What I did instead for now is that the 3 random enemies have different strengths.

