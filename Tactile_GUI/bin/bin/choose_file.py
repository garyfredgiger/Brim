#Select a file for opening:
import Tkinter, tkFileDialog

myFormats = [
    ('Windows Bitmap','*.bmp'),
    ('Portable Network Graphics','*.png'),
    ('JPEG / JFIF','*.jpg'),
    ('Any Image',('*.bmp','*.png','*.jpg')),
    ('Any file', '*')
    ]

root = Tkinter.Tk()
root.withdraw()

filename = tkFileDialog.askopenfilename(initialdir = 'C:\\',filetypes=myFormats,title='Choose a file')
print filename

