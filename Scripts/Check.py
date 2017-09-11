#!/usr/bin/env python
#!-*- coding:utf-8 -*-

def read(filename):
    dic=[]
    with open(filename,'r') as fp:
        while True:
            lines = fp.readlines(10000)
            if not lines :
                break
            for line in lines:
                #line = line.strip('\n')
                dic.append(line)
    return dic

def Write(file,dic):
    with open(file,'w') as fp:
        for i in dic:
            fp.write(i)

if __name__=='__main__':
    test = read('output.txt')
    test += read("dire.txt")
    print test
    Write('output.txt',set(test))
