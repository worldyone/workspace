import numpy as np


def get_comp_score(n):
    while(len(n) != 1):
        n = n[1:] + n[:-1]
        n = n % 101
    return n[0]


s, t = input().split()
ns = [ord(s) - ord('`') for s in s]
nt = [ord(t) - ord('`') for t in t]

anst = np.array(ns + nt)
ants = np.array(nt + ns)

ans = max(get_comp_score(anst), get_comp_score(ants))
print(ans)
