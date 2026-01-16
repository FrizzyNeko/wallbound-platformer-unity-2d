# Wallbound (Unity 2D)

**Wallbound** is a 2D platformer game developed in Unity.
The game focuses on precise movement, environmental interaction, and classic platformer mechanics such as collectibles, hazards, enemies, and level-based progression.

The player navigates handcrafted platforming levels, collects coins to increase their score, and must carefully manage their limited lives to complete the game.

---

## 🎮 Gameplay Overview

* The game consists of **3 platforming levels**
* The player completes parkour-style sections to progress
* **Coins** can be collected to increase the score
* **Mushrooms** act as bounce pads, launching the player higher to assist traversal
* The player starts with **3 lives**
* Falling into water or touching spikes causes death and reduces one life
* When all lives are lost:

  * The game restarts from the beginning
  * The score is reset to zero

---

## ✨ Core Features

* 2D platformer movement and traversal
* Three handcrafted levels
* Coin-based scoring system
* Life and death management system
* Environmental hazards (water, spikes)
* Bounce mechanics using mushroom platforms
* Basic enemy AI
* Player shooting mechanic triggered by input
* Sound effects and visual feedback
* State-driven camera system
* Sprite-based animations with state transitions

---

## 🛠️ Technical Focus & Systems

This project was built with an emphasis on understanding and applying core Unity systems and game development principles.

### Gameplay & Systems

* Basic enemy AI behavior
* Persistent game objects across scenes
* Singleton pattern usage
* Bullet system using `Instantiate` and `Destroy`
* Coroutine-based delays and timing control
* Life and score management systems

### Unity Engine Concepts

* Unity lifecycle methods and execution order
* New Unity Input System
* Tilemap and Tileset creation
* Rule Tile usage for scalable level design
* Prefab Variants for modular content
* Physics Material 2D for movement behavior
* Layer and Tag-based collision handling
* Audio integration for gameplay feedback

### Architecture & Design

* State-driven camera behavior
* Animation state management (idle, run, climb)
* Awareness of performance implications of `Instantiate` and `Destroy`
* Application of SOLID principles in gameplay code

---

## 🎯 What This Project Demonstrates

* Clean separation of gameplay responsibilities
* Practical use of Unity’s core systems
* Iterative development from mechanics to full game loop
* Foundational game architecture suitable for extension

---

## 🚀 Possible Improvements

* Enemy variety and more advanced AI behaviors
* Additional levels and difficulty progression
* Checkpoint system to reduce full restarts
* Expanded combat mechanics
* Visual polish and particle effects
* UI improvements for lives and score feedback

---

## 📌 Development Notes

Wallbound was developed alongside a structured Unity learning process.
