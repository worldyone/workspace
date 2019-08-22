from collections import deque

N, Q = list(map(int, input().split()))
edges = {n+1:[] for n in range(N)}
nodes = [0 for _ in range(N+1)]
cr = deque()

for n in range(N-1):
    a, b = list(map(int, input().split()))
    edges[a].append(b)

for q in range(Q):
    p, x = list(map(int, input().split()))
    cr.append(p)
    while len(cr):
        c = cr.popleft()
        nodes[c] += x
        cr.extend(edges[c])

print(*nodes[1:])
