print("NOGK"[list(map(lambda a, b, c: a < b * c, *
                      list(map(lambda x: [int(x)], input().split()))))[0]::2])
