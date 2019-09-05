S, a, b = list(map(int, input().split()))

while True:
    S += 10
    if S > a:
        print("B", S-10)
        break
    S += 1000
    if S > b:
        print("A", S-1000)
        break
