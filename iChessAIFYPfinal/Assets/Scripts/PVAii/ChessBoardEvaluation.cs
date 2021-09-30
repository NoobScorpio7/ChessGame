using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardEvaluation 
{

    public ChessPiece1[,] ChessPieces1 { set; get; }

    int[,] PawnWhiteBoardWeight = new int[,]
    {
        { 5,  5,  5,  5,  5,  5,  5,  5},
        {30, 30, 30, 30, 30, 30, 30, 30},
        {10, 10, 20, 30, 30, 20, 10, 10},
        { 5,  5, 10, 25, 25, 10,  5,  5},
        { 0,  0,  0, 20, 20,  0,  0,  0},
        { 5, -5,-10,  0,  0,-10, -5,  5},
        { 5, 10, 10,-20,-20, 10, 10,  5},
        { 5,  5,  5,  5,  5,  5,  5,  5}
    };

    int[,] PawnBlackBoardWeight = new int[,]
    {
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 5, 10, 10,-20,-20, 10, 10,  5},
        { 5, -5,-10,  0,  0,-10, -5,  5},
        { 0,  0,  0, 20, 20,  0,  0,  0},
        { 5,  5, 10, 25, 25, 10,  5,  5},
        {10, 10, 20, 30, 30, 20, 10, 10},
        {30, 30, 30, 30, 30, 30, 30, 30},
        { 5,  5,  5,  5,  5,  5,  5,  5},
    };

    int[,] BishopWhiteBoardWeight = new int[,]
    {
        {-20,-10,-10,-10,-10,-10,-10,-20},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-10,  0,  5, 10, 10,  5,  0,-10},
        {-10,  5,  5, 10, 10,  5,  5,-10},
        {-10,  0, 10, 10, 10, 10,  0,-10},
        {-10, 10, 10, 10, 10, 10, 10,-10},
        {-10,  5,  0,  0,  0,  0,  5,-10},
        {-20,-10,-10,-10,-10,-10,-10,-20}
    };

    int[,] BishopBlackBoardWeight = new int[,]
    {
        {-20,-10,-10,-10,-10,-10,-10,-20},
        {-10,  5,  0,  0,  0,  0,  5,-10},
        {-10, 10, 10, 10, 10, 10, 10,-10},
        {-10,  0, 10, 10, 10, 10,  0,-10},
        {-10,  5,  5, 10, 10,  5,  5,-10},
        {-10,  0,  5, 10, 10,  5,  0,-10},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-20,-10,-10,-10,-10,-10,-10,-20}
    };

    int[,] KnightWhiteBoardWeight = new int[,]
    {
        {-50,-40,-30,-30,-30,-30,-40,-50},
        {-40,-20,  0,  0,  0,  0,-20,-40},
        {-30,  0, 10, 15, 15, 10,  0,-30},
        {-30,  5, 15, 25, 25, 15,  5,-30},
        {-30,  0, 15, 25, 25, 15,  0,-30},
        {-30,  5, 10, 15, 15, 10,  5,-30},
        {-40,-20,  0,  5,  5,  0,-20,-40},
        {-50,-40,-30,-30,-30,-30,-40,-50}
    };

    int[,] KnightBlackBoardWeight = new int[,]
    {
        {-50,-40,-30,-30,-30,-30,-40,-50},
        {-40,-20,  0,  5,  5,  0,-20,-40},
        {-30,  5, 10, 15, 15, 10,  5,-30},
        {-30,  0, 15, 25, 25, 15,  0,-30},
        {-30,  5, 15, 25, 25, 15,  5,-30},
        {-30,  0, 10, 15, 15, 10,  0,-30},
        {-40,-20,  0,  0,  0,  0,-20,-40},
        {-50,-40,-30,-30,-30,-30,-40,-50}
    };

    int[,] RookWhiteBoardWeight = new int[,]
    {
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 5, 10, 10, 10, 10, 10, 10,  5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        { 0,  0,  0,  5,  5,  0,  0,  0}
    };

    int[,] RookBlackBoardWeight = new int[,]
    {
        { 0,  0,  0,  5,  5,  0,  0,  0},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        { 5, 10, 10, 10, 10, 10, 10,  5},
        { 0,  0,  0,  0,  0,  0,  0,  0}
    };

    int[,] QueenWhiteBoardWeight = new int[,]
    {
        {-20,-10,-10, -5, -5,-10,-10,-20},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-10,  0,  5, 10, 10,  5,  0,-10},
        { -5,  0, 10, 15, 15, 10,  0, -5},
        {  0,  0, 10, 15, 15, 10,  0, -5},
        {-10,  5,  5, 10, 10,  5,  0,-10},
        {-10,  0,  5,  0,  0,  0,  0,-10},
        {-20,-10,-10, -5, -5,-10,-10,-20}
    };

    int[,] QueenBlackBoardWeight = new int[,]
    {
        {-20,-10,-10, -5, -5,-10,-10,-20},
        {-10,  0,  5,  0,  0,  0,  0,-10},
        {-10,  5,  5,  5,  5,  5,  0,-10},
        {  0,  0,  5,  5,  5,  5,  0, -5},
        { -5,  0,  5,  5,  5,  5,  0, -5},
        {-10,  0,  5,  5,  5,  5,  0,-10},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-20,-10,-10, -5, -5,-10,-10,-20}
    };

    int[,] KingWhiteBoardWeight =
    {
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-20,-30,-30,-40,-40,-30,-30,-20},
        {-10,-20,-20,-20,-20,-20,-20,-10},
        { 20, 20,  0,  0,  0,  0, 20, 20},
        { 20, 30, 10,  0,  0, 10, 30, 20}
    };

    int[,] KingBlackBoardWeight =
    {
        { 20, 30, 10,  0,  0, 10, 30, 20},
        { 20, 20,  0,  0,  0,  0, 20, 20},
        {-10,-20,-20,-20,-20,-20,-20,-10},
        {-20,-30,-30,-40,-40,-30,-30,-20},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
    };


    public int GetBoardWeight(int AxisX, int AxisY, bool team)
    {
        ChessPiece1 cp = LocalBoard.Instance.ChessPieces1[AxisX, AxisY];
        //ChessPiece cp = ChessBoardManager.Instance.ChessPieces[AxisX, AxisY];

        if (team == true)
        {
            if (cp.GetType() == typeof(ChessPawnPiece1))
            {
                return PawnWhiteBoardWeight[(int)AxisX, (int)AxisY];
            }
            else if (cp.GetType() == typeof(ChessRookPiece1) && cp.isPieceWhite)
            {
                return RookWhiteBoardWeight[(int)AxisX, (int)AxisY];
            }
            else if (cp.GetType() == typeof(ChessKnightPiece1) && cp.isPieceWhite)
            {
                return KnightWhiteBoardWeight[(int)AxisX, (int)AxisY];
            }
            else if (cp.GetType() == typeof(ChessQueenPiece1) && cp.isPieceWhite)
            {
                return QueenWhiteBoardWeight[(int)AxisX, (int)AxisY];
            }
            else if (cp.GetType() == typeof(ChessBishopPiece1) && cp.isPieceWhite)
            {
                return BishopWhiteBoardWeight[(int)AxisX, (int)AxisY];
            }
            else if (cp.GetType() == typeof(ChessKingPiece1) && cp.isPieceWhite)
            {
                return KingWhiteBoardWeight[(int)AxisX, (int)AxisY];
            }
        }
        else if (team == false)
        {
            if (cp.GetType() == typeof(ChessPawnPiece1) && !cp.isPieceWhite)
            {
                return PawnBlackBoardWeight[(int)AxisX, (int)AxisY];
            }
            else if (cp.GetType() == typeof(ChessRookPiece1) && !cp.isPieceWhite)
            {
                return RookBlackBoardWeight[(int)AxisX, (int)AxisY];
            }
            else if (cp.GetType() == typeof(ChessKnightPiece1) && !cp.isPieceWhite)
            {
                return KnightBlackBoardWeight[(int)AxisX, (int)AxisY];
            }
            else if (cp.GetType() == typeof(ChessQueenPiece1) && !cp.isPieceWhite)
            {
                return QueenBlackBoardWeight[(int)AxisX, (int)AxisY];
            }
            else if (cp.GetType() == typeof(ChessBishopPiece1) && !cp.isPieceWhite)
            {
                return BishopBlackBoardWeight[(int)AxisX, (int)AxisY];
            }
            else if (cp.GetType() == typeof(ChessKingPiece1) && !cp.isPieceWhite)
            {
                return KingBlackBoardWeight[(int)AxisX, (int)AxisY];
            }
            /*
            if (team == true)
            {
                if (cp.GetType() == typeof(ChessPawnPiece1))
                {
                   // int a = 0;
                   // a = PawnWhiteBoardWeight[(int)AxisX, (int)AxisY];
                   // Debug.Log("Pawn Weight:  " + a);
                    return PawnWhiteBoardWeight[(int)AxisX, (int)AxisY];
                }
                else if (cp.GetType() == typeof(ChessRookPiece1) && cp.isPieceWhite)
                {
                    return RookWhiteBoardWeight[(int)AxisX, (int)AxisY];
                }
                else if (cp.GetType() == typeof(ChessKnightPiece1) && cp.isPieceWhite)
                {
                    return KnightWhiteBoardWeight[(int)AxisX, (int)AxisY];
                }
                else if (cp.GetType() == typeof(ChessQueenPiece1) && cp.isPieceWhite)
                {
                    return QueenWhiteBoardWeight[(int)AxisX, (int)AxisY];
                }
                else if (cp.GetType() == typeof(ChessBishopPiece1) && cp.isPieceWhite)
                {
                    return BishopWhiteBoardWeight[(int)AxisX, (int)AxisY];
                }
               else if (cp.GetType() == typeof(ChessKingPiece1) && cp.isPieceWhite)
                {
                    return KingWhiteBoardWeight[(int)AxisX, (int)AxisY];
                }
            }
            else if (team == false)
            {
                if (cp.GetType() == typeof(ChessPawnPiece1) && !cp.isPieceWhite)
                {
                    return PawnBlackBoardWeight[(int)AxisX, (int)AxisY];
                }
                else if (cp.GetType() == typeof(ChessRookPiece1) && !cp.isPieceWhite)
                {
                    return RookBlackBoardWeight[(int)AxisX, (int)AxisY];
                }
               else if (cp.GetType() == typeof(ChessKnightPiece1) && !cp.isPieceWhite)
                {
                    return KnightBlackBoardWeight[(int)AxisX, (int)AxisY];
                }
               else if (cp.GetType() == typeof(ChessQueenPiece1) && !cp.isPieceWhite)
                {
                    return QueenBlackBoardWeight[(int)AxisX, (int)AxisY];
                }
               else if (cp.GetType() == typeof(ChessBishopPiece1) && !cp.isPieceWhite)
                {
                    return BishopBlackBoardWeight[(int)AxisX, (int)AxisY];
                }
                else if (cp.GetType() == typeof(ChessKingPiece1) && !cp.isPieceWhite)
                {
                    return KingBlackBoardWeight[(int)AxisX, (int)AxisY];
                }
            */
        }
        return -1;
        }

    public int GetPieceWeight(int AxisX, int AxisY)
    {
        //ChessPiece cp = ChessBoardManager.Instance.ChessPieces[AxisX, AxisY];
        ChessPiece1 cp = LocalBoard.Instance.ChessPieces1[AxisX, AxisY];
        /* bool Pawn = cp.GetType() == typeof(ChessPawnPiece1);
         bool Rook = cp.GetType() == typeof(ChessRookPiece1);
         bool Knight = cp.GetType() == typeof(ChessKnightPiece1);
         bool Bishop = cp.GetType() == typeof(ChessBishopPiece1);
         bool Queen = cp.GetType() == typeof(ChessQueenPiece1);
         bool King = cp.GetType() == typeof(ChessKingPiece1);*/
        /* switch (type)*/

        /*if (cp.GetType() == typeof(ChessPawnPiece1) && cp.isPieceWhite) {
            return 1;
        }
        else if (cp.GetType() == typeof(ChessPawnPiece1) && !cp.isPieceWhite)
        {
            return -1;
        }
        else if (cp.GetType() == typeof(ChessRookPiece1) && cp.isPieceWhite) {
                return 5;
            }
        else if (cp.GetType() == typeof(ChessRookPiece1) && !cp.isPieceWhite)
        {
            return -5;
        }
        else if (cp.GetType() == typeof(ChessKnightPiece1) && cp.isPieceWhite)
        {
            return 3;
            }
        else if (cp.GetType() == typeof(ChessKnightPiece1) && !cp.isPieceWhite)
        {
            return -3;
        }
        else if (cp.GetType() == typeof(ChessBishopPiece1) && cp.isPieceWhite)
        {
            return 3;
            }
        else if (cp.GetType() == typeof(ChessBishopPiece1) && !cp.isPieceWhite)
        {
            return -3;
        }
        else if (cp.GetType() == typeof(ChessQueenPiece1) && cp.isPieceWhite)
        {
            return 9;
            }
        else if (cp.GetType() == typeof(ChessQueenPiece1) && !cp.isPieceWhite)
        {
            return -9;
        }
        else if (cp.GetType() == typeof(ChessKingPiece1) && cp.isPieceWhite)
        {
            return 999;
            }
        else if (cp.GetType() == typeof(ChessKingPiece1) && !cp.isPieceWhite)
        {
            return -999;
        }
        else {
                return -1;
            }
        */
        //if (cp.isPieceWhite)
        //{
        if (cp.GetType() == typeof(ChessBishopPiece1))
            {
                return 3;

            }
            else if (cp.GetType() == typeof(ChessPawnPiece1))
            {
                return 1;
            }
            else if (cp.GetType() == typeof(ChessRookPiece1))
            {
                return 5;
            }
            else if (cp.GetType() == typeof(ChessKnightPiece1))
            {
                return 3;
            }
            else if (cp.GetType() == typeof(ChessQueenPiece1))
            {
                return 9;
            }
            else if (cp.GetType() == typeof(ChessKingPiece1))
            {
                return 10000;
            }
            //   return -1;
            /* }
             else if (!cp.isPieceWhite)
             {
                 if (cp.GetType() == typeof(ChessBishopPiece1))
                 {
                     return -3;

                 }
                 else if (cp.GetType() == typeof(ChessPawnPiece1))
                 {
                     return -1;
                 }
                 else if (cp.GetType() == typeof(ChessRookPiece1))
                 {
                     return -5;
                 }
                 else if (cp.GetType() == typeof(ChessKnightPiece1))
                 {
                     return -3;
                 }
                 else if (cp.GetType() == typeof(ChessQueenPiece1))
                 {
                     return -9;
                 }
                 else if (cp.GetType() == typeof(ChessKingPiece1))
                 {
                     return -1000000;
                 }
                 return -1;
             }*/

            else
            {
                return -1;
            }
        }

}


