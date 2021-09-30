using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool x = true;
    int count = 0;
    // Update is called once per frame
    void Update()
    {
        
        ChessBoardManager.Instance.UpdateCameraAndChessBoardSelection();
        ChessBoardManager.Instance.DrawLinedChessBoardBase();
        //IsKingCheck();
        if (x == true)
        {
            ChessBoardManager.Instance.button.SetActive(false);
            ChessBoardManager.Instance.save.SetActive(false);
            puzzel_1();
            x = false;
        }


       


        if (Input.GetMouseButtonDown(0))
        {
            //white queen need to go 6,7
            //black castle need to kill the queen
            //knight moves to 5,6 to checkmate the king 
            //
            if (ChessBoardManager.Instance.ChessBoardTileSelectX >= 0 && ChessBoardManager.Instance.ChessBoardTileSelectY >= 0)
            {
                if (ChessBoardManager.Instance.SelectedChessPiece == null)
                {
                    ChessBoardManager.Instance.SelectChessPiece(ChessBoardManager.Instance.ChessBoardTileSelectX, ChessBoardManager.Instance.ChessBoardTileSelectY);
                }

                else
                {
                    ChessBoardManager.Instance.MoveChessPiecePlayers2(ChessBoardManager.Instance.ChessBoardTileSelectX, ChessBoardManager.Instance.ChessBoardTileSelectY);
                    if(ChessBoardManager.Instance.PuzzleCounter == 3)
        {
                        foreach (GameObject gameobject in ChessBoardManager.Instance.ActiveChessPieces)
                        {
                            Destroy(gameobject);

                        }
                        ChessBoardManager.Instance.IsItWhiteTurn = true;
                        puzzel_1();
                        ChessBoardManager.Instance.PuzzleCounter = 0;
                    }
                }
            }
        }

        //      foreach (GameObject gameobject in ChessBoardManager.Instance.ActiveChessPieces)
        //        Destroy(gameObject);
        //         count = 1;
        //        puzzel_1();
      

        else if (!ChessBoardManager.Instance.IsItWhiteTurn)
            {
            Debug.Log("HERE");
                ChessBoardManager.Instance.SelectChessPiece1(4, 7);
                ChessBoardManager.Instance.MoveChessPiecePlayers1(6, 7);
            }
        



    }



    public void puzzel_2()
    {

        //ChessBoardManager.Instance
        ChessBoardManager.Instance.ActiveChessPieces = new List<GameObject>();

        ChessBoardManager.Instance.ChessPieces = new ChessPiece[8, 8];

        ChessBoardManager.Instance.SpecialMoveEnPassant = new int[2] { -1, -1 };
        //Spawn White Chess Pieces
        //King spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(0, 4, 0);

        //Queen spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(1, 1, 6);

        //Rooks spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(2, 0, 0);
       

        //Bishops spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(3, 2, 0);
        ChessBoardManager.Instance.SpawnChessPieces(3, 5, 0);


        

        //Pawn Spawn and loaction
        //As we have 8 of thoise we will use loop for it


        ChessBoardManager.Instance.SpawnChessPieces(5, 0, 1);
        ChessBoardManager.Instance.SpawnChessPieces(5, 1, 1);
        ChessBoardManager.Instance.SpawnChessPieces(5, 2, 1);
        ChessBoardManager.Instance.SpawnChessPieces(5, 5, 3);
        ChessBoardManager.Instance.SpawnChessPieces(5, 6, 2);
       



        //-------------

        //Spawn Black Chess Pieces

        //King spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(6, 3, 4);

        //Queen spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(7, 7, 0);

        //Rooks spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(8, 0, 7);
        ChessBoardManager.Instance.SpawnChessPieces(8, 5, 7);


        //Bishops spawn and Location
        
        ChessBoardManager.Instance.SpawnChessPieces(11, 2, 4);

        //Knights spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(10, 2, 5);


        //Pawn Spawn and loaction
        //As we have 8 of thoise we will use loop for it


        ChessBoardManager.Instance.SpawnChessPieces(11, 0, 6);
        ChessBoardManager.Instance.SpawnChessPieces(11, 3, 5);
        ChessBoardManager.Instance.SpawnChessPieces(11, 5, 6);
        ChessBoardManager.Instance.SpawnChessPieces(11, 6, 6);
        ChessBoardManager.Instance.SpawnChessPieces(11, 7, 6);

    }

    public void puzzel_3()
    {

        //ChessBoardManager.Instance
        ChessBoardManager.Instance.ActiveChessPieces = new List<GameObject>();

        ChessBoardManager.Instance.ChessPieces = new ChessPiece[8, 8];

        ChessBoardManager.Instance.SpecialMoveEnPassant = new int[2] { -1, -1 };
        //Spawn White Chess Pieces
        //King spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(0, 6, 0);

        //Queen spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(1, 3, 0);

        //Rooks spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(2, 0, 0);
        ChessBoardManager.Instance.SpawnChessPieces(2, 5, 0);

        //Bishops spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(3, 4, 5);
        ChessBoardManager.Instance.SpawnChessPieces(3, 7, 5);

        //Knights spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(4, 6, 6);
        

        //Pawn Spawn and loaction
        //As we have 8 of thoise we will use loop for it


        ChessBoardManager.Instance.SpawnChessPieces(5, 0, 1);
        ChessBoardManager.Instance.SpawnChessPieces(5, 1, 3);
        ChessBoardManager.Instance.SpawnChessPieces(5, 2, 2);
        ChessBoardManager.Instance.SpawnChessPieces(5, 5, 1);
        ChessBoardManager.Instance.SpawnChessPieces(5, 6, 3);
        ChessBoardManager.Instance.SpawnChessPieces(5, 7, 2);
        



        //-------------

        //Spawn Black Chess Pieces

        //King spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(6, 5, 7);

        //Queen spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(7, 3, 7);

        //Rooks spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(8, 0, 7);
        ChessBoardManager.Instance.SpawnChessPieces(8, 7, 7);


        //Bishops spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(9, 1, 5);
        ChessBoardManager.Instance.SpawnChessPieces(9, 4, 3);

        //Knights spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(10, 2, 5);
        ChessBoardManager.Instance.SpawnChessPieces(10, 4, 6);


        //Pawn Spawn and loaction
        //As we have 8 of thoise we will use loop for it


        ChessBoardManager.Instance.SpawnChessPieces(11, 0, 6);
        ChessBoardManager.Instance.SpawnChessPieces(11, 1, 6);
        ChessBoardManager.Instance.SpawnChessPieces(11, 2, 6);
        ChessBoardManager.Instance.SpawnChessPieces(11, 3, 5);
        ChessBoardManager.Instance.SpawnChessPieces(11, 3, 3);
        ChessBoardManager.Instance.SpawnChessPieces(11, 7, 6);
        
    }

    public void puzzel_1()
    {
        //ChessBoardManager.Instance
        ChessBoardManager.Instance.ActiveChessPieces = new List<GameObject>();

        ChessBoardManager.Instance.ChessPieces = new ChessPiece[8, 8];

        ChessBoardManager.Instance.SpecialMoveEnPassant = new int[2] { -1, -1 };

        //spawn white chess pieces

        ChessBoardManager.Instance.SpawnChessPieces(0, 6, 0);
        //queen spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(1, 3, 4);



        //Rooks spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(2, 0, 0);
        ChessBoardManager.Instance.SpawnChessPieces(2, 5, 0);



        //Knights spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(4, 7, 5);

        //Pawn Spawn and loaction



        ChessBoardManager.Instance.SpawnChessPieces(5, 1, 1);
        ChessBoardManager.Instance.SpawnChessPieces(5, 2, 1);
        ChessBoardManager.Instance.SpawnChessPieces(5, 5, 1);
        ChessBoardManager.Instance.SpawnChessPieces(5, 6, 1);
        ChessBoardManager.Instance.SpawnChessPieces(5, 7, 1);
        ChessBoardManager.Instance.SpawnChessPieces(5, 0, 2);
        ChessBoardManager.Instance.SpawnChessPieces(5, 2, 3);
        ChessBoardManager.Instance.SpawnChessPieces(5, 4, 3);


        //-------------

        //Spawn Black Chess Pieces

        //King spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(6, 7, 7);

        //Queen spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(7, 3, 7);

        //Rooks spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(8, 4, 7);


        //Bishops spawn and Location
        ChessBoardManager.Instance.SpawnChessPieces(9, 1, 6);
        ChessBoardManager.Instance.SpawnChessPieces(9, 6, 4);

        //Pawn Spawn and loaction
        //As we have 8 of thoise we will use loop for it
        ChessBoardManager.Instance.SpawnChessPieces(11, 1, 5);
        ChessBoardManager.Instance.SpawnChessPieces(11, 0, 6);
        ChessBoardManager.Instance.SpawnChessPieces(11, 2, 6);
        ChessBoardManager.Instance.SpawnChessPieces(11, 3, 6);
        ChessBoardManager.Instance.SpawnChessPieces(11, 6, 6);
        ChessBoardManager.Instance.SpawnChessPieces(11, 7, 6);

    }

}
