"""
C048.pyの問題を見て、級数の問題を解く。

nが与えられた時に、割引率が50%だと、2nになるが、
途中で切り捨てが入ると、2nにならない。

ここでは、以下の式の損失（2n-Actual)が仮定と合っているか
確認するためのプログラムである。

式：
2^m - 2^n + 1
仮定：
損失が m - n + 1　になる。

具体例：
"""
bekijo = list(map(int, input().split()))
print(bekijo)
ans = 0
p = 0.5

# 2のべき乗で表される数
N = 0
for b in bekijo:
    N += pow(2, b)
N += 1

X = N
print("X", X)

while(X != 0):
    ans += X
    X = int(X * p)
    print(X)

print("ans:", ans)
print("2*n:", 2*N)

print("loss:", 2*N - ans)
