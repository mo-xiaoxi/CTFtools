#!/usr/bin/env python
#-*- coding:utf-8 -*-
import hashlib
# for i in  xrange(0,999999999):
#     test = hashlib.md5()
#     test.update(str(i))
#     if(test.hexdigest()[0:6]=='c27e9e'):
#         print i,test.hexdigest()
#         break


i = 0
while(True):
	s = str(i)
	if hashlib.md5(s).hexdigest().startswith('9bc519'):
		print '[!]', 'found:', s
		break
	i+=1





