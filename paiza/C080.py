N, Y = map(int, input().split())
M = int(input())
logs = map(int, input().split())
x, y = 0, 0
i = 0

for m in logs:
    if m == i + 1:
        x += 1
    else:
        y += 1
    i = (i + 1) % N

if y < Y:
    print(x * 1000)
else:
    print(-1)
