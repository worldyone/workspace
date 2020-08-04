S = input()
n = len(S)

ans = 0
p = 1
c = pow(2, n - 1)

for i in range(n):
    for j in range(i, n):
        if j == i:
            ans += p * c * int(S[-j - 1])
            c //= 2
        else:
            ans += p * c * int(S[-j - 1])
    p *= 10

print(ans)
