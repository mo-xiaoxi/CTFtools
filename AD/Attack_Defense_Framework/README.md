## AD线下赛攻防框架

> 改编自https://github.com/WangYihang/Attack_Defense_Framework







## 说明

1. 若碰到非PHP的网站，需修改网站流量伪造函数。详细修改core/obfs/get_all函数，把.php改成其它。
2. Exploit_all.py 使用时，需注意token、端口和IP地址的修改。
3. fake_requests.py需要配置网站本地路径、网站绝对目录下flag路径、网站根目录，有时候timeout也需要修改
4. submit_flag.py 需要修改对应的ip和端口

