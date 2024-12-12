import React, { createContext, useContext, useState } from 'react';
import { DeviceContextType } from '../../types/types';



const IdContext = createContext<DeviceContextType | undefined>(undefined);

export const IdProvider: React.FC<React.PropsWithChildren<{}>> = ({ children }) => {
    const [deviceId, setdeviceId] = useState<number>(0);
    const [customerId, setcustomerId] = useState<number>(0);

    return (
        <IdContext.Provider value={{ deviceId, customerId, setdeviceId, setcustomerId }}>
            {children}
        </IdContext.Provider>
    );
};

export const useIdContext = () => {
    const context = useContext(IdContext);
    if (context === undefined) {
        throw new Error('useDeviceContext must be used within a DeviceProvider');
    }
    return context;
}