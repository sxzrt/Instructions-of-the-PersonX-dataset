%function  recarea= myrec(input,la)
clc
clear


cam= 3;   % the id of camera

rootdir='Data/'; % the path of generated data

b_dir=['bounding-box-images/camera',num2str(cam),'-img/']; % pate save the cropped images

camera=strcat('_c',num2str(cam),'s1_'); % name of camera

if ~exist(b_dir,'dir') 
    mkdir(b_dir);
end
 for kk=1:1 %id   
    for nn=1:36 %viewpoint
    A=imread([rootdir,'A',num2str(kk),'(Clone)_r/',num2str(nn-1),'.png']);
    [m,n,l]=size(A);
    %matric to save the pixel
    
    b_X=zeros(1,1);  %x
    b_Y=zeros(1,1);  %y
    beg=1;
    
    for i=1:m
        for j=1:n
            if (A(i,j,1)~=0&&A(i,j,2)~=0&&A(i,j,3)~=0)
            b_Y(beg)=i;  
            b_X(beg)=j;
            beg=beg+1;
            end
        end
    end
    [xmi X_min]=min(b_X);
    [xma X_max]=max(b_X);
    [ymi Y_min]=min(b_Y);
    [yma Y_max]=max(b_Y);

    w=xma-xmi;
    h=yma-ymi;
  %   imshow(A);
    rect=[xmi ymi w h];
     bounding_img = imcrop(A,rect);
    %rectangle('Position',rect,'EdgeColor','g');
    %figure(2)
      B=imread([rootdir,'A',num2str(kk),'(Clone)/',num2str(nn-1),'.png']);
     %imshow('Data/A1(Clone)/0.png');
      bounding_img2 = imcrop(B,rect);
    %rectangle('Position',rect);
      imagesname=strcat(b_dir,num2str(kk,'%04d'),camera,num2str(nn,'%02d'),'.jpg');
      imwrite(bounding_img2,imagesname);
    end
 end