N = int(input())
H, W = map(int, input().split())

print(H * W % N)
