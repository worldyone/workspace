N, M = map(int, input().split())

for i in range(N):
    a, b = map(int, input().split())
    if max(a - 5*b, 0) >= M:
        print(i+1)
