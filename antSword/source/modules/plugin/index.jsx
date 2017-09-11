//
// 插件中心
//

const LANG = antSword['language']['plugin'];
const LANG_T = antSword['language']['toastr'];

class Plugin {

  constructor() {
    antSword['menubar'].reg('plugin', this.open.bind(this));
    this.homepage = 'http://u.uyu.us/';
  }

  open() {
    const tabbar = antSword['tabbar'];
    // 判断是否已经打开
    if (tabbar.tabs('tab_plugin')) {
      return tabbar.tabs('tab_plugin').setActive();
    };
    tabbar.addTab(
      'tab_plugin',
      '<i class="fa fa-cart-arrow-down"></i>',
      null, null, true, true
    );
    const cell = tabbar.tabs('tab_plugin');
    //
    // @创建浏览器工具栏:后退、前进、刷新、主页、停止
    //
    const toolbar = cell.attachToolbar();
    toolbar.loadStruct([
      { id: 'back', type: 'button', text: '', icon: 'chevron-left' },
      { id: 'forward', type: 'button', text: '', icon: 'chevron-right' },
      { id: 'refresh', type: 'button', text: '', icon: 'refresh' },
      { id: 'home', type: 'button', text: '', icon: 'home' },
      { id: 'stop', type: 'button', text: '', icon: 'remove' }
    ]);

    // 开始加载web
    cell.progressOn();
    const frame = cell.attachURI(this.homepage);
    frame.addEventListener('did-start-loading', cell.progressOn.bind(cell));
    frame.addEventListener('did-finish-load', cell.progressOff.bind(cell));
    frame.addEventListener('did-fail-load', (err) => {
      cell.progressOff();
      // cell.close();
      let err_msg = `Code: ${err['errorCode']}`;
      err_msg += err['errorDescription'] ? `<br/>Desc: ${err['errorDescription']}` : '';
      return toastr.error(LANG['error'](err_msg), LANG_T['error']);
    });

    // 工具栏点击事件
    toolbar.attachEvent('onClick', (id) => {
      switch(id) {
        case 'back':
          frame.goBack();
          break;
        case 'forward':
          frame.goForward();
          break;
        case 'refresh':
          frame.reloadIgnoringCache();
          break;
        case 'home':
          frame.goToIndex(0);
          break;
        case 'stop':
          frame.stop();
          break;
      }
    });

  }

}

export default Plugin;
