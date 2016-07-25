#!/usr/bin/python
# -*- coding: utf-8 -*-
import socket 
address = ('127.0.0.1', 31500)  
s = socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
s.bind(address)
s.connect(('120.76.114.164',21080))
s.send('GET / HTTP/1.1\r\nHost: 120.76.114.164:21080\r\nConnection: close\r\n\r\n')
# 接收数据:
buffer = []
while True:
    # 每次最多接收1k字节:
    d = s.recv(1024)
    if d:
        buffer.append(d)
    else:
        break
data = ''.join(buffer)

s.close()

header, html = data.split('\r\n\r\n', 1)
print header
# 把接收的数据写入文件:
with open('sina.html', 'wb') as f:
    f.write(html)