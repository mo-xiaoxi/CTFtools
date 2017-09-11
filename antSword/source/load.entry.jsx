/**
 * 中国蚁剑::前端加载模块
 * 开写: 2016/04/23
 * 更新: 2016/04/28
 * 作者: 蚁逅 <https://github.com/antoor>
 */

'use strict';

// 加载jQuery
window.$ = window.jQuery = require('../static/libs/jquery/jquery.js');

// 开始加载时间
let APP_START_TIME = +new Date;

$(document).ready(() => {
  /**
   * 时间格式化函数
   * @param  {String} format 格式化字符串，如yyyy/mm/dd hh:ii:ss
   * @return {String}        格式化完毕的字符串
   */
  Date.prototype.format = function(format) {
    let o = {
      "M+" : this.getMonth()+1,
      "d+" : this.getDate(),
      "h+" : this.getHours(),
      "m+" : this.getMinutes(),
      "s+" : this.getSeconds(),
      "q+" : Math.floor((this.getMonth()+3)/3),
      "S" : this.getMilliseconds()
    }
    if(/(y+)/.test(format)) {
      format=format.replace(RegExp.$1, (this.getFullYear()+"").substr(4- RegExp.$1.length))
    };
    for(let k in o) {
      if(new RegExp("("+ k +")").test(format)) {
        format = format.replace(RegExp.$1, RegExp.$1.length==1? o[k] : ("00"+ o[k]).substr((""+ o[k]).length));
      }
    }
    return format;
  }


  /**
   * 加载JS函数
   * @param  {String}   js js地址
   * @return {Promise}     返回Promise操作对象
   */
  function loadJS(js) {
    return new Promise((res, rej) => {
      let script = document.createElement('script');
      script.src = js;
      script.onload = res;
      document.head.appendChild(script);
    });
  }

  /**
   * 加载CSS函数
   * @param  {String}   css css地址
   * @return {Promise}      返回Promise操作对象
   */
  function loadCSS(css) {
    return new Promise((res, rej) => {
      let style = document.createElement('link');
      style.rel = 'stylesheet';
      style.href = css;
      style.onload = res;
      document.head.appendChild(style);
    });
  }

  // 开始加载css
  loadCSS(
    '../static/libs/bmenu/bmenu.css'
  ).then(() => {
    return loadCSS('../static/libs/toastr/toastr.min.css');
  }).then(() => {
    return loadCSS('../static/libs/layer/src/skin/layer.css');
  }).then(() => {
    return loadCSS('../static/libs/layer/src/skin/layer.ext.css');
  }).then(() => {
    return loadCSS('../static/libs/laydate/need/laydate.css');
  }).then(() => {
    return loadCSS('../static/libs/laydate/skins/default/laydate.css');
  }).then(() => {
    return loadCSS('../static/libs/terminal/css/jquery.terminal.css');
  }).then(() => {
    return loadCSS('../static/libs/font-awesome/css/font-awesome.min.css');
  }).then(() => {
    return loadCSS('../static/libs/dhtmlx/codebase/dhtmlx.css');
  }).then(() => {
    return loadCSS('../static/libs/dhtmlx/skins/mytheme/dhtmlx.css');
  }).then(() => {
    return loadCSS('../static/css/index.css');
  });

  // 加载js资源
  loadJS(
    '../static/libs/ace/ace.js'
  ).then(() => {
    return loadJS('../static/libs/ace/ext-language_tools.js');
  }).then(() => {
    return loadJS('../static/libs/bmenu/bmenu.js');
  }).then(() => {
    return loadJS('../static/libs/toastr/toastr.js');
  }).then(() => {
    return loadJS('../static/libs/layer/src/layer.js');
  }).then(() => {
    return loadJS('../static/libs/laydate/laydate.js');
  }).then(() => {
    return loadJS('../static/libs/terminal/js/jquery.terminal-min.js');
  }).then(() => {
    return loadJS('../static/libs/dhtmlx/codebase/dhtmlx.js');
  }).then(() => {
    /**
     * 配置layer弹出层
     * @param  {[type]} {extend: 'extend/layer.ext.js'} [description]
     * @return {[type]}          [description]
     */
    layer.config({extend: 'extend/layer.ext.js'});
    // 加载babel引擎
    require('babel/register')();
    // 加载程序入口
    require('./app.entry.jsx');
    // LOGO
    console.log(`%c
        _____     _   _____                 _
       |  _  |___| |_|   __|_ _ _ ___ ___ _| |
       |     |   |  _|__   | | | | . |  _| . |
       |__|__|_|_|_| |_____|_____|___|_| |___|%c

   ->| Ver: %c${antSword.package.version}%c
 -+=>| Git: %c${antSword.package.repository['url']}%c
   -*| End: %c${+new Date - APP_START_TIME}%c/ms`,
    'color: #F44336;', 'color: #9E9E9E;',
    'color: #4CAF50;', 'color: #9E9E9E;',
    'color: #2196F3;', 'color: #9E9E9E;',
    'color: #FF9800;', 'color: #9E9E9E;');
  });
});
