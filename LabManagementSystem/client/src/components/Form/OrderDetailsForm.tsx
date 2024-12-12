import React, { useState } from 'react';
import { TextField, Button, Box, Typography, Alert } from '@mui/material';
import { addOrderDetails } from '../../api/orderService';
import { useIdContext } from './IdContext';

interface OrderDetailsFormProps {
    onSubmit: (order: { estimatedPrice: string; comments: string }) => void;
}

const OrderDetailsForm: React.FC<OrderDetailsFormProps> = ({ onSubmit }) => {
    const [estimatedPrice, setEstimatedPrice] = useState('');
    const [comments, setComments] = useState('');
    const [error, setError] = useState<string | null>(null);
    const [successMessage, setSuccessMessage] = useState<string | null>(null);
    const { customerId, deviceId } = useIdContext();
    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        if (estimatedPrice != '' && customerId > 0 && deviceId > 0) {
            event.preventDefault();
            try {
                debugger
                const message = await addOrderDetails({ customerId, deviceId, orderDto: { estimatedPrice, comments } });
                setSuccessMessage(message);
                setEstimatedPrice('');
                setComments('');
                setError(null);
            } catch (error) {
                setError(error instanceof Error ? error.message : 'שגיאה לא ידועה');
                setSuccessMessage(null);
            }
        }
        else {
            setError('אנא מלא את הפרטים החסרים');
            setSuccessMessage(null);
        }

    };

    return (
        <Box component="form" onSubmit={handleSubmit} sx={{ padding: '2rem', backgroundColor: '#f5f5f5', borderRadius: '8px', boxShadow: '0px 4px 12px rgba(0, 0, 0, 0.1)' }}>
            <Typography variant="h6" component="h2" gutterBottom>
                הזן פרטים נוספים להזמנה
            </Typography>
            <TextField
                label="מחיר משוער"
                type="number"
                fullWidth
                value={estimatedPrice}
                onChange={(e) => setEstimatedPrice(e.target.value)}
                sx={{ marginBottom: '1rem' }}
            />
            <TextField
                label="הערות"
                multiline
                fullWidth
                rows={3}
                value={comments}
                onChange={(e) => setComments(e.target.value)}
                sx={{ marginBottom: '1rem' }}
            />
            <Button variant="contained" color="primary" type="submit">
                שמור פרטי הזמנה
            </Button>
            {successMessage && (
                <Alert severity="success" sx={{ mt: 2 }}>{successMessage}</Alert>
            )}
            {error && (
                <Alert severity="error" sx={{ mt: 2 }}>{error}</Alert>
            )}
        </Box>
    );
};

export default OrderDetailsForm;