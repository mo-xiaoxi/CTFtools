//
// 程序更新模块
//
/* 更新流程：
   -------
  1. 获取远程github上的package.json信息
  2. 和本地版本进行判断，不一致则提示更新
  3. 下载用户选择的更新源文件到临时目录`.antSword-{now}`
  4. 替换程序中的`resources/app.asar`文件
  5. 提示用户手动重启，关闭应用
*/

'use strict';

const os = require('os'),
  fs = require('fs'),
  path = require('path'),
  unzip = require('extract-zip'),
  crypto = require('crypto'),
  nugget = require('nugget'),
  logger = require('log4js').getLogger('Update'),
  superagent = require('superagent');

class Update {

  constructor(electron) {
    const ipcMain = electron.ipcMain;
    this.info = {};
    ipcMain
      .on('update-check', (event, arg) => {
        this.check(arg['local_ver'], (hasUpdate, retVal) => {
          logger.debug('check-result', hasUpdate, retVal);
          event.sender.send('update-check', {
            hasUpdate: hasUpdate,
            retVal: retVal
          });
        });
      })
      .on('update-download', (event, source) => {
        logger.debug('update-download', source);
        const info = this.info['update'];
        const downloadUrl = info['sources'][source];
        this.download(downloadUrl, info['md5'], (done, retVal) => {
          event.sender.send('update-download', {
            done: done,
            retVal: retVal
          });
        });
      });
  }

  // 检查是否有更新
  // 参数{localVer: 本地版本号, callback: 回调函数(是否有更新， 是？更新信息：错误信息)}
  check(localVer, callback) {
    logger.debug('check', localVer);
    superagent
      .get('https://raw.githubusercontent.com/antoor/antSword/master/package.json')
      .timeout(9527)
      .end((err, res) => {
        if (err) { return callback(false, err.toString()) };
        try {
          const info = JSON.parse(res.text);
          this.info = info;
          callback(info['version'] !== localVer, info);
        } catch (e) {
          return callback(false, e.toString());
        }
      });
  }

  // 下载更新
  // 参数{downloadUrl: 下载地址, md5: 校验MD5, callback: 回调（成功？(true, null):(false, err)）}
  download(downloadUrl, md5, callback) {
    // 创建临时文件
    const tmpDir = os.tmpDir();
    const fileName = '.antSword-' + (+new Date);
    const tmpFileName = path.join(tmpDir, fileName);
    // 当前目录环境
    const curDir = path.join(__dirname, '../../');
    // 开始下载文件
    nugget(
      downloadUrl,
      {
        target: fileName,
        dir: tmpDir,
        resume: true,
        verbose: true,
        strictSSL: downloadUrl.startsWith('https')
      },
      (err) => {
        if (err) { return callback(false, err.toString()) };
        // 校验MD5
        const _md5 = crypto.createHash('md5').update(fs.readFileSync(tmpFileName)).digest('hex');
        if (_md5 !== md5) { return callback(false, { type: 'md5', err: _md5 }) };
        // ZIP解压
        unzip(tmpFileName, {
          dir: tmpDir
        }, (e) => {
          if (e) { return (callback(false, { type: 'unzip', err: e })) };
          // 删除旧asar
          // fs.unlinkSync(path.join(curDir, 'app.asar'));
          // 移动新asar
          fs.rename(
            path.join(tmpDir, 'antSword.update'),
            path.join(curDir, 'app.asar'),
            (_e) => {
              _e ? callback(false, _e.toString()) : callback(true);
            }
          );
        });
      }
    );
  }


}

module.exports = Update;
