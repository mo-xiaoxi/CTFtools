### What's this

Script allow monitor changes to files in specified directories.
When changes detected, it'll notify administrator by email or/and write logs
this changes.

``` 
[  modified]    /home/user/www/fs_monitor/fsmon.php       5.1 kb  28.09.2009 23:37
[       new]    /home/user/www/fs_monitor/1/sc.phps       1 kb    28.09.2009 23:09
[   deleted]    /home/user/www/fs_monitor/1/op.phps       2 kb    01.01.1970 03:00
```

### Setup

Make sure `.cache` file php-writable
```
touch .cache
chmod g+w ./.cache
```


### Basic Configure

See options in `config.php`


### Run task with cron

```
crontab -e
0 	3 	* 	* 	* 	/usr/bin/env php -f /home/user/fsmon.php > /dev/null 2>&1 
```

[Описание на русском](http://www.skillz.ru/dev/php/article-Skript_monitoringa_izmenenii_faylov.html)
