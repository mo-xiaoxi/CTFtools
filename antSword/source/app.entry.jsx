/**
 * 中国蚁剑::程序入口
 * 创建：2015/12/20
 * 更新：2016/04/16
 * 作者：蚁逅 <https://github.com/antoor>
 */

'use strict';

const electron = global.require('electron');
const shell = electron.shell;
const remote = electron.remote;
const ipcRenderer = electron.ipcRenderer;

import Menubar from './base/menubar';
import CacheManager from './base/cachemanager';

const antSword = window.antSword = {
  /**
   * XSS过滤函数
   * @param  {String} html 过滤前字符串
   * @return {String}      过滤后的字符串
   */
  noxss: (html) => {
    return String(html).replace(/&/g, "&amp;").replace(/>/g, "&gt;").replace(/</g, "&lt;").replace(/"/g, "&quot;");
  },
  /**
   * 核心模块
   * @type {Object}
   */
  core: {},
  /**
   * 操作模块
   * @type {Object}
   */
  modules: {},
  /**
   * localStorage存储API
   * ? 如果只有一个key参数，则返回内容，否则进行设置
   * @param  {String} key   存储键值，必选
   * @param  {String} value 存储内容，可选
   * @param  {String} def   默认内容，可选
   * @return {None}       [description]
   */
  storage: (key, value, def) => {
    // 读取
    if (!value) {
      return localStorage.getItem(key) || def;
    };
    // 设置
    localStorage.setItem(key, value);
  }
};

// 加载核心模板
antSword['core'] = require('./core/index');

// 加载语言模板
antSword['language'] = require('./language/index');

// 加载代理
const aproxy = {
  mode: antSword['storage']('aproxymode', false, 'noproxy'),
  port: antSword['storage']('aproxyport'),
  server: antSword['storage']('aproxyserver'),
  password: antSword['storage']('aproxypassword'),
  username: antSword['storage']('aproxyusername'),
  protocol: antSword['storage']('aproxyprotocol')
}
antSword['aproxymode'] = aproxy['mode'];

antSword['aproxyauth'] = (
  !aproxy['username'] || !aproxy['password']
) ? '' : `${aproxy['username']}:${aproxy['password']}`;

antSword['aproxyuri'] = `${aproxy['protocol']}:\/\/${antSword['aproxyauth']}@${aproxy['server']}:${aproxy['port']}`;

// 通知后端设置代理
ipcRenderer.send('aproxy', {
  aproxymode: antSword['aproxymode'],
  aproxyuri: antSword['aproxyuri']
});

antSword['shell'] = shell;
antSword['ipcRenderer'] = ipcRenderer;
antSword['CacheManager'] = CacheManager;
antSword['menubar'] = new Menubar();
antSword['package'] = global.require('../package');

// 加载模块列表
// antSword['tabbar'] = new dhtmlXTabBar(document.getElementById('container'));
// 更新：使用document.body作为容器，可自动适应UI
antSword['tabbar'] = new dhtmlXTabBar(document.body);
[
  'shellmanager',
  'settings',
  'plugin'
].map((_) => {
  let _module = require(`./modules/${_}/`);
  antSword['modules'][_] = new _module();
});
// 移除加载界面&&设置标题
$('#loading').remove();
document.title = antSword['language']['title'] || 'AntSword';
