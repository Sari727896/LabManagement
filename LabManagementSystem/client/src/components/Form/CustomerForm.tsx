import React, { useEffect, useState } from 'react';
import { TextField, Button, Typography, Box, Grid, Alert } from '@mui/material';
import { useCustomer } from '../../hooks/useCustomer';
import { useIdContext } from './IdContext';

const CustomerForm: React.FC = () => {
    const { customerId, setcustomerId } = useIdContext();
    const [phoneNumber, setPhoneNumber] = useState<string>('');
    const [addedCustomer, setAddedCustomer] = useState<boolean>(false);
    const [isButtonDisabled, setIsButtonDisabled] = useState<boolean>(false);

    const {
        customerExists,
        customerDetails,
        loading,
        error,
        successMessage,
        validationErrors,
        checkCustomerExists,
        submitCustomer,
        handleDetailsChange
    } = useCustomer();

    useEffect(() => {
        if (customerExists) {
            setcustomerId(customerDetails.customerId);
            setIsButtonDisabled(true);
        }
    }, [customerExists, customerDetails, setcustomerId]);

    const handlePhoneNumberChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setPhoneNumber(e.target.value);
    };

    const handleCheckCustomer = async () => {
        await checkCustomerExists(phoneNumber);
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        await submitCustomer();
    };

    useEffect(() => {
        if (successMessage) {
            setAddedCustomer(true);
            setIsButtonDisabled(true);
            checkCustomerExists(phoneNumber);
        }
    }, [successMessage, phoneNumber, checkCustomerExists]);

    return (
        <Box sx={{ padding: '2rem', backgroundColor: '#f5f5f5', borderRadius: '8px', boxShadow: '0px 4px 12px rgba(0, 0, 0, 0.1)', direction: 'rtl' }}>
            <Typography variant="h6" component="h2" gutterBottom>
                הזן פרטי לקוח
            </Typography>

            <TextField
                label="מספר טלפון"
                variant="outlined"
                fullWidth
                value={phoneNumber}
                onChange={handlePhoneNumberChange}
                sx={{ marginBottom: '1rem' }}
                required
                error={!!validationErrors.phoneNumber}
                helperText={validationErrors.phoneNumber}
                InputProps={{
                    style: {
                        textAlign: 'right'
                    }
                }}
                InputLabelProps={{
                    style: {
                        textAlign: 'right'
                    }
                }}
            />

            <Button
                variant="contained"
                color="primary"
                fullWidth
                onClick={handleCheckCustomer}
                disabled={loading || !phoneNumber || isButtonDisabled}
                sx={{
                    marginBottom: '1rem',
                    backgroundColor: isButtonDisabled ? 'lightgray' : '',
                }}
            >
                בדוק אם הלקוח קיים
            </Button>

            {customerExists !== null && !addedCustomer && (
                customerExists ? (
                    <Alert severity="success">לקוח קיים במערכת</Alert>
                ) : (
                    <Box mt={3} component="form" onSubmit={handleSubmit}>
                        <Alert severity="info">הלקוח לא קיים במערכת, אנא השלם את פרטי הלקוח</Alert>
                        <Grid container spacing={2} sx={{ marginTop: '1rem', direction: 'rtl' }}>
                            <Grid item xs={12}>
                                <TextField
                                    label="שם מלא"
                                    variant="outlined"
                                    fullWidth
                                    name="fullName"
                                    value={customerDetails.fullName}
                                    onChange={handleDetailsChange}
                                    required
                                    error={!!validationErrors.fullName}
                                    helperText={validationErrors.fullName}
                                    InputProps={{
                                        style: {
                                            textAlign: 'right'
                                        }
                                    }}
                                    InputLabelProps={{
                                        style: {
                                            textAlign: 'right'
                                        }
                                    }}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    label="אימייל"
                                    variant="outlined"
                                    fullWidth
                                    name="email"
                                    value={customerDetails.email}
                                    onChange={handleDetailsChange}
                                    required
                                    error={!!validationErrors.email}
                                    helperText={validationErrors.email}
                                    InputProps={{
                                        style: {
                                            textAlign: 'right'
                                        }
                                    }}
                                    InputLabelProps={{
                                        style: {
                                            textAlign: 'right'
                                        }
                                    }}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <Button
                                    variant="contained"
                                    color="primary"
                                    fullWidth
                                    type="submit"
                                    disabled={loading || !customerDetails.fullName || !customerDetails.email || !customerDetails.phoneNumber}
                                >
                                    הוסף לקוח חדש
                                </Button>
                            </Grid>
                        </Grid>
                    </Box>
                )
            )}

            {error && (
                <Alert severity="error" variant="outlined" sx={{ marginTop: '1rem' }}>
                    {error}
                </Alert>
            )}

            {addedCustomer && (
                <Alert severity="success" variant="outlined" sx={{ marginTop: '1rem' }}>
                    הלקוח נוסף בהצלחה
                </Alert>
            )}
        </Box>
    );
};

export default CustomerForm;
