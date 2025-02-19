import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { MainLayout } from "./layout/layout";
import { createTheme, MantineProvider } from '@mantine/core';
import { SignalRProvider } from '@/services/SignalRContext';
import '@mantine/core/styles.css';

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);

const theme = createTheme({
  /* colorScheme: 'dark', */
});

root.render(
  <React.StrictMode>
    <SignalRProvider>
      <MantineProvider theme={theme}>
        <MainLayout>
          <App />
        </MainLayout>
      </MantineProvider>
    </SignalRProvider>
  </React.StrictMode>
);