#!/usr/bin/env python

from scapy.all import *

PA = 24L
packets = rdpcap('./syc_security_system_traffic2.pcap')
client = '192.168.1.180'
size = 2 # size of e and n is packed into 2 bytes
list_n = []
list_m = []
list_id = []

for packet in packets:
    if packet[TCP].flags == PA:
           src = packet[IP].src
           raw_data = packet[TCP].load
           e = 19
           head = raw_data.strip()[:7]
           if head == "We have":
               n = raw_data.strip().replace("We have got N is ","").split('\r\ne is ')
               print "{\"n\":",n[0],",","\"e\":",n[1],","
           if head == "encrypt":
               m = raw_data.replace('encrypted messages is 0x','')[:-1]
               print "\"m\":",int(m,16),"}"
