# First Person Shooter

## Overview

First Person Shooter is a 3D game prototype built with Unity that implements player movement, interaction, physics, and basic gameplay mechanics such as shooting and survival statistics tracking. The game features essential controls, zombie interactions, and the ability to track and display player performance data. Additionally, it includes backend integration to store and retrieve game statistics such as survival time, zombie hits, accuracy, and bullets fired, which can be visualized in charts using a React frontend.

## Features

- **Player Movement**: Navigate using arrow keys.
- **Jumping**: Jump with the spacebar.
- **Sprinting**: Increase speed by holding the left shift key.
- **Crouching**: Press the left control key to crouch.
- **Interaction**: Pick up items using the F key.
- **Shooting**: Fire bullets with the left mouse button.
- **Zombie Interaction**: Track zombie hits and calculate shooting accuracy.
- **Game Stats**:
  - Tracks bullets fired, zombie hits, survival time, and accuracy.
  - Sends these stats to a backend server for storage.
  - Displays performance data in charts using a React frontend.
- **Backend Integration**: The game sends player stats (e.g., survival time, bullets fired, accuracy) to a backend server and stores them in MongoDB.
- **Frontend Visualization**: A React app retrieves and visualizes game stats using interactive charts (Chart.js).

## Requirements

### Unity Game Setup

- **Unity**: Version 2020.3 or later.
- **Input System Package**: Unity Input System package installed for player controls.

### Backend Setup

- **Node.js**: Version 16.x or later.
- **Express.js**: For creating RESTful APIs.
- **MongoDB**: Installed locally or remotely for storing game statistics.

### Frontend Setup

- **React.js**: For building the user interface.
- **Chart.js**: For displaying game stats in a graphical format.

## Installation

### 1. Game Setup (Unity)

1. Clone the repository:
   git clone https://github.com/kianvdb/DEV5.git
   open the project in Unity.
2. Install the Unity Input System package.

### 2. Backend setup

1. Navigate to the backend directory.
2. Install the necessary dependencies.
3. Ensure MongoDB is installed and running locally or remotely. Create a database named fps-database and a collection called gameStats.
4. Start the backend server.

### 3. Frontend setup

1. Navigate to the fps-stats-frontend directory.
2. Install the frontend dependencies.
3. Start the frontend React application.

### 4. MonoDB Setup

1. Ensure MongoDB is installed and running.
2. Create a database called fps-database to store stats.
3. Create a collection named gameStats inside the fps-database.

### 5. Testing the game

1. Open the Unity project and click Play.
2. Move the player using the arrow keys, and use other controls (spacebar, left shift, etc.).
3. After the game ends, the game stats will be sent to the backend.
4. Open the React frontend to view the performance data and charts at http://localhost:3001.

### 6. Usage

BACKEND

1. POST /api/stats: Used for saving game stats like survival time, bullets fired, zombie hits, and accuracy.
2. GET /api/stats: Retrieves all game statistics for visualization in the frontend.

FRONTEND

1. Displays the stats in a graphical format using interactive charts built with Chart.js.
2. Shows metrics like survival time, accuracy, bullets fired, and zombie hits.
3. All stats are fetched from the backend server and rendered dynamically.

### 7. Displaying Stats in the Frontend

The React frontend will visualize the game stats in various interactive charts. Here's how the data will be displayed:

1. Survival Time: A bar chart showing the survival time of each game.
2. Accuracy: A percentage displayed showing the player's accuracy based on the number of zombie hits versus bullets fired.
3. Bullets Fired and Zombie Hits: Bar charts comparing the number of bullets fired versus zombies hit.

### 8. Contributing
If you'd like to contribute to the project, feel free to submit a pull request. Please follow these guidelines:

Write clear commit messages.
Ensure your code is well-tested.
Follow Unity and React coding standards.

### 9. License
This project is licensed under the MIT License - see the LICENSE.md file for details.

## 10. Sources

Game development:
https://www.youtube.com/watch?v=rJqP5EesxLk&list=PLGUw8UNswJEOv8c5ZcoHarbON6mIEUFBC
https://www.youtube.com/watch?v=woXLV8cIe7s&list=PLtLToKUhgzwm1rZnTeWSRAyx9tl8VbGUE&index=2

Troubleshooting and data analytics:

