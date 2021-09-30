using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalBoard : MonoBehaviour
{
    // Start is called before the first frame update
    public static LocalBoard Instance { set; get; }
    Stack<MoveData> moveStack1 = new Stack<MoveData>();


    public List<GameObject> ActiveChessPieces1 = new List<GameObject>();
    public List<GameObject> ActiveChessPieces2 = new List<GameObject>();
    public List<GameObject> ChessPiecesPrefabs2;
    public List<GameObject> KilledChessPieces = new List<GameObject>();
    ChessMedium cm = new ChessMedium();
    public bool IsItWhiteTurn = true;
    public ChessPiece1[,] ChessPieces1 { set; get; }
    public ChessPiece1[,] ChessPieces2 { set; get; }
    private const float ChessBoardTileSize = 1.0f;

    private ChessPiece1 SelectedChessPiece;
    private ChessPiece1 SelectedChessPiece2;
    private const float ChessBoardTileOffset = 0.5f;
    public bool[,] AllowedChessMoves { set; get; }
    public bool[,] AllowedChessMoves2 { set; get; }
    //ChessMedium cm = new ChessMedium();
    public int[] SpecialMoveEnPassant { set; get; }
    void Start()
    {
        Instance = this;
        //SpawnChessPiecesAll1();
    }

    void Update()
    {

    }


    public void DoAiMove1() {
        //if (!ChessBoardManager.Instance.IsItWhiteTurn)
        {
            destroyAll();
            SpawnChessPiecesAll();
            //MoveChessPiece(0,6,0,5);
            MoveData move = cm.GetMove();
            //Debug.Log("MOVE CHESSPIECE"+move.SecondPositionX +""+ move.SecondPositionY);
            //ChessBoardManager.Instance.MoveChessPiece1(move.FirstPositionX, move.FirstPositionY, move.SecondPositionX, move.SecondPositionY);
            //Debug.Log("MOVE CHESSPIECE" + move.SecondPositionX + "" + move.SecondPositionY);
            ChessBoardManager.Instance.SelectChessPiece1(move.FirstPositionX, move.FirstPositionY);
            ChessBoardManager.Instance.MoveChessPiecePlayers1(move.SecondPositionX, move.SecondPositionY);
            //destroyAll();
            //ChessPiecePositions();

        }
    }

  

    public void destroyAll() {
        foreach (GameObject gameobject in ActiveChessPieces1)
        {
            //Debug.Log("DestroyAll");
            Destroy(gameobject);
        }
    }

    private Vector3 GetChessBoardTileCenter(int AxisX, int AxisZ)
    {

        //Vector3 ChessBoardOrigin = Vector3.zero;
        Vector3 ChessBoardOrigin;
        ChessBoardOrigin.x = 14;
        ChessBoardOrigin.y = 14;
        ChessBoardOrigin.z = 0;
        //Debug.Log("X:" + ChessBoardOrigin.x);
        // Debug.Log("Y:" + ChessBoardOrigin.y);
        // Debug.Log("Z:" + ChessBoardOrigin.z);
        ChessBoardOrigin = new Vector3(ChessBoardOrigin.x, ChessBoardOrigin.y, ChessBoardOrigin.z);
        // Debug.Log("ChesssBoardOrigin: " + ChessBoardOrigin);
        //ChessBoardOrigin = transform.position;
        //ChessBoardOrigin.x = 1.3f;
        ChessBoardOrigin.x = ChessBoardOrigin.x + (ChessBoardTileSize * AxisX) + ChessBoardTileOffset;
        ChessBoardOrigin.z = ChessBoardOrigin.y + (ChessBoardTileSize * AxisZ) + ChessBoardTileOffset;

        return ChessBoardOrigin;

    }
    private void SpawnChessPieces(int ChessIndex, int AxisX, int AxisY)
    {
        //Debug.Log("Axis X:" + AxisX);
        //Debug.Log("Axis Y:" + AxisY);
        GameObject gameobject = Instantiate(ChessPiecesPrefabs2[ChessIndex], GetChessBoardTileCenter(AxisX, AxisY), Quaternion.identity) as GameObject;

        gameobject.transform.SetParent(transform);

        ChessPieces1[AxisX, AxisY] = gameobject.GetComponent<ChessPiece1>();
        //Debug.Log("Piece Added");
        ChessPieces1[AxisX, AxisY].SetChessPosition(AxisX, AxisY);

        ActiveChessPieces1.Add(gameobject);
        //ChessPiecePositions();

    }

    public void SpawnChessPiecesAll()
    {
        SpecialMoveEnPassant = new int[2] { -1, -1 };
        ActiveChessPieces1 = new List<GameObject>();
        // Debug.Log("Inside Spawn Chess Piece");
        //ChessPieces = new ChessPiece[8, 8];
        //White
        int count = 0;
        ChessPieces1 = new ChessPiece1[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                ChessPiece cp = ChessBoardManager.Instance.ChessPieces[i, j];
                //ChessPiece cp = ChessPieces[i, j];
                if (cp != null && cp.isPieceWhite)
                {
                    if (cp.GetType() == typeof(ChessKingPiece))
                    {
                        SpawnChessPieces(0, i, j);
                    }
                    if (cp.GetType() == typeof(ChessQueenPiece))
                    {
                        SpawnChessPieces(1, i, j);
                    }
                    if (cp.GetType() == typeof(ChessRookPiece))
                    {
                        SpawnChessPieces(2, i, j);
                    }
                    if (cp.GetType() == typeof(ChessBishopPiece))
                    {
                        SpawnChessPieces(3, i, j);
                    }
                    if (cp.GetType() == typeof(ChessKnightPiece))
                    {
                        SpawnChessPieces(4, i, j);
                    }
                    if (cp.GetType() == typeof(ChessPawnPiece))
                    {
                        SpawnChessPieces(5, i, j);
                        count++;
                    }
                }
                else if (cp != null && !cp.isPieceWhite)
                {
                    if (cp.GetType() == typeof(ChessKingPiece))
                    {
                        SpawnChessPieces(6, i, j);
                    }
                    if (cp.GetType() == typeof(ChessQueenPiece))
                    {
                        SpawnChessPieces(7, i, j);
                    }

                    if (cp.GetType() == typeof(ChessRookPiece))
                    {
                        SpawnChessPieces(8, i, j);

                    }
                    if (cp.GetType() == typeof(ChessBishopPiece))
                    {
                        SpawnChessPieces(9, i, j);
                    }
                    if (cp.GetType() == typeof(ChessKnightPiece))
                    {
                        SpawnChessPieces(10, i, j);
                    }
                    if (cp.GetType() == typeof(ChessPawnPiece))
                    {
                        SpawnChessPieces(11, i, j);
                    }
                }
            }
        }
    }
    
    public void MoveChessPiece(int AxisX1, int AxisY1, int AxisX2, int AxisY2, string pieceKilled, string piceMoved)
    {
        //Debug.Log("ONCE");

        SelectedChessPiece = ChessPieces1[AxisX1, AxisY1];
        //this if statement  will see if the move is legal
        //Then the selected chesspiece
        //if (AllowedChessMoves[AxisX2, AxisY2])
        {

            //The below if statement will remove the chesspiece from the board
            //We will create instance of chesspiece and take the position of the chesspiece
            ChessPiece1 cp = ChessPieces1[AxisX2, AxisY2];

            //This condition will check
            //1. if there is piece where the other piece is going to be placed
            //2. and is it white turn or not
            if ((cp != null && cp.isPieceWhite) || (cp != null && !cp.isPieceWhite))// != IsItWhiteTurn)
            {
                //Debug.Log("PIECE KILLED : "+ pieceKilled + " AT AxisX: " + AxisX2 + " AxisY: "+AxisY2 +" FROM AxisX: "+AxisX1+" AxisY1: " +AxisY1);
                //This will see if it is the king then end the game
                if (cp.GetType() == typeof(ChessKingPiece))
                {
                    //Debug.Log("PIECE KILLED");
                    //End game
                    //ChessBoardManager.Instance.GameWon();
                    //return;
                }

                //Capture pice
                //If it is then we will remove the chess piece from the active peices on the board
                //Debug.Log("PIECE KILLED");
                ActiveChessPieces1.Remove(cp.gameObject);
                //  KilledChessPieces.Add(cp.gameObject);
                Destroy(cp.gameObject);

            }
            //if()

            /*//this is for the en passant move
             if (AxisX2 == SpecialMoveEnPassant[0] && AxisY2 == SpecialMoveEnPassant[1])
             {
                 //for white
                 if (IsItWhiteTurn)
                 {
                     cp = ChessPieces1[AxisX2, AxisY2 - 1];
                     ActiveChessPieces1.Remove(cp.gameObject);
                     Destroy(cp.gameObject);
                 }

                 else
                 {
                     cp = ChessPieces1[AxisX2, AxisY2 + 1];
                     ActiveChessPieces1.Remove(cp.gameObject);
                     Destroy(cp.gameObject);
                 }
             }
             SpecialMoveEnPassant[0] = -1;
             SpecialMoveEnPassant[1] = -1;*/
            if (SelectedChessPiece.GetType() == typeof(ChessPawnPiece))
            {
                //For special move promotion
                if (AxisY2 == 7)
                {
                    ActiveChessPieces1.Remove(SelectedChessPiece.gameObject);
                    Destroy(SelectedChessPiece.gameObject);
                    SpawnChessPieces(1, AxisX2, AxisY2);
                    SelectedChessPiece = ChessPieces1[AxisX2, AxisY2];
                }
                else if (AxisY2 == 0)
                {
                    ActiveChessPieces1.Remove(SelectedChessPiece.gameObject);
                    Destroy(SelectedChessPiece.gameObject);
                    SpawnChessPieces(7, AxisX2, AxisY2);
                    SelectedChessPiece = ChessPieces1[AxisX2, AxisY2];
                }
                //for white
                /* if (SelectedChessPiece.CurrentPositionY == 1 && AxisY2 == 3)
                 {
                     SpecialMoveEnPassant[0] = AxisX2;
                     SpecialMoveEnPassant[1] = AxisY2 - 1;
                 }
                 //for black
                 else if (SelectedChessPiece.CurrentPositionY == 6 && AxisY2 == 4)
                 {
                     SpecialMoveEnPassant[0] = AxisX2;
                     SpecialMoveEnPassant[1] = AxisY2 + 1;
                 }*/
            }

            //Then the current position of the chess piece to be moved is set to null because the piece is about to be moved
            // Debug.Log(" DO MOVE: PieceType:" + piceMoved + "FirstPositionX: " + AxisX1 + " FirstPositionY: " + AxisY1 + " SecondPositionX: " + AxisX2 + " SecondPositionY: " + AxisY2);
                ChessPieces1[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;

            //This line of code will get the new position of the piece ans it will enable the movement of the chess piece
                SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX2, AxisY2);
            
            //Then we will save the new position of the chess piece in the 2D array
            //It will put it at the right spot in the 2D array.
            ChessPieces1[AxisX2, AxisY2] = SelectedChessPiece;
            SelectedChessPiece.SetChessPosition(AxisX2, AxisY2);
            //This line of code will check whore turn it is and disbale white or blacl pieces repsectively
            //IsItWhiteTurn = !IsItWhiteTurn;
        }
        //If the move is not possible in general then it will unselect the piece
        SelectedChessPiece = null;





    }

    public void UndoMoveChessPiece(int AxisX1, int AxisY1, int AxisX2, int AxisY2, string piecekilled, string pieceMoved)
    {
        //Debug.Log("ONCE");
        //if (piecekilled == null)
        {
            SelectedChessPiece = ChessPieces1[AxisX1, AxisY1];
            //this if statement  will see if the move is legal
            //Then the selected chesspiece
            //if (AllowedChessMoves[AxisX2, AxisY2])
            {

                //The below if statement will remove the chesspiece from the board
                //We will create instance of chesspiece and take the position of the chesspiece
                ChessPiece1 cp = ChessPieces1[AxisX2, AxisY2];

                //This condition will check
                //1. if there is piece where the other piece is going to be placed
                //2. and is it white turn or not
                if (piecekilled != null)
                {
                    if ((cp != null && cp.isPieceWhite) || (cp != null && !cp.isPieceWhite))//(cp != null )//&& !cp.isPieceWhite)// && cp.isPieceWhite != IsItWhiteTurn)
                    {
                        //    Debug.Log("Piece killed Before");
                        //Debug.Log("PIECE KILLED");
                        //This will see if it is the king then end the game
                        if (cp.GetType() == typeof(ChessKingPiece))
                        {
                            //     Debug.Log("PIECE KILLED King");
                            //End game
                            ChessBoardManager.Instance.GameWon();
                            return;
                        }

                        //Capture pice
                        //If it is then we will remove the chess piece from the active peices on the board
                        //Debug.Log("PIECE KILLED");
                        ActiveChessPieces1.Remove(cp.gameObject);
                        //  KilledChessPieces.Add(cp.gameObject);
                        Destroy(cp.gameObject);

                    }
                }
                //if()

                //this is for the en passant move
                /* if (AxisX2 == SpecialMoveEnPassant[0] && AxisY2 == SpecialMoveEnPassant[1])
                 {
                     //for white
                     if (IsItWhiteTurn)
                     {
                         cp = ChessPieces1[AxisX2, AxisY2 - 1];
                         ActiveChessPieces1.Remove(cp.gameObject);
                         Destroy(cp.gameObject);
                     }

                     else
                     {
                         cp = ChessPieces1[AxisX2, AxisY2 + 1];
                         ActiveChessPieces1.Remove(cp.gameObject);
                         Destroy(cp.gameObject);
                     }
                 }
                 SpecialMoveEnPassant[0] = -1;
                 SpecialMoveEnPassant[1] = -1;
                 if (SelectedChessPiece.GetType() == typeof(ChessPawnPiece))
                 {
                     //For special move promotion
                     if (AxisY2 == 7)
                     {
                         ActiveChessPieces1.Remove(SelectedChessPiece.gameObject);
                         Destroy(SelectedChessPiece.gameObject);
                         SpawnChessPieces(1, AxisX2, AxisY2);
                         SelectedChessPiece = ChessPieces1[AxisX2, AxisY2];
                     }
                     else if (AxisY2 == 0)
                     {
                         ActiveChessPieces1.Remove(SelectedChessPiece.gameObject);
                         Destroy(SelectedChessPiece.gameObject);
                         SpawnChessPieces(7, AxisX2, AxisY2);
                         SelectedChessPiece = ChessPieces1[AxisX2, AxisY2];
                     }
                     //for white
                     if (SelectedChessPiece.CurrentPositionY == 1 && AxisY2 == 3)
                     {
                         SpecialMoveEnPassant[0] = AxisX2;
                         SpecialMoveEnPassant[1] = AxisY2 - 1;
                     }
                     //for black
                     else if (SelectedChessPiece.CurrentPositionY == 6 && AxisY2 == 4)
                     {
                         SpecialMoveEnPassant[0] = AxisX2;
                         SpecialMoveEnPassant[1] = AxisY2 + 1;
                     }
                 }*/

                //Then the current position of the chess piece to be moved is set to null because the piece is about to be moved
                // Debug.Log(" UNDO MOVE: PieceType:" + pieceMoved + " SecondPositionX: " + AxisX1 + " SecondPositionY: " + AxisY1 + " TO FirstPositionX: " + AxisX2 + " FirstPositionY: " + AxisY2);
                // try
                {
                    ChessPieces1[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                }
                //   catch (Exception e)
                {
                    // Debug.Log(" UNDO MOVE: PieceType: + cp.name + SecondPositionX: " + AxisX1 + " SecondPositionY: " + AxisY1 + " FirstPositionX: " + AxisX2 + " FirstPositionY: " + AxisY2);
                    //     Debug.Log(e.Message);
                }
                //This line of code will get the new position of the piece ans it will enable the movement of the chess piece
                SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX2, AxisY2);

                //Then we will save the new position of the chess piece in the 2D array
                //It will put it at the right spot in the 2D array.
                ChessPieces1[AxisX2, AxisY2] = SelectedChessPiece;
                SelectedChessPiece.SetChessPosition(AxisX2, AxisY2);
                //This line of code will check whore turn it is and disbale white or blacl pieces repsectively
                //IsItWhiteTurn = !IsItWhiteTurn;
            }
            //If the move is not possible in general then it will unselect the piece
            SelectedChessPiece = null;
        }
        if (piecekilled != null)
        {
            // Debug.Log("PieceUnkilled: "+piecekilled+ " AT AxisX: " + AxisX1 + "AxisY: " + AxisY1);
            if (piecekilled == "pawn black(Clone)")
            {
                SpawnChessPieces(11, AxisX1, AxisY1);
            }

            else if (piecekilled == "knight black(Clone)")
            {
                SpawnChessPieces(10, AxisX1, AxisY1);
            }
            else if (piecekilled == "bishop black(Clone)")
            {
                SpawnChessPieces(9, AxisX1, AxisY1);
            }
            else if (piecekilled == "rook black(Clone)")
            {
                SpawnChessPieces(8, AxisX1, AxisY1);
            }
            else if (piecekilled == "queen black(Clone)")
            {
                SpawnChessPieces(7, AxisX1, AxisY1);
            }
            else if (piecekilled == "king black(Clone)")
            {
                SpawnChessPieces(6, AxisX1, AxisY1);
            }
            else if (piecekilled == "pawn white(Clone)")
            {
                SpawnChessPieces(5, AxisX1, AxisY1);
            }
            else if (piecekilled == "knight white(Clone)")
            {
                SpawnChessPieces(4, AxisX1, AxisY1);
            }
            else if (piecekilled == "bishop white(Clone)")
            {
                SpawnChessPieces(3, AxisX1, AxisY1);
            }
            else if (piecekilled == "rook white(Clone)")
            {
                SpawnChessPieces(2, AxisX1, AxisY1);
            }
            else if (piecekilled == "queen white(Clone)")
            {
                SpawnChessPieces(1, AxisX1, AxisY1);
            }
            else if (piecekilled == "king white(Clone)")
            {
                SpawnChessPieces(0, AxisX1, AxisY1);
            }

        }
        //IsItWhiteTurn = !IsItWhiteTurn;
    }

    public void SelectChessPiece(int AxisX, int AxisY)
    {
        //if nothing selected or is there chess main selected
        //And which piece is selected is it black or white

        if (ChessPieces1[AxisX, AxisY] == null)
        {
            return;
        }
        if (ChessPieces1[AxisX, AxisY].isPieceWhite != IsItWhiteTurn)
        {
            return;
        }



        //to see if there are any possible moves for the piece if not then it will not be slected
        bool CanChessPieceMove = false;
        AllowedChessMoves = ChessPieces1[AxisX, AxisY].LegalMoves();

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (AllowedChessMoves[i, j])
                {
                    CanChessPieceMove = true;

                }
            }
        }
        if (!CanChessPieceMove)
        {
            return;
        }


        //This will get the allowed move the current piece
        AllowedChessMoves = ChessPieces1[AxisX, AxisY].LegalMoves();

        //This will save the current position of the piece moved
        SelectedChessPiece = ChessPieces1[AxisX, AxisY];


    }



    public void MoveChessPiece(int AxisX, int AxisY)
    {



        //this if statement  will see if the move is legal
        //Then the selected chesspiece
        //if (AllowedChessMoves[AxisX, AxisY])
        {

            //The below if statement will remove the chesspiece from the board
            //We will create instance of chesspiece and take the position of the chesspiece
            ChessPiece1 cp = ChessPieces1[AxisX, AxisY];

            //This condition will check
            //1. if there is piece where the other piece is going to be placed
            //2. and is it white turn or not
            //if (cp != null && cp.isPieceWhite != IsItWhiteTurn)
            if ((cp != null && cp.isPieceWhite) || (cp != null && !cp.isPieceWhite))
            {

                //This will see if it is the king then end the game
                if (cp.GetType() == typeof(ChessKingPiece))
                {
                    //End game
                    //GameWon();
                    return;
                }

                //Capture pice
                //If it is then we will remove the chess piece from the active peices on the board
                ActiveChessPieces1.Remove(cp.gameObject);
                Destroy(cp.gameObject);
            }

            //this is for the en passant move
            /*  if (AxisX == SpecialMoveEnPassant[0] && AxisY == SpecialMoveEnPassant[1])
              {
                  //for white
                  if (IsItWhiteTurn)
                  {
                      cp = ChessPieces[AxisX, AxisY - 1];
                      ActiveChessPieces1.Remove(cp.gameObject);
                      Destroy(cp.gameObject);
                  }

                  else
                  {
                      cp = ChessPieces[AxisX, AxisY + 1];
                      ActiveChessPieces1.Remove(cp.gameObject);
                      Destroy(cp.gameObject);
                  }
              }
              SpecialMoveEnPassant[0] = -1;
              SpecialMoveEnPassant[1] = -1;

              if (SelectedChessPiece.GetType() == typeof(ChessPawnPiece))
              {


                  //For special move promotion
                  if (AxisY == 7)
                  {
                      ActiveChessPieces.Remove(SelectedChessPiece.gameObject);
                      Destroy(SelectedChessPiece.gameObject);
                      SpawnChessPieces(1, AxisX, AxisY);
                      SelectedChessPiece = ChessPieces[AxisX, AxisY];
                  }

                  else if (AxisY == 0)
                  {
                      ActiveChessPieces.Remove(SelectedChessPiece.gameObject);
                      Destroy(SelectedChessPiece.gameObject);
                      SpawnChessPieces(7, AxisX, AxisY);
                      SelectedChessPiece = ChessPieces[AxisX, AxisY];
                  }


                  //for white
                  if (SelectedChessPiece.CurrentPositionY == 1 && AxisY == 3)
                  {
                      SpecialMoveEnPassant[0] = AxisX;
                      SpecialMoveEnPassant[1] = AxisY - 1;
                  }

                  //for black
                  else if (SelectedChessPiece.CurrentPositionY == 6 && AxisY == 4)
                  {
                      SpecialMoveEnPassant[0] = AxisX;
                      SpecialMoveEnPassant[1] = AxisY + 1;
                  }
              }*/

            //Then the current position of the chess piece to be moved is set to null because the piece is about to be moved
            ChessPieces1[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;

            //This line of code will get the new position of the piece ans it will enable the movement of the chess piece
            SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);

            //Then we will save the new position of the chess piece in the 2D array
            //It will put it at the right spot in the 2D array.
            ChessPieces1[AxisX, AxisY] = SelectedChessPiece;
            SelectedChessPiece.SetChessPosition(AxisX, AxisY);
            //This line of code will check whore turn it is and disbale white or blacl pieces repsectively
            //IsItWhiteTurn = !IsItWhiteTurn;


        }
        //If the move is not possible in general then it will unselect the piece
        SelectedChessPiece = null;



    }

    private Vector3 GetChessBoardTileCenter1(int AxisX, int AxisZ)
    {

        //Vector3 ChessBoardOrigin = Vector3.zero;
        Vector3 ChessBoardOrigin;
        ChessBoardOrigin.x = 34;
        ChessBoardOrigin.y = 34;
        ChessBoardOrigin.z = 0;
        //Debug.Log("X:" + ChessBoardOrigin.x);
        // Debug.Log("Y:" + ChessBoardOrigin.y);
        // Debug.Log("Z:" + ChessBoardOrigin.z);
        ChessBoardOrigin = new Vector3(ChessBoardOrigin.x, ChessBoardOrigin.y, ChessBoardOrigin.z);
        // Debug.Log("ChesssBoardOrigin: " + ChessBoardOrigin);
        //ChessBoardOrigin = transform.position;
        //ChessBoardOrigin.x = 1.3f;
        ChessBoardOrigin.x = ChessBoardOrigin.x + (ChessBoardTileSize * AxisX) + ChessBoardTileOffset;
        ChessBoardOrigin.z = ChessBoardOrigin.y + (ChessBoardTileSize * AxisZ) + ChessBoardTileOffset;

        return ChessBoardOrigin;

    }

    private void SpawnChessPieces1(int ChessIndex, int AxisX, int AxisY)
    {
        //Debug.Log("Axis X:" + AxisX);
        //Debug.Log("Axis Y:" + AxisY);
        GameObject gameobject = Instantiate(ChessPiecesPrefabs2[ChessIndex], GetChessBoardTileCenter1(AxisX, AxisY), Quaternion.identity) as GameObject;
        gameobject.transform.SetParent(transform);
        ChessPieces2[AxisX, AxisY] = gameobject.GetComponent<ChessPiece1>();
        //Debug.Log("Piece Added");
        ChessPieces2[AxisX, AxisY].SetChessPosition(AxisX, AxisY);

        ActiveChessPieces2.Add(gameobject);
        //ChessPiecePositions();
    }

    public void SpawnChessPiecesAll1()
    {
        SpecialMoveEnPassant = new int[2] { -1, -1 };
        ActiveChessPieces2 = new List<GameObject>();
        // Debug.Log("Inside Spawn Chess Piece");
        //ChessPieces = new ChessPiece[8, 8];
        //White
        //int count = 0;

        ChessPieces1 = new ChessPiece1[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                ChessPiece cp = ChessBoardManager.Instance.ChessPieces[i, j];
                //ChessPiece cp = ChessPieces[i, j];
                if (cp != null && cp.isPieceWhite)
                {
                    if (cp.GetType() == typeof(ChessKingPiece))
                    {
                        SpawnChessPieces1(0, i, j);
                        Debug.Log("KING SPAWNWDS");
                    }
                    if (cp.GetType() == typeof(ChessQueenPiece))
                    {
                        SpawnChessPieces1(1, i, j);
                    }
                    if (cp.GetType() == typeof(ChessRookPiece))
                    {
                        SpawnChessPieces1(2, i, j);
                    }
                    if (cp.GetType() == typeof(ChessBishopPiece))
                    {
                        SpawnChessPieces1(3, i, j);
                    }
                    if (cp.GetType() == typeof(ChessKnightPiece))
                    {
                        SpawnChessPieces1(4, i, j);
                    }
                    if (cp.GetType() == typeof(ChessPawnPiece))
                    {
                        SpawnChessPieces1(5, i, j);
                    }
                }
                else if (cp != null && !cp.isPieceWhite)
                {
                    if (cp.GetType() == typeof(ChessKingPiece))
                    {
                        SpawnChessPieces1(6, i, j);
                    }
                    if (cp.GetType() == typeof(ChessQueenPiece))
                    {
                        SpawnChessPieces1(7, i, j);
                    }
                    if (cp.GetType() == typeof(ChessRookPiece))
                    {
                        SpawnChessPieces1(8, i, j);

                    }
                    if (cp.GetType() == typeof(ChessBishopPiece))
                    {
                        SpawnChessPieces1(9, i, j);
                    }
                    if (cp.GetType() == typeof(ChessKnightPiece))
                    {
                        SpawnChessPieces1(10, i, j);
                    }
                    if (cp.GetType() == typeof(ChessPawnPiece))
                    {
                        SpawnChessPieces1(11, i, j);
                    }
                }
            }
        }
    }

    public string CheckMate(string team)
    {
        foreach (GameObject gameobject in ActiveChessPieces1)
        {
            //Debug.Log("DestroyAll");
            Destroy(gameobject);
        }
        SpawnChessPiecesAll();
        if (team == "Black")
        {
            bool CheckMateTrue = false;
            bool CheckTrue = false;
            //bool checkMate = true;
            List<MoveData> allMoves = GetMoves("Black");
            foreach (MoveData move in allMoves)
            {
                moveStack1.Push(move);
                MoveChessPiece(move.FirstPositionX, move.FirstPositionY, move.SecondPositionX, move.SecondPositionY, move.PieceKilled, move.PieceMoved);
                if (IsKingCheck() == "Black is in check") {
                    Debug.Log("Here In If");
                    //checkMate = false;
                    //check = 1;
                    CheckMateTrue = true;
                    UndoFakeMove();
                }
                else
                {
                    CheckTrue = true;
                    Debug.Log(move.FirstPositionX+" "+ move.FirstPositionY + "" + move.SecondPositionX + "" + move.SecondPositionY + "" + move.PieceKilled + "" + move.PieceMoved);
                    Debug.Log("Here In Else");
                    //check = 2;
                    //checkMate = true;
                    UndoFakeMove();
                    //return "CheckMate";
                    //return "Checkmate";
                }
                //UndoFakeMove();
            }

            if (CheckMateTrue == true && CheckTrue == false )
            {
                Debug.Log("Here In CheckMate");
                return "CheckMate";
            }
            else if (CheckTrue == true)
            {
                return "dsf";
            }
        }
        else if (team == "White")
        {
            bool CheckMateTrue = false;
            bool CheckTrue = false;
            List<MoveData> allMoves1 = GetMoves("White");
            foreach (MoveData move in allMoves1)
            {
                moveStack1.Push(move);
                MoveChessPiece(move.FirstPositionX, move.FirstPositionY, move.SecondPositionX, move.SecondPositionY, move.PieceKilled, move.PieceMoved);
                if (IsKingCheck() == "White is In check")
                {
                    CheckMateTrue = true;
                    Debug.Log("here in if white");
                    //checkMate = true;
                    UndoFakeMove();
                }

                else
                {
                    CheckTrue = true;
                    Debug.Log("Here In Else White");
                    UndoFakeMove();
                }
            }
            if (CheckMateTrue == true && CheckTrue == false)
            {
                Debug.Log("Here In CheckMate");
                return "CheckMate";
            }
            else if (CheckTrue == true)
            {
                return "dsf";
            }

            /*else
            {
                return "CheckMate";
            }*/
        }
        foreach (GameObject gameobject in ActiveChessPieces2)
        {
            //Debug.Log("DestroyAll");
            Destroy(gameobject);
        }
        return "defalut";

    }

    public string IsKingCheck()
    {
        for (int a = 0; a < 8; a++)
        {
            for (int k = 0; k < 8; k++)
            {
                if (ChessPieces1[a, k] == null)
                {
                    continue;
                }
                else
                {
                    bool[,] possibleMoves = ChessPieces1[a, k].LegalMoves();
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (possibleMoves[i, j])
                            {
                                ChessPiece1 cp = ChessPieces1[i, j];
                                //SelectedChessPiece = ChessPieces[i, j];
                                if (cp != null/* && cp.isPieceWhite != IsItWhiteTurn*/)
                                {
                                    if (cp.GetType() == typeof(ChessKingPiece1) && cp.isPieceWhite)
                                    {

                                       // print("White is In check");
                                        return "White is In check";

                                    }
                                    else if (cp.GetType() == typeof(ChessKingPiece1) )
                                    {
                                       // print("Black is in check");
                                        return "Black is in check";

                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
        return "Default";
    }
    public List<MoveData> GetMoves(string checkTeam)
    {
        // Debug.Log("Possible Moves");
        List<MoveData> turnMove = new List<MoveData>();
        //ChessPiece SelectedChessPiece;
        ChessPiece1 cp, cp1;
        if (checkTeam == "Black")
        {
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    cp = ChessPieces1[i, j];
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
                                        turnMove.Add(new MoveData { FirstPositionX = i, FirstPositionY = j, SecondPositionX = k, SecondPositionY = l, PieceKilled = cp1.name, PieceMoved = cp.name });
                                    }
                                    else
                                    {

                                        turnMove.Add(new MoveData { FirstPositionX = i, FirstPositionY = j, SecondPositionX = k, SecondPositionY = l, PieceMoved = cp.name });
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        else if (checkTeam == "White")
        {
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    cp = ChessPieces1[i, j];
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
                                        turnMove.Add(new MoveData { FirstPositionX = i, FirstPositionY = j, SecondPositionX = k, SecondPositionY = l, PieceKilled = cp1.name, PieceMoved = cp.name });
                                    }
                                    else
                                    {
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
    public void UndoFakeMove()
    {
        //Debug.Log("POP");
        // Debug.Log("UndoFake");
        MoveData tempMove = moveStack1.Pop();
        int FirstPositionX = tempMove.FirstPositionX;
        int FirstPositionY = tempMove.FirstPositionY;
        int SecondPositionX = tempMove.SecondPositionX;
        int SecondPositionY = tempMove.SecondPositionY;
        string PieceKilled = tempMove.PieceKilled;
        string PieceMoved = tempMove.PieceMoved;
        LocalBoard.Instance.UndoMoveChessPiece(SecondPositionX, SecondPositionY, FirstPositionX, FirstPositionY, PieceKilled, PieceMoved);
    }
    public void UndoMoveChessPiece1(int AxisX1, int AxisY1, int AxisX2, int AxisY2, string piecekilled, string pieceMoved)
    {
        //Debug.Log("ONCE");
        //if (piecekilled == null)
        {
            SelectedChessPiece2 = ChessPieces2[AxisX1, AxisY1];
            //this if statement  will see if the move is legal
            //Then the selected chesspiece
            //if (AllowedChessMoves[AxisX2, AxisY2])
            {

                //The below if statement will remove the chesspiece from the board
                //We will create instance of chesspiece and take the position of the chesspiece
                ChessPiece1 cp = ChessPieces2[AxisX2, AxisY2];

                //This condition will check
                //1. if there is piece where the other piece is going to be placed
                //2. and is it white turn or not
                if (piecekilled != null)
                {
                    if ((cp != null && cp.isPieceWhite) || (cp != null && !cp.isPieceWhite))//(cp != null )//&& !cp.isPieceWhite)// && cp.isPieceWhite != IsItWhiteTurn)
                    {
                        //    Debug.Log("Piece killed Before");
                        //Debug.Log("PIECE KILLED");
                        //This will see if it is the king then end the game
                        if (cp.GetType() == typeof(ChessKingPiece))
                        {
                            //     Debug.Log("PIECE KILLED King");
                            //End game
                            ChessBoardManager.Instance.GameWon();
                            return;
                        }

                        //Capture pice
                        //If it is then we will remove the chess piece from the active peices on the board
                        //Debug.Log("PIECE KILLED");
                        ActiveChessPieces2.Remove(cp.gameObject);
                        //  KilledChessPieces.Add(cp.gameObject);
                        Destroy(cp.gameObject);

                    }
                }
                //if()

                //this is for the en passant move
                /* if (AxisX2 == SpecialMoveEnPassant[0] && AxisY2 == SpecialMoveEnPassant[1])
                 {
                     //for white
                     if (IsItWhiteTurn)
                     {
                         cp = ChessPieces1[AxisX2, AxisY2 - 1];
                         ActiveChessPieces1.Remove(cp.gameObject);
                         Destroy(cp.gameObject);
                     }

                     else
                     {
                         cp = ChessPieces1[AxisX2, AxisY2 + 1];
                         ActiveChessPieces1.Remove(cp.gameObject);
                         Destroy(cp.gameObject);
                     }
                 }
                 SpecialMoveEnPassant[0] = -1;
                 SpecialMoveEnPassant[1] = -1;
                 if (SelectedChessPiece.GetType() == typeof(ChessPawnPiece))
                 {
                     //For special move promotion
                     if (AxisY2 == 7)
                     {
                         ActiveChessPieces1.Remove(SelectedChessPiece.gameObject);
                         Destroy(SelectedChessPiece.gameObject);
                         SpawnChessPieces(1, AxisX2, AxisY2);
                         SelectedChessPiece = ChessPieces1[AxisX2, AxisY2];
                     }
                     else if (AxisY2 == 0)
                     {
                         ActiveChessPieces1.Remove(SelectedChessPiece.gameObject);
                         Destroy(SelectedChessPiece.gameObject);
                         SpawnChessPieces(7, AxisX2, AxisY2);
                         SelectedChessPiece = ChessPieces1[AxisX2, AxisY2];
                     }
                     //for white
                     if (SelectedChessPiece.CurrentPositionY == 1 && AxisY2 == 3)
                     {
                         SpecialMoveEnPassant[0] = AxisX2;
                         SpecialMoveEnPassant[1] = AxisY2 - 1;
                     }
                     //for black
                     else if (SelectedChessPiece.CurrentPositionY == 6 && AxisY2 == 4)
                     {
                         SpecialMoveEnPassant[0] = AxisX2;
                         SpecialMoveEnPassant[1] = AxisY2 + 1;
                     }
                 }*/

                //Then the current position of the chess piece to be moved is set to null because the piece is about to be moved
                // Debug.Log(" UNDO MOVE: PieceType:" + pieceMoved + " SecondPositionX: " + AxisX1 + " SecondPositionY: " + AxisY1 + " TO FirstPositionX: " + AxisX2 + " FirstPositionY: " + AxisY2);
                // try
                {
                    ChessPieces2[SelectedChessPiece2.CurrentPositionX, SelectedChessPiece2.CurrentPositionY] = null;
                }
                //   catch (Exception e)
                {
                    // Debug.Log(" UNDO MOVE: PieceType: + cp.name + SecondPositionX: " + AxisX1 + " SecondPositionY: " + AxisY1 + " FirstPositionX: " + AxisX2 + " FirstPositionY: " + AxisY2);
                    //     Debug.Log(e.Message);
                }
                //This line of code will get the new position of the piece ans it will enable the movement of the chess piece
                SelectedChessPiece2.transform.position = GetChessBoardTileCenter(AxisX2, AxisY2);

                //Then we will save the new position of the chess piece in the 2D array
                //It will put it at the right spot in the 2D array.
                ChessPieces2[AxisX2, AxisY2] = SelectedChessPiece;
                SelectedChessPiece2.SetChessPosition(AxisX2, AxisY2);
                //This line of code will check whore turn it is and disbale white or blacl pieces repsectively
                //IsItWhiteTurn = !IsItWhiteTurn;
            }
            //If the move is not possible in general then it will unselect the piece
            SelectedChessPiece2 = null;
        }
        if (piecekilled != null)
        {
            // Debug.Log("PieceUnkilled: "+piecekilled+ " AT AxisX: " + AxisX1 + "AxisY: " + AxisY1);
            if (piecekilled == "pawn black(Clone)")
            {
                SpawnChessPieces1(11, AxisX1, AxisY1);
            }

            else if (piecekilled == "knight black(Clone)")
            {
                SpawnChessPieces1(10, AxisX1, AxisY1);
            }
            else if (piecekilled == "bishop black(Clone)")
            {
                SpawnChessPieces1(9, AxisX1, AxisY1);
            }
            else if (piecekilled == "rook black(Clone)")
            {
                SpawnChessPieces1(8, AxisX1, AxisY1);
            }
            else if (piecekilled == "queen black(Clone)")
            {
                SpawnChessPieces1(7, AxisX1, AxisY1);
            }
            else if (piecekilled == "king black(Clone)")
            {
                SpawnChessPieces1(6, AxisX1, AxisY1);
            }
            else if (piecekilled == "pawn white(Clone)")
            {
                SpawnChessPieces1(5, AxisX1, AxisY1);
            }
            else if (piecekilled == "knight white(Clone)")
            {
                SpawnChessPieces1(4, AxisX1, AxisY1);
            }
            else if (piecekilled == "bishop white(Clone)")
            {
                SpawnChessPieces1(3, AxisX1, AxisY1);
            }
            else if (piecekilled == "rook white(Clone)")
            {
                SpawnChessPieces1(2, AxisX1, AxisY1);
            }
            else if (piecekilled == "queen white(Clone)")
            {
                SpawnChessPieces1(1, AxisX1, AxisY1);
            }
            else if (piecekilled == "king white(Clone)")
            {
                SpawnChessPieces1(0, AxisX1, AxisY1);
            }

        }
        //IsItWhiteTurn = !IsItWhiteTurn;
    }

    public void MoveChessPiece1(int AxisX1, int AxisY1, int AxisX2, int AxisY2, string pieceKilled, string piceMoved)
    {
        //Debug.Log("ONCE");

        SelectedChessPiece2 = ChessPieces2[AxisX1, AxisY1];
        //this if statement  will see if the move is legal
        //Then the selected chesspiece
        //if (AllowedChessMoves[AxisX2, AxisY2])
        {

            //The below if statement will remove the chesspiece from the board
            //We will create instance of chesspiece and take the position of the chesspiece
            ChessPiece1 cp = ChessPieces2[AxisX2, AxisY2];

            //This condition will check
            //1. if there is piece where the other piece is going to be placed
            //2. and is it white turn or not
            if ((cp != null && cp.isPieceWhite) || (cp != null && !cp.isPieceWhite))// != IsItWhiteTurn)
            {
                //Debug.Log("PIECE KILLED : "+ pieceKilled + " AT AxisX: " + AxisX2 + " AxisY: "+AxisY2 +" FROM AxisX: "+AxisX1+" AxisY1: " +AxisY1);
                //This will see if it is the king then end the game
                if (cp.GetType() == typeof(ChessKingPiece))
                {
                    //Debug.Log("PIECE KILLED");
                    //End game
                    //ChessBoardManager.Instance.GameWon();
                    //return;
                }

                //Capture pice
                //If it is then we will remove the chess piece from the active peices on the board
                //Debug.Log("PIECE KILLED");
                ActiveChessPieces2.Remove(cp.gameObject);
                //  KilledChessPieces.Add(cp.gameObject);
                Destroy(cp.gameObject);

            }
            //if()

            /*//this is for the en passant move
             if (AxisX2 == SpecialMoveEnPassant[0] && AxisY2 == SpecialMoveEnPassant[1])
             {
                 //for white
                 if (IsItWhiteTurn)
                 {
                     cp = ChessPieces1[AxisX2, AxisY2 - 1];
                     ActiveChessPieces1.Remove(cp.gameObject);
                     Destroy(cp.gameObject);
                 }

                 else
                 {
                     cp = ChessPieces1[AxisX2, AxisY2 + 1];
                     ActiveChessPieces1.Remove(cp.gameObject);
                     Destroy(cp.gameObject);
                 }
             }
             SpecialMoveEnPassant[0] = -1;
             SpecialMoveEnPassant[1] = -1;*/
            /*if (SelectedChessPiece2.GetType() == typeof(ChessPawnPiece))
            {
                //For special move promotion
                if (AxisY2 == 7)
                {
                    ActiveChessPieces2.Remove(SelectedChessPiece.gameObject);
                    Destroy(SelectedChessPiece.gameObject);
                    SpawnChessPieces(1, AxisX2, AxisY2);
                    SelectedChessPiece = ChessPieces1[AxisX2, AxisY2];
                }
                else if (AxisY2 == 0)
                {
                    ActiveChessPieces2.Remove(SelectedChessPiece.gameObject);
                    Destroy(SelectedChessPiece.gameObject);
                    SpawnChessPieces(7, AxisX2, AxisY2);
                    SelectedChessPiece = ChessPieces1[AxisX2, AxisY2];
                }
                //for white
                /* if (SelectedChessPiece.CurrentPositionY == 1 && AxisY2 == 3)
                 {
                     SpecialMoveEnPassant[0] = AxisX2;
                     SpecialMoveEnPassant[1] = AxisY2 - 1;
                 }
                 //for black
                 else if (SelectedChessPiece.CurrentPositionY == 6 && AxisY2 == 4)
                 {
                     SpecialMoveEnPassant[0] = AxisX2;
                     SpecialMoveEnPassant[1] = AxisY2 + 1;
                 }
        }*/
        //Then the current position of the chess piece to be moved is set to null because the piece is about to be moved
        // Debug.Log(" DO MOVE: PieceType:" + piceMoved + "FirstPositionX: " + AxisX1 + " FirstPositionY: " + AxisY1 + " SecondPositionX: " + AxisX2 + " SecondPositionY: " + AxisY2);
        // try
        {
            ChessPieces2[SelectedChessPiece2.CurrentPositionX, SelectedChessPiece2.CurrentPositionY] = null;
        }
        // catch (Exception e)
        {
            //Debug.Log(" DO MOVE: PieceType:" + pieceMoved + "FirstPositionX: " + AxisX1 + " FirstPositionY: " + AxisY1 + " SecondPositionX: " + AxisX2 + " SecondPositionY: " + AxisY2);
            //  Debug.Log(e.Message);

        }
        //This line of code will get the new position of the piece ans it will enable the movement of the chess piece
        // try
        {
            SelectedChessPiece2.transform.position = GetChessBoardTileCenter(AxisX2, AxisY2);
        }
        //catch (Exception e)
        {
            // Debug.Log(" DO MOVE: PieceType:" + pieceMoved + "FirstPositionX: " + AxisX1 + " FirstPositionY: " + AxisY1 + " SecondPositionX: " + AxisX2 + " SecondPositionY: " + AxisY2);
            //    Debug.Log(e.Message);
        }
        //Then we will save the new position of the chess piece in the 2D array
        //It will put it at the right spot in the 2D array.
        ChessPieces2[AxisX2, AxisY2] = SelectedChessPiece;
        SelectedChessPiece2.SetChessPosition(AxisX2, AxisY2);
        //This line of code will check whore turn it is and disbale white or blacl pieces repsectively
        //IsItWhiteTurn = !IsItWhiteTurn;
    }
    //If the move is not possible in general then it will unselect the piece
    SelectedChessPiece2 = null;


        


    }
}
