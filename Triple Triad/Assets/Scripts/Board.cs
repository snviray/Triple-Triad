using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Board : MonoBehaviour
{
    public Dictionary<int, Card> cards;
    public Dictionary<int, int[]> edges;
    public  Dictionary<int, int> cardOrder;
    public GameObject boardObject;
    public GameObject boardContent;
    public GameObject cardPrefab;
    public GameObject placeholderPrefab;
    public Dictionary<int, int> posToOrder;
    public int numCards;
    public Board() {
        cards = new Dictionary<int, Card>()
        {
            {0, null}, {1, null}, {2, null},
            {10, null}, {11, null}, {12, null},
            {20, null}, {21, null}, {22, null}
        };
        edges = new Dictionary<int, int[]>()
        {
            {0, new int[] {1, 10}}, {1, new int[]{0,2,11}}, {2, new int[]{1, 12}},
            {10, new int[]{0,11,20}}, {11, new int[]{1, 10, 12, 21}}, {12, new int[]{2, 11, 22}},
            {20, new int[]{10, 21}}, {21, new int[]{20,22,11}}, {22, new int[]{21,12}}
        };
        cardOrder = new Dictionary<int, int>() {
            {0, 0}, {1, 1}, {2, 2},
            {3, 10}, {4, 11}, {5, 12},
            {6, 20}, {7, 21}, {8, 22}
        };
        posToOrder = new Dictionary<int, int>() {
            {0, 0}, {1, 1}, {2, 2},
            {10,3}, {11,4}, {12,5},
            {20, 6}, {21,7}, {22,8}
        };
        numCards = 0;
    }

 
    public bool PlaceCard(int position, Card card) {
        if (this.cards[position] == null) {
            card.position = position;
            ArrayList boardList = GetBoardList(card, position);
            ClearBoard();
            FillBoard(boardList);
            card.PlayCard(position);
            numCards += 1;
            return true;
        }
        return false;
    }

    public ArrayList GetNeighbors(int position) {
        ArrayList cards = new ArrayList();
        int[] n = this.edges[position];
        foreach(int i in n) {
            if (this.cards[i] != null) {
                cards.Add(this.cards[i]);
            }
        }
        return cards;
    }

    public void ClearBoard() { 
        foreach (Transform child in boardContent.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    // makes a board list for a new turn 
    public ArrayList GetBoardList(Card c, int position) {
        ArrayList boardList = new ArrayList();
        for (int i = 0; i < 9; i++) {
            if (i == posToOrder[position]) {
                boardList.Add(c.GetInfo());
            } else {
                GameObject cell = boardContent.transform.GetChild(i).gameObject;
                if (cell.GetComponent<Card>() != null) {
                    boardList.Add(cell.GetComponent<Card>().GetInfo());
                } else {
                    boardList.Add(cardOrder[i]);
                }
            } 
        }
        return boardList;
    }
    public void CreatePlaceholderPrefab(int i) {
        GameObject c = PrefabUtility.InstantiatePrefab(placeholderPrefab as GameObject) as GameObject;
        c.GetComponent<Placeholder>().position = i;
        c.transform.parent = GameObject.Find("Content").transform;
    }

    public GameObject CreateCardPrefab() {
        GameObject c = PrefabUtility.InstantiatePrefab(cardPrefab as GameObject) as GameObject;
        c.transform.parent = GameObject.Find("Content").transform;
        return c;
    }

    public void FillBoard(ArrayList boardList) {
        for (int i = 0; i < 9; i++) {
            if (boardList[i] is int) {
                CreatePlaceholderPrefab( (int) boardList[i]);
            } else {
                ArrayList lst = (ArrayList) boardList[i];
                GameObject card = CreateCardPrefab();
                Card c = card.GetComponent<Card>();
                c.up = (int) lst[0];
                c.down = (int) lst[1];
                c.left = (int) lst[2];
                c.right = (int) lst[3];
                c.position = (int) lst[4];
                c.player = (Player) lst[5];
                c.color = (Color) lst[6];
                c.ChangeColor();
                c.FillCardNumbers();
                cards[c.position] = c;
            }
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
