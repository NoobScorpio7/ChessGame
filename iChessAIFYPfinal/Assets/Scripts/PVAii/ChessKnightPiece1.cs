using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessKnightPiece1 : ChessPiece1
{


    public override bool[,] LegalMoves()
    {
        bool[,] AllMoves = new bool[8, 8];

        // Knight Move UP Left
        PieceMoveOfKnight(CurrentPositionX - 1, CurrentPositionY + 2, ref AllMoves);

        // Knight Move UP Right
        PieceMoveOfKnight(CurrentPositionX + 1, CurrentPositionY + 2, ref AllMoves);

        // Knight Move Rght Up
        PieceMoveOfKnight(CurrentPositionX + 2, CurrentPositionY + 1, ref AllMoves);

        // Knight Move Right Down
        PieceMoveOfKnight(CurrentPositionX + 2, CurrentPositionY - 1, ref AllMoves);

        // Knight Move Down Left
        PieceMoveOfKnight(CurrentPositionX - 1, CurrentPositionY - 2, ref AllMoves);

        // Knight Move Down Right
        PieceMoveOfKnight(CurrentPositionX + 1, CurrentPositionY - 2, ref AllMoves);

        // Knight Move Left Up
        PieceMoveOfKnight(CurrentPositionX - 2, CurrentPositionY + 1, ref AllMoves);

        // Knight Move left Down
        PieceMoveOfKnight(CurrentPositionX - 2, CurrentPositionY - 1, ref AllMoves);

        return AllMoves;
    }


    //We made method to keep the code clean and simple as the same method will be called again and again for the same move with differnt postions
    public void PieceMoveOfKnight( int AxisX, int AxisY, ref bool[,] AllMoves)
    {
        ChessPiece1 K;

        if (AxisX >= 0 && AxisX < 8 && AxisY >= 0 && AxisY < 8)
        {
            K = LocalBoard.Instance.ChessPieces1[AxisX, AxisY];

            if (K == null)
            {
                AllMoves[AxisX, AxisY] = true;
            }
            else if (isPieceWhite != K.isPieceWhite)
            {
                AllMoves[AxisX, AxisY] = true;
            }
        }
    }
}
