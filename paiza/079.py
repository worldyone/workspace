N, M = map(int, input().split())
li = [0 for _ in range(M)]

for n in range(N):
    number = int(input()) - 1
    li[number] += 1
    if all(li):
        print(n + 1)
        break
else:
    print("unlucky")
