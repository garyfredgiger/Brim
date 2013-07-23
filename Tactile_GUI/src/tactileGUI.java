import java.awt.Container;
import java.awt.Font;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

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
	
	JTextField searchbox;
	File image_lib;
	
	public tactileGUI(){
		
		right = new JPanel();
		left = new JPanel();
		
		search = new JButton();
		browse = new JButton();
		generate = new JButton();
		searchbox = new JTextField();
		next = new JButton();
		pick_one = new JButton();
		
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
		searchbox.setFont(new Font("Arial", Font.BOLD,20));
		
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
		if (e.getSource() == browse){
			
			final JFileChooser fc = new JFileChooser();
            image_lib = new File("C:\\Users\\Poornima\\Pictures");
            fc.setCurrentDirectory(image_lib);
            fc.showOpenDialog(this);
            try {
				Runtime.getRuntime().exec("explorer.exe C:\\Users\\Poornima\\Pictures");
			} catch (IOException e1) {
				System.out.println("error");
				e1.printStackTrace();
			}
            
		}
		if (e.getSource() == generate){
			try {
				Runtime.getRuntime().exec("python yourapp.py");
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
}