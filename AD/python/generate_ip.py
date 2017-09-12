f = open('ip.txt', 'w')
for i in range(120, 134):
    if i == 131 or i == 133:
        continue
    ip = '192.168.168.' + str(i)
    f.write(ip + '\n')
f.close()
