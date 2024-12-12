import React from 'react';
import { Box, Typography } from '@mui/material';
import CustomerForm from '../components/Form/CustomerForm';
import DeviceForm from '../components/Form/DeviceForm';
import OrderDetailsForm from '../components/Form/OrderDetailsForm';
import { IdProvider } from '../components/Form/IdContext';

const FormPage: React.FC = () => {
    const handleOrderSubmit = (orderDetails: { estimatedPrice: string; comments: string }) => {
        console.log('Order details:', orderDetails);
    };

    return (
        <IdProvider>
            <Box
                sx={{
                    maxWidth: 800,
                    margin: '2rem auto',
                    padding: '2rem',
                    backgroundColor: '#fafafa',
                    borderRadius: '8px',
                    boxShadow: '0px 4px 12px rgba(0, 0, 0, 0.1)',
                    direction: 'rtl' // Right-to-left direction for Hebrew
                }}
            >
                <Typography variant="h4" component="h1" gutterBottom>
                    הוסף מכשיר למעבדה
                </Typography>
                <CustomerForm />
                <DeviceForm />
                <OrderDetailsForm onSubmit={handleOrderSubmit} />
            </Box>
        </IdProvider>
    );
};

export default FormPage;
