import math as m

N = int(input())
li = [list(map(int, input().split())) for _ in range(N)]
xs,ys = list(zip(*li))

ans = 0
for i in range(N):
    for j in range(N):
        ans += m.sqrt((xs[i]-xs[j])**2 + (ys[i]-ys[j])**2)

print(ans/N)
