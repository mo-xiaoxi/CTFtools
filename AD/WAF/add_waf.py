#-*- coding:utf-8 -*
'''
批量添加WAF的python脚本
'''
import os
base_dir = '/var/www/html' #web path
def scandir(startdir) :
    
    os.chdir(startdir)
    for obj in os.listdir(os.curdir) :
        path = os.getcwd() + os.sep + obj
        if os.path.isfile(path) and '.php' in obj:
        	modifyip(path,'<?php','<?php\nrequire_once(\'waf.php\');') 
        if os.path.isdir(obj) :
            scandir(obj)
            os.chdir(os.pardir) 
def modifyip(tfile,sstr,rstr):
    try:
        lines=open(tfile,'r').readlines()
        flen=len(lines)-1
        for i in range(flen):
            if sstr in lines[i]:
                lines[i]=lines[i].replace(sstr,rstr)
        open(tfile,'w').writelines(lines)
        
    except Exception,e:
        print e
        
scandir(base_dir)