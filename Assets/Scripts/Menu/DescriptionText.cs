using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionText : MonoBehaviour
{
    /*
        Item and quest description text for menu button selected.
    */

	//  Quest description text for quest log, key is the value in the Questslot.cs dictionary.
	public Dictionary<string, string> questDescriptionTextDict = new Dictionary<string, string>(){
	};

    //  Item description text for player inventory.
    public Dictionary<string, string> itemTextDictPlayer = new Dictionary<string, string>(){
        ["Currency"] = "Gold!",
        ["Berries"] = "Wild berries. Probably not poisonous. Recover 5 health",
        ["Meat"] = "Looks like meat's back on the menu",
        ["Leek"] = "Green and healthy. Recover 5 health",
        ["Potion Magicka"] = "Recover 30 magicka",
        ["Potion Mystery"] = "Not fit for a beast, let alone a man",
        ["Potion Health"] = "Recover 30 health", 
        ["Scroll"] = "Ancient and flaky, a dusty old scroll",
        //  Note: max length for description.
        ["Mysterious Book"] = "An old book, tattered cover etched with symbols, portal diagrams on torn pages",
        ["Leaf"] = "Bright, pretty foliage with an odious scent",
        ["Rum"] = "Homemade Rum",
        ["Boots"] = "With the fur",
        ["Hat"] = "Stylish hat for the modern humanoid", 
        ["Feather"] = "A suspiciously large feather with a blue aura. Recover 5 magicka",
        ["Bone"] = "A friendly bone",
        ["Crystal"] = "A shiny, glimmering crystal",
		["Bellblossom"] = "A grumpy, mushroom-like creature",
    };

    //  Item description text for vendor 1 inventory.
    public Dictionary<string, string> itemTextDictVendor1 = new Dictionary<string, string>(){
        ["Potion Magicka"] = "Recover 30 magicka",
        ["Potion Mystery"] = "Not fit for a beast, let alone a man",
        ["Potion Health"] = "Recover 30 health",
        ["Orb Of Light"] = "A purple glowing orb. Will glow when light is needed"
    };
}
