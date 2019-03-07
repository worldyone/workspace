A,B = map(int, input().split())
ans = ""

while (A+B != 0):
    ans += str((A+B)%10)
    A, B = A//10, B//10

print(ans[::-1])