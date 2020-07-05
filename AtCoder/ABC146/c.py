A, B, X = list(map(int, input().split()))

d = len(str(X))

for i in range(d):
    a = (X-i*B)//A
    if len(str(a)) <= i:
        print(min(max(0,a),10**9))
        break
else:
    print(0)
