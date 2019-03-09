def keep_and_plus(sum_li, index):
    if index == N or sum_li >= S:
        global ans
        ans += sum_li >= S
        return sum_li >= S
    
    keep_and_plus(sum_li, index+1)
    keep_and_plus(sum_li + n[index], index+1)

S = int(input())
N = int(input())

n = []
for i in range(N):
    n.append(int(input()))
n = sorted(n)[::-1]

ans = 0
keep_and_plus(0, 0)

print(ans)