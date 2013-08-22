import java.awt.Component;
import java.awt.Container;
import java.awt.Font;
import java.awt.FontFormatException;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.FocusEvent;
import java.awt.event.FocusListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.awt.geom.AffineTransform;
import java.awt.image.AffineTransformOp;
import java.awt.image.BufferedImage;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.List;

import javax.imageio.ImageIO;
import javax.sound.sampled.AudioFormat;
import javax.sound.sampled.AudioInputStream;
import javax.sound.sampled.AudioSystem;
import javax.sound.sampled.DataLine;
import javax.sound.sampled.LineUnavailableException;
import javax.sound.sampled.SourceDataLine;
import javax.swing.*;        
 
@SuppressWarnings("serial")
public class tactileGUI extends JFrame implements ActionListener, FocusListener, MouseListener{

	private File soundFile;
    private AudioInputStream audioStream;
    private AudioFormat audioFormat;
    private SourceDataLine sourceLine;

    static JPanel right_1;
	static JPanel right_2;
	static JPanel right_3;
	static JPanel right_4;
	static JPanel left;
	
	static JLabel picLabel;
	static JLabel dispLabel;
	static JLabel searchLabel;
	
	static String [] text = {"","",""};
	static int displaynum;
	static int size = 2;
	
	JRadioButton iscaption;
	
	JButton small;
	JButton medium;
	JButton large;
	JButton sel_size;
	
	JButton search;
	JButton browse;
	JButton generate;
	JButton next;
	JButton compare;
	JButton pick_one;
	JButton fselect;
	JButton print;
	
	Container pane;
	JRootPane rootpane;
	JEditorPane Pane_1,Pane_2,Pane_3;
	JFileChooser fc;
	@SuppressWarnings("rawtypes")
	JList flist;

	JTextField searchbox;
	
	File image_lib;
	File fetchd_file;
	
	String searchstr;
	String s = null;
	String filename = null;
	String tfcap = "true"; 
	
