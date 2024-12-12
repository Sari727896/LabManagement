import React from 'react';
import { Link } from 'react-router-dom';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import DashboardIcon from '@mui/icons-material/Dashboard';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import FormIcon from '@mui/icons-material/Assignment'; // סמל לטופס

const Navbar = () => {
  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
        </Typography>
        <Box>
          {[
            { name: 'Dashboard', icon: <DashboardIcon />, link: '/dashboard' },
            { name: 'הזמנות', icon: <ShoppingCartIcon />, link: '/orders' },
            { name: 'טופס הזנת פרטי מעבדה', icon: <FormIcon />, link: '/' }
          ].map((item, index) => (
            <IconButton
              key={item.name}
              component={Link}
              to={item.link}
              color="inherit"
              sx={{ marginLeft: 2 }}
            >
              {item.icon}
              <Typography variant="body1" sx={{ marginLeft: 1 }}>
                {item.name}
              </Typography>
            </IconButton>
          ))}
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default Navbar;
