using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeholder : MonoBehaviour
{
    public int position;
    public static int selectedPlaceholder;
    // Start is called before the first frame update
    
    public void PlaceCard() {
        Card c = GameManager._instance.GetSelectedCard();
        Board b = GameManager._instance.GetBoard();
        Debug.Log(position);
        b.PlaceCard(position, c);
        GameManager._instance.DeleteSelectedCard();
        GameManager._instance.acted = true;
        GameManager._instance.player1turn = !GameManager._instance.player1turn;
    }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
