#!/usr/bin/python
#!/usr/bin/env python
import requests
list=[".swp",".bak","~","zip",".tar.gz",".rar",".tar",".old",".7z",".gz",".txt",".inc",".copy",".src",".tmp",".orig",".dev"]
list1=[".git",".DS_store",".svn","CVS","robots.txt"]
url="http://218.2.197.234:2050/index.php"
url1="http://218.2.197.234:2050/"
s = requests.Session()
for i in range(len(list)):
    r = s.get(url+ list[i])
    if(r.status_code == 200):
       print list[i]
print "part 1 has done!"
for i in range(len(list1)):
    r = s.get(url1 + list1[i])
    if(r.status_code==200):
       print list1[i]
print "part 2 has done!"
