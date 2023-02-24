using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject room;
    public Vector2 offset;

    public Vector2 size;
    public int startPos = 0;
    public int iterations = 1000;
    

    List<Cell> board;
    private class Cell{
        public bool visted = false;
        public bool[] status = new bool[4];

    }
    void Start(){
        GridGenerator();
    }


    void RoomGenerator(){
        for(int i = 0; i < size.x; i++){
            for(int j=0; j < size.y; j++){
                Instantiate(room, new Vector3(i* offset.x+this.transform.position.x, 0, -j*offset.y + this.transform.position.y),Quaternion.identity, transform);
                //update room
            }

        }

    }
    void GridGenerator(){
        board = new List<Cell>();
        for(int i = 0; i < size.x; i++){
            for(int j = 0; j < size.y; j++){
                board.Add(new Cell());
            }

        }
        int currentCell = startPos;
        Stack<int> path = new Stack<int>();
        int k = 0;
        while(k < iterations){
            k++;
            board[currentCell].visted = true;
            List<int> neighbors = CheckNeighbors(currentCell);
            if(neighbors.Count ==0){
                if(path.Count==0){
                    break;
                }
                else{
                    currentCell = path.Pop();
                }
            }else{
                path.Push(currentCell);
                int newCell = neighbors[Random.Range(0,neighbors.Count)];

                if (newCell > currentCell){
                    if(newCell -1 == currentCell){
                        //set wall = true;
                        currentCell = newCell;
                        //set back wall is aslo true
                    }else{
                        //setwall = true
                        currentCell = newCell;
                    }   
                }else{
                    if(newCell + 1 == currentCell){
                        //left
                        currentCell = newCell;
                        //right
                    }else{
                        currentCell = newCell;
                    }
                }
            }
        }
        RoomGenerator();
    }
    List<int> CheckNeighbors(int cell){
        List<int> n = new List<int>();
        if(cell - size.x >= 0 && !board[Mathf.FloorToInt(cell-size.x)].visted){
                n.Add(Mathf.FloorToInt(cell-size.x));

        }
        if(cell + size.x < board.Count && !board[Mathf.FloorToInt(cell+size.x)].visted){
                n.Add(Mathf.FloorToInt(cell+size.x));

        }
        if((cell+1) % size.x != 0 && !board[Mathf.FloorToInt(cell+1)].visted){
                n.Add(Mathf.FloorToInt(cell+1));

        }
        if(cell % size.x != 0 && !board[Mathf.FloorToInt(cell-1)].visted){
                n.Add(Mathf.FloorToInt(cell-1));

        }
        return n;

    }
}
