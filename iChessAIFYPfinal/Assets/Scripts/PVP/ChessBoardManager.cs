using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ChessBoardManager : MonoBehaviour
{
    //This script will manage all the initial activiteis on the board and it is the component of the ChessBoard GmaeObject

    //This will lets us access this class from anywhere we want
    [SerializeField] public GameObject victoryscreen;
    [SerializeField] private GameObject specialmoves;
    Text whitescore, 
        whiteturns, 
        blackscore, 
        blackturns, 
        playerturn, 
        victorynamewhite , 
        specialmovenamechange,
        victorynameblack;

    int whitescorevalue = 0;
    int whiteturnvalue = 0;
    int Blackscorevalue = 0;
    int Blackturnvalue = 0;
    string Whitewin, BlackWin;
    public GameObject WinScreen;
    public GameObject button, save;
    int index = 1;
    //public GameObject Pvpprefeb;
    public string playerturnvalue="Black turn";



    public int PuzzleCounter = 0;
    public static ChessBoardManager Instance { set; get; }


    //Stack<string> Movehistory = new Stack<string>();

    //This String will be used to indicate the current player turn
    //public string PlayerTurn;
    // public GameObject PlayerTurnDisplay;


    //Declare two new materials
    //These will be used to chage the texture of the selected piece in the chessBoard
    //It is private so that texture should not be transfered to the original pieces in the editor.
    public Material PriorMaterials;
    //New texture will be set to piece in the unity editor.
    public Material SelectedChessPieceMaterial;

    //this will store the alowed chess moves
    public bool[,] AllowedChessMoves { set; get; }

    //This variable will be used for the 
    public const float ChessBoardTileSize = 1.0f;


    //This variable will determine the offset of the tiles from the origin
    public const float ChessBoardTileOffset = 0.5f;
    


    //These varaible will show us whihc tiles are selected and an indication will be shown
    //THe default value will be -1 when nothing is selected
    public int ChessBoardTileSelectX = -1;
    public int ChessBoardTileSelectY = -1;


    //We will create a list of GameObject and we will store our ChessPieces Prefab in those
    //Unity’s Prefab system allows you to create, configure, and store a GameObject complete with all its components, property values, and child GameObjects as a reusable Asset.The Prefab Asset acts as a template from which you can create new Prefab instances in the Scene
    //This list will only be for the prefabs
    public List<GameObject> ChessPiecesPrefabs;


    //This will be for the active ChessPieces. 
    public List<GameObject> ActiveChessPieces = new List<GameObject>();



    //This variable will determine wheter it is whites turn or black
    public bool IsItWhiteTurn = true;

    //Quaternion is used to repesrnt rotaion.
    //In our project we will use it to rotate our pieces on the chessBoard
    //private Quaternion ChessPieceOrientation = Quaternion.Euler(0, 180, 0);


    //Through this two dimensional arary of chesspiece we will be able to fetch the peieces
    //This list of chessPiece will set position for chessPieces
    public ChessPiece[,] ChessPieces { set; get; }
    //public ChessPiece1[,] ChessPieces1 { set; get; }


    //This class object will tell us whcih piece is selected
    public ChessPiece SelectedChessPiece;

    //public bool[,] whiteKingFreeMove { set; get; }
    //private bool[,] blackKingFreeMove { set; get; }
    //
    public int[] SpecialMoveEnPassant { set; get; }


    //Update is the method that is called every frame. If you want to operate code every frame (to like move something across the screen), you'd do so in 'Update'.
    private void Update()
    {
        whitescoreandturn();
        Blackscoreandturn();
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
        //playerturnis();
    }

    
    private void Start()
    {

        //Debug.Log("HEHEHEHEHE");

       // playerturnis();
        //whitescoreandturn();
        Blackscoreandturn();


        Instance = this;
        //PVP pvp = Ob
        // SpawnChessPiecesAll();
        //p.Start();
        //LoadGame();
        //easyai = new EasyAI();
        //GameState();
        //ChessMedium ch = new ChessMedium();
        //ch.GameState();

        //LocalBoard.Instance.SpawnChessPiecesAll();

    }


    public void goback()
    {
        SceneManager.LoadScene(0);

    }


    //ChessBoardManager.instance.ResetGamne();
    public void ResetGame()
    {
        foreach (GameObject gameobject in ActiveChessPieces)
        {
            Destroy(gameobject);
        }
        whitescorevalue = 0;
        whiteturnvalue = 0;
        Blackscorevalue = 0;
        Blackturnvalue = 0;
        IsItWhiteTurn = true;
        ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
        SpawnChessPiecesAll();
    }


    public void specialmovesscreennamechange()
    {
        specialmovenamechange = GameObject.Find("ChessBoard/specialmoves/Image/movename").GetComponent<Text>();
        specialmovenamechange.text = "" + 1;

    }
    public void playpvpgame()
    {
        SceneManager.LoadScene(2);


    }


    public void playagain()
    {
        victoryscreen.SetActive(false);
        ChessBoardManager.Instance.ResetGame();

    }

    public void whitescoreandturn()
    {
        whitescore = GameObject.Find("ChessBoard/Canvas/panelwhite/scorevalue").GetComponent<Text>();
        whitescore.text = "" + whitescorevalue;
        whiteturns = GameObject.Find("ChessBoard/Canvas/panelwhite/turncountno").GetComponent<Text>();
        whiteturns.text = "" + whiteturnvalue;

    }
    public void Blackscoreandturn()
    {
        blackscore = GameObject.Find("ChessBoard/Canvas/panelblack/scorevalue").GetComponent<Text>();
        blackscore.text = "" + Blackscorevalue;
        blackturns = GameObject.Find("ChessBoard/Canvas/panelblack/turncountno").GetComponent<Text>();
        blackturns.text = "" + Blackturnvalue;

    }
    public void playerturnis()
    {
        playerturn = GameObject.Find("ChessBoard/Canvas/Turn/turnvalue").GetComponent<Text>();
        playerturn.text = playerturnvalue;
    }

    public void PlayAgain() {
        victoryscreen.SetActive(false);
    }

    public void winningteam(int index)
    {
        victoryscreen.SetActive(true);
        victoryscreen.transform.GetChild(index).gameObject.SetActive(true);

    }


    public void Save()
    {
        SaveGame();
    }


    // DrawLinedChessBoardBase this function will draw lined chessboad in the gameview.
    public void DrawLinedChessBoardBase()
    {
        //Vector3 is a representation of 3D vectors and points. This structure is used throughout Unity to pass 3D positions and directions around.


        //ChessBoardWidthlineBase represents the width of the board that will be drawn by Vector3
        //Vector3.right will make 1 meter of vector representaion on the plane and if it is multiplayed by 8 it will make 8 line on the plane facing right.
        //ChessBoardWidthLineBase will draw single line in right direction in gameview that will be covering 8 boxes in game view.
        Vector3 ChessBoardWidthLineBase = Vector3.right * 8;

        //ChessBoardHeightlineBase represents the height of the board that will be drawn by Vector3
        //Vector3.forward will make 1 meter of vector representaion on the plane and if it is multiplayed by 8 it will make 8 line on the plane facing forward.
        //ChessBoardHeightLineBase will draw single line in forward dreiction in gameview that will be covering 8 boxes in game view.
        Vector3 ChessBoardHeightLineBase = Vector3.forward * 8;

        //Draw the Lines on the plane we will use two loops one for right and one for forward
        //After this a chessborad made of Vector3 will be visible.
        //The loops will use the above declared value and will draw a chessbaord after multiplying two vectors in two loops.

        for (int i = 0; i <= 8; i++)
        {
            //This Vectror3 will behave as origin for the other Vector3 values declared above.
            Vector3 ChessBoardWidthLineBaseStart = Vector3.forward * i;

            //Debug.Drawline draws a line between specified start and end points, it takes two parameter.
            //The first parameter will act as origin for each line to be drawn, the second parameter will draw line form the origin from the origin given by first parameter.
            //As these will will be incremented by the for loop and lines will be drawn till the loop is finished incremented.
            //After this 8 lines will be drawn in the gameview that will be towards right
            Debug.DrawLine(ChessBoardWidthLineBaseStart, ChessBoardWidthLineBaseStart + ChessBoardWidthLineBase);

            for (int j = 0; j <= 8; j++)
            {
                //This Vectror3 will behave as origin for the other Vector3 values declared above.
                Vector3 ChessBoardHeightLineBaseStart = Vector3.right * i;

                //The first parameter will act as origin for each line to be drawn.will the second parameter will draw line form the origin.
                //As these will will be incremented by the for loop and lines will be drawn till the loop is finished incremented.
                //After this 8 lines will be drawn in the gameview that will be drawn facing forward.
                Debug.DrawLine(ChessBoardHeightLineBaseStart, ChessBoardHeightLineBaseStart + ChessBoardHeightLineBase);
            }
        }


        //Draw The Selection
        /*if (ChessBoardTileSelectX >= 0 && ChessBoardTileSelectY >= 0)
        {
            //It will draw line on the tile on which the mouse is placed
            //It will create diagnol lines on the tiles where the mouse is placed
            Debug.DrawLine(
                Vector3.forward * ChessBoardTileSelectY + Vector3.right * ChessBoardTileSelectX,
                Vector3.forward * (ChessBoardTileSelectY + 1) + Vector3.right * (ChessBoardTileSelectX + 1)
                );
            Debug.DrawLine(
               Vector3.forward * (ChessBoardTileSelectY + 1) + Vector3.right * ChessBoardTileSelectX,
               Vector3.forward * ChessBoardTileSelectY + Vector3.right * (ChessBoardTileSelectX + 1)
               );
        }*/

    }


    //This function will update every single frame using camera and mouse position
    public void UpdateCameraAndChessBoardSelection()
    {
        //If there is no camera function then it will return out of this function.
        if (!Camera.main)
        {
            return;
        }

        //Raycast is the method you use to cast a ray (or a vector) from a point in a direction with distance (or not) and then reports if the ray hits something
        //This vriable will be used if we hit somethinng.
        RaycastHit raycasthit;
        //We will use the rayacsthit for the layerplane only to get feed back
        //This if statement will give us feedbask if rayacst hist something.

        //Physics.raycast has four parameter
        //Parameter one will be the origin as it will cast ray from origin, in which case it will Returns a ray going from camera through a screen point which will be The current mouse position in pixel coordinates
        //The second parameter is the direction as if the raycasthit hit something it will return a value to the if fucntion. the out means there will be values comiong out of raycasthit
        //The third parameter will be the max distance the ray should check for collisions.
        //The third parameter is the Layer mask that is used to selectively ignore Colliders when casting a ray.
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycasthit, 25.0f, LayerMask.GetMask("ChessPlaneLayer")))
        {
            //Through this we can cast on which selection the raycasthit cast a ray
            //On which selected tile it is one
            ChessBoardTileSelectX = (int)raycasthit.point.x;
            ChessBoardTileSelectY = (int)raycasthit.point.z;
        }
        else
        {
            //If there is no feedback then the default values will be set to the following values
            //If it is not hitting anything
            ChessBoardTileSelectX = -1;
            ChessBoardTileSelectY = -1;
        }
    }


    //This Function will spawn the chess pieces on the board
    public void SpawnChessPieces(int ChessIndex, int AxisX, int AxisY)
    {
        //By using the gameobject we can Clone the object original and returns the clone.
        //The first paprameter (Original) takes An existing object that you want to make a copy of.
        //The second parameter (Position) takes Position for the new object.
        //The third parameter (rotation) takess Orientation of the new object.
        //Then we will cast it as GameObject
        //Then the Object will return The instantiated clone.
        GameObject gameobject = Instantiate(ChessPiecesPrefabs[ChessIndex], GetChessBoardTileCenter(AxisX, AxisY), Quaternion.identity) as GameObject;

        //The tranform object takes care of Position, rotation and scale of an object.
        //This lets the Transform keep its local orientation rather than its global orientation
        gameobject.transform.SetParent(transform);

        //The Getcomponent will return the component of Type "type" if the game object has one attached, null if it doesn't.
        //The below code of line will get the component and place it in the chesspiece 2D array.
        ChessPieces[AxisX, AxisY] = gameobject.GetComponent<ChessPiece>();

        //This will be done to set the position of the object
        //To assign x and y to every position
        //After this all the chess objects will have the chesspiece scripton it
        //THe objects will be present in the array
        ChessPieces[AxisX, AxisY].SetChessPosition(AxisX, AxisY);

        //After the clone is made and tranformation is done we will add to our active chesspieces.
        ActiveChessPieces.Add(gameobject);
    }


    //This function will set the Pieces on the correct tiles and location on the board Tile.
    public Vector3 GetChessBoardTileCenter(int AxisX, int AxisZ)
    {
        Vector3 ChessBoardOrigin = Vector3.zero;
        ChessBoardOrigin.x = ChessBoardOrigin.x + (ChessBoardTileSize * AxisX) + ChessBoardTileOffset;
        ChessBoardOrigin.z = ChessBoardOrigin.y + (ChessBoardTileSize * AxisZ) + ChessBoardTileOffset;
        //print("ChessBoardOrigin");
        //print((int)ChessBoardOrigin.x + "  ,  "+ (int)ChessBoardOrigin.z);
        return ChessBoardOrigin;

    }


    //This function will spawn all chess pieces
    public void SpawnChessPiecesAll()
    {
        //The below list will store the active chesspieces
        ActiveChessPieces = new List<GameObject>();

        //We will initialize the chess pieces here.
        ChessPieces = new ChessPiece[8, 8];

        SpecialMoveEnPassant = new int[2] { -1, -1 };


        //Spawn White Chess Pieces
        //King spawn and Location
        SpawnChessPieces(0, 4, 0);

        //Queen spawn and Location
        SpawnChessPieces(1, 3, 0);

        //Rooks spawn and Location
        SpawnChessPieces(2, 0, 0);
        SpawnChessPieces(2, 7, 0);

        //Bishops spawn and Location
        SpawnChessPieces(3, 2, 0);
        SpawnChessPieces(3, 5, 0);

        //Knights spawn and Location
        SpawnChessPieces(4, 1, 0);
        SpawnChessPieces(4, 6, 0);

        //Pawn Spawn and loaction
        //As we have 8 of thoise we will use loop for it

        for (int i = 0; i < 8; ++i)
        {
            SpawnChessPieces(5, i, 1);
        }

        //-------------

        //Spawn Black Chess Pieces

        //King spawn and Location
        SpawnChessPieces(6, 4, 7);

        //Queen spawn and Location
        SpawnChessPieces(7, 3, 7);

        //Rooks spawn and Location
        SpawnChessPieces(8, 0, 7);
        SpawnChessPieces(8, 7, 7);

        //Bishops spawn and Location
        SpawnChessPieces(9, 2, 7);
        SpawnChessPieces(9, 5, 7);

        //Knights spawn and Location
        SpawnChessPieces(10, 1, 7);
        SpawnChessPieces(10, 6, 7);

        //Pawn Spawn and loaction
        //As we have 8 of thoise we will use loop for it

        for (int i = 0; i < 8; ++i)
        {
            SpawnChessPieces(11, i, 6);
        }

    }


    //This will determine wheter the piece is selected
    //Is the piece on the given tile availabe or not
    //Is the peice seleted black or wgite according to thier turn
    public void SelectChessPiece(int AxisX, int AxisY)
    {
        //if nothing selected or is there chess main selected
        //And which piece is selected is it black or white

        if (ChessPieces[AxisX, AxisY] == null)
        {
            return;
        }
        if (ChessPieces[AxisX, AxisY].isPieceWhite != IsItWhiteTurn)
        {
            return;
        }

        //to see if there are any possible moves for the piece if not then it will not be slected
        bool CanChessPieceMove = false;
        AllowedChessMoves = ChessPieces[AxisX, AxisY].LegalMoves();

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
        AllowedChessMoves = ChessPieces[AxisX, AxisY].LegalMoves();

        //This will save the current position of the piece moved
        SelectedChessPiece = ChessPieces[AxisX, AxisY];

        //The below three lines of code will change the material colour of the slected piece
        //In the PriorMaterial Field The original material of the piece will be saved so that it can be get back to default material
        PriorMaterials = SelectedChessPiece.GetComponent<MeshRenderer>().material;
        //we will transfer the material of the selected peice so that only its texture can be changed and not some other piece
        SelectedChessPieceMaterial.mainTexture = PriorMaterials.mainTexture;
        //then the texture will be passed to the selected piece and its texture will be set to new colour.
        SelectedChessPiece.GetComponent<MeshRenderer>().material = SelectedChessPieceMaterial;

        //In this line of code we will send the allowed moves to the highlight class so they can be highlighted
        ChessPieceMoveHighlight.Instance.ChessPiecellowedMoveHighlight(AllowedChessMoves);
    }

    public void SelectChessPiece1(int AxisX, int AxisY)
    {
        //if nothing selected or is there chess main selected
        //And which piece is selected is it black or white

        if (ChessPieces[AxisX, AxisY] == null)
        {
            return;
        }
        if (ChessPieces[AxisX, AxisY].isPieceWhite != IsItWhiteTurn)
        {
            return;
        }

        //to see if there are any possible moves for the piece if not then it will not be slected
        bool CanChessPieceMove = false;
        AllowedChessMoves = ChessPieces[AxisX, AxisY].LegalMoves();

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
        AllowedChessMoves = ChessPieces[AxisX, AxisY].LegalMoves();

        //This will save the current position of the piece moved
        SelectedChessPiece = ChessPieces[AxisX, AxisY];

        //The below three lines of code will change the material colour of the slected piece
        //In the PriorMaterial Field The original material of the piece will be saved so that it can be get back to default material
        // PriorMaterials = SelectedChessPiece.GetComponent<MeshRenderer>().material;
        //we will transfer the material of the selected peice so that only its texture can be changed and not some other piece
        //  SelectedChessPieceMaterial.mainTexture = PriorMaterials.mainTexture;
        //then the texture will be passed to the selected piece and its texture will be set to new colour.
        //  SelectedChessPiece.GetComponent<MeshRenderer>().material = SelectedChessPieceMaterial;

        // //In this line of code we will send the allowed moves to the highlight class so they can be highlighted
        //  ChessPieceMoveHighlight.Instance.ChessPiecellowedMoveHighlight(AllowedChessMoves);
    }

    //Throught this function we will be able to move our pieces

    public void MoveChessPiecePlayers1(int AxisX, int AxisY)
    {



        //this if statement  will see if the move is legal
        //Then the selected chesspiece
        if (AllowedChessMoves[AxisX, AxisY])
        {

            //The below if statement will remove the chesspiece from the board
            //We will create instance of chesspiece and take the position of the chesspiece
            ChessPiece cp = ChessPieces[AxisX, AxisY];

            //This condition will check
            //1. if there is piece where the other piece is going to be placed
            //2. and is it white turn or not
            if (cp != null && cp.isPieceWhite != IsItWhiteTurn)
            {

                //This will see if it is the king then end the game
                if (cp.GetType() == typeof(ChessKingPiece))
                {
                    //End game
                    GameWon();
                    winningteam(1);
                    return;
                }

                //Capture pice
                //If it is then we will remove the chess piece from the active peices on the board
                ActiveChessPieces.Remove(cp.gameObject);
                Destroy(cp.gameObject);
                if (IsItWhiteTurn)
                {
                    whitescorevalue++;
                }
                else if (!IsItWhiteTurn)
                {
                    Blackscorevalue++;
                }
                whitescorevalue = 0;
                whiteturnvalue = 0;
                Blackscorevalue = 0;
                Blackturnvalue = 0;
            }

            //this is for the en passant move
            if (AxisX == SpecialMoveEnPassant[0] && AxisY == SpecialMoveEnPassant[1])
            {
                //for white
                if (IsItWhiteTurn)
                {
                    cp = ChessPieces[AxisX, AxisY - 1];
                    ActiveChessPieces.Remove(cp.gameObject);
                    Destroy(cp.gameObject);
                }

                else
                {
                    cp = ChessPieces[AxisX, AxisY + 1];
                    ActiveChessPieces.Remove(cp.gameObject);
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
            }

            //Then the current position of the chess piece to be moved is set to null because the piece is about to be moved
            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;

            //This line of code will get the new position of the piece ans it will enable the movement of the chess piece
            SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);

            //Then we will save the new position of the chess piece in the 2D array
            //It will put it at the right spot in the 2D array.
            ChessPieces[AxisX, AxisY] = SelectedChessPiece;
            SelectedChessPiece.SetChessPosition(AxisX, AxisY);
            if (IsItWhiteTurn)
            {
                whiteturnvalue++;
            }
            else if (!IsItWhiteTurn)
            {
                Blackturnvalue++;
            }
            //print("Mesh");
            //This line of code will check whore turn it is and disbale white or blacl pieces repsectively
            IsItWhiteTurn = !IsItWhiteTurn;
            PuzzleCounter++;

        }




        //This will set back the piece to its original texture.
        //SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;

        //This will hide the highlight when we dont need it anymore
        //ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();

        //If the move is not possible in general then it will unselect the piece
        SelectedChessPiece = null;



    }
    public void MoveChessPiecePlayers2(int AxisX, int AxisY)
    {
        if (IsKingCheck() == "Default")
        {
            //Debug.Log("Degffd");
            //this if statement  will see if the move is legal
            //Then the selected chesspiece
            if (AllowedChessMoves[AxisX, AxisY])
            {
                //The below if statement will remove the chesspiece from the board
                //We will create instance of chesspiece and take the position of the chesspiece
                ChessPiece cp = ChessPieces[AxisX, AxisY];
                //This condition will check
                //1. if there is piece where the other piece is going to be placed
                //2. and is it white turn or not
                if (cp != null && cp.isPieceWhite != IsItWhiteTurn)
                {
                    int PreviousPositionX = SelectedChessPiece.CurrentPositionX;
                    int PreviousPositionY = SelectedChessPiece.CurrentPositionY;
                    //This will see if it is the king then end the game
                    if (cp.GetType() == typeof(ChessKingPiece))
                    {
                        //End game
                        GameWon();
                        winningteam(1);
                        return;
                    }
                    //Capture pice
                    //If it is then we will remove the chess piece from the active peices on the board
                    //ActiveChessPieces.Remove(cp.gameObject);
                    //KilledChessPieces.Add(cp.gameObject);
                    //cp.gameObject.SetActive(false);
                    //Destroy(cp.gameObject);
                    //Debug.Log(KilledChessPieces.Count);
                   // if (IsItWhiteTurn)
                  //  {
                     //   whitescorevalue++;
                 //   }
                 //   else if (!IsItWhiteTurn) {
                     //   Blackscorevalue++;
                 //   }
                    
                    if (cp.GetType() == typeof(ChessPawnPiece) && cp.isPieceWhite)
                        {
                            ActiveChessPieces.Remove(cp.gameObject);
                            Destroy(cp.gameObject);
                             ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                            ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if ((IsKingCheck() == "White is In check" && IsItWhiteTurn) || (IsKingCheck() == "Black is in check" && !IsItWhiteTurn))
                        {
                            if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                            {
                                print("Wrong Move");
                                ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                                SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                                ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                                SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                                SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                                ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                                SpawnChessPieces(5, AxisX, AxisY);
                                //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                                SelectedChessPiece = null;
                                if (IsItWhiteTurn)
                                {
                                    whitescorevalue--;
                                }
                                else if (!IsItWhiteTurn)
                                {
                                    Blackscorevalue--;
                                }
                                return;
                            }
                            }
                        if (IsItWhiteTurn)
                        {
                            whitescorevalue++;
                        }
                        else if (!IsItWhiteTurn)
                        {
                            Blackscorevalue++;
                        }
                    }
                        else if (cp.GetType() == typeof(ChessPawnPiece) && !cp.isPieceWhite)
                        {
                            ActiveChessPieces.Remove(cp.gameObject);
                            Destroy(cp.gameObject);
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                            ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if ((IsKingCheck() == "White is In check" && IsItWhiteTurn) || (IsKingCheck() == "Black is in check" && !IsItWhiteTurn))
                        {
                            if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                            {
                                print("Wrong Move");
                                ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                                SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                                ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                                SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                                SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                                ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                                SpawnChessPieces(11, AxisX, AxisY);
                                //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                                SelectedChessPiece = null;
                                
                                return;
                            }
                        }
                        if (IsItWhiteTurn)
                        {
                            whitescorevalue++;
                        }
                        else if (!IsItWhiteTurn)
                        {
                            Blackscorevalue++;
                        }
                    }
                        else if (cp.GetType() == typeof(ChessQueenPiece) && cp.isPieceWhite)
                        {
                            ActiveChessPieces.Remove(cp.gameObject);
                            Destroy(cp.gameObject);
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                            ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if ((IsKingCheck() == "White is In check" && IsItWhiteTurn) || (IsKingCheck() == "Black is in check" && !IsItWhiteTurn))
                        {
                            if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                            {
                                print("Wrong Move");
                                ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                                SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                                ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                                SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                                SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                                ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                                SpawnChessPieces(1, AxisX, AxisY);
                                // ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                                SelectedChessPiece = null;
                                if (IsItWhiteTurn)
                                {
                                    whitescorevalue--;
                                }
                                else if (!IsItWhiteTurn)
                                {
                                    Blackscorevalue--;
                                }
                                return;
                            }
                        }
                        if (IsItWhiteTurn)
                        {
                            whitescorevalue++;
                        }
                        else if (!IsItWhiteTurn)
                        {
                            Blackscorevalue++;
                        }
                    }
                        else if (cp.GetType() == typeof(ChessQueenPiece) && !cp.isPieceWhite)
                        {
                            ActiveChessPieces.Remove(cp.gameObject);
                            Destroy(cp.gameObject);
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                            ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if ((IsKingCheck() == "White is In check" && IsItWhiteTurn) || (IsKingCheck() == "Black is in check" && !IsItWhiteTurn))
                        {
                            if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                            {
                                print("Wrong Move");
                                ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                                SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                                ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                                SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                                SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                                ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                                SpawnChessPieces(7, AxisX, AxisY);
                                //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                                SelectedChessPiece = null;
                                if (IsItWhiteTurn)
                                {
                                    whitescorevalue--;
                                }
                                else if (!IsItWhiteTurn)
                                {
                                    Blackscorevalue--;
                                }
                                return;
                            }
                        }
                        if (IsItWhiteTurn)
                        {
                            whitescorevalue++;
                        }
                        else if (!IsItWhiteTurn)
                        {
                            Blackscorevalue++;
                        }
                    }
                        else if (cp.GetType() == typeof(ChessKnightPiece) && cp.isPieceWhite)
                        {
                            ActiveChessPieces.Remove(cp.gameObject);
                            Destroy(cp.gameObject);
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                            ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if ((IsKingCheck() == "White is In check" && IsItWhiteTurn) || (IsKingCheck() == "Black is in check" && !IsItWhiteTurn))
                        {
                            if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                            {
                                ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                                SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                                ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                                SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                                SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                                ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                                SpawnChessPieces(4, AxisX, AxisY);
                                // ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                                SelectedChessPiece = null;
                                if (IsItWhiteTurn)
                                {
                                    whitescorevalue--;
                                }
                                else if (!IsItWhiteTurn)
                                {
                                    Blackscorevalue--;
                                }
                                return;
                            }
                        }
                        if (IsItWhiteTurn)
                        {
                            whitescorevalue++;
                        }
                        else if (!IsItWhiteTurn)
                        {
                            Blackscorevalue++;
                        }
                    }
                        else if (cp.GetType() == typeof(ChessKnightPiece) && !cp.isPieceWhite)
                        {
                            ActiveChessPieces.Remove(cp.gameObject);
                            Destroy(cp.gameObject);
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                            ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if ((IsKingCheck() == "White is In check" && IsItWhiteTurn) || (IsKingCheck() == "Black is in check" && !IsItWhiteTurn))
                        {
                            if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                            {
                                ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                                SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                                ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                                SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                                SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                                ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                                SpawnChessPieces(10, AxisX, AxisY);
                                //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                                SelectedChessPiece = null;
                                if (IsItWhiteTurn)
                                {
                                    whitescorevalue--;
                                }
                                else if (!IsItWhiteTurn)
                                {
                                    Blackscorevalue--;
                                }
                                return;
                            }
                        }
                        if (IsItWhiteTurn)
                        {
                            whitescorevalue++;
                        }
                        else if (!IsItWhiteTurn)
                        {
                            Blackscorevalue++;
                        }
                    }
                        else if (cp.GetType() == typeof(ChessRookPiece) && cp.isPieceWhite)
                        {
                            ActiveChessPieces.Remove(cp.gameObject);
                            Destroy(cp.gameObject);
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                            ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if ((IsKingCheck() == "White is In check" && IsItWhiteTurn) || (IsKingCheck() == "Black is in check" && !IsItWhiteTurn))
                        {
                            if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                            {
                                ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                                SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                                ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                                SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                                SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                                ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                                SpawnChessPieces(2, AxisX, AxisY);
                                //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                                SelectedChessPiece = null;
                                if (IsItWhiteTurn)
                                {
                                    whitescorevalue++;
                                }
                                else if (!IsItWhiteTurn)
                                {
                                    Blackscorevalue++;
                                }
                                return;
                            }
                        }
                        }
                        else if (cp.GetType() == typeof(ChessRookPiece) && !cp.isPieceWhite)
                        {
                            ActiveChessPieces.Remove(cp.gameObject);
                            Destroy(cp.gameObject);
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                            ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if ((IsKingCheck() == "White is In check" && IsItWhiteTurn) || (IsKingCheck() == "Black is in check" && !IsItWhiteTurn))
                        {
                            if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                            {
                                ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                                SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                                ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                                SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                                SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                                ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                                SpawnChessPieces(8, AxisX, AxisY);
                                //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                                SelectedChessPiece = null;
                                if (IsItWhiteTurn)
                                {
                                    whitescorevalue--;
                                }
                                else if (!IsItWhiteTurn)
                                {
                                    Blackscorevalue--;
                                }
                                return;
                            }
                        }
                        if (IsItWhiteTurn)
                        {
                            whitescorevalue++;
                        }
                        else if (!IsItWhiteTurn)
                        {
                            Blackscorevalue++;
                        }
                    }
                        else if (cp.GetType() == typeof(ChessBishopPiece) && cp.isPieceWhite)
                        {
                            ActiveChessPieces.Remove(cp.gameObject);
                            Destroy(cp.gameObject);
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                            ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if ((IsKingCheck() == "White is In check" && IsItWhiteTurn) || (IsKingCheck() == "Black is in check" && !IsItWhiteTurn))
                        {
                            if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                            {
                                ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                                SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                                ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                                SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                                SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                                ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                                SpawnChessPieces(3, AxisX, AxisY);
                                //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                                SelectedChessPiece = null;
                                if (IsItWhiteTurn)
                                {
                                    whitescorevalue--;
                                }
                                else if (!IsItWhiteTurn)
                                {
                                    Blackscorevalue--;
                                }
                                return;
                            }
                        }
                        if (IsItWhiteTurn)
                        {
                            whitescorevalue++;
                        }
                        else if (!IsItWhiteTurn)
                        {
                            Blackscorevalue++;
                        }
                    }
                        else if (cp.GetType() == typeof(ChessBishopPiece) && !cp.isPieceWhite)
                        {
                            ActiveChessPieces.Remove(cp.gameObject);
                            Destroy(cp.gameObject);
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                            ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if ((IsKingCheck() == "White is In check" && IsItWhiteTurn) || (IsKingCheck() == "Black is in check" && !IsItWhiteTurn))
                        {
                            if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                            {
                                ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                                SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                                ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                                SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                                SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                                ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                                SpawnChessPieces(9, AxisX, AxisY);
                                //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                                SelectedChessPiece = null;
                                if (IsItWhiteTurn)
                                {
                                    whitescorevalue--;
                                }
                                else if (!IsItWhiteTurn)
                                {
                                    Blackscorevalue--;
                                }
                                return;
                            }
                        }
                        if (IsItWhiteTurn)
                        {
                            whitescorevalue++;
                        }
                        else if (!IsItWhiteTurn)
                        {
                            Blackscorevalue++;
                        }
                    }
                   
                }
                //this is for the en passant move
                if (AxisX == SpecialMoveEnPassant[0] && AxisY == SpecialMoveEnPassant[1])
                {
                    //for white
                    if (IsItWhiteTurn)
                    {
                        cp = ChessPieces[AxisX, AxisY - 1];
                        ActiveChessPieces.Remove(cp.gameObject);
                        Destroy(cp.gameObject);
                    }
                    else
                    {
                        cp = ChessPieces[AxisX, AxisY + 1];
                        ActiveChessPieces.Remove(cp.gameObject);
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
                        specialmoves.SetActive(true);
                    }
                    else if (AxisY == 0)
                    {
                        ActiveChessPieces.Remove(SelectedChessPiece.gameObject);
                        Destroy(SelectedChessPiece.gameObject);
                        SpawnChessPieces(7, AxisX, AxisY);
                        SelectedChessPiece = ChessPieces[AxisX, AxisY];
                        specialmoves.SetActive(true);
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
                }

                int PreviousPositionXx = SelectedChessPiece.CurrentPositionX;
                int PreviousPositionYy = SelectedChessPiece.CurrentPositionY;
                //Then the current position of the chess piece to be moved is set to null because the piece is about to be moved
                ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;

                //This line of code will get the new position of the piece ans it will enable the movement of the chess piece
                SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                //Debug.Log(AxisX + " " + AxisY);
                //Then we will save the new position of the chess piece in the 2D array
                //It will put it at the right spot in the 2D array.
                ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                //SaveGame(AxisX, AxisY);
                if ((IsKingCheck() == "White is In check" && IsItWhiteTurn) || (IsKingCheck() == "Black is in check" && !IsItWhiteTurn))
                {
                    print("Wrong Move");
                    ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                    SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionXx, PreviousPositionYy);
                    ChessPieces[PreviousPositionXx, PreviousPositionYy] = SelectedChessPiece;
                    SelectedChessPiece.SetChessPosition(PreviousPositionXx, PreviousPositionYy);
                    SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                    ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                    //SpawnChessPieces(5, AxisX, AxisY);
                    SelectedChessPiece = null;
                   // if (IsItWhiteTurn)
                    {
                    //    whitescorevalue--;
                    }
                   /// else if (!IsItWhiteTurn)
                    {
                    //    Blackscorevalue--;
                    }


                    //ActiveChessPieces.Add(ChessPieces[x, y].gameObject);
                    return;
                }

                //else if(IsKingCheck() == "White is In check" &&)

                //      print("Mesh");
                //This line of code will check whore turn it is and disbale white or blacl pieces repsectively
                if (IsItWhiteTurn)
                {
                    whiteturnvalue++;
                }
                else if (!IsItWhiteTurn)
                {
                    Blackturnvalue++;
                }
                IsItWhiteTurn = !IsItWhiteTurn;
                PuzzleCounter++;
                
                //SaveGame(AxisX, AxisY);
                if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                {
                    if (IsKingCheck() == "White is In check")
                    {
                        //LocalBoard.Instance.CheckMate("White");
                        if (LocalBoard.Instance.CheckMate("White") == "CheckMate")
                        {
                            GameWon();
                            winningteam(1);
                        }
                    }
                    else if (IsKingCheck() == "Black is in check")
                    {
                        if (LocalBoard.Instance.CheckMate("Black") == "CheckMate")
                        {
                            GameWon();
                            winningteam(2);
                        }
                    }
                }

            }
        }

        else if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
        {
            if (IsKingCheck() == "White is In check")
            {
                if (LocalBoard.Instance.CheckMate("White") == "CheckMate")
                {
                    Debug.Log("CheckMate");
                    GameWon();
                    winningteam(1);
                }
            }
            else if (IsKingCheck() == "Black is in check")
            {
                if (LocalBoard.Instance.CheckMate("Black") == "CheckMate")
                {
                    Debug.Log("CheckMate");
                    GameWon();
                    winningteam(1);
                }
            }
            //SaveGame();
            if (AllowedChessMoves[AxisX, AxisY])
            {

                //The below if statement will remove the chesspiece from the board
                //We will create instance of chesspiece and take the position of the chesspiece
                ChessPiece cp = ChessPieces[AxisX, AxisY];
                // int PreviousPositionX = SelectedChessPiece.CurrentPositionX;
                // int PreviousPositionY = SelectedChessPiece.CurrentPositionY;
                //This condition will check
                //1. if there is piece where the other piece is going to be placed
                //2. and is it white turn or not
                if (cp != null && cp.isPieceWhite != IsItWhiteTurn)
                {
                    int PreviousPositionX = SelectedChessPiece.CurrentPositionX;
                    int PreviousPositionY = SelectedChessPiece.CurrentPositionY;
                    //This will see if it is the king then end the game
                    if (cp.GetType() == typeof(ChessKingPiece) && cp.isPieceWhite)
                    {
                        //End game
                        GameWon();
                        return;
                    }

                    else if (cp.GetType() == typeof(ChessKingPiece) && !cp.isPieceWhite)
                    {
                        GameWon();
                        return;

                    }
                    else if (cp.GetType() == typeof(ChessPawnPiece) && cp.isPieceWhite)
                    {
                        ActiveChessPieces.Remove(cp.gameObject);
                        Destroy(cp.gameObject);

                        ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                        SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                        ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                        SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                        {
                            print("Wrong Move");
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                            ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                            SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                            ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                            SpawnChessPieces(5, AxisX, AxisY);
                            //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                            SelectedChessPiece = null;
                            return;
                        }
                    }
                    else if (cp.GetType() == typeof(ChessPawnPiece) && !cp.isPieceWhite)
                    {
                        ActiveChessPieces.Remove(cp.gameObject);
                        Destroy(cp.gameObject);
                        ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                        SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                        ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                        SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                        {
                            print("Wrong Move");
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                            ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                            SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                            ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                            SpawnChessPieces(11, AxisX, AxisY);
                            //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                            SelectedChessPiece = null;
                            return;
                        }
                    }
                    else if (cp.GetType() == typeof(ChessQueenPiece) && cp.isPieceWhite)
                    {
                        ActiveChessPieces.Remove(cp.gameObject);
                        Destroy(cp.gameObject);
                        ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                        SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                        ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                        SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                        {
                            print("Wrong Move");
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                            ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                            SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                            ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                            SpawnChessPieces(1, AxisX, AxisY);
                            // ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                            SelectedChessPiece = null;
                            return;
                        }
                    }
                    else if (cp.GetType() == typeof(ChessQueenPiece) && !cp.isPieceWhite)
                    {
                        ActiveChessPieces.Remove(cp.gameObject);
                        Destroy(cp.gameObject);
                        ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                        SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                        ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                        SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                        {
                            print("Wrong Move");
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                            ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                            SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                            ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                            SpawnChessPieces(7, AxisX, AxisY);
                            //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                            SelectedChessPiece = null;
                            return;
                        }
                    }
                    else if (cp.GetType() == typeof(ChessKnightPiece) && cp.isPieceWhite)
                    {
                        ActiveChessPieces.Remove(cp.gameObject);
                        Destroy(cp.gameObject);
                        ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                        SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                        ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                        SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                        {
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                            ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                            SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                            ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                            SpawnChessPieces(4, AxisX, AxisY);
                            // ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                            SelectedChessPiece = null;
                            return;
                        }
                    }
                    else if (cp.GetType() == typeof(ChessKnightPiece) && !cp.isPieceWhite)
                    {
                        ActiveChessPieces.Remove(cp.gameObject);
                        Destroy(cp.gameObject);
                        ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                        SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                        ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                        SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                        {
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                            ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                            SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                            ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                            SpawnChessPieces(10, AxisX, AxisY);
                            //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                            SelectedChessPiece = null;
                            return;
                        }
                    }
                    else if (cp.GetType() == typeof(ChessRookPiece) && cp.isPieceWhite)
                    {
                        ActiveChessPieces.Remove(cp.gameObject);
                        Destroy(cp.gameObject);
                        ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                        SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                        ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                        SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                        {
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                            ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                            SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                            ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                            SpawnChessPieces(2, AxisX, AxisY);
                            //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                            SelectedChessPiece = null;
                            return;
                        }
                    }
                    else if (cp.GetType() == typeof(ChessRookPiece) && !cp.isPieceWhite)
                    {
                        ActiveChessPieces.Remove(cp.gameObject);
                        Destroy(cp.gameObject);
                        ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                        SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                        ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                        SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                        {
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                            ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                            SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                            ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                            SpawnChessPieces(8, AxisX, AxisY);
                            //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                            SelectedChessPiece = null;
                            return;
                        }
                    }
                    else if (cp.GetType() == typeof(ChessBishopPiece) && cp.isPieceWhite)
                    {
                        ActiveChessPieces.Remove(cp.gameObject);
                        Destroy(cp.gameObject);
                        ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                        SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                        ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                        SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                        {
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                            ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                            SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                            ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                            SpawnChessPieces(3, AxisX, AxisY);
                            //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                            SelectedChessPiece = null;
                            return;
                        }
                    }
                    else if (cp.GetType() == typeof(ChessBishopPiece) && !cp.isPieceWhite)
                    {
                        ActiveChessPieces.Remove(cp.gameObject);
                        Destroy(cp.gameObject);
                        ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                        SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                        ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                        SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                        if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                        {
                            ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                            SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionX, PreviousPositionY);
                            ChessPieces[PreviousPositionX, PreviousPositionY] = SelectedChessPiece;
                            SelectedChessPiece.SetChessPosition(PreviousPositionX, PreviousPositionY);
                            SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                            ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                            SpawnChessPieces(9, AxisX, AxisY);
                            //ActiveChessPieces.Add(ChessPieces[AxisY, AxisY].gameObject);
                            SelectedChessPiece = null;
                            return;
                        }
                    }



                    //Capture pice
                    //If it is then we will remove the chess piece from the active peices on the board


                }
                int PreviousPositionXx = SelectedChessPiece.CurrentPositionX;
                int PreviousPositionYy = SelectedChessPiece.CurrentPositionY;

                ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
                {
                    print("Wrong Move");
                    ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                    SelectedChessPiece.transform.position = GetChessBoardTileCenter(PreviousPositionXx, PreviousPositionYy);
                    ChessPieces[PreviousPositionXx, PreviousPositionYy] = SelectedChessPiece;
                    SelectedChessPiece.SetChessPosition(PreviousPositionXx, PreviousPositionYy);
                    SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
                    ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
                    //SpawnChessPieces(5, AxisX, AxisY);
                    SelectedChessPiece = null;

                    //ActiveChessPieces.Add(ChessPieces[x, y].gameObject);
                    return;
                }
                else
                {
                    if (IsItWhiteTurn)
                    {
                        whiteturnvalue++;
                    }
                    else if (!IsItWhiteTurn)
                    {
                        Blackturnvalue++;
                    }
                    //SelectedChessPiece = null;
                    IsItWhiteTurn = !IsItWhiteTurn;
                }

                //this is for the en passant move
                if (AxisX == SpecialMoveEnPassant[0] && AxisY == SpecialMoveEnPassant[1])
                {
                    //for white
                    if (IsItWhiteTurn)
                    {
                        cp = ChessPieces[AxisX, AxisY - 1];
                        ActiveChessPieces.Remove(cp.gameObject);
                        Destroy(cp.gameObject);
                    }

                    else
                    {
                        cp = ChessPieces[AxisX, AxisY + 1];
                        ActiveChessPieces.Remove(cp.gameObject);
                        Destroy(cp.gameObject);
                    }
                }


                SpecialMoveEnPassant[0] = -1;
                SpecialMoveEnPassant[1] = -1;

                //   if (SelectedChessPiece.GetType() == typeof(ChessPawnPiece))

                //For special move promotion
                /* if (AxisY == 7)
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
                 }*/

            }
            
        }

        //This will set back the piece to its original texture.
        SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;

        //This will hide the highlight when we dont need it anymore
        ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();

        //If the move is not possible in general then it will unselect the piece
        SelectedChessPiece = null;


        



    }



    public void MoveChessPiece1(int AxisX, int AxisY)
    {

        //IsKingCheck();
        // if (IsKingCheck() != "Black is in check" || IsKingCheck() != "White is in check")

        if (IsKingCheck() == "Default")
        {
            if (AllowedChessMoves[AxisX, AxisY])
            {

                ChessPiece cp = ChessPieces[AxisX, AxisY];

                if (cp != null && cp.isPieceWhite != IsItWhiteTurn)
                {
                    print("cp is not null 111");
                    if (cp.GetType() == typeof(ChessKingPiece))
                    {
                        //End game
                        GameWon();
                        return;
                    }
                    ActiveChessPieces.Remove(cp.gameObject);
                    Destroy(cp.gameObject);
                }

                //Then the current position of the chess piece to be moved is set to null because the piece is about to be moved
                ChessPieces[SelectedChessPiece.CurrentPositionX, SelectedChessPiece.CurrentPositionY] = null;
                //This line of code will get the new position of the piece ans it will enable the movement of the chess piece
                SelectedChessPiece.transform.position = GetChessBoardTileCenter(AxisX, AxisY);
                //Then we will save the new position of the chess piece in the 2D array
                //It will put it at the right spot in the 2D array.
                ChessPieces[AxisX, AxisY] = SelectedChessPiece;
                SelectedChessPiece.SetChessPosition(AxisX, AxisY);
                //This line of code will check whore turn it is and disbale white or blacl pieces repsectively
                IsItWhiteTurn = !IsItWhiteTurn;
            }
        }


        else if (IsKingCheck() == "White is In check" || IsKingCheck() == "Black is in check")
        {

            if (AllowedChessMoves[AxisX, AxisY])
            {
                // List<Vector2> possibleMovements = new List<Vector2>();
                ChessPiece cp = ChessPieces[AxisX, AxisY];
                int x = AxisX;
                int y = AxisY;
                if (cp != null && cp.isPieceWhite != IsItWhiteTurn)
                {
                    print("cp is not null 111");
                    if (cp.GetType() == typeof(ChessKingPiece) && cp.isPieceWhite)
                    {
                        //End game
                        GameWon();

                        return;
                    }
                    ActiveChessPieces.Remove(cp.gameObject);
                    Destroy(cp.gameObject);
                    //ActiveChessPieces.Add(cp.gameObject);
                }
                    print("Here11");
                    IsItWhiteTurn = !IsItWhiteTurn;
            }
        }
        //This will set back the piece to its original texture.
        SelectedChessPiece.GetComponent<MeshRenderer>().material = PriorMaterials;
        //This will hide the highlight when we dont need it anymore
        ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
        //If the move is not possible in general then it will unselect the piece
        SelectedChessPiece = null;
    }


    public void GameWon()
    {
        if (IsItWhiteTurn)
        {
            Debug.Log("White team wins");
            victoryscreen.SetActive(true);

            //WinScreeen.GetComponent<UnityEngine.UI.Text>().text;
            //WinScreen.GetComponent<UnityEngine.UI.Text>();
            //victorynamewhite = GameObject.Find("ChessBoard/Canvas/Image/blackwins").GetComponent<UnityEngine.UI.Text>();
            //victorynamewhite.text = Whitewin;
            //WinScreeen = GameObject.Find("ChessBoard/Canvas/Image/blackwins").GetComponent<Text>();




        }
        else
        {
            Debug.Log("Black Team wins");
            victoryscreen.SetActive(true);

            //victorynameblack = GameObject.Find("ChessBoard/Canvas/Image/blackwins").GetComponent<UnityEngine.UI.Text>();
            //victorynameblack.text = "Black team Wins".ToString();
        }
        foreach (GameObject gameobject in ActiveChessPieces)
        {
            Destroy(gameobject);
        }

        IsItWhiteTurn = true;
        ChessPieceMoveHighlight.Instance.HideChessmoveHighlights();
        SpawnChessPiecesAll();

    }



    public List<GameObject> GetAllActiveFigures()
    {
        List<GameObject> unityGameObjects = new List<GameObject>();
        for (int i = 0; i < ActiveChessPieces.Count; i++)
        {
            if (ActiveChessPieces[i].name == "knight black(Clone)"
                || ActiveChessPieces[i].name == "pawn black(Clone)"
                || ActiveChessPieces[i].name == "queen black(Clone)"
                || ActiveChessPieces[i].name == "rook black(Clone)"
                || ActiveChessPieces[i].name == "bishop black(Clone)"
                || ActiveChessPieces[i].name == "king black(Clone)")
            {
                unityGameObjects.Add(ActiveChessPieces[i]);

            }
        }
        return unityGameObjects;
    }


   
   
    public string IsKingCheck()
    {
        List<Vector2> possibleMovements = new List<Vector2>();
        for (int a = 0; a < 8; a++)
        {
            for (int k = 0; k < 8; k++)
            {
                if (ChessPieces[a, k] == null)
                {
                    continue;
                }
                else
                {
                    bool[,] possibleMoves = ChessPieces[a, k].LegalMoves();
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (possibleMoves[i, j])
                            {
                                ChessPiece cp = ChessPieces[i, j];
                                //SelectedChessPiece = ChessPieces[i, j];
                                if (cp != null/* && cp.isPieceWhite != IsItWhiteTurn*/)
                                {
                                    if (cp.GetType() == typeof(ChessKingPiece) && cp.isPieceWhite)
                                    {
                                        possibleMovements.Add(new Vector2(i, j));

                                       // print("White is In check");
                                        return "White is In check";

                                    }
                                    else if (cp.GetType() == typeof(ChessKingPiece))
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


    public void SaveGame()
    {
        StreamWriter sw = new StreamWriter("Assets/Scripts/PVP/SaveGame.txt");

        sw.WriteLine(/*ChessBoardManager.Instance.*/IsItWhiteTurn + "," + 1 + "," + 2, false);
        for (int a = 0; a < 8; a++)
        {
            for (int k = 0; k < 8; k++)
            {
                ChessPiece cp = ChessPieces[a, k];
                if (cp == null)
                {
                    continue;
                }
                else if (cp.isPieceWhite)
                {
                    sw.WriteLine(cp.name + "," + a + "," + k, false);
                }
                else if (!cp.isPieceWhite)
                {
                    sw.WriteLine(cp.name + "," + a + "," + k, false);
                }
            }
        }
        sw.Close();
    }

    public void LoadGame()
    {
        ActiveChessPieces = new List<GameObject>();

        //We will initialize the chess pieces here.
        ChessPieces = new ChessPiece[8, 8];

        SpecialMoveEnPassant = new int[2] { -1, -1 };
        //StreamReader sr = new StreamReader("Assets/Scripts/PVP/SaveGame.txt");
        string[] lines = File.ReadAllLines("Assets/Scripts/PVP/SaveGame.txt");
        foreach (string line in lines)
        {
            Debug.Log("LINE COUNT" + lines.Length);
            string[] col = line.Split(',');
            // process col[0], col[1], col[2]
            string name = col[0];
            int AxisX1 = int.Parse(col[1]);
            int AxisY1 = int.Parse(col[2]);
           // Debug.Log("this is my fucking Axis");
            Debug.Log(name + " " + AxisX1 + " " + AxisY1);
            if (name == "False")
            {
                IsItWhiteTurn = false;
            }
            else if (name == "True")
            {
                IsItWhiteTurn = true;
            }
            if (name == "pawn black(Clone)")
            {
                SpawnChessPieces(11, AxisX1, AxisY1);
            }

            else if (name == "knight black(Clone)")
            {
                SpawnChessPieces(10, AxisX1, AxisY1);
            }
            else if (name == "bishop black(Clone)")
            {
                SpawnChessPieces(9, AxisX1, AxisY1);
            }
            else if (name == "rook black(Clone)")
            {
                SpawnChessPieces(8, AxisX1, AxisY1);
            }
            else if (name == "queen black(Clone)")
            {
                SpawnChessPieces(7, AxisX1, AxisY1);
            }
            else if (name == "king black(Clone)")
            {
                SpawnChessPieces(6, AxisX1, AxisY1);
            }
            else if (name == "pawn white(Clone)")
            {
                SpawnChessPieces(5, AxisX1, AxisY1);
            }
            else if (name == "knight white(Clone)")
            {
                SpawnChessPieces(4, AxisX1, AxisY1);
            }
            else if (name == "bishop white(Clone)")
            {
                SpawnChessPieces(3, AxisX1, AxisY1);
            }
            else if (name == "rook white(Clone)")
            {
                SpawnChessPieces(2, AxisX1, AxisY1);
            }
            else if (name == "queen white(Clone)")
            {
                SpawnChessPieces(1, AxisX1, AxisY1);
            }
            else if (name == "king white(Clone)")
            {
                SpawnChessPieces(0, AxisX1, AxisY1);
            }
            else
            {
                continue;
            }
        }

    }

}

