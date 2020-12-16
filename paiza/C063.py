N = int(input())
blossoms = [0 for _ in range(60)]
for n in range(N):
    bloom_days = sum(map(int, input().split()))
    blossoms[bloom_days] += 1

print(blossoms.index(max(blossoms)))
