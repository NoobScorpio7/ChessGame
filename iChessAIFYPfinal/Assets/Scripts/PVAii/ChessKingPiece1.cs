using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessKingPiece1 : ChessPiece1
{
    public override bool[,] LegalMoves()
    {
        bool[,] AllMoves = new bool[8, 8];
        ChessPiece1 K;

        int i;
        int j;

        //Top 

        //Beacuse we are gonna start diagonal left
        i = CurrentPositionX - 1;
        j = CurrentPositionY + 1;

        if (CurrentPositionY != 7)
        {
            //this loop is gonna run 3 times
            //beacuse there are three possiblies moves on top of it
            for (int k = 0; k < 3; k++)
            {
                //If we are inside the boundries 
                if(i >= 0 && i < 8)
                {
                    //K = ChessBoardManager.Instance.ChessPieces[i, j];
                    K = LocalBoard.Instance.ChessPieces1[i, j];
                    if (K == null)
                    {
                        AllMoves[i, j] = true;
                    }
                    else if (isPieceWhite != K.isPieceWhite)
                    {
                        AllMoves[i, j] = true; 
                    }
                    
                }
                i++;
            }
        }


        //Down

        //Beacuse we are gonna start diagonal left
        i = CurrentPositionX - 1;
        j = CurrentPositionY - 1;

        if (CurrentPositionY > 0)
        {
            //this loop is gonna run 3 times
            //beacuse there are three possiblies moves on Down side of it
            for (int k = 0; k < 3; k++)
            {
                //If we are inside the boundries 
                if (i >= 0 && i < 8)
                {
                    K = LocalBoard.Instance.ChessPieces1[i, j];

                    if (K == null)
                    {
                        AllMoves[i, j] = true;
                    }
                    else if (isPieceWhite != K.isPieceWhite)
                    {
                        AllMoves[i, j] = true;
                    }
                    i++;
                }
            }
        }


        //Left 
        if (CurrentPositionX != 0)
        {
            K = LocalBoard.Instance.ChessPieces1[CurrentPositionX - 1, CurrentPositionY];

            if (K == null)
            {
                AllMoves[CurrentPositionX - 1, CurrentPositionY] = true;

            }

            else if (isPieceWhite != K.isPieceWhite)
            {
                AllMoves[CurrentPositionX - 1, CurrentPositionY] = true;
            }
        }

        //Right 
        if (CurrentPositionX != 7)
        {
            K = LocalBoard.Instance.ChessPieces1[CurrentPositionX + 1, CurrentPositionY];

            if (K == null)
            {
                AllMoves[CurrentPositionX + 1, CurrentPositionY] = true;

            }

            else if (isPieceWhite != K.isPieceWhite)
            {
                AllMoves[CurrentPositionX + 1, CurrentPositionY] = true;
            }
        }


        return AllMoves;
    }
}
