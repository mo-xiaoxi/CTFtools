<?php
/**
*/

error_reporting(0);
ini_set('display_errors', 'Off');
ini_set('allow_url_fopen', 'Off');

/**
* log
*/
function log()
{
	/* waf conf */
	$conf = array(
		"log_info" => array(
				"GET"    => $_SERVER['REQUEST_URI'], 
				"POST"   => $_POST, 
				"COOKIE" => $_COOKIE, 
		),
	);
	/* SESSION */
	if (isset($_SESSION)) {
		$conf['log_info']['SESSION'] = $_SESSION;
	}
	/* HTTP_HEADERS*/
	global $headers; 
	foreach ($_SERVER as $key => $value) { 
    	if ('HTTP_' == substr($key, 0, 5)) { 
        	$headers[str_replace('_', '-', substr($key, 5))] = $value; 
    	}
	}
	$conf['log_info']['HEADER'] = $headers;
	/* the same ip to write the same file */
	$ip = $_SERVER['REMOTE_ADDR'];
	$f = fopen("/tmp/njweb/$ip.log", "a");//修改！！！log写入地址
	/* Requests time */
	$t = date("H:i:s", $_SERVER['REQUEST_TIME']);
	fwrite($f, "===========$t===========\n");
	foreach ($conf['log_info'] as $key => $value) {
		if (is_array($value)) {
			$value = json_encode($value);
		}
		fwrite($f, "$key\t===>\t$value\n");
	}
	fwrite($f, "\n");
	fclose($f);
}

log();


?>
