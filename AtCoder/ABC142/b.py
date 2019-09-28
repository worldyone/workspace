N, K = map(int, input().split())
li = map(int, input().split())

ans = len(list(filter(lambda x: x>=K, li)))
print(ans)
