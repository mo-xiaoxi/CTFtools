/**
 * Shell数据库管理模块
 * 更新：2016/04/28
 * 作者：蚁逅 <https://github.com/antoor>
 */

'use strict';

const fs = require('fs'),
  dns = require('dns'),
  path = require('path'),
  CONF = require('./config'),
  logger = require('log4js').getLogger('Database'),
  Datastore = require('nedb'),
  qqwry = require("geoips").info();

class Database {

  /**
   * 初始化数据库
   * @param  {electron} electron electron对象
   * @return {[type]}          [description]
   */
  constructor(electron) {
    this.cursor = new Datastore({
      filename: CONF.dataPath,
      autoload: true
    });
    // 监听事件
    electron.ipcMain
      .on('shell-add', this.addShell.bind(this))
      .on('shell-del', this.delShell.bind(this))
      .on('shell-edit', this.editShell.bind(this))
      .on('shell-move', this.moveShell.bind(this))
      .on('shell-find', this.findShell.bind(this))
      .on('shell-clear', this.clearShell.bind(this))
      .on('shell-findOne', this.findOneShell.bind(this))
      .on('shell-addDataConf', this.addDataConf.bind(this))
      .on('shell-delDataConf', this.delDataConf.bind(this))
      .on('shell-getDataConf', this.getDataConf.bind(this))
      .on('shell-renameCategory', this.renameShellCategory.bind(this));
  }

  /**
   * 查询shell数据
   * @param  {Object} event ipcMain对象
   * @param  {Object} opts  查询配置
   * @return {[type]}       [description]
   */
  findShell(event, opts = {}) {
    logger.debug('findShell', opts);
    this.cursor
      .find(opts)
      .sort({
        utime: -1
      })
      .exec((err, ret) => {
        event.returnValue = ret || [];
      });
  }

  /**
   * 查询单一shell数据
   * @param  {Object} event ipcMain对象
   * @param  {String} opts  shell id
   * @return {[type]}       [description]
   */
  findOneShell(event, opts) {
    logger.debug('findOneShell', opts);
    this.cursor.findOne({
      _id: opts
    }, (err, ret) => {
      event.returnValue = err || ret;
    });
  }

  /**
   * 添加shell数据
   * @param {Object} event ipcMain对象
   * @param {Object} opts  数据（url,category,pwd,type,encode,encoder
   */
  addShell(event, opts) {
    logger.info('addShell', opts);
    // 获取目标IP以及地理位置
    // 1. 获取域名
    let parse = opts['url'].match(/(\w+):\/\/([\w\.\-]+)[:]?([\d]*)([\s\S]*)/i);
    if (!parse || parse.length < 3) { return event.returnValue = 'Unable to resolve domain name from URL' };
    // 2. 获取域名IP
    dns.lookup(parse[2], (err, ip) => {
      if (err) { return event.returnValue = err.toString() };
      // 3. 查询IP对应物理位置
      const addr = qqwry.searchIP(ip);
      // 插入数据库
      this.cursor.insert({
        category: opts['category'] || 'default',
        url: opts['url'],
        pwd: opts['pwd'],
        type: opts['type'],
        ip: ip,
        addr: `${addr.Country} ${addr.Area}`,
        encode: opts['encode'],
        encoder: opts['encoder'],
        ctime: +new Date,
        utime: +new Date
      }, (err, ret) => {
        event.returnValue = err || ret;
      });
    });
  }

  /**
   * 编辑shell数据
   * @param  {Object} event ipcMain对象
   * @param  {Object} opts  数据（url,_id,pwd,type,encode,encoder
   * @return {[type]}       [description]
   */
  editShell(event, opts) {
    logger.warn('editShell', opts);
    // 获取目标IP以及地理位置
    // 1. 获取域名
    let parse = opts['url'].match(/(\w+):\/\/([\w\.\-]+)[:]?([\d]*)([\s\S]*)/i);
    if (!parse || parse.length < 3) { return event.returnValue = 'Unable to resolve domain name from URL' };
    // 2. 获取域名IP
    dns.lookup(parse[2], (err, ip) => {
      if (err) { return event.returnValue = err.toString() };
      // 3. 查询IP对应物理位置
      const addr = qqwry.searchIP(ip);
      // 更新数据库
      this.cursor.update({
        _id: opts['_id']
      }, {
        $set: {
          ip: ip,
          addr: `${addr.Country} ${addr.Area}`,
          url: opts['url'],
          pwd: opts['pwd'],
          type: opts['type'],
          encode: opts['encode'],
          encoder: opts['encoder'],
          utime: +new Date
        }
      }, (err, num) => {
        event.returnValue = err || num;
      })
    });
  }

