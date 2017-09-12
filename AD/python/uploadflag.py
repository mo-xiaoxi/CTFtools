#!/usr/bin/env python
# encoding: utf-8

import requests, re
url = 'http://www.baidu.com'
s = requests.session()
c = s.get(url).content
print c
r = re.findall(r'<br/>([\s|\S]*?)=<', c)[0].strip()
print eval(r)
c1 = s.post(url, data={'v': eval(r)}).content
print c1.decode('utf-8')