at, df, ag = map(int, input().split())
N = int(input())

flg = False
for _ in range(N):
    monster = input().split()
    name = monster[0]
    minat, maxat, mindf, maxdf, minag, maxag = map(int, monster[1:])
    if minat <= at <= maxat and mindf <= df <= maxdf and minag <= ag <= maxag:
        flg = True
        print(name)

if not flg:
    print("no evolution")
