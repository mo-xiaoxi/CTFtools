## 修改组建

### 编辑代码
你可以选择[atom][atom-url]、[sublimeText][st-url]或者[vsCode][vscode-url]作为本项目的开发IDE，也可以选择任何你喜欢的文本编辑器进行编写。

### 组建源码
我们采用了`webpack`进行前端代码构建，但是其中的细节你不需要太多的知识去了解，只需要编辑好代码，然后执行下面命令进行构建编译即可：
``` sh
$ npm run build
```

> 该命令执行后，会进行代码监听，如有代码改动则会及时进行构建处理，如若只需编译一次，则直接`Ctrl+C`结束命令即可。    
> 也可以去掉`package.json`中对应命令的`--watch`选项

[atom-url]: https://atom.io/
[vscode-url]: https://code.visualstudio.com/
[st-url]: http://www.sublimetext.com/3
