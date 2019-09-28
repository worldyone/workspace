N = int(input())
A = map(int, input().split())

li = list(zip(A, range(1, N+1)))
li = sorted(li)
li = list(zip(*li))[1]

print(*li)
