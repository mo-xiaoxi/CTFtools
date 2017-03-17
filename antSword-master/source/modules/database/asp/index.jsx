//
// 数据库驱动::ASP
// 支持数据库:access,sqlserver,mysql
//

class ASP {

  constructor(opt) {
    this.opt = opt;
    this.core = this.opt.core;
    this.manager = this.opt.super;
    //
    // * 数据库驱动列表
    //
    this.conns = {
      'dsn': 'Dsn=DsnName;',
      'mysql': 'Driver={MySQL};Server=localhost;database=mysql;UID=root;PWD=',
      'access': 'Driver={Microsoft Access Driver(*.mdb)};DBQ=c:\\test.mdb',
      'sqlserver': 'Driver={Sql Server};Server=(local);Database=master;Uid=sa;Pwd=',
      'sqloledb_1': 'Provider=SQLOLEDB.1;User ID=sa;Password=;Initial Catalog=master;Data Source=(local);',
      'sqloledb_1_sspi': 'Provider=SQLOLEDB.1;Initial Catalog=master;Data Source=(local);Integrated Security=SSPI;',
      'oracle': 'Provider=OraOLEDB.Oracle;Data Source=test;User Id=sys;Password=;Persist Security Info=True;',
      'microsoft_jet_oledb_4_0': 'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\test.mdb'
    };
    // 1. 初始化TREE UI
    this.tree = this.manager.list.layout.attachTree();
    // 2. 加载数据库配置
    this.parse();
    // 3. tree单击::设置当前配置&&激活按钮
    this.tree.attachEvent('onClick', (id) => {
      // 更改按钮状态
      id.startsWith('conn::') ? this.enableToolbar() : this.disableToolbar();
      // 设置当前配置
      const tmp = id.split('::');
      const arr = tmp[1].split(':');
      // 设置当前数据库
      this.dbconf = antSword['ipcRenderer'].sendSync('shell-getDataConf', {
        _id: this.manager.opt['_id'],
        id: arr[0]
      });
      if (arr.length > 1) {
        this.dbconf['database'] = new Buffer(arr[1], 'base64').toString();
        // 更新SQL编辑器
        this.enableEditor();
        // manager.query.update(this.currentConf);
      }else{
        this.disableEditor();
      }
    });
    // 4. tree双击::加载库/表/字段
    this.tree.attachEvent('onDblClick', (id) => {
      const arr = id.split('::');
      if (arr.length < 2) { throw new Error('ID ERR: ' + id) };
      switch(arr[0]) {
        // 获取数据库列表
        case 'conn':
          this.getDatabases(arr[1]);
          break;
        // 获取数据库表名
        case 'database':
          let _db = arr[1].split(':');
          this.getTables(
            _db[0],
            new Buffer(_db[1], 'base64').toString()
          );
          break;
        // 获取表名字段
        case 'table':
          let _tb = arr[1].split(':');
          this.getColumns(
            _tb[0],
            new Buffer(_tb[1], 'base64').toString(),
            new Buffer(_tb[2], 'base64').toString()
          );
          break;
        // 生成查询SQL语句
        case 'column':
          let _co = arr[1].split(':');
          const table = new Buffer(_co[2], 'base64').toString();
          const column = new Buffer(_co[3], 'base64').toString();

          const sql = `SELECT TOP 20 [${column}] FROM [${table}] ORDER BY 1 DESC;`;
          this.manager.query.editor.session.setValue(sql);
          break;
      }
    });
    // 5. tree右键::功能菜单
    this.tree.attachEvent('onRightClick', (id, event) => {
      if (!id.startsWith('conn::')) { return };
      this.tree.selectItem(id);
      this.tree.callEvent('onClick', [id]);
      bmenu([
        {
          text: '添加配置',
          icon: 'fa fa-plus-circle',
          action: this.addConf.bind(this)
        }, {
          divider: true
        }, {
          text: '删除配置',
          icon: 'fa fa-remove',
          action: this.delConf.bind(this)
        }
      ], event);
    });
  }

  // 加载配置列表
  parse() {
    // 获取数据
    const info = antSword['ipcRenderer'].sendSync('shell-findOne', this.manager.opt['_id']);
    const conf = info['database'] || {};
    // 刷新UI
    // 1.清空数据
    this.tree.deleteChildItems(0);
    // 2.添加数据
    let items = [];
    for (let _ in conf) {
      items.push({
        id: `conn::${_}`,
        // text: `${conf[_]['type']}:\/\/${conf[_]['user']}@${conf[_]['host']}`,
        text: conf[_]['type'].toUpperCase(),
        im0: this.manager.list.imgs[0],
        im1: this.manager.list.imgs[0],
        im2: this.manager.list.imgs[0]
      });
    }
    // 3.刷新UI
    this.tree.parse({
      id: 0,
      item: items
    }, 'json');
    // 禁用按钮
    this.disableToolbar();
    this.disableEditor();
  }

