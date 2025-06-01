import React from 'react';
import './styles/main.css';
import { createRoot } from 'react-dom/client';
import App from './components/App';

const container = document.getElementById('react-root');
const root = createRoot(container);
root.render(<App />);
  