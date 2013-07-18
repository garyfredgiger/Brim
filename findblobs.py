from SimpleCV import *

lakeimg = Image('http://i.stack.imgur.com/ku8F8.jpg') #load this image from web, or could be locally if you wanted.
invimg = lakeimg.invert() #we invert because blobs looks for white blobs, not black
lakes = invimg.findBlobs() # you can always change parameters to find different sized blobs
if lakes: lakes.draw() #if it finds blobs then draw around them
invimg.show() #display the image
