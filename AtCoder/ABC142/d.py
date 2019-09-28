def gcd(a, b):
    if b == 0:
        return a
    return gcd(b, a%b)

A, B = map(int, input().split())
if A > B:
    A, B = B, A
c = gcd(A, B)
li = [1]

for i in range(2, c+1):
    if i > c:
        break
    if c%i == 0:
        li.append(i)
        while c%i == 0:
            c = c//i

print(len(li))
