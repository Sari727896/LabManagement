// src/pages/AddDevicePage.tsx

import React from 'react';
import CustomerForm from '../components/Form/CustomerForm';
import DeviceForm from '../components/Form/DeviceForm';
import { Typography } from '@mui/material';

const AddDevicePage: React.FC = () => {
    return (
        <div>
            <Typography variant="h4" component="h1" sx={{ textAlign: 'center', margin: '2rem 0' }}>
                הוספת מכשיר חדש למעבדה
            </Typography>
            <CustomerForm />
            <DeviceForm />
        </div>
    );
};

export default AddDevicePage;
