#!/usr/bin/python
# -*- coding:utf-8 -*-
__Author__ = 'moxiaoxi'
__Filename__ = 'Cryptography.py'

'''
1. 字符串与ASCII码
'''
import binascii
plain = 'plain'
print '原文:',plain
#把a转换成2进制然后用16进制表示
b = binascii.b2a_hex(plain)
print 'ascii的16进制:',b
print 'ascii的16进制转为原文:',binascii.a2b_hex(b)

c = binascii.hexlify(plain)
print 'ascii的16进制:',c
print 'ascii的16进制转为原文:',binascii.unhexlify(c)


'''
2. Base64/32/16编解码
编码原理：Base64编码要求把3个8位字节转化为4个6位的字节，
之后在6位的前面补两个0，形成8位一个字节的形式，6位2进制能
表示的最大数是2的6次方是64，这也是为什么是64个字符(A-Z,a
-z，0-9，+，/这64个编码字符，=号不属于编码字符，而是填充字
符)的原因
'''
import base64
plain = 'base64test'
base64encode = base64.b64encode(plain)
base64decode = base64.b64decode(base64encode)
base32encode = base64.b32encode(plain)
base32decode = base64.b32decode(base32encode)
base16encode = base64.b16encode(plain)
base16decode = base64.b16decode(base16encode)

print 'plain:',plain
print 'base64encode:',base64encode
print 'base64decode:',base64decode
print 'base32encode:',base32encode
print 'base32decode:',base32decode
print 'base16encode:',base16encode
print 'base16decode:',base16decode

'''
3. shellcode编码
作用：
    避免出现Bad字符，\x00 \xa9等
    避开IDS或其他网络监测器的检测
    遵循字符串过滤器
'''  
import binascii
plain='The quick brown fox jumps over the lazy dog' 
shellcode=''
hexcode=''
for code in plain:
    shellcode+="\\x"+code.encode("hex")
    hexcode+=code.encode("hex")
print 'shellcode:',shellcode
print 'hex:',hexcode

cleanshellcode=shellcode.replace("\\x","")
print cleanshellcode
decodeplain = cleanshellcode.decode("hex")
print 'decodeToPlain:',decodeplain
























