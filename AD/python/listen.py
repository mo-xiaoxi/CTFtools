#!/usr/bin/env python
# encoding: utf-8

import gevent
from gevent import monkey,pool; monkey.patch_all()
from gevent import Timeout
from gevent import socket
import threading
import logging
import re


logging.basicConfig(level=logging.DEBUG,
                format='%(asctime)s %(filename)s[line:%(lineno)d][thread:%(thread)d] %(levelname)s %(message)s',
                datefmt='%a, %d %b %Y %H:%M:%S',
                filename='flag.log',
                filemode='w')
#################################################################################################
#定义一个StreamHandler，将INFO级别或更高的日志信息打印到标准错误，并将其添加到当前的日志处理对象#
console = logging.StreamHandler()
console.setLevel(logging.DEBUG)
formatter = logging.Formatter('[line:%(lineno)d] %(levelname)s: %(message)s')
console.setFormatter(formatter)
logging.getLogger('').addHandler(console)


def start(laddr):
	proxy = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
	proxy.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
	try:
		proxy.bind(laddr)
		proxy.listen(0)
		logging.info("Proxy Client {0} Listening Success...".format(laddr))
		while True:
			# connection,peer = proxy.accept()
			# gevent.spawn(handler,connection,peer)
			proxy_thread = threading.Thread(target=handler,args=proxy.accept())
			proxy_thread.daemon = True
			proxy_thread.start()
	except socket.error as msg:
		logging.error(msg)
	except Exception, e:
		logging.info(e)
	finally:
		proxy.close()
		logging.info('proxy destory success...')


def handler(connection,peer):
	data = ''
	while True:
		tmp = connection.recv(1000)
		data +=tmp
		if len(tmp)<1000:
			break
	logging.info("data:{0}from:{1}".format(data,peer))
	logging.info(re.findall(r"flag{.*}",data))
	logging.info(peer)
	logging.info("Get Flag:{0} from IP:{1}".format(re.findall(r"flag{.*}",data),peer))
	connection.send('hi')



if __name__=='__main__':
	LTCP_ADDR = ('0.0.0.0',3333)
	start(LTCP_ADDR)
