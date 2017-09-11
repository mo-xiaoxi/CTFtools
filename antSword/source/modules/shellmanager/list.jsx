/**
 * 左侧shell数据管理模块
 */

import Terminal from '../terminal/';
import Database from '../database/';
import FileManager from '../filemanager/';

const LANG_T = antSword['language']['toastr'];
const LANG = antSword['language']['shellmanager'];

class List {

  constructor(cell, manager) {
    // cell.hideHeader();
    // cell.setText(`<i class="fa fa-list-ul"></i> ${LANG['list']['title']}`);
    // 删除折叠按钮
    document.getElementsByClassName('dhxlayout_arrow dhxlayout_arrow_va')[0].remove();

    // 初始化工具栏
    // const toolbar = cell.attachToolbar();
    // toolbar.loadStruct([
    //   { id: 'add', type: 'button', text: `<i class="fa fa-plus-circle"></i> ${LANG.list.toolbar['add']}` },
    //   { type: 'separator' },
    //   { id: 'edit', type: 'button', text: `<i class="fa fa-edit"></i> ${LANG.list.toolbar['edit']}` }
    // ]);

    // 初始化数据表格
    const grid = cell.attachGrid();

    grid.setHeader(`
      ${LANG['list']['grid']['url']},
      ${LANG['list']['grid']['ip']},
      ${LANG['list']['grid']['addr']},
      ${LANG['list']['grid']['ctime']},
      ${LANG['list']['grid']['utime']}
    `);
    grid.setColTypes("ro,ro,ro,ro,ro");
    grid.setColSorting('str,str,str,str,str');
    grid.setInitWidths("200,120,*,140,140");
    grid.setColAlign("left,left,left,center,center");
    grid.enableMultiselect(true);

    // 右键
    grid.attachEvent('onRightClick', (id, lid, event) => {
      // 获取选中ID列表
      let ids = (grid.getSelectedId() || '').split(',');

      // 如果没有选中？则选中右键对应选项
      if (ids.length === 1) {
        grid.selectRowById(id);
        ids = [id];
      }

      // 获取选中的单条数据
      let info = {};
      if (id && ids.length === 1) {
        info = antSword['ipcRenderer'].sendSync('shell-findOne', id);
        // info = {
        //   id: id,
        //   ip: grid.getRowAttribute(id, 'data')[1],
        //   url: grid.getRowAttribute(id, 'data')[0],
        //   pwd: grid.getRowAttribute(id, 'pwd'),
        //   type: grid.getRowAttribute(id, 'type'),
        //   encode: grid.getRowAttribute(id, 'encode') || 'utf-8',
        //   encoder: grid.getRowAttribute(id, 'encoder') || 'default'
        // }
      };

      bmenu([
        { text: LANG['contextmenu']['terminal'], icon: 'fa fa-terminal', disabled: !id || ids.length !== 1, action: () => {
          new Terminal(info);
        } },
        { text: LANG['contextmenu']['filemanager'], icon: 'fa fa-folder-o', disabled: !id || ids.length !== 1, action: () => {
          new FileManager(info);
        } },
        { text: LANG['contextmenu']['database'], icon: 'fa fa-database', disabled: !id || ids.length !== 1, action: () => {
          new Database(info);
        } },
        { divider: true },
        { text: LANG['contextmenu']['plugin'], icon: 'fa fa-puzzle-piece', disabled: !id || ids.length !== 1 || true, subMenu: [] },
        { text: LANG['contextmenu']['pluginCenter'], icon: 'fa fa-cart-arrow-down',  action: antSword['menubar'].run.bind(antSword['menubar'], 'plugin') },
        { divider: true },
        { text: LANG['contextmenu']['add'], icon: 'fa fa-plus-circle', action: manager.addData.bind(manager) },
        { text: LANG['contextmenu']['edit'], icon: 'fa fa-edit', disabled: !id || ids.length !== 1, action: () => {
          manager.editData(id);
        } },
        { text: LANG['contextmenu']['delete'], icon: 'fa fa-remove', disabled: !id, action: () => {
          manager.delData(ids);
        } },
        { divider: true },
        { text: LANG['contextmenu']['move'], icon: 'fa fa-share-square', disabled: !id, subMenu: (() => {
          const items = manager.category.sidebar.getAllItems();
          const category = manager.category.sidebar.getActiveItem();
          let ret = [];
          items.map((_) => {
            ret.push({
              text: _ === 'default' ? LANG['category']['default'] : _,
              icon: 'fa fa-folder-o',
              disabled: category === _,
              action: ((c) => {
                return () => {
                  const ret = antSword['ipcRenderer'].sendSync('shell-move', {
                    ids: ids,
                    category: c
                  });
                  if (typeof(ret) === 'number') {
                    toastr.success(LANG['list']['move']['success'](ret), LANG_T['success']);
                    manager.loadData();
                    manager.category.sidebar.callEvent('onSelect', [c]);
                  }else{
                    toastr.error(LANG['list']['move']['error'](ret), LANG_T['error']);
                  }
                }
              })(_)
            });
          });
          return ret;
        })() },
        { text: LANG['contextmenu']['search'], icon: 'fa fa-search', action: manager.searchData.bind(manager), disabled: true },
        { divider: true },
        { text: LANG['contextmenu']['clearCache'], icon: 'fa fa-trash-o', disabled: !id, action: () => {
          manager.clearCache(id);
        } },
        { text: LANG['contextmenu']['clearAllCache'], icon: 'fa fa-trash', action: manager.clearAllCache.bind(manager) }
      ], event);

      return true;
    });

    // 双击
    grid.attachEvent('onRowDblClicked', (id) => {
      const info = antSword['ipcRenderer'].sendSync('shell-findOne', id);
      new FileManager(info);
    });

    // 隐藏右键菜单
    grid.attachEvent('onRowSelect', bmenu.hide);
    $('.objbox').on('click', bmenu.hide);
    $('.objbox').on('contextmenu', (e) => {
      (e.target.nodeName === 'DIV' && grid.callEvent instanceof Function) ? grid.callEvent('onRightClick', [grid.getSelectedRowId(), '', e]) : 0;
    });

    grid.init();

    // 变量赋值
    this.grid = grid;
    this.cell = cell;
    this.toolbar = toolbar;
  }

  // 更新标题
  updateTitle(num) {
    this.cell.setText(`<i class="fa fa-list-ul"></i> ${LANG['list']['title']} (${num})`);
  }
}

export default List;
