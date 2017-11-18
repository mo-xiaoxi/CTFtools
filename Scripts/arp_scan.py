#!/usr/bin/python
# -*- coding: UTF-8 -*-
# sendARP.py
# 探测局域网 IP

import socket
import struct
import binascii
import dpkt
import pcap
import thread


# 将16进制的MAC地址转换成，需要的字符格式
def mac2a(m):
	return binascii.a2b_hex(m)

# 将ip地址列表转换成，需要的字符格式
def ip2a(i):
	ipi = i[0]*256*256*256+i[1]*256*256+i[2]*256+i[3]
	iph = hex(ipi)
	return binascii.a2b_hex(iph[2:])

# 向指定IP发送ARP报
def send(ip):
	# 设置协议族类型和上层协议
	rawS = socket.socket(socket.PF_PACKET,socket.SOCK_RAW,
			socket.htons(0x0806))
 
	# 绑定网卡和端口号
	rawS.bind(("en0",socket.htons(0x0800)))

	# 以太网头封包，6s:6B的目的端MAC，6s:6B 发送端MAC，2s:2B的协议类型
	tha = mac2a('ffffffffffff')	#target hardware address 目的端硬件地址
	sha = mac2a('34238769543e')	#sender hardware address 发送端硬件地址
	ethHdr = tha+sha+'\x08\x06'

	# ARP数据封包
	hrd = '\x00\x01'	#硬件地址类型，默认1：以太网
	pro = '\x08\x00'	#网络地址类型，0800：IP地址
	hln = '\x06'		#硬件地址长度，单位B
	pln = '\x04'		#网络地址长度，单位B
	op = '\x00\x01'		#操作类型，1：arp请求，2：arp响应
	#sha = sha			#发送端硬件地址
	spa = ip2a([192,168,199,112])			#发送端网络地址
	#tha = tha			#目的端硬件地址
	tpa = ip2a(ip)			#目的端网络地址
	arpPkt = hrd+pro+hln+pln+op+sha+spa+tha+tpa

	# 发送
	rawS.send(ethHdr+arpPkt)

def a2ip(a):
	return '%d.%d.%d.%d'%tuple(map(ord,list(a))) 

def a2mac(a):
	h = binascii.b2a_hex(a)
	return ':'.join([h[i:i+2] for i in xrange(0,len(h),2)])


#监听ARP响应
def listen():
	pc=pcap.pcap("wlo1")    #注，参数可为网卡名，如eth0
 
	for pt,pd in pc:         #pt为收到时间，pd为收到数据
		eth = dpkt.ethernet.Ethernet(pd)
		if eth.type==2054:
			arp = eth.data
			if arp.op==2:   #ARP响应
				print "在线",a2ip(arp.spa)," ",a2mac(arp.sha)

#listen()
thread.start_new_thread(listen,())

print "扫描开始"

for i in xrange(1,255):
	send([192,168,199,i])


print "扫描结束"