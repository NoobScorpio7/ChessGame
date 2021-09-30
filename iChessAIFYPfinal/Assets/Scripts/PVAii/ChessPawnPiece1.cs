using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPawnPiece1 : ChessPiece1
{

    public override bool[,] LegalMoves()
    {

        bool[,] AllMoves = new bool[8, 8];
        ChessPiece1 p1, p2;

        //Enpassant move aray to store the vectors
        //int[] enpassant = ChessBoardManager.Instance.SpecialMoveEnPassant;


        //White Team Move

        if (isPieceWhite)
        {
            //Diagonal left
            if (CurrentPositionX != 0 && CurrentPositionY != 7)
            {
                //For ENPassant move
                /*if (enpassant[0] == CurrentPositionX - 1 && enpassant[1] == CurrentPositionY + 1)
                {
                    AllMoves[CurrentPositionX - 1, CurrentPositionY + 1] = true;
                }*/

                p1 = LocalBoard.Instance.ChessPieces1[CurrentPositionX - 1, CurrentPositionY + 1];
                if (p1 != null && !p1.isPieceWhite)
                {
                    AllMoves[CurrentPositionX - 1, CurrentPositionY + 1] = true;
                }
            }
            //Diagonal Right
            if (CurrentPositionX != 7 && CurrentPositionY != 7)
            {

                //For ENPassant move
                /*if (enpassant[0] == CurrentPositionX + 1 && enpassant[1] == CurrentPositionY + 1)
                {
                    AllMoves[CurrentPositionX + 1, CurrentPositionY + 1] = true;
                }*/


                p1 = LocalBoard.Instance.ChessPieces1[CurrentPositionX + 1, CurrentPositionY + 1];
                if (p1 != null && !p1.isPieceWhite)
                    AllMoves[CurrentPositionX + 1, CurrentPositionY + 1] = true;
            }

            //Forward
            if (CurrentPositionY != 7)
            {
                p1 = LocalBoard.Instance.ChessPieces1[CurrentPositionX, CurrentPositionY + 1];
                if (p1 == null)
                {
                    AllMoves[CurrentPositionX, CurrentPositionY + 1] = true;
                }
            }
            //Middle on First move

            if (CurrentPositionY == 1)
            {
                p1 = LocalBoard.Instance.ChessPieces1[CurrentPositionX, CurrentPositionY + 1];
                p2 = LocalBoard.Instance.ChessPieces1[CurrentPositionX, CurrentPositionY + 2];
                if (p1 == null && p2 == null)
                {
                    AllMoves[CurrentPositionX, CurrentPositionY + 2] = true;
                }
            }
        }


        //Black Team Move
        else
        {

            //Diagonal left
            if (CurrentPositionX != 0 && CurrentPositionY != 0)
            {

                //For ENPassant move
               /* if (enpassant[0] == CurrentPositionX - 1 && enpassant[1] == CurrentPositionY - 1)
                {
                    AllMoves[CurrentPositionX - 1, CurrentPositionY - 1] = true;
                }*/

                p1 = LocalBoard.Instance.ChessPieces1[CurrentPositionX - 1, CurrentPositionY - 1];
                if (p1 != null && p1.isPieceWhite)
                    AllMoves[CurrentPositionX - 1, CurrentPositionY - 1] = true;
            }

            //Diagonal Right
            if (CurrentPositionX != 7 && CurrentPositionY != 0)
            {

                //For ENPassant move
                /*if (enpassant[0] == CurrentPositionX + 1 && enpassant[1] == CurrentPositionY - 1)
                {
                    AllMoves[CurrentPositionX + 1, CurrentPositionY - 1] = true;
                }*/

                p1 = LocalBoard.Instance.ChessPieces1[CurrentPositionX + 1, CurrentPositionY - 1];
                if (p1 != null && p1.isPieceWhite)
                    AllMoves[CurrentPositionX + 1, CurrentPositionY - 1] = true;
            }

            //Forwards
            if (CurrentPositionY != 0)
            {
                p1 = LocalBoard.Instance.ChessPieces1[CurrentPositionX, CurrentPositionY - 1];
                if (p1 == null)
                {
                    AllMoves[CurrentPositionX, CurrentPositionY - 1] = true;
                }
            }

            //Middle on First move
            if (CurrentPositionY == 6)
            {
                p1 = LocalBoard.Instance.ChessPieces1[CurrentPositionX, CurrentPositionY - 1];
                p2 = LocalBoard.Instance.ChessPieces1[CurrentPositionX, CurrentPositionY - 2];
                if (p1 == null && p2 == null)
                {
                    AllMoves[CurrentPositionX, CurrentPositionY - 2] = true;
                }
            }

        }
        return AllMoves;
    }
    
}
