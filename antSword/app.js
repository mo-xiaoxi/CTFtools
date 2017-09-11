'use strict';

const electron = require('electron');
const app = electron.app;
const BrowserWindow = electron.BrowserWindow;

app
  .once('window-all-closed', app.quit)
  .once('ready', () => {
    let mainWindow = new BrowserWindow({
      width: 1040,
      height: 699,
      minWidth: 1040,
      minHeight: 699,
      webgl: false,
      title: 'AntSword'
    });

    // 加载views
    mainWindow.loadURL(`file:\/\/${__dirname}/views/index.html`);

    // 调整部分UI
    const reloadUI = () => {
      mainWindow.webContents.executeJavaScript(`
        setTimeout(() => {
          antSword.modules.shellmanager.category.cell.setWidth(222);
        }, 500);
      `);
    };

    // 窗口事件监听
    mainWindow
      .on('closed', () => { mainWindow = null })
      .on('resize', reloadUI)
      .on('maximize', reloadUI)
      .on('unmaximize', reloadUI)
      .on('enter-full-screen', reloadUI)
      .on('leave-full-screen', reloadUI);

    // 打开调试控制台
    // mainWindow.webContents.openDevTools();

    // 初始化模块
    ['menubar', 'request', 'database', 'cache', 'update'].map((_) => {
      new ( require(`./modules/${_}`) )(electron, app, mainWindow);
    });
  });
