N = int(input())
T = [list(map(int, input().split())) for _ in range(N)]

x=y=0
flg = False
for t,dx,dy in T:
    if abs(x-dx) + abs(y-dy) <= t and (t-dx-dy)%2==0:
        x,y = dx,dy
    else:
        break
else:
    flg = True

print("NYoe s"[flg::2])
