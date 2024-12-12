import axios from 'axios';
import { MonthlyOrderData, OrderDetail, OrderCreationDto } from '../types/types';

const BASE_URL = 'https://localhost:7253/api/order';

export const addOrderDetails = async (orderCreationDto: OrderCreationDto): Promise<string> => {
    try {
        await axios.post(BASE_URL, orderCreationDto);
        return 'הפרטים נשמרו במערכת';
    } catch (error) {
        if (axios.isAxiosError(error) && error.response) {
            throw new Error('שגיאה בשמירת הפרטים');
        } else {
            throw new Error('שגיאת תקשורת עם השרת');
        }
    }
};
export const fetchOrderDetails = async (): Promise<OrderDetail[]> => {
    try {
        debugger
        const response = await axios.get<OrderDetail[]>(`${BASE_URL}/detailed`);
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error) && error.response) {
            throw new Error('שגיאה בטעינת פרטי ההזמנות');
        } else {
            throw new Error('שגיאת תקשורת עם השרת');
        }
    }
};
export const updateFinalPrice = async (id: number, finalPrice: number): Promise<void> => {
    try {
        console.log(`Sending price update for order ${id} with price:`, finalPrice);
        await axios.put(`${BASE_URL}/${id}/finalPrice`, finalPrice, {
            headers: {
                'Content-Type': 'application/json'
            }
        }); console.log("Update successful")
    } catch (error) {
        if (axios.isAxiosError(error) && error.response) {
            throw new Error('שגיאה בעדכון המחיר');
        } else {
            throw new Error('שגיאת תקשורת עם השרת');
        }
    }
};
export const fetchMonthlyOrders = async (year: number): Promise<MonthlyOrderData[]> => {
    const response = await axios.get<MonthlyOrderData[]>(`${BASE_URL}/monthly-orders/${year}`);
    return response.data;
}