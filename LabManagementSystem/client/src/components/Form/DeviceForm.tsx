import React, { useState, useEffect } from 'react';
import { TextField, Button, Typography, Box, Alert } from '@mui/material';
import { useDevice } from '../../hooks/useDevice';
import { getDeviceTypes } from '../../api/deviceService';
import { DeviceTypeDto } from '../../types/types';
import { SelectChangeEvent } from '@mui/material';
import { useIdContext } from './IdContext';
import DeviceFormFields from './DeviceFormFields';

const DeviceForm: React.FC = () => {
    const { deviceId, setdeviceId } = useIdContext();

    const [model, setModel] = useState<string>('');
    const [deviceTypes, setDeviceTypes] = useState<DeviceTypeDto[]>([]);
    const {
        deviceExists,
        deviceDetails,
        loading,
        error,
        successMessage,
        validationErrors,
        handleInputChange,
        handleSelectChange,
        checkDeviceExists,
        submitDevice
    } = useDevice();

    // Fetch device types on component mount
    useEffect(() => {
        const fetchDeviceTypes = async () => {
            try {
                const types = await getDeviceTypes();
                setDeviceTypes(types);
            } catch (error) {
                console.error('Failed to load device types', error);
            }
        };
        fetchDeviceTypes();
    }, []);

    // Update deviceId and increment fullFields when device exists
    useEffect(() => {
        if (deviceExists) {
            setdeviceId(deviceDetails.deviceId);
        }
    }, [deviceExists, deviceDetails, setdeviceId]);

    const handleModelChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setModel(e.target.value);
        handleInputChange(e);
    };

    const handleCheckDevice = async () => {
        await checkDeviceExists(model);
    };

    const handleSelectChangeWrapper = (e: SelectChangeEvent<string>) => {
        handleSelectChange(e);
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        await submitDevice();
    };

    return (
        <Box sx={{ padding: '2rem', backgroundColor: '#f5f5f5', borderRadius: '8px', boxShadow: '0px 4px 12px rgba(0, 0, 0, 0.1)' }}>
            <Typography variant="h6" component="h2" gutterBottom>
                הזן פרטי מכשיר
            </Typography>

            <TextField
                label="דגם מכשיר"
                variant="outlined"
                fullWidth
                value={model}
                onChange={handleModelChange}
                name="model"
                sx={{ marginBottom: '1rem' }}
                required
                error={!!validationErrors.model}
                helperText={validationErrors.model}
            />

            <Button
                variant="contained"
                color="primary"
                fullWidth
                onClick={handleCheckDevice}
                disabled={loading || !model || !!successMessage || deviceExists === true}
                sx={{ marginBottom: '1rem' }}
            >
                בדוק אם המכשיר קיים
            </Button>

            {deviceExists !== null && !successMessage && (
                deviceExists ? (
                    <Alert severity="success">מכשיר קיים במערכת</Alert>
                ) : (
                    <Box mt={3} component="form" onSubmit={handleSubmit}>
                        <Alert severity="info">המכשיר לא קיים במערכת, אנא השלם את פרטי המכשיר</Alert>

                        <DeviceFormFields
                            deviceDetails={deviceDetails}
                            deviceTypes={deviceTypes}
                            validationErrors={validationErrors}
                            handleInputChange={handleInputChange}
                            handleSelectChange={handleSelectChangeWrapper}
                        />

                        <Button
                            variant="contained"
                            color="primary"
                            fullWidth
                            type="submit"
                            disabled={
                                loading ||
                                !deviceDetails.deviceType ||
                                !deviceDetails.model ||
                                !deviceDetails.issueDescription ||
                                !deviceDetails.unlockCode ||
                                !!validationErrors.model ||
                                !!validationErrors.deviceType ||
                                !!validationErrors.issueDescription ||
                                !!validationErrors.unlockCode
                            }
                        >
                            הוסף מכשיר חדש
                        </Button>
                    </Box>
                )
            )}

            {error && (
                <Alert severity="error" variant="outlined" sx={{ marginTop: '1rem' }}>
                    {error}
                </Alert>
            )}

            {successMessage && (
                <Alert severity="success" variant="outlined" sx={{ marginTop: '1rem' }}>
                    {successMessage}
                </Alert>
            )}
        </Box>
    );
};

export default DeviceForm;