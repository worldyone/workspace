#!/usr/bin/env python3
import math as m

def main():
    N, K = map(int, input().split())
    A = list(map(int, input().split()))

    # 最小値がある場所から左端まで埋める回数と右端まで埋める回数を数える
    idx_min_A = A.index(min(A))

    ans = 0

    # 左側の計算
    ans += m.ceil(idx_min_A / (K - 1))

    # 右側の計算
    ans += m.ceil((N - idx_min_A - (ans * (K - 1) % max(1, idx_min_A)) - 1) / (K - 1))

    if N == K:
        ans = 1

    print(ans)


main()

