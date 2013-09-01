import java.awt.Image;
import java.io.File;

import javax.swing.Icon;
import javax.swing.ImageIcon;
import javax.swing.filechooser.FileView;


class ThumbNailView extends FileView{
 public Icon getIcon(File f){
  Icon icon=null;
  if(isImageFile(f.getPath())){
   icon=createImageIcon(f.getPath(),null);
  }
  else {
   icon = createImageIcon("folder.jpg",null);
  }
  return icon;
 }
 
 @Override
 public String getName(File file){
     return null;
 }
 
 private ImageIcon createImageIcon(String path,String description) {
  if (path != null) {
   ImageIcon icon=new ImageIcon(path);
   Image img = icon.getImage(); 
   Image newimg = img.getScaledInstance( 200, 200,  java.awt.Image.SCALE_SMOOTH ) ;
  
   return new ImageIcon(newimg);
  } else {
   System.err.println("Couldn't find file: " + path);
   return null;
   }
}

private boolean isImageFile(String filename){
	 String suffix = getSuffix(filename.toLowerCase());
	    boolean isImage = false;

	    if(suffix != null) {
	      isImage = suffix.equals("gif") || 
	            suffix.equals("bmp") ||
	              suffix.equals("jpg") ||
	              	suffix.equals("png") ||
	              	suffix.equals("jpeg");
	    }
	    return isImage;//return true if this is image
}
private String getSuffix(String filestr) {
    String suffix = null;
    int i = filestr.lastIndexOf('.');

    if(i > 0 && i < filestr.length()) {
      suffix = filestr.substring(i+1).toLowerCase();  
    }
    return suffix;
  }
}