import axios from 'axios';
import { PriceComparisonData } from '../types/types';
const BASE_URL = 'https://localhost:7253/api/Order';

export const fetchPriceComparisonData = async (): Promise<PriceComparisonData[]> => {
  try {

    const response = await axios.get<PriceComparisonData[]>(`${BASE_URL}/price-comparison`);
    return response.data;
  } catch (error) {
    console.error('Failed to fetch price comparison data:', error);
    return [];
  }
};
