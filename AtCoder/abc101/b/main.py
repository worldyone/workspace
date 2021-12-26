#!/usr/bin/env python3

def main():
    strN = input()
    N = int(strN)
    S = sum([int(n) for n in strN])

    if N % S == 0:
        print("Yes")
    else:
        print("No")

main()
