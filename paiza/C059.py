N = int(input())
ans = 0
for _ in range(N):
    n = int(input(), 2)
    ans = ans ^ n

print('{:04b}'.format(ans))
