from functools import reduce
N = int(input())
li = sorted(list(map(int, input().split())))

ans = reduce(lambda x,y: (x+y)/2, li)
print(ans)
