using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVP : MonoBehaviour
{
    // Start is called before the first frame update

    public void pvp1()
    {
        //Start();
        //Update();

    }
   
    public void Start()
    {
        Debug.Log("HERE");
        //ChessBoardManager.Instance.SpawnChessPiecesAll();
        
    }
    bool a = true;
    // Update is called once per frame
    public void Update()
    {
       if (a == true)
        {
            ChessBoardManager.Instance.SpawnChessPiecesAll();
            a = false;

        }



        
        ChessBoardManager.Instance.playerturnis();
        //whiteKingFreeMove = checkKingFreeMove(true);
        // blackKingFreeMove = checkKingFreeMove(false);
        //First We will Update Then Draw
        ChessBoardManager.Instance.UpdateCameraAndChessBoardSelection();
        ChessBoardManager.Instance.DrawLinedChessBoardBase();
        //IsKingCheck();

       if (Input.GetMouseButtonDown(0))
       {

            //This if condition will check if the selection of the board tile is in given criteria
           if (ChessBoardManager.Instance.ChessBoardTileSelectX >= 0 && ChessBoardManager.Instance.ChessBoardTileSelectY >= 0)
           {

                //This condition will check if the Selected peiece is seleced or not
               if (ChessBoardManager.Instance.SelectedChessPiece == null)
               {
                    //Select chess Piece
                   ChessBoardManager.Instance.SelectChessPiece(ChessBoardManager.Instance.ChessBoardTileSelectX, ChessBoardManager.Instance.ChessBoardTileSelectY);
               }
               else
                {
                    //IsKingCheck();
                    //if (IsKingCheck() != "Black is in check")
                    {
                        //Move the Chess Piece
                        ChessBoardManager.Instance.MoveChessPiecePlayers2(ChessBoardManager.Instance.ChessBoardTileSelectX, ChessBoardManager.Instance.ChessBoardTileSelectY);
                        //LocalBoard.Instance.SpawnChessPiecesAll();
                        //LocalBoard.Instance.destroyAll();
                        //LocalBoard.Instance.SpawnChessPiecesAll();
                        //cm.GameState();
                        //cm.GetMoves("Black");
                        // LocalBoard.Instance.MoveChessPiece(0,1,1,1);
                        //LocalBoard.Instance.DoAiMove();

                    }
                }
            }
        }
        
    }
}
