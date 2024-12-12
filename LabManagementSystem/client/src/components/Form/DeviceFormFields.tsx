import React from 'react';
import { Grid, TextField, Select, MenuItem, InputLabel, FormControl, Typography, SelectChangeEvent } from '@mui/material';
import { DeviceTypeDto } from '../../types/types';

interface DeviceFormFieldsProps {
    deviceDetails: {
        model: string;
        deviceType: string;
        issueDescription: string;
        unlockCode: string;
    };
    deviceTypes: DeviceTypeDto[];
    validationErrors: { [key: string]: string };
    handleInputChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
    handleSelectChange: (e: SelectChangeEvent<string>) => void;
}

const DeviceFormFields: React.FC<DeviceFormFieldsProps> = ({
    deviceDetails,
    deviceTypes,
    validationErrors,
    handleInputChange,
    handleSelectChange
}) => {
    return (
        <Grid container spacing={2} sx={{ marginTop: '1rem' }}>
            <Grid item xs={12}>
                <FormControl fullWidth sx={{ marginBottom: '1rem' }}>
                    <InputLabel id="device-type-label">סוג מכשיר</InputLabel>
                    <Select
                        labelId="device-type-label"
                        name="deviceType"
                        value={deviceDetails.deviceType}
                        onChange={handleSelectChange}
                        required
                        error={!!validationErrors.deviceType}
                    >
                        {deviceTypes.map((type) => (
                            <MenuItem key={type.deviceTypeId} value={type.typeName}>
                                {type.typeName}
                            </MenuItem>
                        ))}
                    </Select>
                    {validationErrors.deviceType && (
                        <Typography variant="body2" color="error">
                            {validationErrors.deviceType}
                        </Typography>
                    )}
                </FormControl>
            </Grid>
            <Grid item xs={12}>
                <TextField
                    label="תיאור התקלה"
                    variant="outlined"
                    fullWidth
                    name="issueDescription"
                    value={deviceDetails.issueDescription}
                    onChange={handleInputChange}
                    required
                    error={!!validationErrors.issueDescription}
                    helperText={validationErrors.issueDescription}
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    label="קוד פתיחה"
                    variant="outlined"
                    fullWidth
                    name="unlockCode"
                    value={deviceDetails.unlockCode}
                    onChange={handleInputChange}
                    required
                    error={!!validationErrors.unlockCode}
                    helperText={validationErrors.unlockCode}
                />
            </Grid>
        </Grid>
    );
};

export default DeviceFormFields;
