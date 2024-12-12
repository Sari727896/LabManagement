export interface CustomerDto {
    customerId: number;
    fullName: string;
    phoneNumber: string;
    email: string;
}

export interface DeviceDto {
    deviceId: number;
    deviceType: string;
    model: string;
    issueDescription: string;
    unlockCode: string;
}

export interface DeviceTypeDto {
    deviceTypeId: number;
    typeName: string;
}


export interface OrderDto {
    estimatedPrice: string;
    comments: string;
}

export interface OrderCreationDto {
    customerId: number;
    deviceId: number;
    orderDto: OrderDto;
}

export interface OrderDetail {
    orderId: number;
    customerName: string;
    statusName: string;
    typeName: string;
    issueDescription: string;
    estimatedPrice: number;
    finalPrice: number;
    customerICountId: number;
    comments: string;
}

export interface Status {
    statusId: number;
    statusName: string;
}
export interface ChangeStatus {
    id: number;
    statusId: number;
}
export interface DeviceContextType {
    deviceId: number;
    customerId: number;
    setcustomerId: React.Dispatch<React.SetStateAction<number>>;
    setdeviceId: React.Dispatch<React.SetStateAction<number>>;
}

export interface StatusDropdownProps {
    orderId: number;
    currentStatusId: number;
    onChangeStatus: (orderId: number, statusId: number) => void;
}
// types/dashboardTypes.ts
export interface MonthlyOrderData {
    month: number;
    orderCount: number;
}
export interface OrdersByMonthChartProps {
    orders: MonthlyOrderData[];
}
export interface TransformedOrderData {
    month: string;
    orderCount: number;
}

export interface PriceComparisonData {
    deviceType: string;
    averageInitialPrice: number;
    averageFinalPrice: number;
}