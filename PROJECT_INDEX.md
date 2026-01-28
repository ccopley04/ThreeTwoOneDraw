# Three Two One...Draw - Project Index

## Project Overview
**Three Two One...Draw** is a deckbuilding RPG with a Wild West theme built in Unity. The game features turn-based combat using a unique time-slot system where players place cards that activate after a delay, creating strategic timing-based gameplay.

## Project Structure

### Core Game Information
- **Project Name**: ThreeTwoOneDraw
- **Unity Version**: 2022.3.x (Universal 2D Template)
- **Render Pipeline**: Universal Render Pipeline (URP)
- **Target Platform**: Standalone (Windows/Mac/Linux)
- **Company**: DefaultCompany
- **Bundle ID**: com.DefaultCompany.2D-URP

### Key Dependencies
- Unity Input System (1.14.0)
- Universal Render Pipeline (17.0.4)
- Cinemachine (3.1.4)
- TextMeshPro (2.0.0)
- Unity 2D Animation Package
- Unity 2D Pixel Perfect Package

## Game Architecture

### Core Systems

#### 1. Card System (`Assets/Scripts/Combat/CardScripts/`)
The game uses an abstract card system with three main card types:

**Abstract Classes:**
- `AbstractCard.cs` - Base class for all cards with name, cost, image, and use method
- `AbstractSkill.cs` - Base class for skill cards
- `AbstractDefend.cs` - Base class for defense cards
- `AbstractBullet.cs` - Base class for bullet/projectile cards

**Card Types:**
- **Skill Cards**: Take Aim, Adrenaline Shot, Bandage, Dynamite, Iron Steel Plate, Sleight of Hand, Smoke Screen, Sweet Rewards
- **Bullet Cards**: Six Shooter Bullet, Tomahawk Bullet, Winchester Bullet
- **Defend Cards**: Defend, Defend Large, Defend Medium, Deflect

#### 2. Player System (`Assets/Scripts/Combat/PlayerScripts/`)
- `AbstractPlayer.cs` - Base class for both player and enemy characters
- `Player.cs` - Player character implementation
- `Enemy.cs` - Base enemy class
- `Cactus.cs` - Specific enemy type
- `BanditBoss.cs` - Boss enemy implementation
- `EnemyStateMachine.cs` - AI state machine for enemies

#### 3. Weapon System (`Assets/Scripts/Combat/WeaponScripts/`)
- `AbstractWeapon.cs` - Base weapon class with time slots and bullets
- `SixShooter.cs` - Six-shooter weapon implementation
- `Tomahawk.cs` - Tomahawk weapon implementation
- `Winchester.cs` - Winchester rifle implementation
- `WeaponMono.cs` - MonoBehaviour for weapon management

#### 4. Time Slot System
Unique gameplay mechanic where cards are placed in numbered time slots (0-9) that activate after a delay:
- Each weapon has different time slot configurations
- Special time slots can modify card effects (e.g., double damage, increased cost)
- Cards activate automatically when their timer expires

### Game Managers

#### Core Managers (`Assets/Scripts/Managers/`)
- `EncounterControl.cs` - Main combat controller and game state manager
- `BulletManager.cs` - Manages bullet projectiles
- `DefenseManager.cs` - Handles defense mechanics
- `HealthBarHelper.cs` - UI health bar management
- `MusicManager.cs` - Audio and music management
- `SoundManager.cs` - Sound effects management
- `PlayerDefense.cs` - Player defense system

#### Overworld System (`Assets/Scripts/OverworldScripts/`)
- `OverworldManager.cs` - Manages overworld gameplay and scene transitions
- `NPCInteraction.cs` - Handles NPC interactions
- `SpriteMovement.cs` - Player movement in overworld
- `DisableOverworld.cs` - Scene management utilities

### Scenes Structure

#### Main Scenes
- `CombatDemo.unity` - Main combat scene
- `OverworldWildWest.unity` - Main overworld scene

