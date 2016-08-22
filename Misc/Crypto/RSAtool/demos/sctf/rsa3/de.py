#!/usr/bin/env python

from scapy.all import *
# from sage.all import *
import zlib
import struct

PA = 24L
packets = rdpcap('./level3/level4/syc_security_system_traffic3.pcap')
client = '192.168.1.180'
size = 2 # size of e and n is packed into 2 bytes
list_n = []
list_m = []
list_id = []

for packet in packets:
    # TCP Flag PA 24 means carry data,
    if packet[TCP].flags == PA or packet[TCP].flags == PA +1:
        src = packet[IP].src
        raw_data = packet[TCP].load
        e = 19
        head = raw_data.strip()[:7]

        if head == "We have":
           n,d= raw_data.strip().replace("We have got N is ","").split('\ne is ')
           print '{"n":'+n.strip()+',"e":'+d+","

        if head == "encrypt":
           m = raw_data.replace('encrypted messages is 0x','').strip()
           print '"c":'+str(int(m,16))+"}"
