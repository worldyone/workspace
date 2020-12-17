T, x, y = map(int, input().split())
max_x = x
for t in range(T):
    a, b = map(int, input().split())
    x, y = x + a, y + b
    max_x = max(x, max_x)
    if y <= 0:
        break

print(max_x)
