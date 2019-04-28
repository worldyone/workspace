N = int(input())
G = input()
flag = True

for i in range(N):
    s = input()
    if G in s:
        print(s)
        flag = False

if flag:
    print("None")
