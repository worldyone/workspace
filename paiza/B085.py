N = int(input())
d1_idx = abs(N) * 2 - int(N > 0)

d2_bin_len = len(bin(N + [1, -1][int(N > 0)])) - \
    2 if abs(N) != 1 else (-N + abs(N)) // 2
d2_idx = d2_bin_len + 2

d1 = d1_idx * (d1_idx + 1) // 2
if N > 0:
    d2 = 2 ** d2_idx - 4 + N
elif N < 0:
    d2 = 2**d2_idx - 4 - 2**d2_bin_len - N
else:
    d2 = 0

print(d1, d2)
