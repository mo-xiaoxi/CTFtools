import base64
from Crypto.PublicKey import RSA

def decrypt_RSA(privkey, message):
    from Crypto.PublicKey import RSA
    from Crypto.Cipher import PKCS1_OAEP
    from base64 import b64decode
    key = open(privkey, "r").read()
    rsakey = RSA.importKey(key)
    rsakey = PKCS1_OAEP.new(rsakey)
    decrypted = rsakey.decrypt(message)
    return decrypted

flag = "69607199517868483359165446696575782986798491333171634561480071976234143538345367234799415119124372101538680310768987381846307094253169982296466477407040424769616420040229453113227524357479071064913500684764256363281150779409814511"
flag = int(flag)
# print flag
flag = hex(flag)#.decode('hex')
print flag
raw_input()
print decrypt_RSA('../whctf/768.pri', flag)
