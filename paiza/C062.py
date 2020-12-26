cnt = 0
rcnt = 0
m = "melon"

T = int(input())
for t in range(T):
    sushi = input()
    rcnt -= 1

    if sushi == m and rcnt <= 0:
        cnt += 1
        rcnt = 11

print(cnt)
