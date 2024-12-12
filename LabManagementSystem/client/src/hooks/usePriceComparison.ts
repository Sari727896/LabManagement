import { useState, useEffect } from 'react';
import { PriceComparisonData } from '../types/types';
import { fetchPriceComparisonData } from '../api/priceComparisonService';

export const usePriceComparison = () => {
  const [data, setData] = useState<PriceComparisonData[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const result = await fetchPriceComparisonData();
        setData(result);
        setLoading(false);
      } catch (err: any) {
        setError(err);
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  return { data, loading, error };
};
