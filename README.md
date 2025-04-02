## **Sample Game Scripts**

This repository contains sample C# scripts developed by Plamp Games, a student-led game development company. These scripts are used in various Unity-based prototypes and game projects, and serve as examples of common systems and features implemented during development.

## **Features**

**Overview**
  - Core player systems: movement, controls, audio, and UI
  - Enemy behavior: melee, ranged, bullets, and base logic
  - Camera and environment interactions
  - Audio beat syncing and background music control
  - Room management and procedural elements
  - Modular, reusable components written in Unity’s C# framework

**Player Systems**
  - PlayerMovement: Handles directional movement and physics
  - PlayerControl: Coordinates inputs and actions
  - PlayerAudio: Controls player-related sound effects
  - PlayerHUD: Manages on-screen player info and health bars

**Enemy Systems**
  - EnemyBase: Inherited by all enemy types, handles common logic
  - EnemyMelee: Melee enemy behavior and attack radius
  - EnemyRanged: Ranged enemy logic and bullet spawn
  - EnemyBullet: Logic for enemy projectiles

**Camera & Environment**
  - CameraFollow: Keeps camera centered on the player
  - FaceCamera: Ensures UI or 3D elements face the player view
  - RoomManager: Basic procedural room management, **currently used in our proprietary MapManager script**

**Audio & Rhythm**
  - BackgroundMusicController: Plays and loops background tracks
  - BeatManager: Syncs gameplay elements to music beats

**UI & Game Flow**
  - PauseMenu: Toggles pause state and menu visibility
  - BulletManager: Controls object pooling and cleanup of projectiles

## **Tech Stack**

  - C# – Scripting language for Unity
  - Unity Engine – Game engine for prototyping and testing
  - Visual Studio – Code editor and debugger
  - Plastic SCM - Version Control System

## **Deployment**

These scripts are not part of a standalone application. They are organized for easy integration into Unity projects:

**To use a script:**

  1. Clone the repository: git clone https://github.com/Jerem-Dough/Game-Dev-Scripts.git
  2. Copy desired files into your Unity project's Scripts/ directory
  3. Attach scripts to GameObjects in the Unity Editor or use them in your systems

## **About Plamp Games**
Plamp Games is a student-founded indie game studio currently developing a rhythm-based roguelike dungeon crawler.
While our official website is under construction, we share early systems and tools here to document our progress and process.
