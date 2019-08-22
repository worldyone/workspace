N = int(input())
l = list(map(int, input().split()))

rev_sum = 0
for ll in l:
    rev_sum += 1/ll

ans = 1/rev_sum

print(ans)
