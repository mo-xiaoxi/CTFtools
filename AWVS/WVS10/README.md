# WVS_Patcher
Script to run wvs in queue, and send mails to you on ending.



### Description

```
.
├── README.md 本文件
├── conf.py  配置文件，主要修改配置处
├── index.html index.html渲染模版
├── mail.py 邮件提醒脚本
├── web.py Web渲染脚本
└── wvs.py 主要完成WVS的渲染工作，调用wvsc.exe进行扫描
```



### Install

+ pip install requests
+ pip install multiprocessing
+ pip install bottle
+ 在conf.py中修改配置


### Use

+ cd wvs
+ python web.py
+ 浏览器访问： http://127.0.0.1:8082/index
