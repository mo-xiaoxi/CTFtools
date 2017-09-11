/**
 * Shell管理模块
 */

'use strict';

import List from './list';
import Category from './category';
import ENCODES from '../../base/encodes';

const LANG_T = antSword['language']['toastr'];
const LANG = antSword['language']['shellmanager'];

class ShellManager {

  constructor() {
    // 初始化tabbar
    const tabbar = antSword['tabbar'];
    tabbar.addTab(
      'tab_shellmanager',
      // `<i class="fa fa-list-ul"></i> ${LANG['title']}`,
      '<i class="fa fa-th-large"></i>',
      null, null, true, false
    )

    const cell = tabbar.cells('tab_shellmanager');
    const layout = cell.attachLayout('2U');

    // 初始化左侧::列表管理
    this.list = new List(layout.cells('a'), this);

    // 初始化右侧::目录管理
    this.category = new Category(layout.cells('b'), this);

    this.cell = cell;
    this.win = new dhtmlXWindows();
    this.win.attachViewportTo(cell.cell);

    // 监听菜单栏消息
    antSword['menubar'].reg('shell-add', this.addData.bind(this));

    this.loadData();

  }
  // 清空缓存
  clearCache(id) {
    layer.confirm(
    LANG['list']['clearCache']['confirm'], {
      icon: 2, shift: 6,
      title: `<i class="fa fa-trash"></i> ${LANG['list']['clearCache']['title']}`
    }, (_) => {
      layer.close(_);
      const ret = antSword['ipcRenderer'].sendSync('cache-clear', {
        id: id
      });
      if (ret === true) {
        toastr.success(LANG['list']['clearCache']['success'], LANG_T['success']);
      }else{
        toastr.error(LANG['list']['clearCache']['error'](ret['errno'] === -2 ? 'Not cache file.' : ret['errno']), LANG_T['error']);
      }
    });
  }

  // 清空所有缓存
  clearAllCache() {
    layer.confirm(
    LANG['list']['clearAllCache']['confirm'], {
      icon: 2, shift: 6,
      title: `<i class="fa fa-trash"></i> ${LANG['list']['clearAllCache']['title']}`
    }, (_) => {
      layer.close(_);
      const ret = antSword['ipcRenderer'].sendSync('cache-clearAll');
      if (ret === true) {
        toastr.success(LANG['list']['clearAllCache']['success'], LANG_T['success']);
      }else{
        toastr.error(LANG['list']['clearAllCache']['error'](ret), LANG_T['error']);
      }
    });
  }

  // 添加数据
  addData() {
    // 判断当前tab是否在主页
    if (antSword['tabbar'].getActiveTab() !== 'tab_shellmanager') { this.cell.setActive() };
    // 初始化窗口
    const win = this.createWin({
      title: LANG['list']['add']['title'],
      width: 450,
      height: 350
    });
    win.denyResize();
    // 工具栏
    const toolbar = win.attachToolbar();
    toolbar.loadStruct([{
      id: 'add',
      type: 'button',
      icon: 'plus-circle',
      text: LANG['list']['add']['toolbar']['add']
    }, {
      type: 'separator'
    }, {
      id: 'clear',
      type: 'button',
      icon: 'remove',
      text: LANG['list']['add']['toolbar']['clear']
    }]);
    // 表单对象
    const form = win.attachForm([
      { type: 'settings', position: 'label-left', labelWidth: 80, inputWidth: 250 },
      { type: 'block', inputWidth: 'auto', offsetTop: 12, list: [
        { type: 'input', label: LANG['list']['add']['form']['url'], name: 'url', required: true },
        { type: 'input', label: LANG['list']['add']['form']['pwd'], name: 'pwd', required: true },
        { type: 'combo', label: LANG['list']['add']['form']['encode'], readonly: true, name: 'encode', options: (() => {
          let ret = [];
          ENCODES.map((_) => {
            ret.push({
              text: _,
              value: _,
              selected: _ === 'UTF8'
            });
          });
          return ret;
        })() },
        { type: 'combo', label: LANG['list']['add']['form']['type'], name: 'type', readonly: true, options: (() => {
          let ret = [];
          for (let c in antSword['core']) {
            let encoders = antSword['core'][c].prototype.encoders;
            ret.push({
              text: c.toUpperCase(),
              value: c,
              selected: c === 'php',
              list: ((c) => {
                let _ = [
                  { type: 'settings', position: 'label-right', offsetLeft: 60, labelWidth: 100 },
                  { type: 'label', label: LANG['list']['add']['form']['encoder'] },
                  { type: 'radio', name: `encoder_${c}`, value: 'default', label: 'default', checked: true }
                ];
                encoders.map((e) => {
                  _.push({
                    type: 'radio',
                    name: `encoder_${c}`,
                    value: e,
                    label: e
                  })
                });
                return _;
              })(c)
            });
          }
          return ret;
        })() }
      ]}
    ], true);

    // toolbar点击
    toolbar.attachEvent('onClick', (id) => {
      switch(id) {
        case 'add':
          // 添加数据
          // 判断表单数据
          if (!form.validate()) {
            return toastr.warning(LANG['list']['add']['warning'], LANG_T['warning']);
          };
          // 解析数据
          let data = form.getValues();
          win.progressOn();
          // 获取编码器
          data['encoder'] = data[`encoder_${data['type']}`] ? data[`encoder_${data['type']}`] : '';
          // 获取分类
          data['category'] = this.category['sidebar'].getActiveItem() || 'default';
          const ret = antSword['ipcRenderer'].sendSync('shell-add', data);
          // 更新UI
          win.progressOff();
          if (ret instanceof Object) {
            win.close();
            toastr.success(LANG['list']['add']['success'], LANG_T['success']);
            this.loadData({
              category: data['category']
            });
          }else{
            toastr.error(LANG['list']['add']['error'](ret.toString()), LANG_T['error']);
          }
          break;
        case 'clear':
          // 清空表单
          form.clear();
          break;
      }
    });
  }

