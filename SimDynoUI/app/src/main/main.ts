import { app, BrowserWindow, ipcMain } from 'electron';
import path from 'path';
import { fileURLToPath } from 'url';
import { dirname } from 'path';

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

function createWindow() {
  const win = new BrowserWindow({
    width: 1600,
    height: 900,
    frame: true,
    webPreferences: {
      preload: app.isPackaged ? path.join(__dirname, "preload.js")
        : path.join(__dirname, "../../dist/main/preload.js")
    },
  });

  win.loadFile(path.join(__dirname, '../../dist/index.html'));

  ipcMain.on('window-minimize', () => win.minimize());
  ipcMain.on('window-maximize', () => win.maximize());
  ipcMain.on('window-restore', () => win.restore());
  ipcMain.on('window-close', () => win.close());

  // Send window state updates
  win.on('maximize', () => {
    win.webContents.send('window-state', 'maximized');
  });
  win.on('unmaximize', () => {
    win.webContents.send('window-state', 'restored');
  });
  win.on('restore', () => {
    win.webContents.send('window-state', win.isMaximized() ? 'maximized' : 'restored');
  });
}

app.whenReady().then(createWindow);

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') app.quit();
});
