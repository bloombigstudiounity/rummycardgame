using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    private Image img;
    private int childIndex;
    public string CardName;
    public string Carddec;
    public int CardNum;
    public int ChildIndex { get => childIndex; set => childIndex = value; }

    private void Awake()
    {
        img = GetComponent<Image>();
    }
    private void Start()
    {
        Debug.Log(this.gameObject.name);
        CardName = this.gameObject.name;
        string[] Splitarray = CardName.Split( char.Parse("_"));
        Carddec = Splitarray[0];
        CardNum = int.Parse( Splitarray[1]);
        Debug.Log(Carddec);
        Debug.Log(CardNum);
    }
    public void SetCardImg(Sprite sprite)
    {
        img.sprite = sprite;
    }

    public void MoveCard()
    {
        
    }

}
