import React, { useEffect, useState } from 'react';
import { DataGrid, GridColDef, GridToolbar } from '@mui/x-data-grid';
import { OrderDetail, Status } from '../types/types';
import { fetchOrderDetails, updateFinalPrice } from '../api/orderService';
import { fetchAllStatuses, updateOrderStatus } from '../api/statusService';
import Tooltip from '@mui/material/Tooltip'; 
import { createTheme, ThemeProvider } from '@mui/material/styles'; 
import CssBaseline from '@mui/material/CssBaseline'; 
import { ToastContainer, toast } from 'react-toastify'; 
import 'react-toastify/dist/ReactToastify.css'; 
import PriceUpdateModal from '../components/Orders/PriceUpdateModal';

const theme = createTheme({
    palette: {
        primary: {
            main: '#556cd6',
        },
        secondary: {
            main: '#19857b',
        },
    },
});

const OrdersDisplay: React.FC = () => {
    const [orders, setOrders] = useState<OrderDetail[]>([]);
    const [modalOpen, setModalOpen] = useState(false);
    const [selectedOrder, setSelectedOrder] = useState<OrderDetail | null>(null);
    const [statuses, setStatuses] = useState<Status[]>([]);

    const isFinalPriceValidForClose = (order: OrderDetail): boolean => {
        return order.finalPrice !== 0;
    };

    const columns: GridColDef[] = [
        { field: 'customerName', headerName: 'שם לקוח', width: 100 },
        { field: 'customerICountId', headerName: ' מזהה לקוח Icount ', width: 150 },
        {
            field: 'statusName',
            headerName: 'סטטוס הזמנה',
            width: 150,
            renderCell: (params) => {
                const isCloseDisabled = params.row.finalPrice === 0; // Check if finalPrice is 0

                return (
                    <Tooltip title={isCloseDisabled ? "Update the final price before closing" : "Click to edit status"}>
                        <select
                            value={params.value}
                            onChange={(e: any) => handleStatusChange(Number(params.id), e.target.value)}//replace with e.target.key
                            className="status-dropdown"
                        >
                            {statuses.map((status) => (
                                <option key={status.statusId} value={status.statusName} disabled={status.statusName === 'close' && isCloseDisabled}>
                                    {status.statusName}
                                </option>
                            ))}
                        </select>
                    </Tooltip>
                );
            },
        },
        { field: 'typeName', headerName: 'סוג המוצר', width: 100 },
        { field: 'issueDescription', headerName: 'תאור התקלה', width: 150 },
        { field: 'estimatedPrice', headerName: 'מחיר משוער', type: 'number', width: 80 },
        {
            field: 'finalPrice', headerName: 'מחיר סופי', type: 'number', width: 150,
            renderCell: (params) => (
                <>
                    {params.value?.toLocaleString() || ''}
                    <button 
                        onClick={() => handleOpenModal(params.row)} 
                        style={{ marginLeft: '10px', color: params.row.statusName === "הסתיים" ? '#ccc' : 'black' }}
                        disabled={params.row.statusName === "הסתיים"}
                    >
                        עדכן מחיר
                    </button>
                </>
            ),
        },       
        { field: 'comments', headerName: 'הערות', width: 150, sortable: false },
    ];

    useEffect(() => {
        loadOrders();
        loadStatuses();
    }, []);

    const loadOrders = async () => {
        try {
            const fetchedOrders = await fetchOrderDetails();
            setOrders(fetchedOrders);
        } catch (error) {
            console.error('Failed to load orders:', error);
            toast.error('Failed to load orders, check console for details.');
        }
    };

    const loadStatuses = async () => {
        try {
            const fetchedStatuses = await fetchAllStatuses();
            setStatuses(fetchedStatuses);
        } catch (error) {
            console.error('Failed to load statuses:', error);
            toast.error('Failed to load statuses, check console for details.');
        }
    };

    const handleOpenModal = (order: OrderDetail) => {
        setSelectedOrder(order);
        setModalOpen(true);
    };

    const handleCloseModal = () => {
        setModalOpen(false);
    };

    const handleStatusChange = async (orderId: number, newStatusName: string) => {
        const selectedOrder = orders.find(order => order.orderId === orderId);
        if (selectedOrder && selectedOrder.statusName === "הסתיים") {
            toast.error('לא ניתן לשנות את הסטטוס לאחר שהסתיים');
            return;
        }
        if (newStatusName === "הסתיים" && selectedOrder && !isFinalPriceValidForClose(selectedOrder)) {
            toast.error('אין אפשרות לסגור את ההזמנה לפני עדכון המחיר הסופי');
            return
        }

        const newStatusId = statuses.find(status => status.statusName === newStatusName)?.statusId;
        if (newStatusId) {
            try {
                await updateOrderStatus(orderId, newStatusId);
                const updatedOrders = orders.map(order =>
                    order.orderId === orderId ? { ...order, statusName: newStatusName } : order
                );
                setOrders(updatedOrders);
                toast.success('הסטטוס עודכן בהצלחה');
            } catch (error) {
                console.error('Failed to update status:', error);
                toast.error('Status update failed, check console for details.');
            }
        }
    };

    const handleUpdateFinalPrice = async (newPrice: number) => {
        if (selectedOrder && newPrice !== undefined) {
            try {
                await updateFinalPrice(selectedOrder.orderId, newPrice);
                setModalOpen(false);
                loadOrders();
                toast.success('המחיר הסופי עודכן בהצלחה');
            } catch (error) {
                console.error('Error updating price:', error);
                toast.error('Error updating price, check console for details.');
            }
        }
    };

    return (
        <ThemeProvider theme={theme}>
            <CssBaseline />
            <div style={{
                height: 600,
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                margin: '30px auto',
            }}>
                <DataGrid
                    rows={orders}
                    columns={columns}
                    getRowId={(row) => row.orderId}
                    checkboxSelection
                    slots={{
                        toolbar: GridToolbar,
                    }}
                    sx={{
                        '& .bold-header': {
                            fontWeight: 'bold',
                        },
                        width: '100%',
                    }}
                />
                {selectedOrder && (
                    <PriceUpdateModal
                        open={modalOpen}
                        initialPrice={selectedOrder.finalPrice}
                        handleClose={handleCloseModal}
                        handleSubmit={handleUpdateFinalPrice}
                    />
                )}
            </div>
            <ToastContainer />
        </ThemeProvider>
    );
};
export default OrdersDisplay;
