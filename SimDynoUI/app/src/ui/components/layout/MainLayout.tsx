import React, { ReactNode } from 'react';
import {
  AppBar,
  Toolbar,
  Box,
  CssBaseline,
  Typography,
} from '@mui/material';
import { createTheme, ThemeProvider } from '@mui/material/styles';

interface MainLayoutProps {
  children: ReactNode;
}

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
    primary: {
      main: '#FA6868',
    },
    secondary: {
      main: '#0BEA99',
    },
    background: {
      default: '#121212',
      paper: '#1E1E1E',
    },
  },
});

export const MainLayout: React.FC<MainLayoutProps> = ({ children }) => {
  const drawerWidth = 240;
  const headerHeight = 64;
  const contentMargin = 5;

  return (
    <ThemeProvider theme={darkTheme}>
      <Box sx={{ height: '100vh', width: '100vw', display: 'flex', flexDirection: 'column' }}>
        <CssBaseline />

        {/* Header */}
        <AppBar
          position="fixed"
          sx={{
            zIndex: (theme) => theme.zIndex.drawer + 1,
            backgroundColor: 'primary.main',
            boxShadow: '0px 2px 4px rgba(0, 0, 0, 0.1)',
            height: `${headerHeight}px`,
          }}
        >
          <Toolbar>
            <Typography
              variant="h6"
              noWrap
              component="div"
              sx={{ flexGrow: 1, fontWeight: 500, color: 'white' }}
            >
              App Logo
            </Typography>
          </Toolbar>
        </AppBar>

        {/* Content Container */}
        <Box
          sx={{
            display: 'flex',
            flexGrow: 1,
            bgcolor: 'background.default',
            height: `calc(100vh - ${headerHeight}px)`,
            marginTop: `${headerHeight}px`,
          }}
        >
          {/* Sidebar */}
          <Box
            sx={{
              width: drawerWidth,
              flexShrink: 0,
              backgroundColor: 'background.paper',
              borderRight: '1px solid rgba(0, 0, 0, 0.12)',
              height: `calc(100vh - ${headerHeight}px)`,
              overflowY: 'auto',
            }}
          >
            <Box sx={{ padding: 2 }}>
              <Typography>Sidebar Content</Typography>
            </Box>
          </Box>

          {/* Main Content View */}
          <Box
            component="main"
            sx={{
              flexGrow: 1,
              padding: 3,
              overflowY: 'auto',
              overflowX: 'hidden',
              width: `calc(100% - ${drawerWidth + contentMargin}px)`,
              marginLeft: `${contentMargin}px`,
              bgcolor: 'background.default',
              height: `calc(100vh - ${headerHeight}px)`,
            }}
          >
            {children}
          </Box>
        </Box>
      </Box>
    </ThemeProvider>
  );
};

export default MainLayout;