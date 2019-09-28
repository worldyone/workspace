S, a, b = list(map(int, input().split()))
a,b = (a-S-10)//1010+1,(b-S)//1010
print("BA"[a>b],min(a*1010,b*1010+10)+S)