  // 编辑数据
  editData(sid) {
    // 获取数据
    // const data = antSword['ipcRenderer'].sendSync('shell-find', {
    //   _id: sid
    // })[0];
    const data = antSword['ipcRenderer'].sendSync('shell-findOne', sid);

    // 初始化窗口
    const win = this.createWin({
      title: LANG['list']['edit']['title'](data['url']),
      width: 450,
      height: 350
    });
    win.setModal(true);
    win.denyResize();
    // 工具栏
    const toolbar = win.attachToolbar();
    toolbar.loadStruct([{
      id: 'save',
      type: 'button',
      icon: 'save',
      text: LANG['list']['edit']['toolbar']['save']
    }, {
      type: 'separator'
    }, {
      id: 'clear',
      type: 'button',
      icon: 'remove',
      text: LANG['list']['edit']['toolbar']['clear']
    }]);
    // 表单对象
    const form = win.attachForm([
      { type: 'settings', position: 'label-left', labelWidth: 80, inputWidth: 250 },
      { type: 'block', inputWidth: 'auto', offsetTop: 12, list: [
        { type: 'input', label: LANG['list']['edit']['form']['url'], name: 'url', required: true, value: data['url'] },
        { type: 'password', label: LANG['list']['edit']['form']['pwd'], name: 'pwd', required: true, value: data['pwd'] },
        { type: 'combo', label: LANG['list']['edit']['form']['encode'], readonly: true, name: 'encode', options: (() => {
          let ret = [];
          ENCODES.map((_) => {
            ret.push({
              text: _,
              value: _,
              selected: _ === data['encode']
            });
          });
          return ret;
        })() },
        { type: 'combo', label: LANG['list']['edit']['form']['type'], name: 'type', readonly: true, options: (() => {

          let ret = [];
          for (let c in antSword['core']) {
            let encoders = antSword['core'][c].prototype.encoders;
            ret.push({
              text: c.toUpperCase(),
              value: c,
              selected: data['type'] === c,
              list: ((c) => {
                let _ = [
                  { type: 'settings', position: 'label-right', offsetLeft: 60, labelWidth: 100 },
                  { type: 'label', label: LANG['list']['add']['form']['encoder'] },
                  { type: 'radio', name: `encoder_${c}`, value: 'default', label: 'default',
                    checked: (
                      data['encoder'] === 'default') ||
                      (c !== data['type']) ||
                      (!encoders.indexOf(data['encoder']))
                  }
                ];
                encoders.map((e) => {
                  _.push({
                    type: 'radio',
                    name: `encoder_${c}`,
                    value: e,
                    label: e,
                    checked: data['encoder'] === e
                  })
                });
                return _;
              })(c)
            });
          }
          return ret;
        })() }
      ]}
    ], true);

    // toolbar点击
    toolbar.attachEvent('onClick', (id) => {
      switch(id) {
        case 'save':
          // 添加数据
          // 判断表单数据
          if (!form.validate()) {
            return toastr.warning(LANG['list']['edit']['warning'], LANG_T['warning']);
          };
          // 解析数据
          let data = form.getValues();
          data['_id'] = sid;
          win.progressOn();
          // 获取编码器
          data['encoder'] = data[`encoder_${data['type']}`] ? data[`encoder_${data['type']}`] : '';
          // 获取分类
          data['category'] = this.category['sidebar'].getActiveItem() || 'default';
          const ret = antSword['ipcRenderer'].sendSync('shell-edit', data);
          // 更新UI
          win.progressOff();
          if (typeof(ret) === 'number') {
            win.close();
            toastr.success(LANG['list']['edit']['success'], LANG_T['success']);
            this.loadData({
              category: data['category']
            });
          }else{
            toastr.error(LANG['list']['edit']['error'](ret.toString()), LANG_T['error']);
          }
          break;
        case 'clear':
          // 清空表单
          form.clear();
          break;
      }
    });
  }

