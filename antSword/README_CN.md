# 中国蚁剑
> 一剑在手，纵横无忧！

**中国蚁剑是一款开源的跨平台网站管理工具，它主要面向于合法授权的渗透测试安全人员以及进行常规操作的网站管理员。**    
**任何人不得将其用于非法用途以及盈利等目的，也禁止未经允许私自修改打包进行发布，否则后果自行承担并将追究其相关责任！**

[![node](https://img.shields.io/badge/node-4.0+-green.svg?style=flat-square)][url-nodejs-download]
[![release](https://img.shields.io/badge/release-v1.3.0-blue.svg?style=flat-square)][url-release]

[英文说明][url-docen] / [开发文档][url-document] / [更新日志][url-changelog] / [官网][url-homepage] / [微博][url-weibo] / [Q群][url-qqgroup]

## 开发栈
 - [Electron][url-electron]
 - [ES6][url-es6]
 - [Babel][url-babel]
 - [dhtmlx][url-dhtmlx]
 - [Nodejs][url-nodejs]
 * 以及其他在项目中调用到的库

中国蚁剑推崇模块化的开发思想，遵循**开源，就要开得漂亮**的原则，致力于为不同层次的人群提供最简单易懂、方便直接的代码展示及其修改说明，努力让大家可以一起为这个项目贡献出力所能及的点滴，让这款工具真正能让大家用得顺心、舒适，让它能为大家施展出最人性化最适合你的能力！

## 开始使用
> 从1.3.0开始，移除了`webpack`以及部分相关模块，开发者不必进行编译即可直接运行！

### 发行版本
此版本专门针对不需要关心代码编写修改的正常使用用户。    
只需要进入[Release][url-release]页面，或者前往[官网下载](http://uyu.us/#/download)，下载对应系统版本的安装包即可运行使用。

### 开发版本
**开发版本针对有一定编程基础的开发者，你可以根据阅读文档或者分析源码了解熟悉整个应用的执行流程，然后便可随意对代码进行修改增强个性化自定义，真正打造出属于自己的一把宝剑！**

#### 下载源码
``` sh
$ git clone https://github.com/antoor/antSword.git
```

#### 安装模块
``` sh
$ cd antSword
$ npm install
```
> 遇到因某墙原因导致模块安装不来的情况，可以试着采用国内`taobao`源进行安装。

  ``` sh
  $ npm --registry=https://registry.npm.taobao.org
  ```

#### 启动应用
``` sh
$ npm start
```
> 编辑代码之后无需关闭应用重启，直接`Ctrl+R`或菜单栏->调试->重启应用即可。

## 致敬感谢
> 中国蚁剑的核心代码模板均改自伟大的**中国菜刀**，在此向作者感谢以及致敬！致敬每一位为网络安全做出点滴贡献的心老前辈！    

当前版本为`1.3.x`，在`1.2.x`的基础上进行了大量的代码更新，在修复BUG提升稳定性能的同时，也进行着新功能的研发改进。    
**一路走来，得到了很多朋友的参与开发以及点滴赞助，在此感谢陪伴，感谢你们能让它越走越远！**

## 开源协议
本项目遵循`MIT`开源协议，详情请查看[LICENSE](LICENSE)。


[url-docen]: README.md
[url-changelog]: CHANGELOG.md
[url-document]: http://doc.uyu.us
[url-nodejs-download]: https://nodejs.org/en/download/
[url-release]: https://github.com/antoor/antSword/releases/tag/1.3.0
[url-electron]: http://electron.atom.io/
[url-es6]: http://es6.ruanyifeng.com/
[url-webpack]: http://webpack.github.io/
[url-dhtmlx]: http://dhtmlx.com/
[url-nodejs]: https://nodejs.org/
[url-babel]: http://babeljs.io/
[url-weibo]: http://weibo.com/antoor
[url-homepage]: http://uyu.us
[url-release]: https://github.com/antoor/antSword/releases
[url-qqgroup]: http://shang.qq.com/wpa/qunwpa?idkey=51997458a52d534454fd15e901648bf1f2ed799fde954822a595d6794eadc521
