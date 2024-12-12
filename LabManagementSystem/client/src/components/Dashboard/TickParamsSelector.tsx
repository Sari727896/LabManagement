import React from 'react';
import { Stack, FormControl, FormLabel, RadioGroup, FormControlLabel, Radio } from '@mui/material';

interface TickParamsSelectorProps {
  tickPlacement: 'end' | 'start' | 'middle' | 'extremities';
  tickLabelPlacement: 'tick' | 'middle';
  setTickPlacement: React.Dispatch<React.SetStateAction<'end' | 'start' | 'middle' | 'extremities'>>;
  setTickLabelPlacement: React.Dispatch<React.SetStateAction<'tick' | 'middle'>>;
}

const TickParamsSelector: React.FC<TickParamsSelectorProps> = ({
  tickPlacement,
  tickLabelPlacement,
  setTickPlacement,
  setTickLabelPlacement,
}) => {
  return (
    <Stack direction="column" spacing={2}>
      <FormControl component="fieldset">
        <FormLabel component="legend">Tick Placement</FormLabel>
        <RadioGroup row value={tickPlacement} onChange={(e) => setTickPlacement(e.target.value as any)}>
          <FormControlLabel value="start" control={<Radio />} label="Start" />
          <FormControlLabel value="end" control={<Radio />} label="End" />
          <FormControlLabel value="middle" control={<Radio />} label="Middle" />
          <FormControlLabel value="extremities" control={<Radio />} label="Extremities" />
        </RadioGroup>
      </FormControl>
      <FormControl component="fieldset">
        <FormLabel component="legend">Tick Label Placement</FormLabel>
        <RadioGroup row value={tickLabelPlacement} onChange={(e) => setTickLabelPlacement(e.target.value as any)}>
          <FormControlLabel value="tick" control={<Radio />} label="Tick" />
          <FormControlLabel value="middle" control={<Radio />} label="Middle" />
        </RadioGroup>
      </FormControl>
    </Stack>
  );
};

export default TickParamsSelector;
