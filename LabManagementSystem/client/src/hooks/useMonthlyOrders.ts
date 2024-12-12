import { useState, useEffect } from 'react';
import { fetchMonthlyOrders } from '../api/orderService';
import { TransformedOrderData } from '../types/types';

function getMonthName(monthNumber: number): string {
  return new Date(0, monthNumber - 1).toLocaleString('en-US', { month: 'long' });
}

export default function useMonthlyOrders(year: number) {
  const [orders, setOrders] = useState<TransformedOrderData[]>([]);
//Will be triggered every time the year variable changes.
  useEffect(() => {
    fetchMonthlyOrders(year)
      .then(data => {
        const transformedData = data.map(item => ({
          ...item,
          month: getMonthName(item.month)
        }));
        setOrders(transformedData);
      })
      .catch(error => console.error('Failed to fetch orders:', error));
  }, [year]);

  return orders;
}
