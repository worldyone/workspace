'''
ブクログで読了本をタイトルだけにする
in.txtに１．を記載すると、２．を出力する

１．
麺・丼・おかずの爆速バズレシピ101
麺・丼・おかずの爆速バズレシピ101
リュウジ  本  2018年7月20日   Amazon.co.jpで見る Amazon.co.jp
読了日： 2019-03-14
 大局観  自分と闘って負けない心 (角川oneテーマ21)
大局観 自分と闘って負けない心 (角川oneテーマ21)
羽生善治  本  2011年2月10日   Amazon.co.jpで見る Amazon.co.jp
読了日： 2019-03-24
 3行レシピでつくる居酒屋おつまみ (青春文庫)
3行レシピでつくる居酒屋おつまみ (青春文庫)
検見崎聡美  本  2005年11月1日   Amazon.co.jpで見る Amazon.co.jp
読了日： 2019-03-2

２.
麺・丼・おかずの爆速バズレシピ101
大局観 自分と闘って負けない心 (角川oneテーマ21）
3行レシピでつくる居酒屋おつまみ (青春文庫）
'''
f = open("in.txt", 'r', encoding="utf-8_sig")

lst = f.readlines()

l = lst[::4]
print(l)

for a in l:
    print(a, end="")
