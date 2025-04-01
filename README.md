# Platformer Microgame Enhancement

This project is an enhanced version of the Platformer Microgame template from Unity Asset Store. It demonstrates gameplay programming, code structure, and project organization in Unity.

## Table of Contents
- [Setup Instructions](#setup-instructions)
- [Features](#features)
- [Testing the Project](#testing-the-project)
- [Third-Party Assets](#third-party-assets)
- [Packages in Use](#packages-in-use)
- [Platform Support](#platform-support)



## Setup Instructions
### 1. Clone the Repository
```sh
 git clone <repository-url>
 cd <project-directory>
```

### 2. Install Unity
- Ensure you have **Unity 2021.3 LTS** (or later) installed.

### 3. Open the Project in Unity
1. Open Unity Hub.
2. Click on **Add** and select **Add Project from Disk**.
3. Navigate to the cloned project directory and open it.
4. Unity will load the project (this may take some time).

## Features
### 1. Health System (HP)
- The player has 3 lives.
- Losing a life occurs upon enemy collision.
- When all lives are lost, the player dies (popup/reset event triggered).
- UI: Three heart icons at the top-left corner represent remaining lives.

### 2. Coin Counter
- Displays the number of collected coins below the heart icons.

### 3. Damage Effect
- Screen shakes slightly upon enemy collision.
- A red flash effect appears on the screen to indicate damage.

### 4. Bubble Item (Special Item)
- Grants the player the ability to fly upon collection.
- Changes the player's color to yellow.
- Automatically ascends with increasing acceleration.
- Holding the Space key causes the player to descend.
- Releasing the Space key resumes ascent.
- Falling too low results in death (similar to falling off the map).
- If the player collides with obstacles (walls, floor), the ability is removed, and normal movement resumes.
- The ability remains active even when collecting coins or defeating enemies.

### 5. Configurable Parameters
- Health and flying speed parameters are adjustable via configuration files located in `Resources/Configs`.
### Running the Game
1. Open the main scene: `Assets/Scenes/MainScene.unity`
2. Click the **Play** button in Unity Editor.
3. Move the character, collect coins, avoid enemies, and test the special Bubble Item mechanic.

### Known Issues & Workarounds
- If UI elements do not update correctly, ensure the UI Canvas is set to **Screen Space - Overlay**.
- If screen shake does not work, check the camera scripts and assigned effects.

## Third-Party Assets
- **Platformer Microgame** (Unity Asset Store)
- Additional UI icons (if any, mention source)
- Custom Sound Effects (mention sources if applicable)

## Packages in Use
- **TextMeshPro** (for advanced UI rendering)
- **Cinemachine** (for camera shake effect)
- **Unity Input System** (for improved input handling)

## Platform Support
- Developed and tested on **Windows 10**.
- Expected to work on **MacOS** with potential minor adjustments.
- **Android/iOS builds not tested** but should work with mobile-compatible controls.

---
This project demonstrates modular game mechanics and best practices in Unity development. Feedback and contributions are welcome!

