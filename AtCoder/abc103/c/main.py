#!/usr/bin/env python3

def main():
    N = int(input())
    an = list(map(int, input().split()))
    print(sum(an) - N)


main()

