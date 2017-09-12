import os
public = open('public.txt').read().strip('\n\r ').split('\n')
for p in public:
    cmd = "echo " + p + " >> " + ".ssh/authorized_keys"
    os.system('ssh user@ip -p 22 ' + '\"%s\"' % (cmd))
