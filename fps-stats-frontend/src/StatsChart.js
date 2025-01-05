import React, { useEffect, useState } from 'react';
import { Bar } from 'react-chartjs-2';
import 'chart.js/auto';

const StatsChart = () => {
    const [stats, setStats] = useState([]);
    
    useEffect(() => {
        fetch("http://localhost:3000/api/stats")
            .then(response => response.json())
            .then(data => setStats(data))
            .catch(error => console.error('Error fetching stats:', error));
    }, []);
    
    const chartData = {
        labels: stats.map(stat => stat.playerName),
        datasets: [
            {
                label: 'Bullets Fired',
                data: stats.map(stat => stat.bulletsFired),
                backgroundColor: 'rgba(75, 192, 192, 0.6)',
            },
            {
                label: 'Zombie Hits',
                data: stats.map(stat => stat.zombieHits),
                backgroundColor: 'rgba(255, 99, 132, 0.6)',
            },
            {
                label: 'Accuracy (%)',
                data: stats.map(stat => stat.accuracy),
                backgroundColor: 'rgba(54, 162, 235, 0.6)',
            },
        ],
    };

    return (
        <div>
            <h2>Game Stats</h2>
            <Bar data={chartData} />
        </div>
    );
};

export default StatsChart;
