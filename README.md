# 🍌 Banana Co. Game Scripts

## 🎮 Introduction
This repository contains all essential scripts for the horror indie game **Banana Co.** These scripts power core game mechanics, including **enemy AI, player movement, sanity system, item interactions, UI menus, and more.** Whether you're modifying existing gameplay or adding new features, this repository provides a solid foundation.

## ⚙️ Setup Instructions
1. **Clone the repository:**
   ```bash
   git clone https://github.com/YourUsername/BananaCo-Game.git
   ```
2. **Open in Unity:** Load the project in Unity.
3. **Ensure required dependencies** (NavMesh, Unity UI, TextMeshPro, etc.) are installed.
4. **Attach scripts** to appropriate GameObjects as needed.
5. **Play and test!**

---

## 📜 Script Descriptions

### **🔹 Core Gameplay Scripts**
- **BananaBoy.cs** – Controls enemy AI, pathfinding, speed variations, and jump scares.
- **PlayerMovement.cs** – Handles player movement, jumping, and sprinting mechanics.
- **Sanity.cs** – Manages the player's sanity, triggering effects as it depletes.
- **StaminaBar_Controller.cs** – Implements the stamina system, limiting sprinting.
- **Victory_Gameover.cs** – Handles victory and game over conditions.

### **🔹 Environment & Interaction**
- **Door.cs** – Manages door interactions, including opening/closing logic.
- **Flashlight.cs** – Implements flashlight toggling and battery drain.
- **Battery.cs** – Controls battery consumption and UI display.
- **FlickeringLights.cs** – Adds random flickering effects to lights for atmosphere.
- **ScarySoundSpawner.cs** – Spawns random horror sound effects.
- **Banana.cs** – Controls collectible bananas and affects the enemy’s speed.
- **Battery_Item.cs** – Manages battery spawning and collection.
- **PlayerActions.cs** – Handles interactions with doors, bananas, and batteries.

### **🔹 UI & Menus**
- **MainMenu.cs** – Controls the main menu, including settings and multiplayer options.
- **PauseMenu.cs** – Implements pause functionality and settings adjustments.
- **SettingsMenu.cs** – Allows players to change volume, resolution, and quality settings.
- **ControlsMenu.cs** – Manages control sensitivity and input configurations.
- **Instruction_manager.cs** – Displays temporary tutorial instructions.

### **🔹 Game Management**
- **LevelManager.cs** – Manages levels, banana collection, and victory conditions.
- **PlayerSpawner.cs** – Spawns the player at designated points.
- **BananaBoySpawner.cs** – Spawns the enemy at different locations.
- **RotatingBananaBoy.cs** – Animates a rotating model of Banana Boy.

---

## ⭐ Highlighted Key Scripts

### **BananaBoy.cs (Enemy AI)**
This script controls **Banana Boy**, the primary enemy, using NavMesh pathfinding. It tracks the player, speeds up over time, and executes a **jump scare** when too close.

### **PlayerMovement.cs (Player Controls)**
Handles movement, sprinting, jumping, and stamina mechanics. It integrates a **head-bobbing effect** for realism.

### **Sanity.cs (Psychological Horror System)**
Decreases sanity when in darkness, triggering visual effects, whispering sounds, and ultimately a game over if it reaches zero.

### **Door.cs (Dynamic Doors)**
Allows players to interact with doors, which swing open/closed depending on the player's position.

### **Victory_Gameover.cs (Game End Conditions)**
Handles both victory and game over screens, including smooth fade-in text effects.

---

## 🎭 How The Game Works
- **Explore the level, avoid Banana Boy, and collect bananas to progress.**
- **Use the flashlight wisely** to conserve battery and maintain sanity.
- **Keep moving!** Stamina depletes while sprinting but regenerates when resting.
- **Survive until all levels are completed** to achieve victory.

---

## 🚀 Contributions & Feedback
Feel free to contribute improvements or report issues! Fork the repository, submit a pull request, or open an issue.

---

🎃 *Enjoy surviving Banana Co.!* 🍌
