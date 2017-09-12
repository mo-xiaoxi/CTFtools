#! python

import os, sys
import shutil
import time
import getopt

base = ''

ignore_type = ['jpeg']

if len(sys.argv) > 1:
    base = sys.argv[1]
else:
    print 'need dir'

dirs = {}


def get_files(now):
    global dirs
    nxt = os.listdir(now)
    dirs[now]=nxt
    for s in nxt:
        to = os.path.join(now,s)
        if os.path.isdir(to):
            get_files(to)

def protect(now):
    global dirs
    nxt = os.listdir(now)
    for s in nxt:
        to = os.path.join(now,s)
        if s not in dirs[now]:
            if os.path.isdir(to):
                print to
                shutil.rmtree(to)
            else:
                if s.split('.')[-1] not in ignore_type:
                    print to
                    os.remove(to)
            continue
        if os.path.isdir(to):
            protect(to)

if os.path.isdir(base):
    print base
    get_files(base)
    print dirs
    while 1:
        protect(base)
        time.sleep(0.5)
else:
    print 'not dir'