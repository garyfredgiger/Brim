#Select a file for opening:
import Tkinter, tkFileDialog
import sys

dirname = sys.argv[1]

myFormats = [
    ('Windows Bitmap','*.bmp'),
    ('Portable Network Graphics','*.png'),
    ('JPEG / JFIF','*.jpg'),
    ('Any Image',('*.bmp','*.png','*.jpg')),
    ('Any file', '*')
    ]

root = Tkinter.Tk()
root.withdraw()

filename = tkFileDialog.askopenfilename(initialdir = dirname ,filetypes=myFormats,title='Choose a file')
print filename

