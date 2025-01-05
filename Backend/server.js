// server.js
const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const cors = require('cors');

const app = express();

// Middleware
app.use(cors());
app.use(bodyParser.json());  // To parse incoming JSON requests

// MongoDB connection
mongoose.connect('mongodb://localhost:27017/gameStats', { useNewUrlParser: true, useUnifiedTopology: true })
  .then(() => console.log('Connected to MongoDB'))
  .catch((error) => console.log('MongoDB connection error: ', error));

// Define the schema for the game stats
const gameStatsSchema = new mongoose.Schema({
  playerName: String,
  survivalTime: Number,
  bulletsFired: Number,
  zombieHits: Number,
  accuracy: Number,
  timestamp: String,
});

// Create a model based on the schema
const GameStat = mongoose.model('GameStat', gameStatsSchema);

// API route to handle POST requests from Unity
app.post('/api/stats', async (req, res) => {
  const { playerName, survivalTime, bulletsFired, zombieHits, accuracy, timestamp } = req.body;

  // Create a new GameStat instance
  const newStat = new GameStat({
    playerName,
    survivalTime,
    bulletsFired,
    zombieHits,
    accuracy,
    timestamp,
  });

  try {
    // Save the new stats to MongoDB
    await newStat.save();
    res.status(201).send({ message: 'Stats saved successfully!' });
  } catch (error) {
    console.error('Error saving stats:', error);
    res.status(500).send({ message: 'Error saving stats.' });
  }
});

// Start the server on port 3000
const port = 3000;
app.listen(port, () => {
  console.log(`Server running on http://localhost:${port}`);
});
