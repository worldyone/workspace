li = []
for i in range(3):
    li.append(int(input()))
X = int(input())

ans = 0

for i in range(li[0]+1):
    for j in range(li[1]+1):
        for k in range(li[2]+1):
            if i*500 + j*100 + k*50 == X:
                ans += 1

print(ans)
