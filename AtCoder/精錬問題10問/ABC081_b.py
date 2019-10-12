N = int(input())
li = list(map(int, input().split()))

ans = 10**9

for i in range(N):
    tmp = 0
    while(li[i]%2 == 0):
        li[i] /= 2
        tmp += 1
    ans = min(ans, tmp)

print(ans)
