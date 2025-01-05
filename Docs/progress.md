# Progress Documentation

## Overview
This document tracks the progress made in the development of the First Person Shooter game, including the integration of data analytics and visualization. It details the tasks completed so far, the reasoning behind specific decisions, and the coding conventions followed. The process includes the development of the game, the collection of player stats, the aggregation of data in a backend system, and the visualization of game statistics in charts using a React frontend.

## Completed Tasks 17/12/24

### 1. **Game Development (Core Mechanics)**
   - **Input System Setup**: 
     - The Unity Input System was integrated to handle player inputs across various devices (keyboard, gamepad, etc.). 
     - **Action Map**: Created an action map for the `OnFoot` movement (with actions such as movement, jumping, crouching, and sprinting).
     - **Control Binding**: Input was bound to the arrow keys for movement to accommodate an AZERTY keyboard layout. This made movement more universal, ensuring compatibility across different keyboard layouts.

   - **Player Input Management**:
     - The `PlayerInput` class was initialized to manage all actions related to player movement and interactions.
     - Action Bindings: Mapped player actions (movement, jump, crouch, sprint) to respective controls to ensure the game responds correctly to player input.

   - **Movement System**:
     - The `PlayerMotor` class was implemented to manage player movement based on inputs, handling basic actions such as walking, sprinting, crouching, and jumping.
     - **Crouch and Sprint Features**: Crouching adjusts player height, with smooth transitions, and sprinting increases movement speed.

   - **Look System**:
     - The `PlayerLook` class was created to allow the player to look around based on mouse or controller input.
     - Camera movement is controlled based on horizontal and vertical input, with constraints on up/down rotation to avoid unnatural viewing angles.

### 2. **Backend Integration and Data Aggregation**

   - **API for Stats Collection**:
     - Built a backend server using Node.js and Express to manage player stats. This server handles POST requests to collect data such as survival time, accuracy, bullets fired, and zombie hits.
     - **MongoDB Integration**: Stats are stored in a MongoDB database, enabling easy retrieval for future use.
     - **Data Aggregation**: After each session, the game sends aggregated stats to the backend where they are stored in MongoDB. This allows us to track performance over multiple gameplay sessions.

   - **Stats to Backend**:
     - A RESTful API endpoint was created to handle the submission of game statistics (e.g., accuracy, survival time, bullets fired) from the Unity game.
     - The server listens for POST requests and saves the received stats to the MongoDB database.

### 3. **Frontend: Visualization of Game Stats**

   - **React Setup**:
     - A React app was created to visualize the collected game stats.
     - Used **Chart.js** to create interactive charts that display metrics such as survival time, shooting accuracy, and the number of zombies hit.
   
   - **Data Fetching**:
     - The React app fetches data from the backend server via API calls. It retrieves aggregated game stats and dynamically renders them into charts.
   
   - **Chart Implementation**:
     - **Bar Charts**: Display survival time and bullets fired versus zombie hits.
     - **Accuracy Metrics**: Show a percentage of successful shots versus total shots fired.
     - **Survival Time**: A bar chart displaying survival times of different sessions for comparison.

### 4. **Code Design and Conventions**

   - **C# Coding Standards**:
     - **PascalCase**: Used for class and method names (`PlayerMotor`, `ProcessMove`).
     - **camelCase**: Used for variables and parameters (`playerInput`, `onFoot`).
     - **Consistent Indentation**: 4 spaces per indentation level for readability.
     - **Avoiding Magic Numbers**: All key gameplay variables (e.g., speeds, heights) were declared as public variables for easy tuning and adjustments.

   - **Code Design Principles**:
     - **Separation of Concerns**: Each script has a distinct responsibility:
       - `PlayerInput` handles input actions.
       - `PlayerMotor` manages player movement and physics.
       - `PlayerLook` handles camera and player orientation.
     - **Single Responsibility Principle (SRP)**: Each class has a single responsibility, improving maintainability.
     - **Encapsulation**: Player movement and camera controls are encapsulated inside their respective classes, exposing only necessary information.
   
   - **Why We Use Certain Code Styles**:
     - **Input System for Flexibility**: Unity’s Input System allows for flexible and easy configuration of player controls, providing compatibility across multiple platforms and input devices.
     - **Vector2 for Movement**: `Vector2` was used for movement as it simplifies handling directional inputs from the keyboard or a gamepad.
     - **CharacterController**: Using Unity’s `CharacterController` simplifies movement and gravity handling, reducing the need for custom physics calculations.

### 5. **Folder Organization and Maintainability**

   - **Organizing Scripts**:
     - To improve project structure, we created specific folders in the `Scripts` directory (e.g., `Player`, `Interactables`). For example:
       - Moved the `Keypad.cs` script to a new folder named `Interactables`.
       - Future interactable objects will inherit from a base class called `Interactable`.
     - This folder structure improves maintainability and makes it easier to manage and extend the project as it grows.

---

### Summary of Progress So Far

- **Game Development**: Core mechanics of the game, including player movement, input system, shooting, and interaction, were successfully implemented using Unity.
- **Data Collection**: Stats such as accuracy, survival time, and bullets fired are tracked in the game and sent to a backend server for storage.
- **Data Aggregation**: The backend server handles the aggregation and storage of player data in MongoDB.
- **Data Visualization**: A React frontend with Chart.js has been set up to visualize aggregated player stats, providing a clear, interactive way to analyze performance data.

In the next steps, we will continue improving the backend to support additional features, such as more granular game statistics, and refine the frontend visualizations to offer deeper insights into player performance.

---

This updated `Progress.md` should provide a clear, chronological overview of your project development, starting from the game mechanics implementation, moving on to data aggregation, and ending with the frontend visualization of game stats. Let me know if you need further adjustments!
