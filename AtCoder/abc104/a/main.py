#!/usr/bin/env python3

def main():
    N = map(int, open(0).read().split())
    if N < 1200:
        print("ABC")
    elif N < 2800:
        print("ARC")
    else:
        print("AGC")


main()