	public tactileGUI(){
		
		searchstr = null;
		right_1 = new JPanel();
		right_2 = new JPanel();
		right_3 = new JPanel();
		right_4 = new JPanel();
		left = new JPanel();
		
		search = new JButton("Search for the Image");
		browse = new JButton("Browse the Image Library");
		generate = new JButton("Generate the braille image preview");
		fselect = new JButton();
		next = new JButton("Next Image filter");
		compare = new JButton("Compare generated images");
		pick_one = new JButton("Choose one image from 5");
		print = new JButton("Select Image for printing");
		iscaption = new JRadioButton();
		sel_size = new JButton();
		small = new JButton("S");
		medium = new JButton("M");
		large = new JButton("L");
		
		search.getAccessibleContext().setAccessibleName("Search for the Image");
		browse.getAccessibleContext().setAccessibleName("Browse the Image Library");
		generate.getAccessibleContext().setAccessibleName("Generate the braille image preview");
		next.getAccessibleContext().setAccessibleName("Next Image filter");
		compare.getAccessibleContext().setAccessibleName("Compare generated images");
		pick_one.getAccessibleContext().setAccessibleName("Choose one image from 5");
		print.getAccessibleContext().setAccessibleName("Select the image for printing");
		
		search.setToolTipText("Search for the Image");
		browse.setToolTipText("Browse the Image Library");
		generate.setToolTipText("Generate the braille image preview");
		next.setToolTipText("Next Image filter");
		compare.setToolTipText("Compare generated images");
		pick_one.setToolTipText("Choose one image from 5");
		print.setToolTipText("Select the image for printing");
		
		searchbox = new JTextField();
		searchbox.setToolTipText("searchbox. Please type name of the object here");
		rootpane = this.getRootPane();
		Pane_1 = new JEditorPane();
		Pane_2 = new JEditorPane();
		Pane_3 = new JEditorPane();
		
		//Location of the image library
		image_lib = new File("C:\\Users\\Public\\Pictures");
		
		pane = this.getContentPane();
		pane.setLayout(null);
		GridLayout GL = new GridLayout(2,2);
		GL.setHgap(40);
		GL.setVgap(40);
		
		left.setLayout(null);
		right_1.setLayout(null);
		right_2.setLayout(null);		
		right_3.setLayout(GL);
		right_4.setLayout(null);
		
		left.setSize(150,800);
		left.setLocation(0,0);
		right_1.setSize(950,800);
		right_1.setLocation(150,0);
		right_2.setSize(950,800);
		right_2.setLocation(150,0);
		right_3.setSize(870,680);
		right_3.setLocation(190,40);
		right_4.setSize(870,680);
		right_4.setLocation(190,40);
		
		
		Pane_1.setSize(900,750);
		Pane_1.setLocation(0,0);
		
		iscaption.setSize(20,20);
		iscaption.setLocation(0,0);
		left.add(iscaption);
		
		search.setSize(120,70);
		search.setLocation(20,20);
		
		browse.setSize(120,70);
		browse.setLocation(20,100);
		
		sel_size.setSize(120,70);
		sel_size.setLocation(20,260);
		
		small.setSize(30,50);
		large.setSize(30,50);
		medium.setSize(30,50);
		
		small.setLocation(20,270);
		large.setLocation(110,270);
		medium.setLocation(65,270);
		
		small.setEnabled(false);
		large.setEnabled(false);
		medium.setEnabled(false);
		
		small.setVisible(false);
		large.setVisible(false);
		medium.setVisible(false);
		
		generate.setSize(120,70);
		generate.setLocation(20,340);	
		
		print.setSize(120,70);
		print.setLocation(20,660);	
		print.setVisible(false);
		print.setEnabled(false);

		compare.setSize(120,70);
		compare.setLocation(20,580);
		compare.setVisible(false); 
		
		next.setSize(120,70);
		next.setLocation(20,500);
		next.setVisible(false);

		fselect.setSize(0,0);
		fselect.setLocation(150,800);

		pick_one.setSize(120,70);
		pick_one.setLocation(20,180);

		searchbox.setSize(850,40);
		searchbox.setLocation(20,20);
		searchbox.setFont(new Font("Arial",Font.BOLD,20));
		
		//Creating the new Braille font from the font file
		try {
			Font.createFont(Font.TRUETYPE_FONT,new File("Braille.ttf"));
		} catch (FontFormatException e) {
			System.out.println("FontFormatException");
			e.printStackTrace();
		} catch (IOException e) {
			System.out.println("IOException");
			e.printStackTrace();
		}
		
		pane.add(left);
		pane.add(right_1);
				
		search.setText("SEARCH");
		search.addActionListener(this);
		search.addFocusListener(this);
		
		small.setText("<html>S</html>");
		small.addActionListener(this);
		small.addFocusListener(this);
		small.setFont(new Font("Arial",Font.PLAIN,15));
		
		large.setText("<html>L</html>");
		large.addActionListener(this);
		large.addFocusListener(this);
		
		medium.setText("<html>M</html>");
		medium.addActionListener(this);
		medium.addFocusListener(this);
		
		sel_size.setText("<html>SELECT IMAGE SIZE</html>");
		sel_size.addActionListener(this);
		sel_size.addFocusListener(this);
		
		browse.setText("<html>BROWSE THE LIBRARY</html>");
		browse.addActionListener(this);
		browse.addFocusListener(this);
		
		generate.setText("<html>GENERATE</html>");
		generate.addActionListener(this);
		generate.setVisible(false);
		generate.addFocusListener(this);
		
		print.setText("<html>Select for Print</html>");
		print.addActionListener(this);
		print.addFocusListener(this);
		
		compare.setText("COMPARE");
		compare.addActionListener(this);		
		compare.addFocusListener(this);
		
		next.setText("NEXT");
		next.addActionListener(this);		
		next.addFocusListener(this);
		this.setTitle("Tactile Graphics");
		
		searchbox.addFocusListener(this);
		
		pick_one.setText("<html>CHOOSE FROM 5</html>");	
		pick_one.addActionListener(this);
		pick_one.addFocusListener(this);
		
		this.addWindowListener( new WindowAdapter() {
		    public void windowOpened( WindowEvent e ){
		        searchbox.requestFocus();
		    }
		}); 
		
		right_1.add(searchbox);
		left.add(search);
		left.add(browse);
		left.add(generate);
		left.add(compare);
		left.add(next);
		left.add(pick_one);
		left.add(print);
		left.add(sel_size);
		left.add(small);
		left.add(large);
		left.add(medium);
		
		rootpane.setDefaultButton(search);
		searchbox.requestFocus();
	}
    
