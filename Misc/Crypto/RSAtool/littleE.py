#!usr/bin/env python
#!-*- coding:utf-8 -*-
import gmpy

def Getanswer(c,n,e):
    while(True):
        k=1
        temp = c + k*n
        x,y=gmpy.root(temp,e)
        if(y):
            return x
        else:
            k+=1

if __name__=='__main__':
    Getanswer()