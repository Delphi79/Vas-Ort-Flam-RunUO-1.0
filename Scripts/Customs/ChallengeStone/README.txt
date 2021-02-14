Authors: Ike(Clarke76) and Rasp
www.csharpstop.com (under construction )
Challenge Game Revised
Version 1.1
[NEW]
New version has been release. I fixed some bugs, and hope I fixed other. Some players where having
problems where the server would crash if they challenged anyone on a horse. I could not in any way
shape or form crash the server. I tried everything I possably could think of. So one or two things 
may have happened. 1)I uploaded the wrong files for the game 2)they are creating this error in a way
I just don't know how to replicate. So if crashes continue PLEASE tell me how. Recreate the problem
don't just tell me this is what my players said. Do testing youself!! I did how ever switch some code
around for how horses are moved ect. So hopfully this fixes any of those bugs.

Other bug fixes:
-problem where the challenge ring was not saving Kills,fame,ect when server crash occured. I've move
the ring out of the timer and is now its own item. This seems to have fixed the problem.

-In challengestone.cs and top of file. There are two logout positions set. One for over 5 murders and 
one for under. For under 5 murders it is set to Brit and for over its now set for Buc Den instead of
also Brit. This is only used when server crashes. Any other time the locations are where the orginal
challenge command was used.

-After server crash, the challenge ring was not removed! This has been fixed.

-Added into game that anyone being challenged has to have full health. This is so the game is not
abused and people use it as a means of an excape.

-Forgot to return base for keepitemsondeath, so make you you update that in you PlayerMobile! Take a 
look at the code below.

I thought about not letting challenges while jailed but decided against it, anyone can add this in
easily if they wish. Just check to see what Region mobile is in and return if it's jail region. This
code would be added into the begingump and target files.


[Old new]
This is a revision of my old Challenge Game. I took out 3vs3 and 4vs4 matches and limited it to 
just 1vs1 and 2vs2. You no longer have to be at the challenge stone in order to be challenged. A new
 command has been created and you may challenge andbody anywhere. If you do not want to recieve
challenge requests. Just use [challenge and click the checkbox to turn it off.  All items will be
returned after death. Mounts and potions are removed and kept in "safe area" till end of match. 
All this is eaily changed in order to remove certain things or add certain things. This system is no 
longer compatable with Nox's Point system and Challengeing using gold has yet to be added to this new
 version. I'll look into adding this for later version. The code is much more fluid and easily read
 with less errors. Enjoy.

All this is saved before entering a challenge:
-Kills
-ShortTerm Murders
-Karma
-Fame

If you would like to edit this list it is found int FinalGump.cs, and the Item is ChallengeRing.
  //		      //
 //	BEGIN	     //
//		    //

-Copy and Paste files into your custom script area.

-Included is a Beta-36 PlayerMobile.cs with everything already added. Only use if you don't have custom
stuff in the PM.

-Also in the PM is everything for the Paintball Game I made. If you indeed use both systems, just
uncomment out the Paintball script code so it becomes active. If not, feel free to leave as is or just
delete it.

-Things you may want to add:
	-Allowing challenges only in certain areas.
	-Not allowing Challenges when in battle or criminal.
	-creating a Challenge arena Region: So certain area effect spells can't be used while
	people are watching or in other arenas.

What to have if you have Custom PlayerMobile:
[UPDATED]


private bool isinchal = false;
private bool canbechal = true;
private BaseMount m_TempMount;

[CommandProperty( AccessLevel.GameMaster ) ]
public BaseMount TempMount
{
	get { return m_TempMount; }
	set { m_TempMount = value; }
}
[CommandProperty(AccessLevel.Counselor)]
public bool IsInChallenge
{
	get{return isinchal;}
	set{isinchal = value;}
}
[CommandProperty(AccessLevel.Counselor)]
public bool CanBeChallenged
{
	get{return canbechal;}
	set{canbechal = value;}
}

public override bool KeepsItemsOnDeath
{
	get
	{
		if(isinchal)
			return true;
		return base.KeepsItemsOnDeath;
	}
}
private static void OnLogout( LogoutEventArgs e )
{
	PlayerMobile pm = e.Mobile as PlayerMobile;
	if( pm.IsInChallenge )
		pm.Kill();
}

Remember to Serialize/DeSerialize.

Ike
www.csharpstop.com (under construction).