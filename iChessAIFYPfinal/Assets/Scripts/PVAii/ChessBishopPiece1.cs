using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBishopPiece1 : ChessPiece1
{
    public override bool[,] LegalMoves()
    {
        bool[,] AllMoves = new bool[8, 8];

        ChessPiece1 B;
        //ChessPiece1 B1;
        int i, j;

        //Top Left

        i = CurrentPositionX;
        j = CurrentPositionY;

        while (true)
        {
            i--;
            j++;

            //to check if the move is inside the board or not
            if (i < 0 || j >= 8)
            {
                break;
            }

            B = LocalBoard.Instance.ChessPieces1[i, j];
          //  B1 = LocalBoard.Instance.ChessPieces1[i, j];
            if (B == null)
            {
                AllMoves[i, j] = true;
            }
            //Serve as collider to not let it jump over other pieces
            else
            {
                if (B.isPieceWhite != isPieceWhite)
                {
                    AllMoves[i, j] = true;
                }
                break;
            }
        }


        //Top Right

        i = CurrentPositionX;
        j = CurrentPositionY;

        while (true)
        {
            i++;
            j++;

            //to check if the move is inside the board or not
            if (i >= 8 || j >= 8)
            {
                break;
            }

            B = LocalBoard.Instance.ChessPieces1[i, j];
          //  B1 = LocalBoard.Instance.ChessPieces1[i, j];
            if (B == null)
            {
                AllMoves[i, j] = true;
            }
            //Serve as collider to not let it jump over other pieces
            else
            {
                if (B.isPieceWhite != isPieceWhite)
                {
                    AllMoves[i, j] = true;
                }
                break;
            }
        }


        //Down left

        i = CurrentPositionX;
        j = CurrentPositionY;

        while (true)
        {
            i--;
            j--;

            //to check if the move is inside the board or not
            if (i < 0 || j < 0)
            {
                break;
            }

            B = LocalBoard.Instance.ChessPieces1[i, j];
         //   B1 = LocalBoard.Instance.ChessPieces1[i, j];
            if (B == null)
            {
                AllMoves[i, j] = true;
            }
            //Serve as collider to not let it jump over other pieces
            else
            {
                if (B.isPieceWhite != isPieceWhite)
                {
                    AllMoves[i, j] = true;
                }
                break;
            }
        }

        //Down Right

        i = CurrentPositionX;
        j = CurrentPositionY;

        while (true)
        {
            i++;
            j--;

            //to check if the move is inside the board or not
            if (i >= 8 || j < 0)
            {
                break;
            }

            B = LocalBoard.Instance.ChessPieces1[i, j];
         //   B1 = LocalBoard.Instance.ChessPieces1[i, j];
            if (B == null)
            {
                AllMoves[i, j] = true;
            }
            //Serve as collider to not let it jump over other pieces
            else
            {
                if (B.isPieceWhite != isPieceWhite)
                {
                    AllMoves[i, j] = true;
                }
                break;
            }
        }

        return AllMoves;
    }
}
