S, T = input(), input()

def func(S, T):

    S_2 = S+S
    len_s = len(S)
    merge_S = list(set(S))
    merge_T = list(set(T))

    for t in merge_T:
        if t not in merge_S:
            return -1

    alp2num = {t: merge_T.index(t) for t in merge_T}
    dis_S = [[-1]*len(merge_T) for _ in range(len_s)]
    pt = S.index(T[0])
    ans = pt+1

    for t in T[1:]:
        it = alp2num[t]
        if dis_S[pt][it] == -1:
            dis_S[pt][it] = S_2[pt+1:].index(t) + 1
        ans += dis_S[pt][it]
        pt = (pt + dis_S[pt][it]) % len_s

    return ans

print(func(S,T))
