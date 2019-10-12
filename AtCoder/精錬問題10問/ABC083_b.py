def SumOfDigits(s):
    return sum(map(int, s))

N, A, B = list(map(int, input().split()))

ans = 0
for i in range(N+1):
    s = SumOfDigits(str(i))
    if A <= s <= B:
        ans += i

print(ans)
