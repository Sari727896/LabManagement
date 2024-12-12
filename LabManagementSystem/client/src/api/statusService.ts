import axios from 'axios';
import { Status } from '../types/types';

const BASE_URL = 'https://localhost:7253/api';

export const fetchAllStatuses = async (): Promise<Status[]> => {
    try {
        const response = await axios.get<Status[]>(`${BASE_URL}/Status`);
        return response.data.map(status => ({
            statusId: status.statusId,
            statusName: status.statusName
        }));
    } catch (error) {
        console.error('Failed to fetch statuses:', error);
        throw new Error('Failed to fetch statuses');
    }
};


export const updateOrderStatus = async (orderId: number, statusId: number): Promise<void> => {
    try {
        await axios.put(`${BASE_URL}/Order/${orderId}/status/${statusId}`);
    } catch (error) {
        throw new Error('Failed to update order status');
    }
};
