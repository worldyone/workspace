from math import sqrt, ceil

# ユークリッドの互除法で最大公約数を返す
def gcd(a, b):
    if b == 0:
        return a
    return gcd(b, a%b)

A, B = map(int, input().split())
if A > B:
    A, B = B, A
g = gcd(A, B)
s = set(); s.add(1)
sqrt_g_plus = ceil(sqrt(g))+1
i = 2

# 最大公約数の平方根までの素数を見つける
while i < sqrt_g_plus:
    if g%i == 0:
        s.add(i)
        g //= i
    else:
        i += 1
# 平方根以上の素数がある場合、追加する（あるとした場合一つだけである）
if g > 1:
    s.add(g)

print(len(s))
