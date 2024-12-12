import axios from 'axios';
import { CustomerDto } from '../types/types';

const BASE_URL = 'https://localhost:7253/api/customer';

export const getCustomerByPhoneNumber = async (phoneNumber: string): Promise<CustomerDto | null> => {
    try {
        const response = await axios.get<CustomerDto>(`${BASE_URL}/${phoneNumber}`);
        return response.data;
    } catch (error: any) {
        if (error.response && error.response.status === 404) {
            return null;
        }
        throw error;
    }
};

export const addCustomer = async (customerDto: CustomerDto): Promise<number> => {
    try {
        debugger
        const response = await axios.post<number>(BASE_URL, customerDto);
        return response.data;
    } catch (error) {
        throw error;
    }
};
