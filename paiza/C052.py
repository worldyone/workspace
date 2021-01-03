H, W = map(int, input().split())
dx, dy = map(int, input().split())

ans = abs(H * dy) + abs(W * dx) - abs(dx * dy)
print(ans)
