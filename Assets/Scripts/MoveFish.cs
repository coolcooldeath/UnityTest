using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class MoveFish : MonoBehaviour
{
  
    private int speed;
    private Vector3 emptyPos;
    private GameObject board;
    public bool fishInPlace = false;
    private RaycastHit2D hitUp,hitDown,hitRight,hitLeft;
    void Start()
    {
        
        board = GameObject.Find("Board");
        emptyPos = transform.position;
        speed = 2;
        CheckFish();

    }


    void CheckPosition(List<BoardCell> boards)
    {
        int count = 0;
        Debug.Log("fishInPlace = " + fishInPlace);
        foreach (BoardCell cell in boards )
            if (gameObject.transform.position.x == cell.X && gameObject.transform.position.y == cell.Y)
                fishInPlace = true;
            else
                count++;

        if (count == board.GetComponent<BoardManager>().BlueFishRow.Count)
            fishInPlace = false;
    }

    void CheckFish() {
        if (gameObject.name == "blue_fish(Clone)")
            CheckPosition(board.GetComponent<BoardManager>().BlueFishRow);
        else if (gameObject.name == "pink_fish(Clone)")
            CheckPosition(board.GetComponent<BoardManager>().PinkFishRow);
        else if (gameObject.name == "kambala(Clone)")
            CheckPosition(board.GetComponent<BoardManager>().KambalaRow);
    }
    void Update()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Fish"))
        {


            CheckFish();


            if (Input.GetKeyDown(KeyCode.W))
                { 
                hitUp = Physics2D.Raycast(transform.position, Vector2.up, (float)0.16, LayerMask.GetMask("Blocking"));
                if (hitUp.collider == null)
                    emptyPos.y += (float)0.16;
                

                }

            

            if (Input.GetKeyDown(KeyCode.D))
                {
                 
                hitRight = Physics2D.Raycast(transform.position, Vector2.right, (float)0.16, LayerMask.GetMask("Blocking"));
                if (hitRight.collider == null)
                    emptyPos.x += (float)0.16;

                }
                if (Input.GetKeyDown(KeyCode.A) )
                {
                 
                hitLeft = Physics2D.Raycast(transform.position, Vector2.left, (float)0.16, LayerMask.GetMask("Blocking"));
                if (hitLeft.collider == null)
                    emptyPos.x -= (float)0.16;

                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                  
                hitDown = Physics2D.Raycast(transform.position, Vector2.down, (float)0.16, LayerMask.GetMask("Blocking"));
                if (hitDown.collider == null)
                    emptyPos.y -= (float)0.16;

                }

             
                MoveFishOnBoard();
            
             

            

        }
    }

    void OnMouseDown()
    {

       
        foreach(GameObject fish in GameObject.FindGameObjectsWithTag("Player"))
        {
            fish.layer = LayerMask.NameToLayer("Blocking");
            fish.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.6f);

        }
        gameObject.layer = LayerMask.NameToLayer("Fish");
       gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);//


    }

    void MoveFishOnBoard()
    {
        if (transform.position != emptyPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, emptyPos, speed * Time.deltaTime);
        }
        
        

       
    }

 
}
