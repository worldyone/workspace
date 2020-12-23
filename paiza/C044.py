N = int(input())
li = [input() for _ in range(N)]
rs = li.count("rock")
ps = li.count("paper")
ss = li.count("scissors")

if rs and ps and ss:
    print("draw")
elif rs and ps:
    print("paper")
elif ps and ss:
    print("scissors")
elif ss and rs:
    print("rock")
else:
    print("draw")
