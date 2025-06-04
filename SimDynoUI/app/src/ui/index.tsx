import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { SignalRProvider } from '@/services/SignalRContext';
import "../../styles/fonts/imports.css";

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);

// Using React.StrictMode causes React to mount, unmount, and then Remount components
// in dev environments. This will cause the useEffect in SignalRProvider to be called
// twice, resulting in two clients connecting to the server. This is expected behavior
// and doesn't cause any issues. Commenting out the <React.StrictMode> wrapper only
// creates a single client.
root.render(
  <React.StrictMode>
    <SignalRProvider>
      <App />
    </SignalRProvider>
  </React.StrictMode>
);