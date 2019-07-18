N, t, s = input().split()

ans = 0
while t != s:
    s = s[1:] + s[0]
    ans = -~ans

print(ans)
