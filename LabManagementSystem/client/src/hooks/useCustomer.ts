
import { useState } from 'react';
import { getCustomerByPhoneNumber, addCustomer } from '../api/customerService';
import { CustomerDto } from '../types/types';

export const useCustomer = () => {
    const [customerExists, setCustomerExists] = useState<boolean | null>(null);
    const [customerDetails, setCustomerDetails] = useState<CustomerDto>({ customerId: 0, fullName: '', phoneNumber: '', email: '' });
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [successMessage, setSuccessMessage] = useState<string | null>(null);
    const [validationErrors, setValidationErrors] = useState<{ [key: string]: string }>({});

    const validatePhoneNumber = (phoneNumber: string): boolean => {
        if (!/^\d{10}$/.test(phoneNumber)) {
            setValidationErrors(prevErrors => ({ ...prevErrors, phoneNumber: 'מספר הטלפון חייב להיות בן 10 ספרות' }));
            return false;
        }
        setValidationErrors(prevErrors => {
            const { phoneNumber, ...rest } = prevErrors; // Remove phone number error if valid
            return rest;
        });
        return true;
    };

    const validateFields = () => {
        const errors: { [key: string]: string } = {};

        // Email Validation
        if (!/^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/.test(customerDetails.email)) {
            errors.email = 'כתובת האימייל אינה תקינה';
        }

        // Full Name Validation
        if (!customerDetails.fullName.trim()) {
            errors.fullName = 'יש להזין שם מלא';
        }

        setValidationErrors(errors);

        return Object.keys(errors).length === 0;
    };

    const checkCustomerExists = async (phoneNumber: string) => {
        if (!validatePhoneNumber(phoneNumber)) {
            return;
        }

        setLoading(true);
        setError(null);
        setSuccessMessage(null);
        try {
            const customer = await getCustomerByPhoneNumber(phoneNumber);
            if (customer) {
                setCustomerDetails(customer);
                setCustomerExists(true);
            } else {
                setCustomerExists(false);
                setCustomerDetails(prevDetails => ({ ...prevDetails, phoneNumber }));
            }
        } catch (err) {
            setError('An error occurred. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    const submitCustomer = async () => {
        if (!validateFields()) return;

        setLoading(true);
        setError(null);
        setSuccessMessage(null);
        try {
            await addCustomer(customerDetails);
            setSuccessMessage('הלקוח נוסף בהצלחה למערכת');
            setCustomerDetails({ customerId: 0, fullName: '', phoneNumber: '', email: '' });
            setCustomerExists(null);
        } catch (err) {
            setError('Failed to add customer. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    const handleDetailsChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setCustomerDetails({
            ...customerDetails,
            [e.target.name]: e.target.value,
        });
    };

    return {
        customerExists,
        customerDetails,
        loading,
        error,
        successMessage,
        validationErrors,
        checkCustomerExists,
        submitCustomer,
        handleDetailsChange,
    };
};
