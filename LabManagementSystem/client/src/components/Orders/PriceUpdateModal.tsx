import React, { useState } from 'react';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';

interface PriceUpdateModalProps {
  open: boolean;
  handleClose: () => void;
  handleSubmit: (price: number) => void;
  initialPrice: number;
}

const PriceUpdateModal: React.FC<PriceUpdateModalProps> = ({ open, handleClose, handleSubmit, initialPrice }) => {
  const [price, setPrice] = useState<string>(initialPrice.toString());

  const handlePriceChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPrice(event.target.value);
  };

  const submit = () => {
    handleSubmit(parseFloat(price));
    handleClose();
  };

  return (
    <Dialog open={open} onClose={handleClose}>
      <DialogTitle>עדכן מחיר</DialogTitle>
      <DialogContent>
        <DialogContentText>
          אנא הכנס את המחיר החדש להזמנה:
        </DialogContentText>
        <TextField
          autoFocus
          margin="dense"
          id="price"
          label="מחיר חדש"
          type="number"
          fullWidth
          variant="outlined"
          value={price}
          onChange={handlePriceChange}
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose}>ביטול</Button>
        <Button onClick={submit}>שמור</Button>
      </DialogActions>
    </Dialog>
  );
};

export default PriceUpdateModal;