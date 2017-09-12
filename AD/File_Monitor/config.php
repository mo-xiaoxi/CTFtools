<?php

// Monitor configuration

return array(

     // Default scan path is `script root`
	 // 'root'   => '/root/to/scan',

     // allowed multi root
     // 'root' => ['/first', '/second', ...]

     //'root' => array(
     //   __DIR__ . '\\1',
     //   __DIR__ . '\\2'
     //),

     // skip this dirs
     //'ignore_dirs' => [
     //  'C:\\path\\fs_monitor\\2\\test1'
     //],

     // ServerTag for text reports, default _SERVER[SERVER_NAME]
	 // 'server' => 'server_name',

     // files pattern
	 'files' => '(\.php.?|\.htaccess|\.txt)$',

      // write logs to ./logs/Ym/d-m-y.log
     'log' => true,

      // notify administrator email
	 'mail' => array(
	 	'from'   => 'info@skillz.ru',
	 	'to'   	 => 'rustyj4ck@gmail.com',

	 	// disabled by default
	 	'enable' => false
	 )

);