  /**
   * 删除shell数据
   * @param  {Object} event ipcMain对象
   * @param  {Array}  opts  要删除的shell-id列表
   * @return {[type]}       [description]
   */
  delShell(event, opts) {
    logger.warn('delShell', opts);
    this.cursor.remove({
      _id: {
        $in: opts
      }
    }, {
      multi: true
    }, (err, num) => {
      event.returnValue = err || num;
    })
  }

  /**
   * 删除分类shell数据
   * @param  {Object} event ipcMain对象
   * @param  {String} opts  shell分类名
   * @return {[type]}       [description]
   */
  clearShell(event, opts) {
    logger.fatal('clearShell', opts);
    this.cursor.remove({
      category: opts
    }, {
      multi: true
    }, (err, num) => {
      event.returnValue = err || num;
    })
  }

  /**
   * 重命名shell分类
   * @param  {Object} event ipcMain对象
   * @param  {Object} opts  配置（oldName,newName
   * @return {[type]}       [description]
   */
  renameShellCategory(event, opts) {
    logger.warn('renameShellCategory', opts);
    this.cursor.update({
      category: opts['oldName']
    }, {
      $set: {
        category: opts['newName']
      }
    }, {
      multi: true
    }, (err, num) => {
      event.returnValue = err || num;
    })
  }

  /**
   * 移动shell数据分类
   * @param  {Object} event ipcMain对象
   * @param  {Object} opts  配置（ids,category
   * @return {[type]}       [description]
   */
  moveShell(event, opts) {
    logger.info('moveShell', opts);
    this.cursor.update({
      _id: {
        $in: opts['ids'] || []
      }
    }, {
      $set: {
        category: opts['category'] || 'default',
        utime: +new Date
      }
    }, {
      multi: true
    }, (err, num) => {
      event.returnValue = err || num;
    })
  }

  /**
   * 添加数据库配置
   * @param {Object} event ipcMain对象
   * @param {Object} opts  配置（_id,data
   */
  addDataConf(event, opts) {
    logger.info('addDataConf', opts);
    // 1. 获取原配置列表
    this.cursor.findOne({
      _id: opts['_id']
    }, (err, ret) => {
      let confs = ret['database'] || {};
      // 随机Id（顺序增长
      const random_id = parseInt(+new Date + Math.random() * 1000).toString(16);
      // 添加到配置
      confs[random_id] = opts['data'];
      // 更新数据库
      this.cursor.update({
        _id: opts['_id']
      }, {
        $set: {
          database: confs,
          utime: +new Date
        }
      }, (_err, _ret) => {
        event.returnValue = random_id;
      });
    });
  }

  /**
   * 删除数据库配置
   * @param  {Object} event ipcMain对象
   * @param  {Object} opts  配置（_id,id
   * @return {[type]}       [description]
   */
  delDataConf(event, opts) {
    logger.info('delDataConf', opts);
    // 1. 获取原配置
    this.cursor.findOne({
      _id: opts['_id']
    }, (err, ret) => {
      let confs = ret['database'] || {};
      // 2. 删除配置
      delete confs[opts['id']];
      // 3. 更新数据库
      this.cursor.update({
        _id: opts['_id']
      }, {
        $set: {
          database: confs,
          utime: +new Date
        }
      }, (_err, _ret) => {
        event.returnValue = _err || _ret;
      });
    })
  }

  /**
   * 获取单个数据库配置
   * @param  {Object} event ipcMain对象
   * @param  {Object} opts  配置（_id,id
   * @return {[type]}       [description]
   */
  getDataConf(event, opts) {
    logger.info('getDatConf', opts);
    this.cursor.findOne({
      _id: opts['_id']
    }, (err, ret) => {
      const confs = ret['database'] || {};
      event.returnValue = err || confs[opts['id']];
    });
  }
}

module.exports = Database;
