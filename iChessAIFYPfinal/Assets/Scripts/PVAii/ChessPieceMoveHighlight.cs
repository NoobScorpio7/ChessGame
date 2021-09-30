using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceMoveHighlight1 : MonoBehaviour
{
    


    //We made a static instance of class so that it can accessed from any where
    public static ChessPieceMoveHighlight1 Instance { set; get; }



    //This list of game object will act as pooling mechanic
    //we will pool gameobject
    //This will serve as the base for the highlight selection
    public GameObject PieceMoveHighlightPrefab;
    //This list will contain the alias of all the highlight prefabs
    private List<GameObject> MoveHighlights;

    // Start is called before the first frame update
    public void Start()
    {
        Instance = this;

        //We will instanciate the highlight list of gameobject
        MoveHighlights = new List<GameObject>();

    }

    //GameObjects are the fundamental objects in Unity that represent characters, props and scenery.
    //They do not accomplish much in themselves but they act as containers for Components, which implement the real functionality. 
    private GameObject GetMoveHighlightObject()
    {
        //this line of code will find and return the first object that mataches the activeself condition
        //the activeSelf returns the local active state of this GameObject
        GameObject gameobject = MoveHighlights.Find(g => !g.activeSelf);

        //if it does not find anything
        if (gameobject == null)
        {
            //Clones the object original and returns the clone.
            //This function makes a copy of an object in a similar way to the Duplicate command in the editor.
            //This will make a clone of the PieceMoveHighlightPrefab
            gameobject = Instantiate(PieceMoveHighlightPrefab);

            //Then the highlight move will be added to the List
            MoveHighlights.Add(gameobject);
        }

        //in either case if found or not we will return the gameobject
        return gameobject;
    }



    //This function will highlight all the possible for a chess piece
    public void ChessPiecellowedMoveHighlight(bool [,] PossibleMoves)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if(PossibleMoves [ i, j ])
                {
                    GameObject gameobject = GetMoveHighlightObject();
                    gameobject.SetActive(true);
                    gameobject.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);
                }
            }
        }
    }


    //We can turn of the highlight if we do not need any
    public void HideChessmoveHighlights()
    {
        foreach (GameObject gameobject in MoveHighlights)
            gameobject.SetActive(false);
    }

}
