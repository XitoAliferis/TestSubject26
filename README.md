# Test Subject 26

## Description
Test Subject 26 is a roguelike game developed using Unity, where players control a robot designed to look and think like a human. The player navigates through procedurally generated floors, engaging in turn-based battles and solving puzzles, to uncover the truth about their identity and powers. 

## Table of Contents
- [Project Description](#project-description)
- [Game Design](#game-design)
- [Story Line](#story-line)
- [Rules](#rules)
- [Individual Contributions](#individual-contributions)
  - [Carmel Kurland](#carmel-kurland)
  - [Xristopher Aliferis](#xristopher-aliferis)
  - [Michelle Menor](#michelle-menor)
  - [Grace Neville](#grace-neville)
  - [Nilufer Karaman](#nilufer-karaman)
- [Resources](#resources)

## Project Description
Test Subject 26 offers an immersive gaming experience where players delve into procedurally generated floors, battling enemies and solving puzzles. The protagonist, Test Subject 26, believes they are part of an experiment to grant magical powers, only to discover that they are actually a robot equipped with advanced technology.

## Game Design
- **Procedurally Generated Environment:** Utilizes dynamic and static grid tiles for diverse 3D floor designs.
- **Turn-Based Battles:** Players engage in combat with enemies upon collision, pausing the overworld.
- **Element-Based Combat:** Attacks leverage elemental strengths and weaknesses for strategic battles.
- **Progression System:** Players unlock new elements, skills, and items as they advance.
- **Health and SP Management:** Survival hinges on managing health and special points.
- **Permadeath Mechanic:** Defeated players respawn with opportunities to enhance stats permanently.
- **Character Customization:** Options available between runs for personalized gameplay.
- **Handcrafted Challenges:** Puzzles and boss battles on specific floors offer unique challenges.

## Story Line
Test Subject 26 is unaware of their true nature as a robot and believes they volunteered for an experiment to gain magical powers. With fragmented memories, they set out from a basement to higher floors to discover the truth. The journey occurs on a weekend with only security robots present. Players solve puzzles and fight enemies, including a pivotal battle with a former test subject, revealing the protagonist's true identity as a robot. Each defeat and subsequent respawn brings new knowledge and strength, driving the player towards the ultimate revelation.

## Rules
- **3D Environment:** Procedurally generated with dynamic and static tiles.
- **Enemy Encounters:** Wandering enemies trigger turn-based battles.
- **Elemental Attacks:** Utilize elemental strengths and weaknesses in combat.
- **Resource Management:** Health and SP meters regulate survival and abilities.
- **Permadeath:** Players respawn with opportunities for permanent stat increases.
- **Items and Traps:** Collect items for temporary benefits and navigate traps that deal HP damage.
- **Boss Battles and Puzzles:** Encounter handcrafted challenges on specific floors.
- **Character Customization:** Customize characters between runs.

## Individual Contributions

### Carmel Kurland
- Implemented the "Tile Subsystem" for dynamic and static grid tiles.
- Developed the HallwaysAndRoomsFloorGenerator for procedural floor generation.
- Created Tile Decorators for game object spawn points.
- Added enemies, decoration, and objects to floors using floor processors.
- Managed floor settings and generation with the AccentManager.
- Designed custom shaders for a semi-cell-shaded visual style.
- Created a test tileset for development.

### Xristopher Aliferis
- Developed Combatant classes, including PlayerCombatant and MonsterCombatant.
- Implemented BasicAttack and ElementalAttack classes with damage values and animations.
- Created a test scene to demonstrate combat mechanics.

### Michelle Menor
- Implemented the PlayerAvatar class for navigation and boundary establishment.
- Developed the PlayerCameraController class for consistent player visibility.
- Created the RoamingMonster class for dynamic enemy behavior and battle triggers.

### Grace Neville
- Developed the GameStateManager class for managing game states and object loading.
- Implemented the BattleCameraManager class for camera repositioning during combat.

### Nilufer Karaman
- Created a global singleton class for managing player-related values.
- Ensured data integrity with validation logic.

## Resources
- [Gameplay Video](https://www.youtube.com/watch?v=PmzGXjRh3Kw)
- [GitHub Repository](https://github.com/XitoAliferis/TestSubject26/tree/main)
