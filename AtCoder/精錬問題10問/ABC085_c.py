N, Y = list(map(int, input().split()))

flg = False
for i in range(N+1):
    for j in range(N+1-i):
        if 10000*i + 5000*j + 1000*(N-i-j) == Y:
            ans = i,j,N-i-j
            flg = True

if flg:
    print(*ans)
else:
    print(-1,-1,-1)
