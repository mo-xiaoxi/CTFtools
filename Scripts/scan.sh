scan()  {

  #statements

  ping -c 1 $1.$2 > /dev/null && echo "$1.$2 is alive"

}

for  i in `seq 1 254`

do

   scan $1 $i  &

done