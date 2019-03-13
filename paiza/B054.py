S1, S2 = input().split()

n1, n2 = 0, 0
for s in S1:
    n1 *= 5
    n1 += ord(s) - ord('A')
for s in S2:
    n2 *= 5
    n2 += ord(s) - ord('A')

ans_10 = n1+n2
ans = ""

if ans_10 == 0:
    ans = "A"

while ans_10 != 0:
    ans += chr(ord('A')+(ans_10%5))
    ans_10 //= 5

print(ans[::-1])