
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;
using System.Linq;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;
    public GameObject BlueFish;
    public GameObject PinkFish;
    public GameObject Kambala;
    public GameObject KambalaSprite;
    public GameObject PinkFishSprite;
    public GameObject BlueFishSprite;
    public GameObject DownBlock;
    public GameObject TopBlock;
    public GameObject MidBlock;
    public List<BoardCell> PinkFishRow;
    public List<BoardCell> BlueFishRow;
    public List<BoardCell> KambalaRow;
    public List<GameObject> fishList;
    private List<float> rowsX;
    public int fishInPlaceCount=0;
    public TMP_Text winText;

    private List<BoardCell> cells;


    void Start()
    {
        instance = GetComponent<BoardManager>();
        cells = new List<BoardCell>();
        rowsX = new List<float>();
        BlueFishRow = new List<BoardCell>();
        KambalaRow = new List<BoardCell>();
        PinkFishRow = new List<BoardCell>();
        FillCells();
        Vector2 offset = BlueFish.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x, offset.y);
        CreateBlock(-0.32, 0.32, -0.16, 0.16);
        GenerateFishRows();
       /* foreach (BoardCell item in cells)
            Debug.Log(item.X + " " + item.Y);*/
        

    }

    private void Update()
    {
        if (IsAllFishInPlace())
        {
            winText.text = "YOU WIN press R to restart";

            if (Input.GetKeyDown(KeyCode.R))
            {
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
           
        }
            
    }

    private void GenerateFishRows()
    {

        Instantiate(BlueFishSprite, new Vector3(rowsX[0], (float)0.60, 0), PinkFish.transform.rotation);
        Instantiate(PinkFishSprite, new Vector3(rowsX[1], (float)0.60, 0), PinkFish.transform.rotation);
        Instantiate(KambalaSprite, new Vector3(rowsX[2], (float)0.60, 0), PinkFish.transform.rotation);
        for (double y = -0.32; y <= 0.32; y += 0.16) 
            BlueFishRow.Add(new BoardCell((float)(rowsX[0]), (float)(y)));
        for (double y = -0.32; y <= 0.32; y += 0.16) 
            PinkFishRow.Add(new BoardCell((float)(rowsX[1]), (float)(y)));
        for (double y = -0.32; y <= 0.32; y += 0.16) 
            KambalaRow.Add(new BoardCell((float)(rowsX[2]), (float)(y)));
        

       



    }
    private void FillCells()
    {

        for (double y = -0.32; y <= 0.32; y += 0.16) 
            cells.Add(new BoardCell((float)(0.32), (float)(y)));


        for (double y = -0.32; y <= 0.32; y += 0.16)
            cells.Add(new BoardCell((float)(0), (float)(y)));



        for (double y = -0.32; y <= 0.32; y += 0.16)
            cells.Add(new BoardCell((float)(-0.32), (float)(y)));

        rowsX.Add((float)0.32);
        rowsX.Add((float)-0.32);
        rowsX.Add((float)0);
        cells = cells.OrderBy(a => Guid.NewGuid()).ToList();
        rowsX = rowsX.OrderBy(a => Guid.NewGuid()).ToList();




    }


    private bool IsAllFishInPlace()
    {
        foreach(GameObject fish in fishList)
        {
            if (fish.GetComponent<MoveFish>().fishInPlace)
            {
                fishInPlaceCount++;
            }
               
        }
        /*Debug.Log("fish in place - "+ fishInPlaceCount);*/
        if (fishInPlaceCount == fishList.Count)
        {
           /* Debug.Log("all fishes in place");*/
            return true;
        }
        else
        {
            fishInPlaceCount = 0;
            return false;
        }
             
         
    }

    private void CreateBoard(float xOffset, float yOffset)
    {
       
        int x = 0;
        GameObject fish = Kambala;
        float startX = transform.position.x;
        float startY = transform.position.y;
        GameObject newTile;
        foreach (BoardCell cell in cells)
        {
            if (x == 10)
                fish = BlueFish;
            if (x == 5)
                fish = PinkFish;
             x++;
            newTile = Instantiate(fish, new Vector3(cell.X, cell.Y, 0), PinkFish.transform.rotation);
            fishList.Add(newTile);
        }
    }

    private void CreateBlock(double yStart, double yEnd, double xStart, double xEnd)
    {
        
        float startX = transform.position.x;
        float startY = transform.position.y;
        GameObject newTile;

        for (double y = yStart; y <= yEnd; y+=0.32)
        {
            for (double x = xStart; x <= xEnd; x+=0.32)
            {
                if(y == yStart)
                newTile = Instantiate(DownBlock, new Vector3((float)x, (float)y, 0), BlueFish.transform.rotation);
                else if(y == yEnd)
                newTile = Instantiate(TopBlock, new Vector3((float)x, (float)y, 0), BlueFish.transform.rotation);
                else
                newTile = Instantiate(MidBlock, new Vector3((float)x, (float)y, 0), BlueFish.transform.rotation);

              


            }
        }
    }

}
