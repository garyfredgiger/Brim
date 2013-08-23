#Select a file for opening:
import Tkinter, tkFileDialog
import sys

myFormats = [
    ('Any file', '*'),
    ('Windows Bitmap','*.bmp'),
    ('Portable Network Graphics','*.png'),
    ('JPEG / JFIF','*.jpg'),
    ('Any Image',('*.bmp','*.png','*.jpg')),
    ]

root = Tkinter.Tk()
root.withdraw()

filename = tkFileDialog.askopenfilename(initialdir = sys.argv[1] ,filetypes=myFormats,title='Choose a file')
print filename

