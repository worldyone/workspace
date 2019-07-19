import chardet

with open("in.txt", "rb") as f:
  print(chardet.detect(f.read()))
