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
plain = 'The quick brown fox jumps over the lazy dog' 
shellcode = ''
hexcode = ''
for code in plain:
    shellcode += "\\x"+code.encode("hex")
    hexcode += code.encode("hex")
print 'shellcode:',shellcode
print 'hex:',hexcode

cleanshellcode = shellcode.replace("\\x","")
print cleanshellcode
decodeplain = cleanshellcode.decode("hex")
print 'decodeToPlain:',decodeplain



'''
4.quoted-printable
编码原理：http://blog.chacuo.net/494.html
'''
import quopri

plain = 'testplain = 数据'
print 'plain:',plain
cipher = quopri.encodestring(plain)
print 'quopriCode:',cipher
decode = quopri.decodestring(cipher)
print 'quopriDecode:',decode


'''
5. XXencode编码
在线编解码：http://web.chacuo.net/charsetxxencode
'''
import math
def XXencode(plain):
    #64个可打印字符
    base = "+-0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
    #每次读取3个字节
    lbyte = 3
    length = len(plain)
    smod = (length/lbyte) 
    snum =  math.floor(length/lbyte)

    plain = plain if smod==0 else plain+(lbyte-smod)*'\0'
    snum = snum if smod==0 else snum+1

    for i in range(0,snum):
        lbyte=3

'''
6. UUencode编码
http://web.chacuo.net/charsetuuencode
'''

'''
7. URL编解码
'''
import urllib
url = 'url plain+21/213 '
print 'url',url
urlcode = urllib.quote(url)
print 'urlcode:',urlcode
urldecode = urllib.unquote(urlcode)
print 'urldecode',urldecode


'''
8. Unicode编码
源文本：The
&#x [Hex]：&#x0054;&#x0068;&#x0065;
&# [Decimal]：&#00084;&#00104;&#00101;
\U [Hex]：\U0054\U0068\U0065
\U+ [Hex]：\U+0054\U+0068\U+0065
在线编解码：http://www.mxcz.net/tools/Unicode.aspx
'''

'''
9. Escape/Unescape编码
Escape/Unescape加密解码/编码解码,又叫%u编码，采用UTF-16BE模式，
Escape编码/加密,就是字符对应UTF-16 16进制表示方式前面加%u。
Unescape解码/解密，就是去掉"%u"后，将16进制字符还原后，由utf-16
转码到自己目标字符。如：字符“中”，UTF-16BE是：“6d93”，因此Escape
是“%u6d93”。

源文本：The

编码后：%u0054%u0068%u0065
'''


import cgi
s1 = "hello word"
s2 = cgi.escape(s1)
print s2 


'''
10. HTML实体编码
'''


'''
11. 敲击码
敲击码(Tap code)是一种以非常简单的方式对文本信息进行编码的方法。
因该编码对信息通过使用一系列的点击声音来编码而命名，敲击码是基于
5×5方格波利比奥斯方阵来实现的，不同点是是用K字母被整合到C中。
  1  2  3  4  5
1 A  B C/K D  E
2 F  G  H  I  J 
3 L  M  N  O  P
4 Q  R  S  T  U
5 V  W  X  Y  Z
'''

'''
12. 莫斯密码
'''
class Morse(object):
    def __init__(self):
        self.morse_code = {
            'A': '.-', 'B': '-...',
            'C': '-.-.', 'D': '-..',
            'E': '.', 'F': '..-.',
            'G': '--.', 'H': '....',
            'I': '..', 'J': '.---',
            'K': '-.-', 'L': '.-..',
            'M': '--', 'N': '-.',
            'O': '---', 'P': '.--.',
            'Q': '--.-', 'R': '.-.',
            'S': '...', 'T': '-',
            'U': '..-', 'V': '...-',
            'W': '.--', 'X': '-..-',
            'Y': '-.--', 'Z': '--..',
            '0': '-----', '1': '.----',
            '2': '..---', '3': '...--',
            '4': '....-', '5': '.....',
            '6': '-....', '7': '--...',
            '8': '---..', '9': '----.'
        }

    def decode(self, morsecode):
        ''' Decodifica codigo morse '''
        code = morsecode.split(' ')
        text = ''
        for item in code:
            for key, value in self.morse_code.items():
                if item == value:
                    text += key
                    break
        return text

    def encode(self, plaintext):
        ''' Codifica em codigo morse '''
        code = ''
        for char in plaintext.upper():
            if char in self.morse_code.keys():
                code += self.morse_code[char] + ' '
        return code
Mo=Morse()
cipher=Mo.encode('plain')
plain=Mo.decode(cipher)
print 'cipher:',cipher
print 'plain:',plain
'''
参考：http://drops.wooyun.org/tips/17609
'''

