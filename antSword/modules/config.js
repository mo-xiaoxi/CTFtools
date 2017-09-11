/**
 * 中国蚁剑::后端配置模块
 * ? 用于进行一些通用的变量如初始化目录等设置
 * 开写：2016/04/26
 * 更新：2016/04/28
 * 作者：蚁逅 <https://github.com/antoor>
 */

'use strict';

const fs = require('fs'),
  path = require('path');

class Conf {

  constructor() {
    // 获取数据存储目录
    this.basePath = path.join(
      process.env.HOME || process.env.LOCALAPPPATH || process.cwd() || '.',
      '.antSword'
    );
    // 创建.antSword目录
    !fs.existsSync(this.basePath) ? fs.mkdirSync(this.basePath) : null;
  }

  /**
   * 获取数据存储路径
   * @return {String} file-path
   */
  get dataPath() {
    return path.join(this.basePath, 'shell.db');
  }

  /**
   * 获取缓存目录
   * @return {String} dir-path
   */
  get cachePath() {
    let _ = path.join(this.basePath, '/cache/');
    // 创建缓存目录
    !fs.existsSync(_) ? fs.mkdirSync(_) : null;
    return _;
  }

  /**
   * 获取package.json配置信息
   * @return {Object} [description]
   */
  get package() {
    return require('../package.json');
  }

}

module.exports = new Conf();
