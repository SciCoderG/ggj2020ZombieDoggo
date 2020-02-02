using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNames
{
    // https://www.fantasynamegenerators.com/zombie-types.php
    private static string[] randomZombieNames = {
"Mumbler",
"Chomper",
"Friendly Zombie",
"Tearer",
"Mourner",
"Nibbler",
"Talker",
"Learner",
"Floater",
"Sloucher",
"Feeder",
"Icicle Zombie",
"Frenzied",
"Popsicle",
"Stinger",
"Chubber",
"Driver",
"Smasher",
"Suckling Zombie",
"Grumbler",
"Higher",
"Wrangler",
"Grower",
"Scuttler",
"Skeletal Zombie",
"Fragment",
"Crazed Zombie",
"Smasher",
"Swarmer",
"Splasher",
"Rotter",
"Pack Zombie",
"Drencher",
"Bender",
"Newborn",
"Savage",
"Fungus Zombie",
"Shuffler",
"Icicle Zombie",
"Vomiter"
    };

    public static string GetRandomZombieName()
    {
        return de.crystalmesh.Utilities.RandomFromArray(randomZombieNames);
    }
}
