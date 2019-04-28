input_line = input().split()

X = int(input_line[0])
P = float(input_line[1])

#print(X, P)

#合計金額を表す
ans = 0

while(X != 0):
    ans += X
    X = int(X*(100-P)/100)

print(ans)
