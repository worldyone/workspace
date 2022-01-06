#!/usr/bin/env python3

def main():
    a, b, c = map(int, input().split())
    a_b, b_c, c_a = abs(a-b), abs(b-c), abs(c-a)
    candidates = [a_b+b_c, b_c+c_a, c_a+a_b]
    print(min(candidates))

main()

