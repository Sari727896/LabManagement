import React, { CSSProperties } from 'react';
import TickPlacementBars from "../components/Dashboard/TickPlacementBars";
import PriceComparisonChart from '../components/Dashboard/PriceComparisonChart';
import '../css/Dashboard.css'; // make sure to import your CSS file for styling

const Dashboard = () => {
  return (
    <div className="dashboardContainer">
      <div className="chartsContainer">
        <TickPlacementBars />
        <PriceComparisonChart />
      </div>
    </div>
  );
};

export default Dashboard;
