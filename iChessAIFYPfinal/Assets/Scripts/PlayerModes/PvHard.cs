﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PvHard : MonoBehaviour
{
    public static PvHard Instance { set; get; }
    public int x = 0;
    // Start is called before the first frame update
    public void Start()
    {
        
        Instance = this;
        //ChessBoardManager.Instance.SpawnChessPiecesAll();
        //MAXDEPTH.Instance.MaxDepth = 4;
        ChessBoardManager.Instance.button.SetActive(false);
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
            MAXDEPTH.Instance.MaxDepth = 4;
            //PvMedium.Instance.Start();
            Debug.Log("HERE");

        }
        ChessBoardManager.Instance.playerturnis();
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
        //ChessBoardManager.Instance.playerturnis();
        else if (!ChessBoardManager.Instance.IsItWhiteTurn)
        {
            LocalBoard.Instance.DoAiMove1();
        }
    }
}
