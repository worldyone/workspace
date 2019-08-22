from collections import deque

N, Q = list(map(int, input().split()))
edges = {n:[] for n in range(N+1)}
nodes = [0 for _ in range(N+1)]
cr = deque()

# 経路
for n in range(N-1):
    a, b = list(map(int, input().split()))
    edges[a].append(b)
    edges[b].append(a)

# 各ノードの点数
for q in range(Q):
    p, x = list(map(int, input().split()))
    nodes[p] += x

# 各ノードを通ったことを覚えておく
flags = [False]*(N+1)

# ルートから探索
cr.append(1)
flags[1] = True

# ルートから葉っぱまで点数を行き渡らせる
while cr:
    c = cr.popleft()
    flags[c] = True

    # 続く幹がないならば飛ばす
    if not edges[c]:
        continue

    # 幹に点数を与え、幹を伸ばす
    pts = nodes[c]
    for trunk in edges[c]:
        if not flags[trunk]:
            flags[trunk] = True
            nodes[trunk] += pts
            cr.append(trunk)

print(*nodes[1:])
