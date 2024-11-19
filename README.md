# Image Wizard UI 
This simple application can help you to create the SpriteSheets for different game engines that supports this feature. I have used the spritesheets made by this program in Unity and Mono Game game engines.

## How to Use It
It has a very comfortable GUI to controll the process of building your SpriteSheet. It is very simple. Here you can see the Screenshot: All the red fields are required. The first one is used to set the folder with Input Images. 
You just need to click on Open Button and then select a folder with your images.

![image](https://github.com/user-attachments/assets/d2c19f90-c94a-41af-a0e3-b4ec2b04fd17)

When the path to the folder with the images is set? the programm will automaticaly scan and display all the images:

![image](https://github.com/user-attachments/assets/de926589-325c-4c91-970b-b0ea30bf4197)

After that you need to setup the output folder. Click the OPen button again and select the path to the folder where you would like to store the SpriteSheet and metadata.
This is a simple step.

Then you need to specify the amount of Columns and Rows. As the SpriteSheet is a big image build from smaller once, this parameters controlls the amount of images horizontaly and the amount of images verticaly.
In this example I would like to have an image 'Matrix' (6 Columns, 4 Rows)

![image](https://github.com/user-attachments/assets/a40492bc-7243-46c0-ae3d-36588a93ed1a)

Then we can specify the output file name. If you will leave this field empty file name will be set to default value. In this example we specify th output file name.

![image](https://github.com/user-attachments/assets/85293e0a-7a63-4342-bcc3-ace865d16866)

And then we can press Start Processing Button.
And in the folder that was set as output we will see 2 files:

![image](https://github.com/user-attachments/assets/3df1b06f-479d-4ffe-9702-948a6b365ca4)

The image file will be your SpriteSheet and the *.json file is the file with aditional metadata. It has an aditional information about the conditions and settings that were used to create a SpriteSheet.
Here is my SpriteSheet:

![image](https://github.com/user-attachments/assets/e2e55f8d-769e-4b61-b306-181bbc8a1731)


Here is my metadata file:

![image](https://github.com/user-attachments/assets/949e9a38-be8d-4327-b4fb-380bc18fc99a)

Of course there will be a path to the images. In this exaple I have removed actual path.

## Aditional settings

In this to aditional sections you can enable Resizing or Offsets for your SpriteSheet. 

![image](https://github.com/user-attachments/assets/163a6f9d-c89a-4d0f-8352-68c2266007a2)

When you check the resize option you will be able to change the initial size of each image that will be used for SpriteSheet creating. 
If the offset option checked you will have horizontal and vertical offsets between images in your SpriteSheet.

## Hope my program will help you to build cool computer games!


