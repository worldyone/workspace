N = int(input())
li = list(map(int, input().split()))
li = sorted(li, reverse=True)

ans = sum(li[::2]) - sum(li[1::2])
print(ans)
