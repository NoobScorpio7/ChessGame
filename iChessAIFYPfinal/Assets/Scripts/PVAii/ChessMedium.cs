using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMedium
{
    ChessBoardEvaluation ChessBoardEvaluation = new ChessBoardEvaluation();
    int whiteScore = 0;
    int blackScore = 0;
    int maxDepth = 3;
    //bool fakeLose = false;
    MoveData bestMove;
    //public ChessPiece1[,] chessPieces1 { set; get; }
    Stack<MoveData> moveStack = new Stack<MoveData>();
    //public bool[,] AllowedChessMoves1 { set; get; }



    public MoveData GetMove()
    {
        //LocalBoard.Instance.destroyAll();
        //LocalBoard.Instance.SpawnChessPiecesAll();
        int depth = MAXDEPTH.Instance.MaxDepth;
        bestMove = CreateMove(0, 0, 0, 0, int.MinValue);
        MiniMax(depth, int.MinValue, int.MaxValue, true);
        //AlphaBetaMax(maxDepth, int.MinValue, int.MaxValue);
        //Debug.Log("MOVE CHESSPIECE: "+ bestMove.PieceMoved +" "+ bestMove.SecondPositionX + "" + bestMove.SecondPositionY);
        moveStack.Clear();
        return bestMove;
    }

    public int MiniMax(int depth, int alpha, int beta, bool max)
    {
        //int maxDepth = 3;
        GameState();

        if (depth == 0)
        {

            //Debug.Log("We are at depth 0");
            return Evaluate();
        }
        if (max)
        {
            int score = int.MinValue;
            List<MoveData> allMoves = GetMoves("Black");
            //Debug.Log("Max Number of AllMoves: " + allMoves.Count);
            foreach (MoveData move in allMoves)
            {
                //  Debug.Log("Max: Depth: " + depth);
                //Debug.Log("Here In Max");
                moveStack.Push(move);
                //Debug.Log("Push");
                //   Debug.Log("Max FakeMove");
                LocalBoard.Instance.MoveChessPiece(move.FirstPositionX, move.FirstPositionY, move.SecondPositionX, move.SecondPositionY, move.PieceKilled, move.PieceMoved);
                //DoMove(move.FirstPositionX, move.FirstPositionY, move.SecondPositionX, move.SecondPositionY);//, move.PieceMoved)
                //Debug.Log("MAX: MovePiece: "+move.PieceMoved+ " FirstPositionX: " + move.FirstPositionX+ " FirstPositionY: " + move.FirstPositionY+ " SecondPositionX: " + move.SecondPositionX + " SecondPositionY" + move.SecondPositionY);
                score = MiniMax(depth - 1, alpha, beta, false);
                UndoFakeMove();

                //  Debug.Log("MIN UNDOFAKEMOVE");
                if (score > alpha)
                {
                    move.score = score;
                    //Debug.Log("MAX:: move score: " + move.score);
                    // Debug.Log("MAX:: bestmove score: " + bestMove.score);
                    Debug.Log("DEPTH: " + MAXDEPTH.Instance.MaxDepth);
                    if (move.score > bestMove.score && depth >= MAXDEPTH.Instance.MaxDepth)
                    {
                        bestMove = move;
                        //Debug.Log("Minimax BestMove" + move);
                    }
                    alpha = score;
                }
                if (score >= beta)
                {
                    break;
                }
            }
            return alpha;
        }
        else
        {

            int score = int.MaxValue;
            List<MoveData> allMoves = GetMoves("White");
            //Debug.Log("Possible moves for white:" + allMoves.Count);
            //Debug.Log("Min: " + max + " Depth:" + depth);
            // Debug.Log("Min Number of AllMoves: " + allMoves.Count);
            foreach (MoveData move in allMoves)
            {
                // Debug.Log("MIN: Depth: " + depth);
                //Debug.Log("Here In Min");
                // Debug.Log("DEPTH at White: " + depth);
                // Debug.Log("Count: White ");
                moveStack.Push(move);
                //Debug.Log("Push");
                //  Debug.Log("Min FakeMove");

                LocalBoard.Instance.MoveChessPiece(move.FirstPositionX, move.FirstPositionY, move.SecondPositionX, move.SecondPositionY, move.PieceKilled, move.PieceMoved);

                //DoMove(move.FirstPositionX, move.FirstPositionY, move.SecondPositionX, move.SecondPositionY);//, move.PieceMoved)
                //Debug.Log("MIN: MovePiece: " + move.PieceMoved + " FirstPositionX: " + move.FirstPositionX + " FirstPositionY: " + move.FirstPositionY + " SecondPositionX: " + move.SecondPositionX + " SecondPositionY" + move.SecondPositionY);
                //Debug.Log("MIN: " + move.PieceMoved + "" + move.FirstPositionX + " " + move.FirstPositionY + " " + move.SecondPositionX + " " + move.SecondPositionY);
                // Debug.Log("Location: "+move.firstPosition + " " + move.secondPosition);
                score = MiniMax(depth - 1, alpha, beta, true);
                //  Debug.Log("Score"+score);
                // Debug.Log("MIN UNDOFAKEMOVE");
                UndoFakeMove();

                //Debug.Log("HERE");
                // Debug.Log("MIN:: move score: " +score);
                if (score < beta)
                {
                    //Debug.Log("MIN:: move score: " + move.score);
                    //Debug.Log("ALPHA SCORE: "+alpha);
                    //Debug.Log("BETA SCORE: " + beta);
                    //Debug.Log("MAX:: bestmove score: " + bestMove.score);
                    move.score = score;
                    beta = score;
                    //Debug.Log("BETA SCORE: " + beta);
                }
                if (score <= alpha)
                {
                    break;
                }
            }
            return beta;
        }
    }



    public void GameState()
    {
        //ChessPiece cp = ChessBoardManager.Instance.ChessPieces[AxisX, AxisY];
        //int count1 = 0;
        //int count2 = 0;
        blackScore = 0;
        whiteScore = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                //ChessPiece cp = ChessBoardManager.Instance.ChessPieces[i, j];
                ChessPiece1 cp = LocalBoard.Instance.ChessPieces1[i, j];
                //ChessPiece cp = ChessPieces[i, j];

                if (cp != null && cp.isPieceWhite)
                {
                    //count1++;
                    whiteScore += ChessBoardEvaluation.GetPieceWeight(i, j);
                }
                else if (cp != null && !cp.isPieceWhite)
                {
                    //count2++;
                    blackScore += ChessBoardEvaluation.GetPieceWeight(i, j);
                }
            }
        }
        //Debug.Log("White Piece Count: "+count1 + " Black Piece Count: "+ count2);
        //Debug.Log("GET BOARD STATE: \nBlackScore: " + (blackScore) + "\nWhiteScore: " + whiteScore);

    }


    int Evaluate()
    {
        float pieceDifference = 0;
        float whiteWeight = 0;
        float blackWeight = 0;
        int count = 0;

        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                //ChessPiece cp = ChessBoardManager.Instance.ChessPieces[i, j];
                ChessPiece1 cp = LocalBoard.Instance.ChessPieces1[i, j];
                //ChessPiece cp = ChessPieces[i, j];
                if (cp != null && cp.isPieceWhite)
                {
                    //Debug.Log("ChessPiece Name: "+ cp.name + " Position: "+i+" " +j+ " PIECE WEIGHT" + ChessBoardEvaluation.GetBoardWeight(i, j, true));
                    count++;
                    whiteWeight += ChessBoardEvaluation.GetBoardWeight(i, j, true);
                }
                else if (cp != null && !cp.isPieceWhite)
                {
                    count++;
                    blackWeight += ChessBoardEvaluation.GetBoardWeight(i, j, false);
                }
            }
        }
        // Debug.Log("GET BOARD STATE: \nBlackScore: " + (blackScore) + "\nWhiteScore: " + whiteScore);
        // Debug.Log("WHITEWEIGHT: " + whiteWeight + "\nBLACKWEIGHT: " + blackWeight);
        pieceDifference = (blackScore + (blackWeight / 100)) - (whiteScore + (whiteWeight / 100));
        Debug.Log("Evaluate Return Value: " + Mathf.RoundToInt(pieceDifference * 100));
        //Debug.Log("Evaluate Run: "+count+"\nEvaluate Return Value: " + Mathf.RoundToInt(pieceDifference * 100));

        return Mathf.RoundToInt(pieceDifference * 100);



    }

    public List<MoveData> GetMoves(string Team)
    {
        // Debug.Log("Possible Moves");
        List<MoveData> turnMove = new List<MoveData>();
        //ChessPiece SelectedChessPiece;
        ChessPiece1 cp, cp1;
        if (Team == "Black")
        {
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    cp = LocalBoard.Instance.ChessPieces1[i, j];
                    if (cp != null && !cp.isPieceWhite)
                    {
                        bool[,] AllowedChessMoves1 = LocalBoard.Instance.ChessPieces1[i, j].LegalMoves();

                        for (int k = 0; k < 8; ++k)
                        {
                            for (int l = 0; l < 8; ++l)
                            {
                                if (AllowedChessMoves1[k, l])
                                {
                                    cp1 = LocalBoard.Instance.ChessPieces1[k, l];
                                    if (cp1 != null && cp1.isPieceWhite)
                                    {
                                        // Debug.Log("PIECE KILLED BOYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY");
                                        // Debug.Log("FirstPositionX:" + i + "FirstPositionY" + j + "PossibileMoveX:" + k + "PossibileMoveY" + l + "ChessPieceKilled:" + cp1.name);
                                        turnMove.Add(new MoveData { FirstPositionX = i, FirstPositionY = j, SecondPositionX = k, SecondPositionY = l, PieceKilled = cp1.name, PieceMoved = cp.name });
                                    }
                                    else
                                    {

                                        //Debug.Log("FirstPositionX:" + i + "FirstPositionY" + j + "PossibileMoveX:" + k + "PossibileMoveY" + l + "ChessPieceMoved:" + cp.name);
                                        turnMove.Add(new MoveData { FirstPositionX = i, FirstPositionY = j, SecondPositionX = k, SecondPositionY = l, PieceMoved = cp.name });

                                        //MoveData newMove = CreateMove();
                                    }
                                }
                            }
                        }
                    }
                    //else if()
                }
            }
        }

        else if (Team == "White")
        {
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    cp = LocalBoard.Instance.ChessPieces1[i, j];
                    if (cp != null && cp.isPieceWhite)
                    {
                        bool[,] AllowedChessMoves1 = LocalBoard.Instance.ChessPieces1[i, j].LegalMoves();

                        for (int k = 0; k < 8; ++k)
                        {
                            for (int l = 0; l < 8; ++l)
                            {
                                if (AllowedChessMoves1[k, l])
                                {
                                    cp1 = LocalBoard.Instance.ChessPieces1[k, l];
                                    if (cp1 != null && !cp1.isPieceWhite)
                                    {
                                        //   Debug.Log("PIECE KILLED BOYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY");
                                        turnMove.Add(new MoveData { FirstPositionX = i, FirstPositionY = j, SecondPositionX = k, SecondPositionY = l, PieceKilled = cp1.name, PieceMoved = cp.name });
                                        //Debug.Log("ChessPieceKilled:" + cp1.name +" FirstPositionX:" + i + "FirstPositionY" + j + "PossibileMoveX:" + k + "PossibileMoveY" + l);
                                    }
                                    else
                                    {
                                        //    Debug.Log("Possible Moves");
                                        //Debug.Log("ChessPiece: " + cp.name+" FirstPositionX: " + i + " FirstPositionY: " + j + " PossibileMoveX: " + k + " PossibileMoveY: " + l);
                                        turnMove.Add(new MoveData { FirstPositionX = i, FirstPositionY = j, SecondPositionX = k, SecondPositionY = l, PieceMoved = cp.name });
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return turnMove;
    }
    public void DoMove(int AxisX1, int AxisY1, int AxisX2, int AxisY2)
    {
        LocalBoard.Instance.SelectChessPiece(AxisX1, AxisY1);
        LocalBoard.Instance.MoveChessPiece(AxisX2, AxisY2);

    }
    public void UndoFakeMove()
    {
        //Debug.Log("POP");
        // Debug.Log("UndoFake");
        MoveData tempMove = moveStack.Pop();
        int FirstPositionX = tempMove.FirstPositionX;
        int FirstPositionY = tempMove.FirstPositionY;
        int SecondPositionX = tempMove.SecondPositionX;
        int SecondPositionY = tempMove.SecondPositionY;
        string PieceKilled = tempMove.PieceKilled;
        string PieceMoved = tempMove.PieceMoved;
       // Debug.Log(" UNDO MOVE: " + " PieceMoved: " + PieceMoved + " TO SecondPositionX: " + SecondPositionX + " SecondPositionY: " + SecondPositionY + " FROM FirstPositionX: " + FirstPositionX + " FirstPositionY: " + FirstPositionY + "Piecekilled: " + PieceKilled);
        LocalBoard.Instance.UndoMoveChessPiece(SecondPositionX, SecondPositionY, FirstPositionX, FirstPositionY, PieceKilled, PieceMoved);
    }
    MoveData CreateMove(int FirstPositionX, int FirstPositionY, int SecondPositionX, int SecondPositionY, int score)
    {

        MoveData tempMove = new MoveData
        {
            FirstPositionX = FirstPositionX,
            FirstPositionY = FirstPositionY,
            SecondPositionX = SecondPositionX,
            SecondPositionY = SecondPositionY,
            score = score

        };
        // Debug.Log("tempMove.firstPosition.Position FOR X AND Y: " + tempMove.firstPosition.Position.x+""+ tempMove.firstPosition.Position.y);
        // Debug.Log("tempMove.pieceMoved.Type: " + tempMove.pieceMoved.Type);
        //Debug.Log("tempMove.secondPosition.Position.x FOR X AND Y" + tempMove.secondPosition.Position.x +""+ tempMove.secondPosition.Position.y);
        /*if (move.CurrentPiece != null)
        {
            tempMove.pieceKilled = move.CurrentPiece;
            //Debug.Log("tempMove.pieceKilled.Type: " + tempMove.pieceKilled.Type);
        }*/
        return tempMove;
    }



}