  // 添加配置
  addConf() {
    const hash = (+new Date * Math.random()).toString(16).substr(2, 8);
    // 创建窗口
    const win = this.manager.win.createWindow(hash, 0, 0, 450, 300);
    win.setText('添加配置');
    win.centerOnScreen();
    win.button('minmax').hide();
    win.setModal(true);
    win.denyResize();
    // 工具栏
    const toolbar = win.attachToolbar();
    toolbar.loadStruct([{
      id: 'add',
      type: 'button',
      icon: 'plus-circle',
      text: '添加'
    }, {
      type: 'separator'
    }, {
      id: 'clear',
      type: 'button',
      icon: 'remove',
      text: '清空'
    }]);

    // form
    const form = win.attachForm([
      { type: 'settings', position: 'label-left', labelWidth: 80, inputWidth: 280 },
      { type: 'block', inputWidth: 'auto', offsetTop: 12, list: [
        { type: 'combo', label: '数据库类型', readonly: true, name: 'type', options: (() => {
          let ret = [];
          for (let _ in this.conns) {
            ret.push({
              text: _.toUpperCase(),
              value: _
            });
          }
          return ret;
        })() },
        { type: 'input', label: '连接字符串', name: 'conn', required: true, value: 'Dsn=DsnName;', rows: 9 }
      ]}
    ], true);

    form.attachEvent('onChange', (_, id) => {
      if (_ !== 'type') { return };
      form.setFormData({
        conn: this.conns[id]
      });
    });

    // 工具栏点击事件
    toolbar.attachEvent('onClick', (id) => {
      switch(id) {
        case 'clear':
          form.clear();
          break;
        case 'add':
          if (!form.validate()) {
            return '填写完整！';
            // return toastr.warning(LANG['list']['add']['warning'], LANG_T['warning']);
          };
          // 解析数据
          let data = form.getValues();
          // 验证是否连接成功(获取数据库列表)
          const id = antSword['ipcRenderer'].sendSync('shell-addDataConf', {
            _id: this.manager.opt['_id'],
            data: data
          });
          win.close();
          toastr.success('添加配置成功!');
          this.tree.insertNewItem(0,
            `conn::${id}`,
            // `${data['type']}:\/\/${data['user']}@${data['host']}`,
            data['type'].toUpperCase(),
            null,
            this.manager.list.imgs[0],
            this.manager.list.imgs[0],
            this.manager.list.imgs[0]
          );
          break;
      }
    });
  }

  // 删除配置
  delConf() {
    const id = this.tree.getSelected().split('::')[1];
    layer.confirm('确定删除此配置吗？', {
      icon: 2, shift: 6,
      title: '删除配置'
    }, (_) => {
      layer.close(_);
      const ret = antSword['ipcRenderer'].sendSync('shell-delDataConf', {
        _id: this.manager.opt['_id'],
        id: id
      });
      if (ret === 1) {
        toastr.success('删除配置成功！');
        this.tree.deleteItem(`conn::${id}`);
        // 禁用按钮
        this.disableToolbar();
        this.disableEditor();
        // ['edit', 'del'].map(this.toolbar::this.toolbar.disableItem);
        // this.parse();
      }else{
        toastr.error('删除配置失败！<br/>' + ret);
      }
    });
  }

  // 获取数据库列表
  getDatabases(id) {
    this.manager.list.layout.progressOn();
    // 获取配置
    const conf = antSword['ipcRenderer'].sendSync('shell-getDataConf', {
      _id: this.manager.opt['_id'],
      id: id
    });

    this.core.request(
      this.core[`database_${conf['type']}`].show_databases(
      {
        conn: conf['conn'],
        dbname: ['access', 'microsoft_jet_oledb_4_0'].indexOf(conf['type']) > -1 ? conf['conn'].match(/[\w]+.mdb$/) : 'database'
      })
    ).then((res) => {
      let ret = res['text'];
      const arr = ret.split('\t');
      if (arr.length === 1 && ret === '') {
        toastr.warning('执行完毕，没有结果返回')
        return this.manager.list.layout.progressOff();
      };
      // 删除子节点
      this.tree.deleteChildItems(`conn::${id}`);
      // 添加子节点
      arr.map((_) => {
        if (!_) { return };
        const _db = new Buffer(_).toString('base64');
        this.tree.insertNewItem(
          `conn::${id}`,
          `database::${id}:${_db}`,
          _, null,
          this.manager.list.imgs[1],
          this.manager.list.imgs[1],
          this.manager.list.imgs[1]);
      });
      this.manager.list.layout.progressOff();
    }).catch((err) => {
      toastr.error('获取数据库列表失败！' + err['status'] || JSON.stringify(err), 'ERROR');
      this.manager.list.layout.progressOff();
    });
  }

  // 获取数据库表数据
  getTables(id, db) {
    this.manager.list.layout.progressOn();
    // 获取配置
    const conf = antSword['ipcRenderer'].sendSync('shell-getDataConf', {
      _id: this.manager.opt['_id'],
      id: id
    });

    this.core.request(
      this.core[`database_${conf['type']}`].show_tables(
      {
        conn: conf['conn'],
        dbname: db
      })
    ).then((res) => {
      let ret = res['text'];
      const arr = ret.split('\t');
      const _db = new Buffer(db).toString('base64');
      // 删除子节点
      this.tree.deleteChildItems(`database::${id}:${_db}`);
      // 添加子节点
      arr.map((_) => {
        if (!_) { return };
        const _table = new Buffer(_).toString('base64');
        this.tree.insertNewItem(
          `database::${id}:${_db}`,
          `table::${id}:${_db}:${_table}`,
          _,
          null,
          this.manager.list.imgs[2],
          this.manager.list.imgs[2],
          this.manager.list.imgs[2]
        );
      });
      this.manager.list.layout.progressOff();
    });
  }

