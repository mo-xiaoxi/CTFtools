#!/usr/bin/env python
#-*- coding:utf-8 -*-

import string

fp = open("basicChar.txt",'w')
basic = string.printable
for i in basic:
    fp.write(i)
    fp.write('\n')

