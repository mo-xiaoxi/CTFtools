import sys
import re

# first argument: unknown coding text
if len(sys.argv) != 2: sys.exit(2)
estr=sys.argv[1]

binrex = re.compile('^[01]+$', re.MULTILINE)
if(binrex.match(estr)): print 'binary'

octrex = re.compile('^[0-7]+$', re.MULTILINE)
if(octrex.match(estr)): print 'octal'

decrex = re.compile('^[0-9]+$', re.MULTILINE)
if(decrex.match(estr)): print 'decimal'

hexrex = re.compile('^[A-Fa-f0-9]+$', re.MULTILINE)
if(hexrex.match(estr)): print 'hexadecimal'

b64rex = re.compile('^[A-Za-z0-9+/]+[=]{0,2}$', re.MULTILINE)
if(b64rex.match(estr)): print 'base64'

uurex = re.compile('^(begin.*\n)?[\x20-\x60\n]+(end[\n]?)?$', re.MULTILINE)
if(uurex.match(estr)): print 'uuencode'

intelhexrex = re.compile('^:[0-9a-fA-F]{8}[0-9a-fA-F]*[0-9a-fA-F]{2}$', re.MULTILINE)
if(intelhexrex.match(estr)): print 'intelhex'

srecrex = re.compile('^S[0-9]{1}[0-9a-fA-F]{6,10}[0-9a-fA-F]*[0-9a-fA-F]{2}$')
if(srecrex.match(estr)): print 'srec'

ascii85rex = re.compile('^[A-Za-z0-9!#$%&()*+\-;<=>?@^_`{|}~]+$', re.MULTILINE)
if(ascii85rex.match(estr)): print 'ascii85'

binhexrex = re.compile(r'^[A-NP-VX-Z0-9a-fh-mp-r\!\"\#\$\%\&\'\(\)\*\+\,\-\@\`\[\:]+$', re.MULTILINE)
if(binhexrex.match(estr)): print 'binhexrex'

xxrex = re.compile('^[A-Za-z0-9+\-]+$')
if estr.startswith('begin'):
    tstr = estr.split('\n')
    tstr = ''.join(tstr[1:len(tstr)-1])
    if(xxrex.match(tstr)): print 'xxencode'
if(xxrex.match(estr)): print 'xxencode'

md5rex = re.compile('^[0-9a-fA-F]{32}$')
if md5rex.match(estr): print 'md5'

sha0and1rex = re.compile('^[0-9a-fA-F]{40}$')
if sha0and1rex.match(estr): print 'sha-0 or sha-1'

sha224 = re.compile('^[0-9a-fA-F]{56}$')
if sha224.match(estr): print 'sha-224'

sha256 = re.compile('^[0-9a-fA-F]{64}$')
if sha256.match(estr): print 'sha-256'

sha384 = re.compile('^[0-9a-fA-F]{96}$')
if sha384.match(estr): print 'sha-384'

sha512 = re.compile('^[0-9a-fA-F]{128}$')
if sha512.match(estr): print 'sha-512'