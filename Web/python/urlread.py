#!usr/bin/python
#-*- coding:utf-8 -*-
import urllib

url = 'http://120.76.114.164:21080/c1/'

#代理服务器
proxies = {'http':'http://127.0.0.1:233'}
#使用代理服务器打开
r = urllib.urlopen(url,proxies = proxies)

print r.info()
print r.getcode()
print r.geturl()

# #打开本地文件
# f = urllib.urlopen(url = 'file:/home/ma6174/a.sh')
# print f.read()

# #打开ftp
# #f = urllib.urlopen(url = 'ftp://username:password@ftpaddress')

# #保存网页并显示进度
# def cbk(a, b, c):
#         '''
#         a: num
#         b: size
#         c: total
#         '''
#         per = 100.0*a*b/c
#         if per > 100:
#                 per = 100
#         print '%.2f%%' % per

# local = 'cnblogs.html'
# urllib.urlretrieve(url,local,cbk)

# #get方法
# params = urllib.urlencode({'spam': 1, 'eggs': 2, 'bacon': 0})
# f = urllib.urlopen("http://www.musi-cal.com/cgi-bin/query?%s" % params)
# print f.read()

# #post方法
# params = urllib.urlencode({'spam': 1, 'eggs': 2, 'bacon': 0})
# f = urllib.urlopen("http://www.musi-cal.com/cgi-bin/query", params)
# print f.read()

# #编码解码
# data = 'name = ~a+3'  

# data1 = urllib.quote(data)   
# print data1 # result: name%20%3D%20%7Ea%2B3   
# print urllib.unquote(data1) # result: name = ~a+3   
  
# data2 = urllib.quote_plus(data)   
# print data2 # result: name+%3D+%7Ea%2B3   
# print urllib.unquote_plus(data2)    # result: name = ~a+3   
  
# data3 = urllib.urlencode({ 'name': 'dark-bull', 'age': 200 })   
# print data3 # result: age=200&name=dark-bull   
  
# data4 = urllib.pathname2url(r'd:/a/b/c/23.php')   
# print data4 # result: ///D|/a/b/c/23.php   
# print urllib.url2pathname(data4)    # result: D:/a/b/c/23.php