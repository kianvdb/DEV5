# Progress Documentation

## Overview
This document tracks the progress made in the development of the first person shooter game with data analytics. It will detail the changes that have been implemented so far, the reasoning behind certain decisions, and the coding conventions used.

## Completed Tasks 17/12/24

### 1. **Input System Setup**
   - **New Input System Integration:** The Unity Input System was integrated to handle player inputs across various devices (keyboard, gamepad, etc.). 
   - **Action Map:** We created an action map for the `OnFoot` movement (with actions such as movement, jumping, crouching, and sprinting).
   - **Control Binding:** Input was bound to the arrow keys for movement to accommodate an AZERTY keyboard layout. This makes the movement more universal, enabling compatibility across different keyboard layouts.

### 2. **Player Input Management**
   - **PlayerInput Class:** The `PlayerInput` class was initialized to manage all actions related to player movement and interactions.
   - **Action Bindings:** We mapped the player actions (movement, jump, crouch, sprint) to the respective controls, ensuring the game responds correctly to the player's input.

### 3. **Movement System**
   - **PlayerMotor Class:** The `PlayerMotor` class was implemented to manage player movement based on inputs. It handles basic actions such as walking, sprinting, crouching, and jumping.
   - **Crouch and Sprint Features:** Crouching was set to adjust player height, with smooth transitions, while sprinting increases the movement speed.

### 4. **Look System**
   - **PlayerLook Class:** The `PlayerLook` class was created to allow the player to look around based on mouse or controller input.
   - **Camera Movement:** The camera is rotated based on horizontal and vertical input, with the camera's up/down rotation limited to avoid unnatural viewing angles.

### 5. **Coding Conventions**
   - **C# Coding Standards:**
     - **PascalCase:** Used for class names and method names (`PlayerMotor`, `ProcessMove`).
     - **camelCase:** Used for variable names and parameters (`playerInput`, `onFoot`).
     - **Consistent Indentation:** 4 spaces for each indentation level to maintain consistency and readability.
     - **Avoiding Magic Numbers:** All values, such as speeds and heights, were declared as public variables to allow easy modification and tuning. No hardcoded numbers were used for key gameplay variables.

### 6. **Code Design Principles**
   - **Separation of Concerns:** Each script has a distinct responsibility:
     - `PlayerInput` handles input actions.
     - `PlayerMotor` manages player movement and physics.
     - `PlayerLook` handles camera and player orientation.
   - **Single Responsibility Principle (SRP):** Each class has only one reason to change (e.g., PlayerMotor handles only movement-related changes, PlayerLook handles viewing).
   - **Encapsulation:** Player movement and camera controls are encapsulated inside their respective classes. Only necessary information is exposed.

### 7. **Why We Use Certain Code Styles**
   - **Input System for Flexibility:** The use of Unity’s new Input System allows easy configuration of player controls, providing flexibility for multiple platforms and input devices.
   - **Vector2 Input for Movement:** We use `Vector2` for movement since it makes it easy to handle directional inputs, such as from the arrow keys or left stick on a gamepad.
   - **CharacterController for Movement:** Using `CharacterController` simplifies movement and gravity handling, which reduces the need for custom physics code.

### 7. **Why We Use extra folders in Scripts folder**
 - **Structure and maintainability**  Created a new folder named 'Interactables' within the 'Scripts' directory to better organize the interactable objects. 
Moved the 'Keypad.cs' script and future interactable objects will inherit from the 'Interactable' base class.
This improves project structure and maintainability. The same reason there is a folder named 'Player' for all the player scripts.