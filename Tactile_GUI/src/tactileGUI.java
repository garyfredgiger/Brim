import java.awt.Container;
import java.awt.Font;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.geom.AffineTransform;
import java.awt.image.AffineTransformOp;
import java.awt.image.BufferedImage;
import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

import javax.imageio.ImageIO;
import javax.swing.*;        
 
@SuppressWarnings("serial")
public class tactileGUI extends JFrame implements ActionListener {

	JPanel right;
	JPanel left;
	
	JButton search;
	JButton browse;
	JButton generate;
	JButton next;
	JButton pick_one;
	
	JLabel picLabel;
	JTextField searchbox;
	File image_lib;
	File fetchd_file;
	String searchstr;
	
	public tactileGUI(){
		
		searchstr = null;
		right = new JPanel();
		left = new JPanel();
		
		search = new JButton();
		browse = new JButton();
		generate = new JButton();
		searchbox = new JTextField();
		next = new JButton();
		pick_one = new JButton();
		
		image_lib = new File("C:\\Users\\Poornima\\Pictures");
		
		Container pane = this.getContentPane();
		pane.setLayout(null);
		left.setLayout(null);
		right.setLayout(null);

		left.setSize(150,800);
		left.setLocation(0,0);
		right.setSize(850,800);
		right.setLocation(150,0);
		
		search.setSize(100,70);
		search.setLocation(20,20);
		
		browse.setSize(100,70);
		browse.setLocation(20,140);
		
		generate.setSize(100,70);
		generate.setLocation(20,380);
	
		next.setSize(100,70);
		next.setLocation(20,650);
		
		pick_one.setSize(100,70);
		pick_one.setLocation(20,260);
		
		searchbox.setSize(900,40);
		searchbox.setLocation(20,20);
		searchbox.setFont(new Font("Arial",Font.BOLD,20));
		
		pane.add(left);
		pane.add(right);
				
		search.setText("Search");
		search.addActionListener(this);
		
		browse.setText("Browse");
		browse.addActionListener(this);
		
		generate.setText("Generate");
		generate.addActionListener(this);
		
		next.setText("Next");
		next.addActionListener(this);		
		
		pick_one.setText("Pick One");
		pick_one.addActionListener(this);
		
		left.add(search);
		left.add(browse);
		left.add(generate);
		left.add(next);
		left.add(pick_one);
		right.add(searchbox);
//		right.add(picLabel);
	}
    
	public static void main(String args[]){
		tactileGUI main_window = new tactileGUI();
		main_window.setTitle("Tactile GUI");
		main_window.setSize(1200, 800);
		main_window.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		main_window.setVisible(true);		
	}
	
	public void actionPerformed (ActionEvent e)
	  {  
		if (e.getSource() == search){			
			searchstr = searchbox.getText();
			if (searchstr != null){
				System.out.println("Searching for "+searchstr);
				fetchd_file = search_file(image_lib, searchstr);
				System.out.println("Searched for "+searchstr);
				if (fetchd_file != null){
					System.out.println("Search: fetched a file");
				}
				System.out.println(fetchd_file);
				BufferedImage myPicture = null;
				BufferedImage scaledIM = null;
				try {
					myPicture = ImageIO.read(fetchd_file);
				} catch (IOException e1) {
					System.out.println(myPicture);
					e1.printStackTrace();
				}
				
				try {
					scaledIM = getScaledImage(myPicture,820,650);
				} catch (IOException e1) {
					e1.printStackTrace();
				}
				
				picLabel = new JLabel(new ImageIcon(scaledIM));	
				System.out.println("here");
				picLabel.setSize(820,650);
				picLabel.setLocation(20,70);
				right.add(picLabel);
			}
		}
		
		if (e.getSource() == browse){
            
            Process p1 = null;
			String s = null;
			String filename = null;
			try {
			  p1 = Runtime.getRuntime().exec("python C:\\Users\\Poornima\\Documents\\GitHub\\tactilegraphicslib\\choose_file.py " + image_lib);
			} catch (IOException e1) {
				System.out.println("error");
				e1.printStackTrace();
			}
			BufferedReader stdInput = new BufferedReader(new 
	        InputStreamReader(p1.getInputStream()));
	    
	        // read the output from the command
	        System.out.println("Here is the standard output of the command:\n");
	        try {
	        	while ((s = stdInput.readLine()) != null) {
				   filename = s;
				}
	        	System.out.println("Browse:fetched a file:" + filename);
			} catch (IOException e1) {
				e1.printStackTrace();
			}	        
		}
		if (e.getSource() == generate){

            Process p2 = null;
			String filename = null;
			try {
			  p2 = Runtime.getRuntime().exec("python C:\\Users\\Poornima\\Documents\\GitHub\\tactilegraphicslib\\cats.py " + "");
			} catch (IOException e1) {
				System.out.println("error");
				e1.printStackTrace();
			}
		}
	  }
	
	public static final File search_file(File rootDir, String fileName) {
	    File[] files = rootDir.listFiles();
	    List<File> directories = new ArrayList<File>(files.length);
	    for (File file : files) {
		    System.out.println(file.getName());
	        if (file.getName().toLowerCase().contains(fileName.toLowerCase())) {
	            return file;
	        } else if (file.isDirectory()) {
	            directories.add(file);
	        }
	    }

	    for (File directory : directories) {
	        File file = search_file(directory,fileName);
	        if (file != null) {
	            return file;
	        }
	    }
	    
	    return null;
	}
	
	public static BufferedImage getScaledImage(BufferedImage image, int width, int height) throws IOException {
	    int imageWidth  = image.getWidth();
	    int imageHeight = image.getHeight();

	    double scaleX = (double)width/imageWidth;
	    double scaleY = (double)height/imageHeight;
	    AffineTransform scaleTransform = AffineTransform.getScaleInstance(scaleX, scaleY);
	    AffineTransformOp bilinearScaleOp = new AffineTransformOp(scaleTransform, AffineTransformOp.TYPE_BILINEAR);

	    return bilinearScaleOp.filter(
	        image,
	        new BufferedImage(width, height, image.getType()));
	}
}