import urllib
import json
import urllib2
from HTMLParser import HTMLParser
import os
import copy
from PIL import Image
import sys
import math

sys.setrecursionlimit(30000)

cat_urls = []

#dictionary of the pixel maxes seen on a call of floodfill
bounding_box = {"top": "none", "bottom": "none", "left": "none", "right":"none"} 

class MyHTMLParser(HTMLParser):
    def handle_starttag(self, tag, attrs):
        #print "Encountered a start tag:", tag
        if (tag == "img"):
        	#print "I thought I saw a pussycat!"
        	#print "you can see it at: "+ attrs[2][1]
        	cat_urls.append(attrs[2][1])



# pulls photos off google images and puts them in a folder <query>
def pullPhotos(query):
	print "looking for", query
	url1 = "https://www.google.com/search?biw=1309&bih=704&sei=bsHjUbvaEILqrQeA-YCYDw&tbs=itp:lineart&tbm=isch&"
	query2 = urllib.urlencode ( { 'q' : query } )
	
	req = urllib2.Request(url1 + query2, headers ={'User-Agent':'Chrome'})
	response = urllib2.urlopen (req).read()
	parser = MyHTMLParser()
	parser.feed(response)
	os.mkdir(query) # make directory to put them in
	
	for i in xrange(10):
		req_cat = urllib2.Request(cat_urls[i], headers ={'User-Agent':'Chrome'})
		response_cat = urllib2.urlopen (req_cat).read()
		name = query + os.sep + query+str(i)+".jpg"
		fd = open(name,"wb")
		fd.write(response_cat)
		fd.close()
		print name, "written", "complexity is ", countComponents(name)
		

	print "done"


def isNotBlank(tup):
	threshold = 180 # out of 255
	return ((tup[0] < threshold) or (tup[1] < threshold) or (tup[2] < threshold))

def lonePixel((r,c), img):
	w, h = img.size
	up = isNotBlank(img.getpixel((r,max(0,c-1))))
	up_l = isNotBlank(img.getpixel((max(0, r-1), max(0,c-1))))
	left = isNotBlank(img.getpixel((max(0,r-1), c)))
	down_l = isNotBlank(img.getpixel((max(0,r-1), min(w,c+1))))
	down = isNotBlank(img.getpixel((r, min(w,c+1))))
	down_r = isNotBlank(img.getpixel((min(h,r+1),min(w, c+1))))
	right = isNotBlank(img.getpixel((min(h, r+1), c)))
	up_r = isNotBlank(img.getpixel((min(h, r+1), max(0,c-1))))
	return not (up or up_l or left or down_l or down or down_r or right or up_r)


def makeArray(f_name):
	image_data = Image.open(f_name)
	(width, height) = image_data.size
	img_arr = [[(0,0) for i in xrange(0, width)] for j in xrange(0, height)]
	for row in xrange(height-1):
		for col in xrange(width-1):
			
			if (isNotBlank(image_data.getpixel((col,row))) and 
				not lonePixel((col,row), image_data)):
				img_arr[row][col] = (1,0) # still hasn't been "visited"
				#print "found pixel"
				#print image_data.getpixel((col,row))
	return img_arr

def noFill(tup):
	if (tup == (1,0)): # something we're interested in
		return True
	else:
		return False

#modifies the global if necessary
def addPoint((r,c)):
	if bounding_box["bottom"] == "none":
		bounding_box["bottom"] = r
	if bounding_box["top"] == "none":
		bounding_box["top"] = r
	if bounding_box["left"] == "none":
		bounding_box["left"] = c
	if bounding_box["right"] == "none":
		bounding_box["right"] = c
	if (r > bounding_box["bottom"]):
		bounding_box["bottom"] = r
	if (r < bounding_box["top"]):
		bounding_box["top"] = r
	if (c > bounding_box["right"]):
		bounding_box["right"] = c
	if (c < bounding_box["left"]):
		bounding_box["left"] = c
	
	return

#8 dimensional floodfill
def floodFill(arr,(r,c)):
	if (arr[r][c][1] == 1): #already seen
		return arr 
	if (arr[r][c][0] == 0): #whitespace
		return arr
	addPoint((r,c))
	w = len(arr[0])
	h = len(arr)
	up = (r,max(0,c-1))
	up_l = (max(0, r-1), max(0,c-1))
	left = (max(0,r-1), c)
	down_l = (max(0,r-1), min(w,c+1))
	down = (r, min(w,c+1))
	down_r = (min(h,r+1),min(w, c+1))
	right = (min(h, r+1), c)
	up_r = (min(h, r+1), max(0,c-1))
	
	arr[r][c] = (1,1) #mark as seen
	
	floodFill(arr,up)
	floodFill(arr,up_l)
	floodFill(arr,left)
	floodFill(arr,down_l)
	floodFill(arr,down)
	floodFill(arr,down_r)
	floodFill(arr,right)
	floodFill(arr,up_r)
	return arr

def avg(a):
	return 1.0 * sum(a) / len(a)

def standardDev(a):
	mean = avg(a)
	return math.sqrt(sum((x-mean) ** 2 for x in a) / len(a))

def indexMax(a):
	maxIndex = 0
	for i in xrange(len(a)):
		if (a[i] > a[maxIndex]):
			maxIndex = i
	#print maxIndex, a
	return maxIndex

def indexMin(a):
	minIndex = 0
	for i in xrange(len(a)):
		if (a[i] < a[minIndex]):
			minIndex = i
	return minIndex

