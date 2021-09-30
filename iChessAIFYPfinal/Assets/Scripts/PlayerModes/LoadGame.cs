using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    bool a = true;
    // Update is called once per frame
    void Update()
    {
        if (a == true)
        {
            ChessBoardManager.Instance.LoadGame();

            a = false;

        }

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
                    
                    {
                        //Move the Chess Piece
                        ChessBoardManager.Instance.MoveChessPiecePlayers2(ChessBoardManager.Instance.ChessBoardTileSelectX, ChessBoardManager.Instance.ChessBoardTileSelectY);
                    }
                    if (ChessBoardManager.Instance.IsItWhiteTurn)
                    {
                        ChessBoardManager.Instance.playerturnvalue = "WHITE TURN!";
                        ChessBoardManager.Instance.playerturnis();

                    }
                    else
                    {
                        ChessBoardManager.Instance.playerturnvalue = "BLACK TURN!";
                        ChessBoardManager.Instance.playerturnis();
                    }
                }
            }
        }

    }
}

