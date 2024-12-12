import axios from 'axios';
import { DeviceDto, DeviceTypeDto } from '../types/types';

const BASE_URL = 'https://localhost:7253/api/device';

export const getDeviceByModel = async (model: string): Promise<DeviceDto | null> => {
    try {
        const response = await axios.get<DeviceDto>(`${BASE_URL}/${model}`);
        return response.data;
    } catch (error: any) {
        if (error.response && error.response.status === 404) {
            return null;
        }
        throw error;
    }
};


export const addDevice = async (deviceDto: DeviceDto): Promise<number> => {
    try {
        const response = await axios.post<number>(BASE_URL, deviceDto);
        return response.data;
    } catch (error: any) {
        throw error;
    }
};

export const getDeviceTypes = async (): Promise<DeviceTypeDto[]> => {
    try {
        const response = await axios.get<DeviceTypeDto[]>(`${BASE_URL}/device-types`);
        return response.data;
    } catch (error: any) {
        throw error;
    }
};
