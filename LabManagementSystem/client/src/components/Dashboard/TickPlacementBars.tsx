import React, { useState } from 'react';
import { BarChart } from '@mui/x-charts/BarChart';
import useMonthlyOrders from '../../hooks/useMonthlyOrders';
import TickParamsSelector from './TickParamsSelector';

interface TickPlacementBarsProps {
  height?: number;
}

export default function TickPlacementBars({ height = 400 }: TickPlacementBarsProps) {
  const [tickPlacement, setTickPlacement] = useState<'end' | 'start' | 'middle' | 'extremities'>('middle');
  const [tickLabelPlacement, setTickLabelPlacement] = useState<'tick' | 'middle'>('middle');
  const currentYear = new Date().getFullYear();
  const orders = useMonthlyOrders(currentYear);

  return (
    <div>
      <TickParamsSelector
        tickPlacement={tickPlacement}
        tickLabelPlacement={tickLabelPlacement}
        setTickPlacement={setTickPlacement}
        setTickLabelPlacement={setTickLabelPlacement}
      />
      <div style={{ width: '100%' }}>
        <BarChart
          dataset={orders.map(({ month, orderCount }) => ({ month, value: orderCount }))}
          xAxis={[{ scaleType: 'band', dataKey: 'month', tickPlacement, tickLabelPlacement }]}
          yAxis={[{ label: 'ספירת הזמנות' }]}
          series={[{ dataKey: 'value', label: 'ספירת הזמנות חודשית' }]}
          height={height}
        />
      </div>
    </div>
  );
}
