# Test Subject 26

## Description
Test Subject 26 is an immersive roguelike game developed using Unity. Players navigate procedurally generated floors, engaging in turn-based battles and solving puzzles to uncover the truth about their identity and powers. The protagonist, Test Subject 26, is a robot designed to look and think like a human, who believes they are part of an experiment to grant magical powers.

## Table of Contents
- [Project Description](#project-description)
- [Game Design](#game-design)
- [Story Line](#story-line)
- [Rules](#rules)
- [Structural Design](#structural-design)
- [Behavioral Design](#behavioral-design)
- [Individual Contributions](#individual-contributions)
  - [Carmel Kurland](#carmel-kurland)
  - [Xristopher Aliferis](#xristopher-aliferis)
  - [Michelle Menor](#michelle-menor)
  - [Grace Neville](#grace-neville)
  - [Nilufer Karaman](#nilufer-karaman)
- [Resources](#resources)

## Project Description
Test Subject 26 offers an immersive gaming experience where players delve into procedurally generated floors, battling enemies and solving puzzles. The protagonist, Test Subject 26, believes they are part of an experiment to gain magical powers, only to discover that they are actually a robot equipped with advanced technology.

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

## Structural Design
The game uses a combination of procedurally generated and handcrafted floor designs. Dynamic and static grid tiles create varied indoor environments, enhancing replayability and player engagement.

## Behavioral Design
Player and enemy behaviors are driven by class-based systems. Enemies roam the floors, engaging players in turn-based combat upon collision. Players use elemental attacks with strategic strengths and weaknesses, managing health and special points to survive and progress.

## Individual Contributions

### Carmel Kurland
- **Tile Subsystem:** Defined dynamic and static grid tiles for floor generation.
- **HallwaysAndRoomsFloorGenerator:** Created the main procedural floor generator.
- **Tile Decorators:** Defined spawn points for game objects on tiles.
- **Floor Processors:** Added enemies, decoration, and objects to procedurally generated floors.
- **AccentManager:** Managed floor settings and generation during gameplay.
- **Custom Shaders:** Developed shaders for a semi-cell-shaded look.
- **Test Tileset:** Created a tileset for development use.

### Xristopher Aliferis
- **Combatant Class:** Developed PlayerCombatant and MonsterCombatant classes with health and movement attributes.
- **Move Class:** Implemented BasicAttack and ElementalAttack classes with damage values and animations.
- **Critical Damage:** Added critical damage mechanics and visual cues for attacks.
- **Elemental Affinity:** Integrated elemental affinity effects in attacks.
- **Test Scene:** Created a test scene to demonstrate combat mechanics.

### Michelle Menor
- **Overworld Obstacles:** Implemented obstacles such as lasers, acid, and fog.
- **Items:** Added collectible items like HealthBox and Test tubes for HP and SP recovery.
- **RoamingMonster Class:** Enabled dynamic enemy behavior and battle triggers.

### Grace Neville
- **GameStateManager Class:** Managed different game states and object loading/unloading.
- **BattleCameraManager Class:** Repositioned the camera during turn-based combat encounters.
- **UI Implementation:** Developed UI for end scenes, stat increases, and trait displays.

### Nilufer Karaman
- **Global Singleton Class:** Managed player-related values like health and special points.
- **Data Integrity:** Ensured data integrity with validation logic.
- **Dialogue Box and Announcement Box:** Added features for immersive storytelling and player updates.

## Resources
- [Gameplay Video](https://www.youtube.com/watch?v=PmzGXjRh3Kw)
- [GitHub Repository](https://github.com/XitoAliferis/TestSubject26/tree/main)
