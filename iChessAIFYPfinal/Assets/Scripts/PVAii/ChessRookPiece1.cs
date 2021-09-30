using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessRookPiece1 : ChessPiece1
{

    public override bool[,] LegalMoves()
    {
        bool[,] AllMoves = new bool[8, 8];

        ChessPiece1 R;

        int i;

        //Rook move to right

        i = CurrentPositionX;
        while (true)
        {
            i++;
            //If it is to far on the board it will break and exit the loop
            if (i >= 8)
            {
                break;
            }

            R = LocalBoard.Instance.ChessPieces1[i, CurrentPositionY];
            if (R == null)
            {
                AllMoves[i, CurrentPositionY] = true;
            }

            //Serve as collider to not let it jump over other pieces
            else
            {
                if (R.isPieceWhite != isPieceWhite)
                {
                    AllMoves [i, CurrentPositionY] = true;
                   
                }
                break;
            }
        }


        //Rook move to left
        i = CurrentPositionX;
        while (true)
        {
            i--;

            //If it is to far on the board it will break and exit the loop
            if (i < 0)
            {
                break;
            }

            R = LocalBoard.Instance.ChessPieces1[i, CurrentPositionY];
            if (R == null)
            {
                AllMoves[i, CurrentPositionY] = true;
            }

            //Serve as collider to not let it jump over other pieces
            else
            {
                if (R.isPieceWhite != isPieceWhite)
                {
                    AllMoves [i, CurrentPositionY] = true;
                    
                }
                break;
            }
        }


        //Rook move Up

        i = CurrentPositionY;
        while (true)
        {
            i++;
            //If it is to far on the board it will break and exit the loop
            if (i >= 8)
            {
                break;
            }

            R = LocalBoard.Instance.ChessPieces1[CurrentPositionX, i];
            if (R == null)
            {
                AllMoves[CurrentPositionX, i] = true;
            }

            //Serve as collider to not let it jump over other pieces
            else
            {
                
                if (R.isPieceWhite != isPieceWhite)
                {
                    AllMoves [CurrentPositionX, i] = true;
                    
                }
                break;
            }
        }


        //Rook move down

        i = CurrentPositionY;
        while (true)
        {
            i--;
            //If it is to far on the board it will break and exit the loop
            if (i < 0)
            {
                break;
            }

            R = LocalBoard.Instance.ChessPieces1[CurrentPositionX, i];
            if (R == null)
            {
                AllMoves[CurrentPositionX, i] = true;
            }

            //Serve as collider to not let it jump over other pieces
            else
            {
                if (R.isPieceWhite != isPieceWhite)
                {
                    AllMoves[CurrentPositionX, i] = true;
                    
                }
                break;
            }
        }

        return AllMoves;
    }
  
}
