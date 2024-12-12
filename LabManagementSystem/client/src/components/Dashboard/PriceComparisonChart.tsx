import React, { useState } from 'react';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';
import { usePriceComparison } from '../../hooks/usePriceComparison';

export default function PriceComparisonChart() {
  const { data, loading } = usePriceComparison();
  const [error, setError] = useState<Error | null>(null);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error.message}</div>;

  const seriesData = data.map(item => ({
    name: item.deviceType,
    averageInitialPrice: Number.isFinite(item.averageInitialPrice) ? item.averageInitialPrice : 0,
    averageFinalPrice: Number.isFinite(item.averageFinalPrice) ? item.averageFinalPrice : 0
  }));
  if (seriesData.some(item => !Number.isFinite(item.averageInitialPrice) || !Number.isFinite(item.averageFinalPrice))) {
    return <div>Invalid data provided to the chart. Please check the data source.</div>;
  }

  return (
    <div>
      <h2>השוואת מחיר משוער לסופי לפי סוג המכשיר</h2>
      <LineChart
        width={500}
        height={300}
        data={seriesData}
        margin={{
          top: 5,
          right: 30,
          left: 20,
          bottom: 5,
        }}
      >
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="name" />
        <YAxis />
        <Tooltip />
        <Legend />
        <Line type="monotone" dataKey="averageInitialPrice" stroke="#8884d8" activeDot={{ r: 8 }} />
        <Line type="monotone" dataKey="averageFinalPrice" stroke="#82ca9d" />
      </LineChart>
    </div>
  );
}
