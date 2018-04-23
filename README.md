# Theme Park Database

## Set up Git for Windows
1. Downlaod [git] (https://git-scm.com/download/win)
2. Install git, you can leave the defaults
3. In your git bash run these commands
	* `git config --global user.name "yourname"`  
	* `git config --global user.email "your@githubemail"`  
4. Create/Find a new directoy to place the project  
5. Open the git bash in the directory by right clicking in the 
folder and choosing _git bash here_   
6. Clone the respository from github  
	* `git clone https:\\github.com/qdgironq/ThemeParkDatabase`
7. Open the project file (.sln file)
8. In views open up Team Explorer and check to make sure that you are   
connect. The display should show boxes that say sync, branch, etc.  
9. If there are no problems you should be connect and ready to start  
working.

## Using Git in Visual Studio  
1. Branching
	* This should be done first, in the bottom right corner you  
	see two arrows spliting apart that say master click on it.  
	You should be in the Team Explorer.  
	![branch_name] (https://github.com/qdgironq/ThemeParkDatabase/tree/master/tutorial_images/branch_name.png)
	* Click on new branch, and name it after yourself   
	![branch_location] (https://github.com/qdgironq/ThemeParkDatabase/tree/master/tutorial_images/branch_loc.png)
2. Fetch/Pull
	* Click on the home button and click Sync, there should be   
	nothing to sync since no changes have been made yet.  
	* Click on fetch to get the latest updates made to the project,  
	then click on pull to apply them to your branch.   
	![fetch] (https://github.com/qdgironq/ThemeParkDatabase/tree/master/tutorial_images/fetch.png)

3. Commit/Push 
	* After doing some work, click on the Team Explorer and click on   
	sync. 
	* It should show you what files you made changes and there should be  
	a box above that says enter a commit message
	* Enter in a descriptive message about the changes you have made  
	**BE SPECIFIC** and maybe tell why if appliciable  
	* Make sure that your Branch says *yourbranchname* and NOT master
	* Click commit all, click on the home button and click on sync, push  
	commit though. Alternatively just click the arrow when you commit all  
	and choose commit all and push.  
	![commit] (https://github.com/qdgironq/ThemeParkDatabase/tree/master/tutorial_images/commit.png)

4. Merge
	* When you are ready to add your changes to the master branch, click   
	on branches in the Team Explorer
	* Double click on master and click merge  
	* Fill in the top box that says Merge from branch with your branch  
	the Into current branch should say master 
	* Push your commit through, and switch back to your branch.
	* Alternatively if you want to merge with other branches stay in your  
	branch and when you click on branches fill in the name of another branch  
	push it and you should have the commits of the other branch.  
	![merge] (https://github.com/qdgironq/ThemeParkDatabase/tree/master/tutorial_images/merge.png)

5. I messed up what do I do
	* Hopefully you messed up on your own branch so you can just create another   
	branch and continute working from there. 
	* If you messed up on the master branch I hate you, you've ruined it for  
	everybody, this is why we can't have nice things, etc... Let everyone know  
	in chat, I will fix it somehow.
	* I did a bad commit and I would like to revert or change the message (wip)
7. General Guidelines (wip)
	* Have 2 branches one for testing and one for stable. The testing is where you  
	put all your nasty code and merges from other branches that are not the master  
	you should then push and merge to stable when you have tested your working code.  
	Test your stable and be completely sure it works few commits should be made here.  
	Finally push your stable to the master when your ready. 
	* Pull from others stable branches and keep your own stable up to date.  

## Todo

## Links



