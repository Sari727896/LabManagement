import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Sidebar from './components/Navbar'; // ודא שהנתיב נכון
import HomePage from './pages/FormPage';
import OrdersDisplay from './pages/OrdersDisplay';
import Dashboard from './pages/Dashboard';

const App = () => {
    return (
        <Router>
            <Sidebar />
            <div style={{ paddingLeft: 250 }}>
                <div style={{ margin: '0 auto', maxWidth: 'calc(100% - 250px)' }}> {/* קונטיינר לשליטה על התוכן */}
                    <Routes>
                        <Route path="/" element={<HomePage />} />
                        <Route path="/orders" element={<OrdersDisplay />} />
                        <Route path="/dashboard" element={<Dashboard />} />
                    </Routes>
                </div>
            </div>
        </Router>
    );
};

export default App;
