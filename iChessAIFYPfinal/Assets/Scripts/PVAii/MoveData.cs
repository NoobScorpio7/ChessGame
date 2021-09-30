using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveData 
{
    //public ChessPiece[,] ChessPieces { set; get; }
    //ChessPiece cp;
    
    public int FirstPositionX = 0;
    public int FirstPositionY = 0;
    public int SecondPositionX = 0;
    public int SecondPositionY = 0;
    public string PieceMoved = null;
    public string PieceKilled = null;
    //Debug.Log("FirstPositionX: " + FirstPositionX);
    //public ChessPiece Type = null;
    //public ChessPiece[,] pieceKilled = null;
    public int score = int.MinValue;



}
