# 线下赛

## Web

1. 流量混淆脚本
2. WAF
3. 批量文件上传
4. 权限维持
   - 不死马 删不死马
   - contrab
   - Webshell
5. [文件监控](https://github.com/rustyJ4ck/FSMon)，防止恶意上传（没用过）
6. 可视化流量分析 [goaccess](https://github.com/allinurl/goaccess)（没用过）
7. PHP中转代理脚本
   - 类似反向代理，反向代理一个你想搞事情的网站
   - [7ghost](https://github.com/BevisGoh/7ghost) 需要.htaccess解析
   - 其实，在中转的时候还可以顺带抓包记录。最骚的场景就是A以为在攻击你（B）,你开启反向代理代理了C，与此同时你抓了A的payload...等于做了一个中间人的工作，在不损失自己flag的前提下还能获取到攻击信息。此外，在7ghost的配置中还能把C返回的flag给替换掉。。是不是很骚😂

## Pwn



## 比赛