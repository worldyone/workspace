import chardet

with open('in.txt', mode = 'rb') as f:
  print(chardet.detect(f.read()))
