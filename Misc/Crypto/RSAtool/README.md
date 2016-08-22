# CTF RSA 工具

- RSA大数分解网站http://www.factordb.com/index.php
- 大数分解平台https://cloud.sagemath.com/
  ```
  factor(0x123)
  factor(123)
  ```
- RSA tool2
- yafu 如果两个素数相差很近，可采用费马分解
  ```
  factor(0x123)
  factor(123)
  ```

- rsatool.py 标准工具 用于已有p,q生成私钥
  ```
  ./rsatool.py -p num1 -q num2 -o priv.key
  ```

- RsaCtfTool.py 提供公钥可输出n,e  
- openssl
  ```
  输出公钥信息
  openssl rsa -noout -text -inform PEM -in public.key -pubin

  openssl rsautl -decrypt -in level1bin.passwd.enc -inkey ../priv.key -out level1.passwd -oaep
  ```
