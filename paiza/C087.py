N = input()

while(N != N[::-1]):
    a = int(N)
    b = int(N[::-1])
    N = str(a + b)

print(N)
