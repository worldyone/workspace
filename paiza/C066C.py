M, N, x = map(int, input().split())
m = [int(input()) for _ in range(M)]

i = 0
ans = 0
n = x
while i < M:
    if n <= m[i]:
        N -= 1
        if N == 0:
            break
        n = x
    else:
        n -= m[i]
        i += 1
        ans += 1

print(ans)