#chooses the "best" photo from a folder of 3
def chooseBestPicture(d):

	#get the stats from the pictures you're considering
	max_consider = 5
	files = os.listdir(d)[0:max_consider]
	files = map(lambda f: d + os.sep + f, files) #add directory to filename
	stats = map(lambda f : countComponents(f), files)
	densities = map(lambda i : stats[i][1], range(max_consider))
	components = map(lambda i: stats[i][0], range(max_consider))
	disjoints = map(lambda i: stats[i][2], range(max_consider))

	#once you have the stats to consider, get rid of the highest one from
	#the components, the highest one from the densities, only way to override the rank
	#is to be more than 1 std dev below the mean
	#calculate the stats
	mean_c = avg(components)
	stdDev_c = standardDev(components)
	mean_d = avg(densities)
	stdDev_d = avg(densities)

	#get rid of the maxs
	files.pop(indexMax(components))
	densities.pop(indexMax(components))
	disjoints.pop(indexMax(components))
	files.pop(indexMax(densities))

	#files.pop(indexMax(disjoints))
	#densities.pop(indexMax(disjoints))
	#calculate the relative differences
	return files[disjoints.index(min(disjoints))]

	"""
	dists = map(lambda x : (mean_d - x > stdDev_d), densities)

	if True not in dists:
		if (indexMin(densities) == indexMin(components)):
			print "overridden", d
			return files[indexMin(densities)]
			
		return files[0]
	else:
		return files[dists.index(True)]

 	"""

#takes in an array and gives back the percentage of non-white pixels
def getFillStats(arr):
	num_rows = len(arr)
	num_cols = len(arr[0])
	total = 0
	for row in xrange(num_rows):
		total += arr[row].count((1,0)) #those marked as filled
	return ((1.0 * total) / (num_rows * num_cols))

#guarenteed that b2 is larger in area than b1
def areaContained(b1, b2):
	[(b1xl, b1yu),(b1xr, b1yl)] = b1
	[(b2xl, b2yu),(b2xr,b2yl)] = b2

	#first check if there is any overlap at all
	if ((b1xl < b2xl) and (b1xr < b2xr)): 
		if (((b1yu < b2yu) and (b1yl < b2yl)) or
			((b1yu > b2yu) and (b1yl > b2yl))):
			#print "no overlap", b1, b2
			return 0
	if ((b1xl > b2xl) and (b1xr > b2xr)):
		if (((b1yu < b2yu) and (b1yl < b2yl)) or
			((b1yu > b2yu) and (b1yl > b2yl))):
			#print "no overlap", b1, b2
			return 0

	#calculate the general overlap
	x_left = max(b1xl,b2xl)
	y_left = max(b1yu,b2yu)
	x_right = min(b1xr, b2xr)
	y_right = min(b1yl,b2yl)
	#print abs((x_right - x_left) * (y_right - y_left))
	return abs((x_right - x_left) * (y_right - y_left))



def find_overlap(box_arr):
	#by_size = lambda x: (x[1][0]) * (x[0][1])
	
	#want to maximize the overlap
	#sort by size
	#print box_arr
	total_overlap = 0
	num_boxes = len(box_arr)
	sorted_arr = sorted(box_arr, key= lambda x : 
								(x[1][0] - x[0][0]) * (x[1][1] - x[0][1]))
	for i in xrange(len(sorted_arr)):
		overlaps = map(lambda x: areaContained(box_arr[i],box_arr[x]), range(i,num_boxes))
		if min(overlaps) == 0:
			total_overlap += 1
	#Sprint total_overlap
	return total_overlap

#takes a picture and counts the number of distinct components
def countComponents(f_name):
	arr = makeArray(f_name)
	percent_filled = getFillStats(arr)
	global bounding_box #refer to the external one
	num_rows = len(arr)
	num_cols = len(arr[0])
	num_comps = 0
	total_pix = 0
	boxes = []
	for row in xrange(num_rows):
		total_pix += arr[row].count((1,0)) + arr[row].count((1,1))
		for col in xrange(num_cols):
			if noFill(arr[row][col]):
				arr = floodFill(arr,(row,col)) #follow that line
				#print bounding_box
				boxes.append(((bounding_box["left"], bounding_box["top"]),
							  (bounding_box["right"],bounding_box["bottom"])))
				bounding_box = {"top": "none", "bottom": "none", "left": "none", "right":"none"} 
				
				if (arr == []):
					print "returning"
					return False
				#print ((1,1) in arr)
				num_comps += 1
			else:
				pass
			#reset the bounding box dictionary
	overlap = 1.0 * find_overlap(boxes) 
	print "overlap proportion is ", overlap
	return num_comps, percent_filled, overlap

"""
for animal in ["dog","hippo","koala","mouse","rat","elephant","snake","lion","horse"]: 
#for animal in ["cat"]:
	cat_urls = []
	pullPhotos(animal)



"""
"""
print countComponents("elephant1.jpg")
print countComponents("elephant2.jpg")
print countComponents("elephant3.png")
print countComponents("paint.jpg")
"""

for animal in ["dog","hippo","koala","mouse","rat","elephant","snake","lion","horse","cat"]:
	print chooseBestPicture(animal)

#print countComponents("paint_test.jpg")
#print countComponents("cat1.jpg")
#print countComponents("hippo0.jpg")
#print countComponents("hippo2.jpg")