n, s, p = map(int, input().split())
start = s * (p - 1) + 1
end = min(n + 1, s * p + 1)
if start > n:
    print("none")
else:
    seq = map(str, range(start, end))
    print(" ".join(seq))
