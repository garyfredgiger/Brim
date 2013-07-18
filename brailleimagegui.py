#Select a file for opening:
import Tkinter,tkFileDialog
import subprocess

def chooseIm():
    myFormats = [
    ('Windows Bitmap','*.bmp'),
    ('Portable Network Graphics','*.png'),
    ('JPEG / JFIF','*.jpg'),
    ('Any Image',('*.bmp','*.png','*.jpg')),
    ('Any file', '*')
    ]
    filename = tkFileDialog.askopenfilename(parent=root,filetypes=myFormats,title='Choose a file')
    print outFileName
#    if filename != None:
#        subprocess.call("python filter.py "+filename +" out.txt 1", shell=True)

root = Tkinter.Tk()

browseButton = Tkinter.Button(root, text = "Browse", command = chooseIm)
browseButton.pack()

root.mainloop()