# /usr/bin/env python
#-*- coding:utf-8 -*-
'''
AWVS11批量扫描工具，改编自http://im1gd.me/2017/05/25/AWVS/、吾爱破解
用于批量提交URL进行AWVS批量扫描，提升运维、渗透狗的幸福体验度。
'''
import hashlib
import requests
import json
import Queue
from gevent.pool import Pool
from gevent import monkey
monkey.patch_all()
queue = Queue.Queue()

serverName = "****"#AWVS网站地址 一般为localhost
host = "https://****:3443"#一般为localhost:3443
filename = "****"#需批量扫描的网站集合文件
username = "****@****.com"#AWVS用户名
password = "****"#AWVS密码

def setLogin():
    loginURL = "{0}/api/v1/me/login".format(host)
    header = {
        'Content-Type': 'application/json;charset=utf-8'
    }
    data = '''{"email":"'''+username+'''","password":"'''+hashlib.sha256(password).hexdigest()+'''","remember_me":false}'''
    try:
        res = requests.post(loginURL, data=data, headers=header, verify=False)
        return res.headers["Set-Cookie"], res.headers["X-Auth"]
    except BaseException:
        print "setLogin error"
        pass

def newTarget(url):
    cookie, x = setLogin()
    reqURL = host + "/api/v1/targets"
    targetURL = url
    header = {
        'Content-Type': 'application/json;charset=utf-8',
        'X-Auth': x,
        'Cookie': cookie,
    }
    data = '{"description":"auto","address":"' + \
        targetURL + '","criticality":"10"}'
    try:
        res = requests.post(reqURL, data=data, headers=header, verify=False)
        return json.loads(res.content)["target_id"], cookie, x
    except BaseException:
        print "setTarget error"

def startScan(url):
    tarID, cookie, x = newTarget(url)
    scanURl = host + '/api/v1/scans'
    header = {
        'Content-Type': 'application/json;charset=utf-8',
        'X-Auth': x,
        'Cookie': cookie,
    }
    data = '{"target_id":' + '\"' + tarID + '\"' + \
        ',"profile_id":"11111111-1111-1111-1111-111111111111","schedule":{"disable":false,"start_date":null,"time_sensitive":false},"ui_session_id":"66666666666666666666666666666666"}'
    try:
        res = requests.post(scanURl, data=data, headers=header, verify=False)
        print "===================================================="
        print"                   add scan "
        print "===================================================="
        print res.content
        print "===================================================="
        print"                   add finished"
        print "===================================================="
    except BaseException:
        print "startScan failed"

def readFile():
    with open(filename, 'r') as f:
        for line in f:
            if line.startswith("http://") or line.startswith("https://"):
                queue.put_nowait(line.replace('\n', ''))
            else:
                queue.put_nowait("http://" + line.replace('\n', ''))

def Run(n):
    while True:
        if not queue.empty():
            startScan(queue.get())
        else:
            break

if __name__ == "__main__":
    pool = Pool(5)
    readFile()
    pool.map(Run, xrange(3))