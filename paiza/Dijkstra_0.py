h, w = map(int, input().split())
fields = [list(map(int, input().split())) for _ in range(h)]
costs = 0

x, y = 0, 0
steps = [[1, 0], (0, 1), (1, 0), (0, -1), (-1, 0)]

for step in steps:
    x += step[0]
    y += step[1]
    costs += fields[y][x]

print(costs)
