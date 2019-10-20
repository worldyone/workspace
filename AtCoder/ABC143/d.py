import bisect

N = int(input())
li = list(map(int, input().split()))
li = sorted(li)

ans = 0

for i in range(N):
    for j in range(i+1, N):
        cnt = bisect.bisect_left(li, li[i]+li[j])
        ans += cnt - j - 1

print(ans)
