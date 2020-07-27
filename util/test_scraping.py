import urllib.request
from bs4 import BeautifulSoup

url = 'https://www.yahoo.co.jp/'
ua = 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_3) '\
     'AppleWebKit/537.36 (KHTML, like Gecko) '\
     'Chrome/55.0.2883.95 Safari/537.36 '

req = urllib.request.Request(url, headers={'User-Agent': ua})
html = urllib.request.urlopen(req)
soup = BeautifulSoup(html, "html.parser")
topicsindex = soup.find('div', attrs={'class': 'topicsindex'})
topics = soup.find_all('li')

for topic in topics:
    #     print(topic.find('a').contents[0])
    print(topic.text)
