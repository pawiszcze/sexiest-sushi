using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensei : Interactive {
	
    override public void Interact()
    {
        switch (gmngr.currentStageProgress)
        {
            case 0:
                Debug.Log("Hai. I am sensei and need food. Go and kill a Glonojad. It's basically at the bottom of the mountain, in the Dojo Gardens.");
                break;

            case 1:
                Debug.Log("Hai. I am sensei and need food. Go and kill an Oyster. It's to the East, in the Oyster Farm. The biggest oysters are in the far east ection, at the feet of the waterfall.");
                break;


            case 2:
                Debug.Log("Hai. I am sensei and need food. Go and kill a Manta Ray. They live in the Rice Farms in the West, shrouded by myst.");
                break;


            case 3:
                Debug.Log("Hai I am sensei senpaj gib another sushi");
                break;


            case 4:
                Debug.Log("Hai I am sensei senpaj gib another sushi");
                break;


            case 5:
                Debug.Log("Hai I am sensei senpaj gib another sushi");
                break;


            case 6:
                Debug.Log("Hai I am sensei senpaj gib another sushi");
                break;


            case 7:
                Debug.Log("Hai I am sensei senpaj gib another sushi");
                break;


            case 8:
                Debug.Log("Hai I am sensei senpaj gib another sushi");
                break;


            case 9:
                Debug.Log("Hai I am sensei senpaj gib another sushi");
                break;


            case 10:
                Debug.Log("Hai I am sensei senpaj gib another sushi");
                break;

            default:
                Debug.Log("Sensei is confusion");
                break;
        }
        
    }
}
