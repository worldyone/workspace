N = int(input())
pairs = {chr(i): [0, 0] for i in range(ord('A'), ord('Z') + 1)}

for n in range(N):
    color, LR = input().split()
    pairs[color][LR == 'R'] += 1

count = 0
for color, pair in pairs.items():
    count += min(pair)

print(count)
