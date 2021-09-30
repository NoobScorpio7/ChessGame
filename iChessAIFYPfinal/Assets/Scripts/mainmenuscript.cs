using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenuscript : MonoBehaviour
{
    
 
    public void selectdifficultylevel()
    {
        SceneManager.LoadScene(1);
        
        
    }
    public void backtomainmenu()
    {
        SceneManager.LoadScene(0);


    }

    public void selectpuzzlelevel()
    {
        SceneManager.LoadScene(7);


    }
    public void selectpuzzlelevel1()
    {
        SceneManager.LoadScene(8);


    }
    public void selectpuzzlelevel2()
    {
        SceneManager.LoadScene(9);


    }
    public void selectpuzzlelevel3()
    {
        SceneManager.LoadScene(10);


    }
    public void playpvpgame()
    {
        SceneManager.LoadScene(2);


    }

    public void easydifficultylevel()
    {
        SceneManager.LoadScene(3);


    }

    public void mediumdifficultylevel()
    {
        SceneManager.LoadScene(4);


    }
    public void harddifficultylevel()
    {
        SceneManager.LoadScene(5);


    }


    public void backbutton()
    {
        SceneManager.LoadScene(0);


    }
    public void loadgame()
    {
        SceneManager.LoadScene(6);

    }



}
