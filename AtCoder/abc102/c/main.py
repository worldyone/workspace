#!/usr/bin/env python3
import math as m

def main():
    N = int(input())
    A = list(map(int, input().split()))
    A_minus_index = [a - i for i, a in enumerate(A, start=1)]
    A_minus_index_sorted = sorted(A_minus_index)

    A_len = len(A)
    if A_len % 2 == 1:
        mean = A_minus_index_sorted[A_len // 2]
    else:
        mean = (A_minus_index_sorted[A_len // 2 - 1] + A_minus_index_sorted[A_len // 2]) // 2

    ans = sum(abs(a - mean) for a in A_minus_index_sorted)
    print(ans)

main()

