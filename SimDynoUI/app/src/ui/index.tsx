import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { SignalRProvider } from '@/services/SignalRContext';
import "../../styles/fonts/imports.css";

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);

root.render(
  <React.StrictMode>
    <SignalRProvider>
      <App />
    </SignalRProvider>
  </React.StrictMode>
);