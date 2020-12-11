# N <= 30 ならば (3,4,5), (5,12,13), (7,24,25)の倍数しかないはず
# と思ったけど、そんなことなかった。
def pm(a, b):
    return min(M // a, N // b) + min(M // b, N // a)


M, N = map(lambda x: int(x) - 1, input().split())
count = 0
count += pm(3, 4)
count += pm(5, 12)
count += pm(7, 24)
count += pm(8, 15)
count += pm(20, 21)

print(count)
