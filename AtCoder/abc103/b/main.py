#!/usr/bin/env python3

def main():
    S = input()
    T = input()

    for _ in S:
        if S == T:
            print("Yes")
            break
        S = S[-1] + S[0:-1]
    else:
        print("No")

main()

