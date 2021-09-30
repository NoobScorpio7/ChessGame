using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessQueenPiece1 : ChessPiece1
{
    public override bool[,] LegalMoves()
    {
        bool[,] AllMoves = new bool[8, 8];
        ChessPiece1 Q;
        int i;
        int j;

        //Same code from rook
        //Queen move to right

        i = CurrentPositionX;
        while (true)
        {
            i++;
            //If it is to far on the board it will break and exit the loop
            if (i >= 8)
            {
                break;
            }

            Q = LocalBoard.Instance.ChessPieces1[i, CurrentPositionY];
            if (Q == null)
            {
                AllMoves[i, CurrentPositionY] = true;
            }

            //Serve as collider to not let it jump over other pieces
            else
            {
                if (Q.isPieceWhite != isPieceWhite)
                {
                    AllMoves[i, CurrentPositionY] = true;

                }
                break;
            }
        }


        //Queen move to left
        i = CurrentPositionX;
        while (true)
        {
            i--;

            //If it is to far on the board it will break and exit the loop
            if (i < 0)
            {
                break;
            }

            Q = LocalBoard.Instance.ChessPieces1[i, CurrentPositionY];
            if (Q == null)
            {
                AllMoves[i, CurrentPositionY] = true;
            }

            //Serve as collider to not let it jump over other pieces
            else
            {
                if (Q.isPieceWhite != isPieceWhite)
                {
                    AllMoves[i, CurrentPositionY] = true;

                }
                break;
            }
        }


        //Queen move Up

        i = CurrentPositionY;
        while (true)
        {
            i++;
            //If it is to far on the board it will break and exit the loop
            if (i >= 8)
            {
                break;
            }

            Q = LocalBoard.Instance.ChessPieces1[CurrentPositionX, i];
            if (Q == null)
            {
                AllMoves[CurrentPositionX, i] = true;
            }

            //Serve as collider to not let it jump over other pieces
            else
            {

                if (Q.isPieceWhite != isPieceWhite)
                {
                    AllMoves[CurrentPositionX, i] = true;

                }
                break;
            }
        }


        //Queen move down

        i = CurrentPositionY;
        while (true)
        {
            i--;
            //If it is to far on the board it will break and exit the loop
            if (i < 0)
            {
                break;
            }

            Q = LocalBoard.Instance.ChessPieces1[CurrentPositionX, i];
            if (Q == null)
            {
                AllMoves[CurrentPositionX, i] = true;
            }

            //Serve as collider to not let it jump over other pieces
            else
            {
                if (Q.isPieceWhite != isPieceWhite)
                {
                    AllMoves[CurrentPositionX, i] = true;

                }
                break;
            }
        }



        //Same code from bishop

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

            Q = LocalBoard.Instance.ChessPieces1[i, j];
            if (Q == null)
            {
                AllMoves[i, j] = true;
            }
            //Serve as collider to not let it jump over other pieces
            else
            {
                if (Q.isPieceWhite != isPieceWhite)
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

            Q = LocalBoard.Instance.ChessPieces1[i, j];
            if (Q == null)
            {
                AllMoves[i, j] = true;
            }
            //Serve as collider to not let it jump over other pieces
            else
            {
                if (Q.isPieceWhite != isPieceWhite)
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

            Q = LocalBoard.Instance.ChessPieces1[i, j];
            if (Q == null)
            {
                AllMoves[i, j] = true;
            }
            //Serve as collider to not let it jump over other pieces
            else
            {
                if (Q.isPieceWhite != isPieceWhite)
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

            Q = LocalBoard.Instance.ChessPieces1[i, j];
            if (Q == null)
            {
                AllMoves[i, j] = true;
            }
            //Serve as collider to not let it jump over other pieces
            else
            {
                if (Q.isPieceWhite != isPieceWhite)
                {
                    AllMoves[i, j] = true;
                }
                break;
            }
        }


        return AllMoves;
    }
}
