#!/usr/bin/env python3

def main():
    N = int(input())
    A = list(map(int, input().split()))

    print(max(A) - min(A))

main()

