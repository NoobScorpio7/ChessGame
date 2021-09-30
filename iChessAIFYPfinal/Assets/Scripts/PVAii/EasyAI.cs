using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EasyAI : MonoBehaviour
{
    System.Random r;
    public Vector3 MakeMove(ChessPiece figure)
    {
        r = new System.Random();

        bool[,] possibleMoves = figure.LegalMoves();

        List<Vector2> possibleMovements = new List<Vector2>();
        List<Vector2> possibleMovements1 = new List<Vector2>();

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (possibleMoves[i, j])
                {
                    
                    
                    possibleMovements.Add(new Vector2(i, j));
                    
                    


                }
            }
        }

        for (int i = 0; i < possibleMovements.Count; i++)
        {
            print("List");
                print(possibleMovements[i]);
            
        }

        if (possibleMovements.Count > 0)
        {
            print("possible move");
            
            return possibleMovements[r.Next(possibleMovements.Count)];
        }
        else 
        { 
            return new Vector2(-1, -1); 
        }
    }

    public ChessPiece SelectChessFigures()
    {
        r = new System.Random();
        List<GameObject> activeFigures = ChessBoardManager.Instance.GetAllActiveFigures();
        for (int i = 0; i < activeFigures.Count; i++)
        {
            print("here");
            print(activeFigures[i].name);   
        }
        GameObject gameObject = activeFigures[r.Next(activeFigures.Count)];
        print("Heelaluja");
        print(gameObject.GetComponent<ChessPiece>());
        return gameObject.GetComponent<ChessPiece>();
    }
}
