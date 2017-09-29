#!/usr/bin/python
# -*- coding: utf-8 -*-
# author='moxiaoxi'

'''
A script for AWVS11 .
Just for the convenience of scanning.
Enjoy it ! :) Xiaoxi

ref:http://0cx.cc/about_awvs11_api.jspx,http://im1gd.me/2017/05/25/AWVS/
'''


import json
import Queue
import requests
import requests.packages.urllib3
from gevent.pool import Pool
from gevent import monkey
'''
import requests.packages.urllib3.util.ssl_
requests.packages.urllib3.util.ssl_.DEFAULT_CIPHERS = 'ALL'
 
or 
 
pip install requests[security]
'''

monkey.patch_all()
queue = Queue.Queue()
requests.packages.urllib3.disable_warnings()

filename='listSite.txt' # 要批量扫描的站点文件 
tarurl = "https://127.0.0.1:3443/"# AWVS url
apikey="yourapikey"
headers = {"X-Auth":apikey,"content-type": "application/json"}
 
def addTask(url=''):
    #添加任务
    data = {"address":url,"description":url,"criticality":"10"}
    try:
        response = requests.post(tarurl+"/api/v1/targets",data=json.dumps(data),headers=headers,timeout=30,verify=False)
        result = json.loads(response.content)
        return result['target_id']
    except Exception as e:
        print(str(e))
        return
 
def startScan(url):
    # 先获取全部的任务.避免重复
    # 添加任务获取target_id
    # 开始扫描
    targets = getScan()
    if url in targets:
        return "Sorry, Target repeat!"
    else:
        target_id = addTask(url)
        data = {"target_id":target_id,"profile_id":"11111111-1111-1111-1111-111111111111","schedule": {"disable": False,"start_date":None,"time_sensitive": False}}
        try:
            response = requests.post(tarurl+"/api/v1/scans",data=json.dumps(data),headers=headers,timeout=30,verify=False)
            result = json.loads(response.content)
            return result['target_id']
        except Exception as e:
            print(str(e))
            return

def stopScan(scan_id):
    # 停止扫描
    try:
        response = requests.post(tarurl+"/api/v1/scans/{0}/abort".format(str(scan_id)),headers=headers,timeout=30,verify=False)
        if response.content=='':
            return 'abort successfully!'
        else:
            print response.content
            return 'abort Wrong!'
    except Exception as e:
        print(str(e))
        return

def getStatus(scan_id):
    # 获取scan_id的扫描状况
    try:
        response = requests.get(tarurl+"/api/v1/scans/"+str(scan_id),headers=headers,timeout=30,verify=False)
        result = json.loads(response.content)
        status = result['current_session']['status']
        #如果是completed 表示结束.可以生成报告
        if status == "completed":
            return status+', download:'+getReports(scan_id)+'.pdf'
        else:
            return result['current_session']['status']
    except Exception as e:
        print(str(e))
        return
 
def getReports(scan_id):
    # 获取scan_id的扫描报告
    data = {"template_id":"11111111-1111-1111-1111-111111111111","source":{"list_type":"scans","id_list":[scan_id]}}
    try:
        response = requests.post(tarurl+"/api/v1/reports",data=json.dumps(data),headers=headers,timeout=30,verify=False)
        result = response.headers
        report = result['Location'].replace('/api/v1/reports/','/reports/download/')
        return tarurl.rstrip('/')+report
    except Exception as e:
        print(str(e))
        return
 
def getScan():
    #获取全部的扫描状态
    targets = []
    try:
        response = requests.get(tarurl+"/api/v1/scans",headers=headers,timeout=30,verify=False)
        results = json.loads(response.content)
        for result in results['scans']:
            targets.append(result['target']['address'])
            print 'State\tscan_id:{0}, address:{1}, status:{2}'.format(result['scan_id'],result['target']['address'],getStatus(result['scan_id']))#,result['target_id']
        return list(set(targets))
    except Exception as e:
        raise e

def readFile():
    # 读取文件，存储要批量扫描的目标站点
    with open(filename, 'r') as f:
        for line in f:
            if line.startswith("http://") or line.startswith("https://"):
                queue.put_nowait(line.replace('\n', ''))
            else:
                queue.put_nowait("http://" + line.replace('\n', ''))

def run(n):
    while True:
        if not queue.empty():
            targeturl = queue.get()
            print 'New scan began.\tURL:{0}\tID:{1}'.format(targeturl,startScan(targeturl))
        else:
            break

def scan():
    pool = Pool(5)
    readFile()
    pool.map(run, xrange(3))
    
if __name__ == '__main__':
    # print startScan('http://testhtml5.vulnweb.com/')
    # print stopScan('7bfe6b71-530d-4bc1-a4ad-bef662222efa')
    scan()
