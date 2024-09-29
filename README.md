# Game Demo

A project utilising Unity with C# and 2D pixel-art to create a game demo in which players catch and collect mushroom-like creatures.

## Features

* Player controller with movement through WSAD and arrow keys
* Automated movement for NPC’s to move along a set path, or towards or away from a set target
* Animations for sprite movement
* Collision system in place for objects in scene
* Lighting with Universal Render Pipeline (URP)
* Audio including background music and SFX upon player interacting with objects in scene
* YarnSpinner dialogue system
* Dialogue set to run upon player key-down interaction or automatically when player is within a collision boundary of another object in scene

## Usage Example

[![Game Demo Scene](https://img.youtube.com/vi/jVbzblS3o9E/0.jpg)](https://www.youtube.com/watch?v=jVbzblS3o9E)

## Playable Demo

Demo available to download in [GameDemo 1.0 Release](https://github.com/BiancaDavey/GameDemo/releases/tag/1.0).

## Full Demo

Full game demo with additional scenes and features available to download on [itch.io](https://lunar-raven.itch.io/strangelands). Read more about the game [here](https://biancadavey.github.io/Site).

### Additional Features

* Inventory system enabling player to obtain, view and use items
* Inventory UI displaying player items, quantity, and item description
* Quest system enabling player progress to be recorded and utilised for scene triggers for dynamic changes in dialogue, item availability, new scenes being unlocked, and lighting and animation changes
* Data persistence system enabling player progress to be saved and loaded
* Pause Menu UI in game enabling player to save game and return to main menu
* Data persistence interface enables data persistence class methods to be implemented across inventory, quest, player status, and dialogue systems
* Status UI displaying player bars for health and magicka
* Player status can be updated by interactions with objects in scene
* Player status can be updated by player using items from inventory (eg. using “berries” adds 10 to health bar)
* Main menu UI enabling player to start a new game, load a saved game, continue the most recently saved game, view settings for the game, or exit the game
* Load game menu displays up to four saved games, the date and time it was saved, and an image representing the scene the game was saved in

