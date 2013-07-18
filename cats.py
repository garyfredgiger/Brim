import urllib
import json
import urllib2
from HTMLParser import HTMLParser
import os
import copy
from PIL import Image

cat_urls = []

class MyHTMLParser(HTMLParser):
    def handle_starttag(self, tag, attrs):
        #print "Encountered a start tag:", tag
        if (tag == "img"):
            print "I thought I saw a pussycat!"
            #print "you can see it at: "+ attrs[2][1]
            cat_urls.append(attrs[2][1])
              
"""
url1 = "https://www.google.com/search?biw=1309&bih=704&sei=bsHjUbvaEILqrQeA-YCYDw&tbs=itp:lineart&tbm=isch&"
query = "cats" #raw_input ( 'Query: ' )
query = urllib.urlencode ( { 'q' : query } )
print url1+query
req = urllib2.Request(url1 + query, headers ={'User-Agent':'Chrome'})
response = urllib2.urlopen (req).read()
#print response
print "got response"

parser = MyHTMLParser()
parser.feed(response)

print "looking at "+ cat_urls[0]
print cat_urls
req_cat = urllib2.Request(cat_urls[0], headers ={'User-Agent':'Chrome'})
response_cat = urllib2.urlopen (req_cat).read()
print response_cat
fd = open("testing2.jpg","wb")
fd.write(response_cat)
fd.close()

#(upper_lx, upper_ly, lower_rx, lower_ry) = image_data.getbbox()
"""
def isNotBlank(tup):
    return (tup != (255,255,255))

def makeArray(f_name):
    image_data = Image.open(f_name)
    (width, height) = image_data.size
    img_arr = [[(0,0) for i in xrange(0, width)] for j in xrange(0, height)]
    for row in xrange(height):
        for col in xrange(width):
            if (isNotBlank(image_data.getpixel((col,row)))):
                img_arr[row][col] = (1,0) # still hasn't been "visited"
    return img_arr

def noFill(tup):
    if (tup == (1,0)): # something we're interested in
        return True
    else:
        return False

seen_cells = []
#8 dimensional floodfill
def floodFill(arr,(r,c)):
    if (arr[r][c][1] == 1):
        #print "FOUND"
		return arr 
    if (arr[r][c][0] == 0):
        #print "don't care"
        return arr
    w = len(arr[0])
    h = len(arr)
    #print x,y
    up = (r,max(0,c-1))
    up_l = (max(0, r-1), max(0,c-1))
    left = (max(0,r-1), c)
    down_l = (max(0,r-1), min(w,c+1))
    down = (r, min(w,c+1))
    down_r = (min(h,r+1),min(w, c+1))
    right = (min(h, r+1), c)
    up_r = (min(h, r+1), max(0,c-1))
    #vals = map(lambda (r,c): arr[r][c], arr)
    #if (False not in (map(lambda x: noFill(x), vals))):
    #    return
    
    #print seen_cells

    #print "changing " , arr[x][y], x,y
    arr[r][c] = (1,1) #mark as seen
    assert(arr[r][c] == (1,1))
    seen_cells.append((r,c))
    floodFill(arr,up)
    floodFill(arr,up_l)
    floodFill(arr,left)
    floodFill(arr,down_l)
    floodFill(arr,down)
    floodFill(arr,down_r)
    floodFill(arr,right)
    floodFill(arr,up_r)
    return arr
    

#takes a picture and counts the number of distinct components
def countComponents(f_name):
    arr = makeArray(f_name)
    #print arr
    num_rows = len(arr)
    num_cols = len(arr[0])
    num_comps = 0
    for row in xrange(num_rows):
        for col in xrange(num_cols):
            if noFill(arr[row][col]):
                #print "flooding"
                arr = floodFill(arr,(row,col)) #flood fill
                if (arr == []):
                    print "returning"
                    return False
                #print ((1,1) in arr)
                num_comps += 1
				#print row, col
            else:
                pass
    return num_comps

print countComponents("16_dot.jpg")
