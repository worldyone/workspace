H, W = map(int, input().split())
fs = [list(map(int, input().split())) for h in range(H)]
fs_s = [[0] * W for h in range(H)]

queue = []
x, y = 0, 0
goal_x, goal_y = W - 1, H - 1
count = 1

queue.append((x, y, count))
while(queue):
    tx, ty, tcount = queue.pop()
    if tx == goal_x and ty == goal_y:
        break

    fs_s[ty][tx] = 1

    for nx, ny in [(0, -1), (0, +1), (-1, 0), (+1, 0)]:
        fx, fy = tx + nx, ty + ny
        if fx < 0 or fx >= W or fy < 0 or fy >= H or fs[fy][fx] == 1 or fs_s[fy][fx] == 1:
            continue
        else:
            queue.append((fx, fy, tcount + 1))

print(tcount)
