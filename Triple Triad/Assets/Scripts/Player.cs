using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public Color color;
    public int score;
    public List<Card> deck;
    [SerializeField] public string playerName;
    public Player() {
        score = 0;
        deck = new List<Card>();
    }
    public void GenerateDeck() {
        for(int i = 0; i < 9; i++) {
            Card newCard = new Card(this);
            this.deck.Add(newCard);
        }
    }

    public void SetColor() {
        if (string.Compare("Player1", playerName) == 0) {
            color = new Color32(255,0,0,255);
        } else {
            color = new Color32(0,0,255,255);
        }
        color.a = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetColor();
        GenerateDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
