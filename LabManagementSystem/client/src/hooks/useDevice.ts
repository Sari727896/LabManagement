import { useState } from 'react';
import { getDeviceByModel, addDevice } from '../api/deviceService';
import { DeviceDto } from '../types/types';
import { SelectChangeEvent } from '@mui/material';

export const useDevice = () => {
    const [deviceExists, setDeviceExists] = useState<boolean | null>(null);
    const [deviceDetails, setDeviceDetails] = useState<DeviceDto>({
        deviceId: 0,
        deviceType: '',
        model: '',
        issueDescription: '',
        unlockCode: ''
    });
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [successMessage, setSuccessMessage] = useState<string | null>(null);
    const [validationErrors, setValidationErrors] = useState<{ [key: string]: string }>({});

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;

        setValidationErrors((prevErrors) => ({
            ...prevErrors,
            [name]: '',
        }));

        setDeviceDetails({
            ...deviceDetails,
            [name]: value,
        });
    };

    const handleSelectChange = (e: SelectChangeEvent<string>) => {
        const { name, value } = e.target;

        // Clear validation errors when input changes
        setValidationErrors((prevErrors) => ({
            ...prevErrors,
            [name!]: '',
        }));

        setDeviceDetails({
            ...deviceDetails,
            [name!]: value as string,
        });
    };

    const checkDeviceExists = async (model: string) => {
        setLoading(true);
        setError(null);
        try {
            const device = await getDeviceByModel(model);
            if (device) {
                setDeviceDetails(device);
                setDeviceExists(true);
            } else {
                setDeviceExists(false);
            }
        } catch (err) {
            setError('Failed to check device. Please try again.');
        } finally {
            setLoading(false);

            // Re-validate the model after checking existence
            if (!model) {
                setValidationErrors((prevErrors) => ({
                    ...prevErrors,
                    model: 'דגם מכשיר הוא שדה חובה',
                }));
            } else {
                setValidationErrors((prevErrors) => ({
                    ...prevErrors,
                    model: '',
                }));
            }
        }
    };

    const submitDevice = async () => {
        // Validate required fields
        if (!deviceDetails.model || !deviceDetails.deviceType || !deviceDetails.issueDescription || !deviceDetails.unlockCode) {
            setValidationErrors({
                model: !deviceDetails.model ? 'דגם מכשיר הוא שדה חובה' : '',
                deviceType: !deviceDetails.deviceType ? 'סוג מכשיר הוא שדה חובה' : '',
                issueDescription: !deviceDetails.issueDescription ? 'תיאור התקלה הוא שדה חובה' : '',
                unlockCode: !deviceDetails.unlockCode ? 'קוד פתיחה הוא שדה חובה' : ''
            });
            return;
        }

        setLoading(true);
        setError(null);
        try {
            await addDevice(deviceDetails);
            setSuccessMessage('המכשיר נוסף בהצלחה');
            setDeviceDetails({
                deviceId: 0,
                deviceType: '',
                model: '',
                issueDescription: '',
                unlockCode: ''
            });
            setDeviceExists(null);
        } catch (err) {
            setError('Failed to add device. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    return {
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
    };
};
