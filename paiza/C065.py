N = int(input())
conditions = []
for _ in range(N):
    line = input().split()
    a, b = line[0], int(line[1])
    conditions.append([a, b])

for i in range(1, 101):
    for op, nm in conditions:
        if op == ">":
            if i <= nm:
                break
        if op == "<":
            if i >= nm:
                break
        if op == "/":
            if i % nm != 0:
                break
    else:
        print(i)
        break
