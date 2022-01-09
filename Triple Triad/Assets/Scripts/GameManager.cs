using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Board gameboard;
    public Card selectedCard;
    public GameObject cardPrefab;

    public System.Random r; 

    public bool player1turn;
    public Player player1;
    public Player player2;
    public GameObject player1Deck;
    public GameObject player2Deck;

    public TextMeshProUGUI player1text;
    public TextMeshProUGUI player2text;

    public static GameManager _instance;
    public bool acted;
    public bool firstTurn;
    public GameObject endPanel;
    public TextMeshProUGUI winnerText;

    public void SetTurnText() {
        if (player1turn) {
            player2text.color = Color.clear;
            player1text.color = Color.black;
        } else {
            player2text.color = Color.black;
            player1text.color = Color.clear;
        }
    }

    public void SetWinnerText() {
        if (player1.score > player2.score) {
            winnerText.text = "Player 1 Wins!";
        } else {
            winnerText.text = "Player 2 Wins!";
        }
    }
    public static GameManager Instance 
    {
        get 
        {
            if (_instance is null) 
            { 
                Debug.LogError("Game Manager is NULL");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        firstTurn = true;
        _instance = this;
        r = new System.Random();
        acted = false;
        player1turn = true;
    }

    public void SetPlayer1Turn() {
        player1turn = true;
        SetTurnText();
        player2Deck.SetActive(false);
        player1Deck.SetActive(true);
    }

    public void SetPlayer2Turn() {
        player1turn = false;
        SetTurnText();
        player2Deck.SetActive(true);
        player1Deck.SetActive(false);
    }
    public Board GetBoard() {
        return gameboard;
    }
    public void SelectCard(Card c) {
        selectedCard = c;
        Debug.Log(selectedCard.up);
    }
    public Card GetSelectedCard() {
        return selectedCard;
    }

    public bool CheckOver() {
        if (GetBoard().numCards == 9) {
            return true;
        }
        return false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckOver()) {
            endPanel.SetActive(true);
            gameboard.gameObject.SetActive(false);
            player1Deck.SetActive(false);
            player2Deck.SetActive(false);
            SetWinnerText();
        }
        bool check = acted == true || firstTurn;
        if (player1turn && check) {
            SetPlayer1Turn();
            acted = false;
        } else if (!player1turn && check) {
            SetPlayer2Turn();
            acted = false;
        }
    }

    // spawn rawndom cards
    public void SpawnCards(Player player) {
        GameObject cardPrefab = CreateCardPrefab(player);
        Card card = cardPrefab.GetComponent<Card>();
        card.up = GameManager._instance.r.Next(1,9);
        card.down = GameManager._instance.r.Next(1,9);
        card.left = GameManager._instance.r.Next(1,9);
        card.right = GameManager._instance.r.Next(1,9);
        card.player = player;
        card.FillCardNumbers();
        card.ChangeColor();
    }

    public GameObject CreateCardPrefab(Player player) {
        GameObject c = PrefabUtility.InstantiatePrefab(GameManager._instance.cardPrefab as GameObject) as GameObject;
        c.transform.parent = GameObject.Find(player.playerName + " Content").transform;
        return c;
    }
    public void DeleteSelectedCard() {
        GameObject.Destroy(selectedCard.gameObject);
    }



}
