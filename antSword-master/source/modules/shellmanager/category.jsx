/**
 * 右侧目录管理模块
 */

'use strict';

const LANG_T = antSword['language']['toastr'];
const LANG = antSword['language']['shellmanager'];

class Category {

  constructor(cell, manager) {
    // cell.setText(`<i class="fa fa-folder"></i> ${LANG['category']['title']}`);
    cell.setWidth(222);
    cell.fixSize(1, 0);

    // 初始化toolbar
    const toolbar = cell.attachToolbar();
    toolbar.loadStruct([
      { id: 'add', type: 'button', text: `<i class="fa fa-plus-circle"></i> ${LANG['category']['toolbar']['add']}` },
      { type: 'separator' },
      { id: 'rename', type: 'button', text: `<i class="fa fa-font"></i> ${LANG['category']['toolbar']['rename']}`, disabled: true },
      { type: 'separator' },
      { id: 'del', type: 'button', text: `<i class="fa fa-trash"></i> ${LANG['category']['toolbar']['del']}`, disabled: true }
    ]);
    // toolbar点击
    toolbar.attachEvent('onClick', (id) => {
      switch(id) {
        case 'add':
          // 添加分类
          layer.prompt({
            title: `<i class="fa fa-plus-circle"></i> ${LANG['category']['add']['title']}`,
            value: new Date().format('yyyyMMdd')
          }, (value, index, ele) => {
            layer.close(index);
            sidebar.callEvent('onSelect', [value]);
          });
          break;
        case 'rename':
          // 重命名分类
          const _category = sidebar.getActiveItem();
          layer.prompt({
            title: `<i class="fa fa-font"></i> ${LANG['category']['rename']['title']}`,
            value: _category
          }, (value, index, ele) => {
            // 禁止的分类名
            if (value === 'default') {
              return toastr.warning(LANG['category']['rename']['disable'], LANG_T['warning']);
            };
            // 判断分类是否存在
            if (sidebar.items(value)) {
              return toastr.warning(LANG['category']['rename']['exists'], LANG_T['warning']);
            };
            layer.close(index);
            // 更新数据库
            const ret = antSword['ipcRenderer'].sendSync('shell-renameCategory', {
              oldName: _category,
              newName: value
            });
            if (typeof ret === 'number') {
              // 更新成功
              toastr.success(LANG['category']['rename']['success'], LANG_T['success']);
              // 删除旧分类
              sidebar.items(_category).remove();
              // 添加新分类
              sidebar.addItem({
                id: value,
                bubble: ret,
                text: `<i class="fa fa-folder-o"></i> ${value}`
              });
              // 跳转分类
              setTimeout(() => {
                sidebar.items(value).setActive();
              }, 233);
            }else{
              toastr.error(LANG['category']['rename']['error'], LANG_T['error']);
            }
          });
          break;
        case 'del':
          // 删除分类
          const category = sidebar.getActiveItem();
          layer.confirm(
            LANG['category']['del']['confirm'], {
              icon: 2, shift: 6,
              // skin: 'layui-layer-molv',
              title: `<i class="fa fa-trash"></i> ${LANG['category']['del']['title']}`,
            }, (_) => {
              layer.close(_);
              // 1. 删除分类数据
              const ret = antSword['ipcRenderer'].sendSync('shell-clear', category);
              if (typeof(ret) === 'number') {
                toastr.success(LANG['category']['del']['success'](category), LANG_T['success']);
                // 2. 跳转到默认分类
                sidebar.callEvent('onSelect', ['default']);
                // 3. 删除侧边栏
                sidebar.items(category).remove();
                setTimeout(this.updateTitle.bind(this), 100);
              }else{
                return toastr.error(LANG['category']['del']['error'](category, ret.toString()), LANG_T['error']);
              }
            });
          break;
      }
    });

    // 初始化sidebar
    const sidebar = cell.attachSidebar({
      template: 'text',
      width: 222
    });
    // 默认分类
    sidebar.addItem({
      id: 'default',
      bubble: 0,
      selected: true,
      text: `<i class="fa fa-folder-o"></i> ${LANG['category']['default']}</i>`
    });
    // sidebar点击事件
    sidebar.attachEvent('onSelect', (id) => {
      // 更改删除按钮状态
      toolbar[(id === 'default') ? 'disableItem' : 'enableItem']('del');
      toolbar[(id === 'default') ? 'disableItem' : 'enableItem']('rename');
      manager.loadData({
        category: id
      });
    });

    this.cell = cell;
    this.toolbar = toolbar;
    this.sidebar = sidebar;
  }

  // 更新标题
  updateTitle() {
    const num = this.sidebar.getAllItems().length;
    this.cell.setText(`<i class="fa fa-folder"></i> ${LANG['category']['title']} (${num})`);
  }

}

export default Category;
