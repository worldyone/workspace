N = int(input())

li = []
for i in range(N):
    li.append(int(input()))

# 初期値
n = li[0]
m = li[1]-li[0]

# 最小値を探しつつ、探している最中の最小値と現在探索している値の最大の差を覚えておく
for a in li[1:]:
    if (a-n) > m:
        m = a-n
    if a < n:
        n = a

print(m)
