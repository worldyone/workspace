N = int(input())
li = list(map(int, input().split()))

ans = 0

for i,di in enumerate(li):
    for j,dj in enumerate(li):
        if i <= j:
            continue
        ans += di*dj

print(ans)
