using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Card : MonoBehaviour
{
    public GameObject cardUI;
    public int up;
    public int down;
    public int left;
    public int right; 
    public bool played;
    public Color color; 
    public Player player; 
    public int position; 
    public TextMeshProUGUI upText;
    public TextMeshProUGUI downText;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    public Card(Player player) {
        this.played = false;
        this.player = player;
        GameManager._instance.SpawnCards(player);
    }

    // played card constructor
    public Card(Player player, int up, int down, int left, int right, int position) {
        this.player = player;
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
        this.position = position;
        this.played = true;
    }
    public void ChangeColor() {
        cardUI.GetComponent<Image>().color = player.color;
    }
    public ArrayList GetInfo() {
        ArrayList infoList = new ArrayList();
        infoList.Add(up);
        infoList.Add(down);
        infoList.Add(left);
        infoList.Add(right);
        infoList.Add(position);
        infoList.Add(player);
        infoList.Add(color);
        return infoList;
    }

    public void PlayCard(int position) {
        this.color = player.color;
        this.position = position;
        this.played = true;
        player.score += 1;

        FlipNeighbors();
       
    }

    // returns the card that wins the fight when you play a card
    public Card Fight(Card neighbor) {
        Debug.Log("fought");
        if (neighbor.player == this.player) {
            return null;
        }
        if (this.position % 10 == neighbor.position % 10) {
            if (this.position > neighbor.position) {
                if (this.up > neighbor.down) {
                    return this;
                } else {
                    return neighbor;
                }
            } else {
                if (this.down > neighbor.up) {
                    return this;
                } else {
                    return neighbor;
                }
            }
        } else {
            if (this.position > neighbor.position) {
                if (this.left > neighbor.right) {
                    return this;
                }
                else {
                    return neighbor;
                } 
            } else {
                if (this.right > neighbor.left) {
                    return this;
                } else {
                    return neighbor;
                }
            }
        }
    } 

    // flips all neighbors with fight as a subroutine
    public void FlipNeighbors() {
        foreach (Card n in GameManager._instance.GetBoard().GetNeighbors(position)) {
            Debug.Log(this.Fight(n) == this);
            if (this.Fight(n) == this) {
                n.color = this.player.color;
                n.player.score -= 1;
                this.player.score += 1; 
                n.player = this.player;
                n.ChangeColor();
            }
        }
    }

    // fills in the card numbers
    public void FillCardNumbers() {
        upText.text = up.ToString();
        downText.text = down.ToString();
        rightText.text = right.ToString();
        leftText.text = left.ToString();
    }

    public void SelectCard() {
        GameManager._instance.SelectCard(this);
    }


}
