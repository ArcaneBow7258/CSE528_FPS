using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    
    public Vector2 offset;

    public Vector2 size;
    public int startPos = 0;
    public int iterations = 10;
    public GameObject[] rooms;
    public float[] chances;
    private SortedList<float, GameObject> roomies = new SortedList<float, GameObject>(); // relative weights please
    private float totalChance = 0;
    List<Cell> board;
    private class Cell{
        public bool visted = false;
        public bool[] status = {false, false, false, false};

    }
    void Awake(){
        if(rooms.Length != chances.Length){
            throw new UnityException("You do not have a room for every chance or a chance for each room");
        }else{
            for(int i = 0; i < rooms.Length; i++)
            roomies.Add(chances[i],rooms[i]);
        }
    }
    void Start(){
        GridGenerator();
    }


    void RoomGenerator(){
        foreach(var k in roomies.Keys){
            totalChance += k;
        }

        for(int i = 0; i < size.x; i++){
            for(int j=0; j < size.y; j++){
                Cell currentCell = board[((int)(i +( j * size.x)))];
                float random = Random.Range(0,totalChance);
                Debug.Log("Init random " + random);
                GameObject room = null;
                foreach(var entry in roomies){
                    if(random <= entry.Key){
                        room = entry.Value;
                        break;
                    }else{
                        random -= entry.Key;
                        Debug.Log("next random" + totalChance);
                    }
                }

                GameObject r = Instantiate(room, new Vector3(i* offset.x+this.transform.position.x, 0, -j*offset.y + this.transform.position.y),Quaternion.identity, transform);
                r.GetComponent<RoomBehavior>().UpdateRoom(currentCell.status);
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
                        board[currentCell].status[2] = true;
                        //set wall = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                        //set back wall is aslo true
                    }else{
                        //setwall = true
                         board[currentCell].status[1] = true;
                        currentCell = newCell;
                         board[currentCell].status[0] = true;
                    }   
                }else{
                    if(newCell + 1 == currentCell){
                        //left
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                        //right
                    }else{
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
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
