# Dissecting-Person-Re-identification-from-the-Viewpoint-of-Viewpoint

In this paper, we build a synthetic data engine PersonX that can generate images under controllable cameras and environment.
The dataset from PersonX is shown to be as indicative as real-world datasets. Second, based on PersonX, we conduct comprehensive experiment to quantitatively assess the influence of pedestrian viewpoint on person re-ID accuracy. Specifically, the PerxonX engine is built on Unity and its detials are as follows.

For researching viewpoint, we use six backgrounds in this version (as shown in above figure), including three pure color backgrounds and three scene backgrounds. There are 1266 hand-crafted identities (547 females and 719 males) and each identety has 36 viewpoints (sampled every <a href="https://www.codecogs.com/eqnedit.php?latex=0^{\circ}" target="_blank"><img src="https://latex.codecogs.com/gif.latex?10^{\circ}" title="10^{\circ}" /></a> from <a href="https://www.codecogs.com/eqnedit.php?latex=0^{\circ}" target="_blank"><img src="https://latex.codecogs.com/gif.latex?0^{\circ}" title="0^{\circ}" /></a> to <a href="https://www.codecogs.com/eqnedit.php?latex=0^{\circ}" target="_blank"><img src="https://latex.codecogs.com/gif.latex?350^{\circ}" title="350^{\circ}" /></a> ), so the total number of images is 273,456 (<a href="https://www.codecogs.com/eqnedit.php?latex=36\times&space;1266&space;\times&space;6" target="_blank"><img src="https://latex.codecogs.com/gif.latex?36\times&space;1266&space;\times&space;6" title="36\times 1266 \times 6" /></a> ).

![fig1](https://github.com/sxzrt/Dissecting-Person-Re-identification-from-the-Viewpoint-of-Viewpoint/blob/master/images/fig1.jpg)  



****
## Datasets
We release the images from different backgrounds, respectively. <br>
The dataset can be downloaded from the following links:<br>
[Baidu Disk]()<br>
[Google Drive]()

The package contains six folders and two files. 
1) The six folders "1" to "6" correspond to the six backgrounds. Each floders contain three sub-folders: "bounding_box_train", "bounding_box_test" and "query". 
     * "bounding_box_test". There are 30,816 images in this folder used for testing.
     * "bounding_box_train". There are 14,760 images in this folder used for training.
     * "query". There are 865 identities. We randomly select one query image for each person of one background. <br>
   `The six backgrounds can be combined in pairs as illustrated in the paper to get PersonX12, PersonX13, etc., datasets.` 
2) "person1-1266.unitypackage" is a Unity package that contain the 3D original models of the identities. 
3) "train_test.mat" is used to split the 1266 identities into the trainning and testing set, in which "0" and "1" represents the person is selected into traning (0) or testing set (1), respectively. In this work, we randomly sample 410
identities for training and 856 identities for testing. 

Following the naming rule of Market-1501 dataset, we name the images as follows:
>> *e.g.，* "0001_c1s1_03.jpg", "0001" is the ID of this person. "c1" is the camera shooting the first background and there are totally 6 backgrounds. "03" meams that the rotation angle (viewpoint) of the person in this images is <a href="https://www.codecogs.com/eqnedit.php?latex=0^{\circ}" target="_blank"><img src="https://latex.codecogs.com/gif.latex?10^{\circ}" title="30^{\circ}" /></a> (3 * 10). "s1" is used to keep similar name rule with the Market-1501 dataset, so it dose not has special meaning.
 
 
