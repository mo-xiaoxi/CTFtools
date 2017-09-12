#!/usr/bin/env python
#-*- coding:utf-8 -*-
# __author__= 'moxiaoxi'
'''
流量混淆脚本
读取ip.txt中的ip地址，向其发送混淆流量。
发送的页面在random_page函数配置，一些恶意关键字在random_keyword配置
'''
import requests
import os
import multiprocessing
from urllib import quote_plus
from random import randint as ri
import time


def random_str(n):
    return ''.join(map(lambda xx: (hex(ord(xx))[2:]), os.urandom(n)))[:n]


def random_keyword():
    keywords = ['select', 'and', 'into', 'upload', 'eval', 'assert', 'system', '/bin/sh', '/bin/bash', 'bin', '']
    return keywords[ri(0, len(keywords) - 1)]


def random_page():
    pages = ['/upload.php', '/index.php', '/config.php', '/inc.php']
    return pages[ri(0, len(pages) - 1)]


def random_dict(num=0):
    if not num:
        num = ri(5, 20)
    d = {}
    for _ in xrange(num): d[random_str(ri(3, 10))] = random_str(ri(3, 10)) + random_keyword() + random_str(ri(3, 10))
    return d


def dict2str(d, isCookie=False):
    s = ""
    for k, v in d.iteritems():
        if isCookie:
            sep = ';'
        else:
            sep = '&'
        s += '{k}={v}{sep}'.format(k=quote_plus(k), v=quote_plus(v), sep=sep)
    return s[:-1]


def random_ua():
    uas = [
        'Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16',
        'Mozilla/5.0 (Linux; U; Android 2.2; en-us; Nexus One Build/FRF91) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1',
        'Mozilla/5.0 (iPad; U; CPU OS 3_2_2 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Version/4.0.4 Mobile/7B500 Safari/531.21.10',
        'Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16',
        'Mozilla/5.0 (SymbianOS/9.4; Series60/5.0 NokiaN97-1/20.0.019; Profile/MIDP-2.1 Configuration/CLDC-1.1) AppleWebKit/525 (KHTML, like Gecko) BrowserNG/7.1.18124'
    ]
    return uas[ri(0, len(uas) - 1)]


def send_dirty(url):
    fake_headers = {
        'User-Agent': random_ua(),
        'Cookie': dict2str(random_dict(), True)
    }
    url += random_page() + '?' + dict2str(random_dict())
    print url
    try:
        requests.get(url, headers=fake_headers, timeout=1)
    except:
        pass


def main():
    while True:
        f = open('ip.txt')
        ip = f.read().strip('\n\r ').split('\n')
        f.close()
        urls = ['http://'+ip[:-1]+'11' for ip in all]
        pool = multiprocessing.Pool(4)
        pool.map_async(send_dirty, urls)
        pool.close()
        pool.join()

        time.sleep(0.5)


if __name__ == '__main__':
    main()
    # print random_dict()
