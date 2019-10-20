import itertools

N = int(input())
li = list(map(int, input().split()))

ans = 0

for a,b,c in itertools.combinations(li, 3):
    if (a < b+c) and (b < c+a) and (c < a+b):
        ans += 1

print(ans)
