import React from 'react';
import './App.css';
import StatsChart from './StatsChart'; // Import the stats chart component

function App() {
  return (
    <div className="App">
      <header className="App-header">
      <h1>FPS Game Statistics</h1>
      </header>
      <main>
        <StatsChart /> {/* Render the statistics chart */}
      </main>
    </div>
  );
}

export default App;