	public static void main(String args[]){
		tactileGUI main_window = new tactileGUI();
		main_window.setTitle("Tactile Graphics");
		main_window.setSize(1200, 800);
		main_window.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		main_window.setVisible(true);		
	}
	

	public void actionPerformed (ActionEvent e)
	  {  
		if (e.getSource() == fselect){
			fetchd_file = fc.getSelectedFile();
			if (fetchd_file!=null){
				pane.removeAll();
				pane.add(left);
				pane.add(right_1);
				pane.repaint();
				displayIM(fetchd_file);
			}
			else{
				System.out.println("whoa");
			}
		}
		if (e.getSource() == search){
			
			sel_size.setEnabled(true);
			sel_size.setVisible(true);
			
			small.setEnabled(false);
			large.setEnabled(false);
			medium.setEnabled(false);
			
			small.setVisible(false);
			large.setVisible(false);
			medium.setVisible(false);
			
			pane.removeAll();
			pane.add(left);
			pane.add(right_1);
			pane.repaint();
			searchstr = searchbox.getText();
			searchstr.replaceAll("\\W", "");
			if (searchstr != null){
				System.out.println("Searching for "+searchstr);
				fetchd_file = search_file(image_lib, searchstr);
				if (fetchd_file != null){
					System.out.println("Search: fetched a file");
				}
				else{
					Process p2 = null;
					Process p5 = null;
					
					try {
					    URL url = new URL("http://www.google.com");
					    URLConnection connection = url.openConnection();

					    if(connection.getContentLength() == -1){
					          System.out.println("Failed to verify connection");
					          playSound("noimage.wav");
					          searchbox.requestFocusInWindow();
					          searchstr = null;
					          return;
					    }
					  } 
					  catch (IOException e1) { 
					      e1.printStackTrace();
					  }
					
					try {
						if (!new File(image_lib.getAbsolutePath()+"\\buffer\\"+searchstr).exists()){
						p2 = Runtime.getRuntime().exec("python pick_animal.py 1 " + searchstr +" "+ image_lib.getAbsolutePath());
						System.out.println("making folder of images");
						p2.waitFor();
						System.out.println("done");
						}
					
						p5 = Runtime.getRuntime().exec("python pick_animal.py 0 " + searchstr +" "+ image_lib.getAbsolutePath());
						System.out.println("picking for the best image");
						p5.waitFor();
						System.out.println("done picking");
						
					} catch (IOException | InterruptedException e1) {
						// Auto-generated catch block
						e1.printStackTrace();
					}
					BufferedReader stdInput = new BufferedReader(new 
					        InputStreamReader(p5.getInputStream()));
					        s = null;
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
					        filename.replace('/', '\\');
					        fetchd_file = new File(filename);
				}
				
				
				       
				
				System.out.println(fetchd_file);
				playSound("beep.wav");
				displayIM(fetchd_file);
				search.requestFocusInWindow();
			}
			else {
				fetchd_file = null;
				generate.setText("Select");
			}
		}
		
		if (e.getSource() == browse){
            
			sel_size.setEnabled(true);
			sel_size.setVisible(true);
			
			small.setEnabled(false);
			large.setEnabled(false);
			medium.setEnabled(false);
			
			small.setVisible(false);
			large.setVisible(false);
			medium.setVisible(false);
			
			pane.removeAll();
			pane.add(left);
			pane.add(right_1);
			pane.repaint();
            Process p1 = null;
			try {
			  p1 = Runtime.getRuntime().exec("python choose_file.py " + image_lib);
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
	        filename.replace('/', '\\');
	        fetchd_file = new File(filename);
	        searchstr = fetchd_file.getName().replaceAll(".jpg", "");
	        searchstr.replaceAll(".png", "");
	        searchstr.replaceAll(".bmp", "");
	        searchstr.replaceAll(".JPG", "");
	 
	        System.out.println(searchstr);
	        displayIM(fetchd_file);
		}
		if (e.getSource() == generate){
			
			small.setEnabled(false);
			large.setEnabled(false);
			medium.setEnabled(false);
			
			small.setVisible(true);
			large.setVisible(true);
			medium.setVisible(true);
			
			if(iscaption.isSelected()){
				tfcap = "false"; 
			}
			Process p4 = null;
			Process p7 = null;
			Process p6 = null;
			
			System.out.println(fetchd_file.getAbsolutePath());
			try {
			  p4 = Runtime.getRuntime().exec("python filter.py " + fetchd_file.getAbsolutePath()+ " C:\\out\\"+searchstr+"_1.txt 1 "+searchstr+" "+tfcap+ " "+size);
			  p6 = Runtime.getRuntime().exec("python filter.py " + fetchd_file.getAbsolutePath()+ " C:\\out\\"+searchstr+"_2.txt 2 "+searchstr+" "+tfcap+ " "+size);
			  p7 = Runtime.getRuntime().exec("python filter.py " + fetchd_file.getAbsolutePath()+ " C:\\out\\"+searchstr+"_3.txt 3 "+searchstr+" "+tfcap+ " "+size);
			} catch (IOException e1) {
				System.out.println("error");
				e1.printStackTrace();
			}
		
			try {
				p4.waitFor();
				p6.waitFor();
				p7.waitFor();
			} catch (InterruptedException e2) {
				// Auto-generated catch block
				e2.printStackTrace();
			}
			
			
			int filternum = 1;
			while(filternum<4){
			BufferedReader in = null;
			try {
				in = new BufferedReader(new FileReader("C:\\out\\"+searchstr+"_"+filternum+".txt"));
			} catch (FileNotFoundException e1) {
				e1.printStackTrace();
			}
			
			char[] buffer = new char[1024];
            int n = 0;
			try {
				n = in.read(buffer);
			} catch (IOException e1) {
				e1.printStackTrace();
			}
			text[filternum-1] = new String(buffer, 0, n);
			try {
				in.close();
			} catch (IOException e1) {
				e1.printStackTrace();
			}			
			filternum += 1;
			}
			
			displayPreview(text[1],2);
			next.setVisible(true);
			compare.setVisible(true);
			generate.setVisible(false);			
			print.setVisible(true);
			print.setEnabled(true);
			playSound("beep.wav");
			next.requestFocusInWindow();
			
		}
		
		if (e.getSource() == next){
			if(displaynum == 3){
				displaynum = 1;
				}
			else{
				displaynum = displaynum+1;
			}
			displayPreview(text[displaynum-1],displaynum);
			playSound("beep.wav");
			next.requestFocusInWindow();
			next.requestFocus(true);
			next.grabFocus();
			next.revalidate();
		}
		
		if (e.getSource() == print){
			int filenum = 0;
			for (int i = 1;i<4;i++){
				File del = new File("C:\\out\\"+searchstr+"_"+i+".txt");
				if (i != displaynum){
					del.delete();
				}
				else{
					boolean check = new File("C:\\out\\"+searchstr+".txt").exists();
					System.out.println(searchstr);
					System.out.println(check);
					if (!check){
							del.renameTo(new File("C:\\out\\"+searchstr+".txt"));
						}
					else{
					  filenum = 1;	
					  while(true){
						boolean check2 = new File("C:\\out\\"+searchstr+filenum+".txt").exists();
						if (!check2)
						{
							del.renameTo(new File("C:\\out\\"+searchstr+filenum+".txt"));
							break;
						}
						else{
							filenum += 1;
						}
					  }
					}
				}
			}						
			try {
				if (filenum > 0){
					Runtime.getRuntime().exec("notepad C:\\out\\"+searchstr+filenum+".txt");
					filenum = 0;
				}
				else{
					Runtime.getRuntime().exec("notepad C:\\out\\"+searchstr+".txt");
				}
			} catch (IOException e1) {
				// Auto-generated catch block
				e1.printStackTrace();
			}

			searchstr = null;
			searchbox.setText(null);
			pane.removeAll();
			pane.add(left);
			right_1.remove(picLabel);
			pane.add(right_1);
			right_1.repaint();
			pane.repaint();
			searchbox.requestFocusInWindow();
		}
		
		if (e.getSource()== compare){	
			
			next.requestFocusInWindow();
			pane.removeAll();
			pane.add(left);
			pane.add(right_3);
			pane.repaint();
			Pane_1.setFont(new Font("Braille", Font.BOLD, 9));
            Pane_1.setText(text[0]);
            Pane_2.setFont(new Font("Braille", Font.BOLD, 9));
            Pane_2.setText(text[1]);
            Pane_3.setFont(new Font("Braille", Font.BOLD, 9));
            Pane_3.setText(text[2]);
            
            Pane_1.setEditable(false);
            Pane_2.setEditable(false);
            Pane_3.setEditable(false);
            
            right_3.removeAll();
            right_3.add(Pane_1);
			right_3.add(Pane_2);
			right_3.add(Pane_3);
			right_3.add(dispLabel);
			right_3.repaint();
			pane.repaint();
		}
		
		if (e.getSource() == sel_size){
			sel_size.setEnabled(false);
			sel_size.setVisible(false);
			
			small.setEnabled(true);
			large.setEnabled(true);
			medium.setEnabled(true);
			
			small.setVisible(true);
			large.setVisible(true);
			medium.setVisible(true);
			small.requestFocusInWindow();
		}
		
		if (e.getSource() == small){
			size = 1;
		}
		if (e.getSource() == medium){
			size = 2;
		}
		if (e.getSource() == large){
			size = 3;
		}
		
		if (e.getSource() == pick_one){
			
			pane.removeAll();
			pane.add(left);
			pane.add(right_2);
			Process p3 = null;
			
			System.out.println(searchstr);
			if (searchstr != null || searchbox.getText()!=null){
				if (searchbox.getText()!= null){
					searchstr =	searchbox.getText();
					searchstr.replaceAll("\\W", "");
				}
			searchLabel = new JLabel("<html><div style=\"text-align: center;\">" + searchstr + "</html>",SwingConstants.CENTER);
			searchLabel.setFont(new Font("Arial",Font.BOLD,30));
			searchLabel.setLocation(20, 3);
			searchLabel.setSize(800, 35);
			right_2.add(searchLabel);
			
			try {
				System.out.println("python pick_animal.py 1 " + searchstr +" "+ image_lib.getAbsolutePath());
				p3 = Runtime.getRuntime().exec("python pick_animal.py 1 " + searchstr +" "+ image_lib.getAbsolutePath());
				p3.waitFor();
			} catch (IOException | InterruptedException e1) {
				e1.printStackTrace();
			}
			
			rootpane.setDefaultButton(fselect);
			left.add(fselect);
			if (fc == null){
				fc = new JFileChooser(image_lib.getAbsolutePath()+"\\buffer\\"+searchstr);
				disableTF(fc);
				right_2.add(fc);
			}
			System.out.println(image_lib.getAbsolutePath()+"\\buffer\\"+searchstr);
						
			ThumbNailView thumbView=new ThumbNailView();
			
			flist.addMouseListener(this);
			fc.setFileView(thumbView);
			
			fc.setSize(950, 800);
			fc.setLocation(0, 0);
			
			pane.remove(right_1);
			pane.add(right_2);
			
			pane.revalidate();
			pane.repaint();
			playSound("beep.wav");
			}
			else {
				//TODO prompt them saying that the text field is blank
			}
		}
		
	  }
	
	public void displayPreview(String str, int i) {
			
			pane.removeAll();
			pane.add(left);
			pane.add(right_4);
			right_4.removeAll();
			JEditorPane DispPane = new JEditorPane();
			DispPane.setEditable(false);
			DispPane.setFont(new Font("Braille", Font.BOLD, 14));
			right_4.add(DispPane);
			DispPane.setText(str);
			
			DispPane.setSize(870,680);
			DispPane.setLocation(0,0);
			
			right_4.repaint();			
			pane.repaint();
			displaynum = i;
	}

	public static final File search_file(File rootDir, String fileName) {
	    File[] files = rootDir.listFiles();
	    List<File> directories = new ArrayList<File>(files.length);
	    for (File file : files) {
		    System.out.println(file.getName());
	        if (file.isFile() && file.getName().toLowerCase().contains(fileName.toLowerCase())) {
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
	
	public static BufferedImage doAgooglesearch(String str){
		return null;		
	}
	
	public void displayIM(File pic_file){
		
		BufferedImage myPicture = null;
		try {
			myPicture = ImageIO.read(pic_file);
		} catch (IOException e1) {
			System.out.println(myPicture);
			e1.printStackTrace();
		}

		BufferedImage scaledIM = null;
		try {
			scaledIM = getScaledImage(myPicture,830,650);
		} catch (IOException e1) {
			e1.printStackTrace();
		}
		
		if (picLabel != null){
			right_1.remove(picLabel);
			right_1.repaint();
			picLabel = null;
		}
		picLabel = new JLabel(new ImageIcon(scaledIM));	
		try {
			dispLabel = new JLabel(new ImageIcon(getScaledImage(myPicture,400,320)));
		} catch (IOException e) {
			//Auto-generated catch block
			e.printStackTrace();
		}
		
		picLabel.setSize(830,650);
		picLabel.setLocation(20,70);
		right_1.add(picLabel);
		generate.setVisible(true);
		right_1.repaint();
	}

	/**
     * @param filename the name of the file that is going to be played
     */
    public void playSound(String filename){

        String strFilename = filename;

        try {
            soundFile = new File(strFilename);
        } catch (Exception e) {
            e.printStackTrace();
            System.exit(1);
        }

        try {
            audioStream = AudioSystem.getAudioInputStream(soundFile);
        } catch (Exception e){
            e.printStackTrace();
            System.exit(1);
        }

        audioFormat = audioStream.getFormat();

        DataLine.Info info = new DataLine.Info(SourceDataLine.class, audioFormat);
        try {
            sourceLine = (SourceDataLine) AudioSystem.getLine(info);
            sourceLine.open(audioFormat);
        } catch (LineUnavailableException e) {
            e.printStackTrace();
            System.exit(1);
        } catch (Exception e) {
            e.printStackTrace();
            System.exit(1);
        }

        sourceLine.start();

        int nBytesRead = 0;
        byte[] abData = new byte[128000];
        while (nBytesRead != -1) {
            try {
                nBytesRead = audioStream.read(abData, 0, abData.length);
            } catch (IOException e) {
                e.printStackTrace();
            }
            if (nBytesRead >= 0) {
                @SuppressWarnings("unused")
                int nBytesWritten = sourceLine.write(abData, 0, nBytesRead);
            }
        }

        sourceLine.drain();
        sourceLine.close();
    }
    
	@Override
	public void focusGained(FocusEvent e) {
		if (e.getSource() == search){
			rootpane.setDefaultButton(search);
			if(!iscaption.isSelected()){
				playSound("search.wav");
			}
		}
		if (e.getSource() == browse){
			rootpane.setDefaultButton(browse);
			if(!iscaption.isSelected()){
				playSound("browse.wav");
			}
		}
		if (e.getSource()== generate){
			rootpane.setDefaultButton(generate);
			if(!iscaption.isSelected()){
					playSound("generate.wav");
			}
		}
		if (e.getSource()== pick_one){
			rootpane.setDefaultButton(pick_one);
			if(!iscaption.isSelected()){
				playSound("choose.wav");
			}
		}
		if (e.getSource()== next){
			rootpane.setDefaultButton(next);
			if(!iscaption.isSelected()){
				playSound("next.wav");
			}
		}
		if (e.getSource() == searchbox){
			rootpane.setDefaultButton(search);
			if(!iscaption.isSelected()){
				playSound("searchbox.wav");
			}
		}
		if (e.getSource() == compare){
			rootpane.setDefaultButton(compare);
			if(!iscaption.isSelected()){
				playSound("compare.wav");
			}
		}
		if (e.getSource()== print){
			rootpane.setDefaultButton(print);
			if(!iscaption.isSelected()){
				playSound("print.wav");
			}
		}
		
		if (e.getSource() == sel_size){
			rootpane.setDefaultButton(sel_size);
			if(!iscaption.isSelected()){
				playSound("sizeimage.wav");
			}
		}
		
		if (e.getSource() == small){
			rootpane.setDefaultButton(small);
			if(!iscaption.isSelected()){
				playSound("small.wav");
			}
		}
		
		if (e.getSource() == large){
			rootpane.setDefaultButton(large);
			if(!iscaption.isSelected()){
				playSound("large.wav");
			}
		}
		
		if (e.getSource() == medium){
			rootpane.setDefaultButton(medium);
			if(!iscaption.isSelected()){
				playSound("medium.wav");
			}
		}
		
	}

	@Override
	public void focusLost(FocusEvent e) {
		// TODO Auto-generated method stub
		
	}
	
	@SuppressWarnings("rawtypes")
	public boolean disableTF(Container c) {
	    Component[] cmps = c.getComponents();
	    
	    for (Component cmp : cmps) {
	    	if (cmp instanceof JList){
	    		flist = (JList)cmp;
		    }
	    	if (cmp instanceof JTextField) {
	            ((JTextField)cmp).setEnabled(false);
	            ((JTextField)cmp).setVisible(false);
	        
	        }
	        
	        if ( cmp instanceof JButton) {
	            ((JButton)cmp).setEnabled(false);
	            ((JButton)cmp).setVisible(false);
	      
	        }
	        
	        if ( cmp instanceof JLabel) {
	            ((JLabel)cmp).setEnabled(false);
	            ((JLabel)cmp).setVisible(false);
	        
	        }
	         
	         if (cmp instanceof JComboBox) {
		            ((JComboBox<?>)cmp).setEnabled(false);
		            ((JComboBox<?>)cmp).setVisible(false);
		     
		     }
	         
	        if (cmp instanceof Container) {
	            if(disableTF((Container) cmp)) return true;
	        }
	    }
	    return false;
	}

	@Override
	public void mouseClicked(MouseEvent e) {
		System.out.println("mouse clicked");
		if (e.getClickCount()==2){
			System.out.println(e.getSource());
			fetchd_file = fc.getSelectedFile();
			System.out.println(fc);
			if (fetchd_file!=null){
				pane.removeAll();
				pane.add(left);
				pane.add(right_1);
				pane.repaint();
				displayIM(fetchd_file);
			}
		}
	}

	@Override
	public void mouseEntered(MouseEvent arg0) {
		//  Auto-generated method stub		
	}

	@Override
	public void mouseExited(MouseEvent arg0) {
		//  Auto-generated method stub
		
	}

	@Override
	public void mousePressed(MouseEvent arg0) {
		//  Auto-generated method stub
		
	}

	@Override
	public void mouseReleased(MouseEvent arg0) {
		//  Auto-generated method stub
		
	}

}