  // 删除数据
  delData(ids) {
    layer.confirm(
    LANG['list']['del']['confirm'](ids.length), {
      icon: 2, shift: 6,
      title: `<i class="fa fa-trash"></i> ${LANG['list']['del']['title']}`
    }, (_) => {
      layer.close(_);
      const ret = antSword['ipcRenderer'].sendSync('shell-del', ids);
      if (typeof(ret) === 'number') {
        toastr.success(LANG['list']['del']['success'](ret), LANG_T['success']);
        // 更新UI
        this.loadData({
          category: this.category['sidebar'].getActiveItem() || 'default'
        });
      }else{
        toastr.error(LANG['list']['del']['error'](ret.toString()), LANG_T['error']);
      }
    });
  }

  // 搜索数据
  searchData() {
    // 判断当前tab是否在主页
    if (antSword['tabbar'].getActiveTab() !== 'tab_shellmanager') { this.cell.setActive() };
    const category = this.category['sidebar'].getActiveItem() || 'default';
    // 初始化窗口
    const win = this.createWin({
      title: '搜索数据 //' + category,
      width: 450,
      height: 350
    });
  }

  // 加载数据
  loadData(arg) {
    // 获取当前分类
    // const _category = this.category.sidebar.getActiveItem() || 'default';
    // 根据分类查询数据
    const ret = antSword['ipcRenderer'].sendSync('shell-find', arg || {});
    let category = {};
    // 解析数据
    let data = [];
    ret.map((_) => {
      category[_['category'] || 'default'] = category[_['category'] || 'default'] || 0;
      category[_['category'] || 'default'] ++;
      if ((arg instanceof Object) && arg['category'] && arg['category'] !== _['category']) {
        return;
      };
      if (!arg && _['category'] !== 'default') {
        return;
      };
      data.push({
        id: _['_id'],
        // pwd: _['pwd'],
        // type: _['type'],
        // encode: _['encode'] || 'utf8',
        // encoder: _['encoder'] || 'default',
        data: [
          _['url'],
          _['ip'],
          _['addr'],
          new Date(_['ctime']).format('yyyy/MM/dd hh:mm:ss'),
          new Date(_['utime']).format('yyyy/MM/dd hh:mm:ss')
        ]
      });
    });
    // 刷新UI::左侧数据
    this.list.grid.clearAll();
    this.list.grid.parse({
      'rows': data
    }, 'json');
    // 刷新UI::右侧目录
    if ((arg instanceof Object) && arg['category'] && !category[arg['category']]) {
      category[arg['category']] = 0;
    };
    if (typeof(category['default']) === 'object') {
      category['default'] = 0;
    };
    // 1. 判断目录是否存在？更新目录bubble：添加目录
    for (let c in category) {
      // 添加category
      if (!this.category['sidebar'].items(c)) {
        this.category['sidebar'].addItem({
          id: c,
          bubble: category[c],
          // selected: true,
          text: `<i class="fa fa-folder-o"></i> ${c}`
        });
      }else{
        this.category['sidebar'].items(c).setBubble(category[c]);
      }
    }
    // 2. 选中默认分类
    this.category['sidebar'].items((arg || {})['category'] || 'default').setActive();
    // 3. 更新标题
    this.list.updateTitle(data.length);
    this.category.updateTitle();
  }

  // 创建窗口
  createWin(opts) {
    let _id = String(Math.random()).substr(5, 10);
    // 默认配置
    let opt = $.extend({
      title: 'Window:' + _id,
      width: 550,
      height: 450
    }, opts);

    // 创建窗口
    let _win = this.win.createWindow(_id, 0, 0, opt['width'], opt['height']);
    _win.setText(opt['title']);
    _win.centerOnScreen();
    _win.button('minmax').show();
    _win.button('minmax').enable();

    // 返回窗口对象
    return _win;
  }

}

export default ShellManager;
