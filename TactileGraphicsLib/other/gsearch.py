import urllib
import json
import urllib2
from HTMLParser import HTMLParser
import os

want_cat = False
cat_urls = []

class MyHTMLParser(HTMLParser):
    def handle_starttag(self, tag, attrs):
        #print "Encountered a start tag:", tag
        if (tag == "img"):
            print "I thought I saw a pussycat!"
            #print "you can see it at: "+ attrs[2][1]
            cat_urls.append(attrs[2][1])
            want_cat = True

    def handle_data(self, data):
        #print data
        if want_cat:
            print "Encountered some data  :", data


url1 = "https://www.google.com/search?biw=1309&bih=704&sei=bsHjUbvaEILqrQeA-YCYDw&tbm=isch&"
query = raw_input ( 'Query: ' )
query = urllib.urlencode ( { 'q' : query } )
print url1+query
req = urllib2.Request(url1 + query, headers ={'User-Agent':'Chrome'})
response = urllib2.urlopen (req).read()
#print response
print "got response"

parser = MyHTMLParser()
parser.feed(response)

print "looking at "+ cat_urls[0]
req_cat = urllib2.Request(cat_urls[0], headers ={'User-Agent':'Chrome'})
response_cat = urllib2.urlopen (req_cat).read()
print response_cat
fd = open("testing.jpg","wb")
fd.write(response_cat)
fd.close()