#### Developer Scenes (Organized by contributor)
- **AddyM/**: AddyM CombatDemo.unity, AddyM Overworld.unity
- **Andy Chen/**: AndyCombatDemo.unity, AndyOverworldWildWest.unity
- **Eugene/**: Combat.EL.unity, Overworld.EL.unity
- **NickN/**: CombatDemoNick.unity, OverworldWildWestNick.unity
- **MichaelPerez/**: Michael.unity
- **Aud/**: aud.unity
- **CalebCopley/**: caleb.unity
- **Nirupama/**: nirupama.unity

### Assets Organization

#### Art Assets (`Assets/Resources/`)
- **CardArt/**: Card artwork and icons
- **CharSprites/**: Character sprites (Bill, NPCs, Bullets)
- **UI/**: User interface elements
- **TimeSlots/**: Time slot visual elements
- **Animations/**: Combat, idle, and movement animations
- **Music/**: Background music tracks
- **SFX/**: Sound effects
- **Objects/**: Environmental objects
- **desertground/**: Desert environment tiles

#### Prefabs (`Assets/Prefabs/`)
- `Card.prefab` - Card visual representation
- `Bullet.prefab` - Bullet projectile
- `NPCPrefab.prefab` - NPC character
- `TimeSlot.prefab` - Time slot UI element

### Game Mechanics

#### Combat System
1. **Turn-Based Combat**: Players and enemies take alternating turns
2. **Time Slot Mechanics**: Cards are placed in numbered slots (0-9) with countdown timers
3. **Card Types**:
   - **Bullets**: Deal damage to enemies
   - **Defends**: Block incoming bullets
   - **Skills**: Provide various effects (healing, buffs, special abilities)
4. **Deck Management**: Draw, discard, and shuffle mechanics
5. **Weapon System**: Different weapons provide different bullets and time slot effects

#### Tutorial System
- Built-in tutorial with popup explanations
- Teaches card types, time slots, and basic mechanics
- Interactive guidance for new players

### Input System
- **Mouse Controls**: Primary input method for card selection
- **Keyboard Controls**:
  - Number keys (0-9): Place cards in corresponding time slots
  - E: Reload deck (shuffle discard pile back into deck)
  - Arrow keys: Navigate cards (when not in mouse mode)
  - Escape: Quit game

### Audio System
- **Music Types**: Theme, Tutorial, Intro
- **Sound Types**: CardDraw, Reload, SixShooterBullet, and various other effects
- Dynamic music switching between overworld and combat

### Technical Features
- **State Machines**: Enemy AI using Unity Animator state machines
- **Object Pooling**: Efficient bullet and card management
- **Scene Management**: Additive scene loading for smooth transitions
- **UI System**: Health bars, card displays, time slot information
- **Animation System**: Character animations for combat and movement

## Development Notes

### Code Architecture
- **Object-Oriented Design**: Extensive use of abstract classes and inheritance
- **Singleton Pattern**: Used for managers (EncounterControl, MusicManager, etc.)
- **Component-Based**: Unity's component system for game objects
- **Event-Driven**: Timer-based events for card activation

### Performance Considerations
- Object pooling for bullets and cards
- Efficient UI updates
- Optimized sprite rendering with 2D Pixel Perfect

### Extensibility
- Modular card system allows easy addition of new cards
- Abstract weapon system supports new weapon types
- Flexible enemy system with state machines
- Configurable time slot effects

## Build Configuration
- **Target Resolution**: 1920x1080 (default)
- **Graphics API**: DirectX 11, OpenGL Core
- **Scripting Backend**: Mono (Editor), IL2CPP (Build)
- **Architecture**: x64

## Project Status
This appears to be a collaborative project with multiple contributors, each having their own scene variants. The core game systems are implemented and functional, with a complete combat system, overworld exploration, and tutorial functionality.

---

*This index was generated on $(date) and reflects the current state of the Three Two One...Draw Unity project.*
