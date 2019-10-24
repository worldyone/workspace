import math
N = int(input())
li = [list(map(int, input().split())) for _ in range(N)]

ans = -1
for i in li:
    for j in li:
        ans = max(ans, (i[0]-j[0])**2 + (i[1]-j[1])**2)

print(math.sqrt(ans))
