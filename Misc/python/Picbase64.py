#!usr/bin/python
# -*- coding:utf-8 -*-
__Author__ = 'moxiaoxi'
__Filename__ = 'Picbase64.py'

'''
图片base64编解码
http://www.motobit.com/util/base64-decoder-encoder.asp
上述的网站还不错
'''
import base64

Pic =  open(r'./input.jpg','rb')#以二进制方式打开图文件
Encode_Pic = base64.b64encode(Pic.read()) #读取文件内容，转换为base64编码
Pic.close()

PicW =  open(r'./output.txt','w')#打开空白文本文件，准备写入
PicW.write(Encode_Pic)
Decode_Pic = base64.b64decode(Encode_Pic)
print Decode_Pic
PicW.flush()
PicW.close()