  // 获取字段
  getColumns(id, db, table) {
    this.manager.list.layout.progressOn();
    // 获取配置
    const conf = antSword['ipcRenderer'].sendSync('shell-getDataConf', {
      _id: this.manager.opt['_id'],
      id: id
    });

    this.core.request(
      this.core[`database_${conf['type']}`].show_columns(
      {
        conn: conf['conn'],
        table: conf['type'] === 'oracle' ? `SELECT * FROM (SELECT A.*,ROWNUM N FROM ${table} A) WHERE N=1` : `SELECT TOP 1 * FROM ${table}`
      })
    ).then((res) => {
      let ret = res['text'];
      const arr = ret.split('\t');
      const _db = new Buffer(db).toString('base64');
      const _table = new Buffer(table).toString('base64');
      // 删除子节点
      this.tree.deleteChildItems(`table::${id}:${_db}:${_table}`);
      // 添加子节点
      arr.map((_) => {
        if (!_) { return };
        const _column = new Buffer(_.split(' ')[0]).toString('base64');
        this.tree.insertNewItem(
          `table::${id}:${_db}:${_table}`,
          `column::${id}:${_db}:${_table}:${_column}`,
          _, null,
          this.manager.list.imgs[3],
          this.manager.list.imgs[3],
          this.manager.list.imgs[3]
        );
      });
      // 更新编辑器SQL语句
      this.manager.query.editor.session.setValue(
        conf['type'] === 'oracle'
        ? `SELECT * FROM (SELECT A.*,ROWNUM N FROM ${table} A ORDER BY 1 DESC) WHERE N>0 AND N<=20`
        : `SELECT TOP 20 * FROM ${table} ORDER BY 1 DESC;`);
      this.manager.list.layout.progressOff();
    });
  }

  // 执行SQL
  execSQL(sql) {
    this.manager.query.layout.progressOn();

    this.core.request(
      this.core[`database_${this.dbconf['type']}`].query({
        conn: this.dbconf['conn'],
        sql: sql
      })
    ).then((res) => {
      let ret = res['text'];
      // 更新执行结果
      this.updateResult(ret);
      this.manager.query.layout.progressOff();
    }).catch((err) => {
      console.error(err);
    });
  }

  // 更新SQL执行结果
  updateResult(data) {
    // 1.分割数组
    const arr = data.split('\n');
    // let arr = [];
    // _arr.map((_) => {
    //   arr.push(antSword.noxss(_));
    // });
    // console.log(_arr, arr);
    // 2.判断数据
    if (arr.length < 2) {
      return console.log('数据不正确');
    };
    // 3.行头
    let header_arr = arr[0].split('\t|\t');
    if (header_arr.length === 1) {
      return toastr.warning('没有查询结果');
    };
    if (header_arr[header_arr.length - 1] === '\r') {
      header_arr.pop();
    };
    arr.shift();
    // 4.数据
    let data_arr = [];
    arr.map((_) => {
      let _data = _.split('\t|\t');
      data_arr.push(_data);
    });
    data_arr.pop();
    // 5.初始化表格
    const grid = this.manager.result.layout.attachGrid();
    grid.clearAll();
    grid.setHeader(header_arr.join(',').replace(/,$/, ''));
    grid.setColSorting(('str,'.repeat(header_arr.length)).replace(/,$/, ''));
    grid.setInitWidths('*');
    grid.setEditable(true);
    grid.init();
    // 添加数据
    let grid_data = [];
    for (let i = 0; i < data_arr.length; i ++) {
      grid_data.push({
        id: i + 1,
        data: data_arr[i]
      });
    }
    grid.parse({
      'rows': grid_data
    }, 'json');
    // 启用导出按钮
    // this.manager.result.toolbar[grid_data.length > 0 ? 'enableItem' : 'disableItem']('dump');
  }

  // 禁用toolbar按钮
  disableToolbar() {
    this.manager.list.toolbar.disableItem('del');
  }

  // 启用toolbar按钮
  enableToolbar() {
    this.manager.list.toolbar.enableItem('del');
  }

  // 禁用SQL编辑框
  disableEditor() {
    ['exec', 'clear'].map(
      this.manager.query.toolbar.disableItem.bind(this.manager.query.toolbar)
    );
    this.manager.query.editor.setReadOnly(true);
  }

  // 启用SQL编辑框
  enableEditor() {
    ['exec', 'clear'].map(
      this.manager.query.toolbar.enableItem.bind(this.manager.query.toolbar)
    );
    this.manager.query.editor.setReadOnly(false);
  }

}

module.exports = ASP;
