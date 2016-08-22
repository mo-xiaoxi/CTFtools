import math
from string import *

e,n = (19,920139713)

def exeuclid(a,b):
    if b == 0:
        return 1,0,a
    else:
        x,y,q = exeuclid(b,a%b)
        x,y = y,(x-a/b*y)
        return x,y,q

def getpq(x):
    for i in xrange(2,int(math.sqrt(x)) + 1):
        if x%i == 0:
            return i,x/i

def encrypt(m,e,n):
    c = pow(m,e,n)
    return c

def getprikey(e,n):
    p,q = getpq(n)
    d,x,y = exeuclid(e,(p-1)*(q-1))
    if d < 0:
        d = (p-1)*(q-1)+d
    return d,n

d,n = getprikey(e,n)

def decrypt(c,d,n):
    m= pow(c,d,n)
    return m

def decryptMSG(msg,n):
    s1 = ''
    msg = msg.split(',')
    for x in msg:
        s1 += chr(decrypt(int(x),d,n))
        return s1

def encryptMSG(msg,n):
    s1 = ''
    for x in msg:
        s1 += str(encrypt(ord(x),e,n))+','
        return s1[:-1]

if __name__ == '__main__':
    fr = open('data.txt','r')
    ciphertexts = fr.readlines()
    fr.close()          #'''
    rettext = ""
    for c in ciphertexts:
        plaintext = decryptMSG(c,n)
        rettext += plaintext
    print rettext
