using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece1 : MonoBehaviour
{

    //It is called auto property, and is essentially a shorthand
    //The below varaibles are shorthand code for set and get
    //These varaible will be used to set chess peice position individually
    public int CurrentPositionX { set; get; }
    public int CurrentPositionY { set; get; }


    //This line will checl wether the pieces is white or black
    public bool isPieceWhite;
    

    public void SetChessPosition(int AxisX, int AxisY)
    {
        CurrentPositionX = AxisX;
        CurrentPositionY = AxisY;
    }


    //this will return 2D array of all possible moves for a selected peice
    //The virtual keyword is used to modify a method, property, indexer, or event declared in the base class and allow it to be overridden in the derived class.
    public virtual bool[,] LegalMoves()
    {
        return new bool[8,8];
    }

}
