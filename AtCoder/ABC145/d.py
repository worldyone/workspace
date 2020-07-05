from math import factorial

x,y = map(int, input().split())

ans = 0

if not (x+y) % 3 and (min(x,y)*2 >= max(x,y)):
    n = (x+y) // 3
    m = min(x,y) - n
    ans = 1
    mod = 10**9 + 7

    for i in range(100):
        print(ans)
        ans = ans * (n - i) % mod * pow(i + 1, mod-2, mod) % mod
    #ans = \
    #            factorial(n) \
    #    //\
    #        factorial(m) // factorial(n-m)

print(ans)

mod = 7
for i in range(10):
    print(i, pow(i, 2, mod))
