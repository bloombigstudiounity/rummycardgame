using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardManager : MonoBehaviour
{
    //Instance so we can access it from all scripts
    public static CardManager instance;
    [SerializeField] private List<Sprite> cardImages;   //ref to card sprites
    public int Length = 9;
    //important gameobjects
    [SerializeField] private GameObject cardHolder, cardPrefab, parentHolder, dummyCardPrefab;
    public Image carddroppoint;
    public CardView selectedCard, previousCard, nextCard;  //ref to cards
    int k, childCount;                                      
    private GameObject dummyCardObj;
    private bool cardbool;
    public CardView SelectedCard { get => selectedCard; }
    public GameObject ParentHolder { get => parentHolder; }
    public static int count = 0;
    //public List<Text> cardnames;
    public Text ScoreText;
    public   int score ;
    public int instCardNumber= 0;
    public GameObject cardObj;
    public  int scorecount = 0;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        ScoreText.text = "Score :" + score.ToString();
        cardbool = false;
        for (int i = 0; i < Length; i++)
        {
            count++;
            k = i;
            SpawnCard();
        }
        Invoke("CardComparison", 1);
       // Invoke("CardSequenceComparison", 1);
        
        //CardComparison();
    }
   

    /// <summary>
    /// Method called when we tap on any card
    /// </summary>
    /// <param name="card">Reference of tapped card</param>
    public void SelectCard(CardView card)
    {//save the selected card SiblingIndex [Child Index for its parent]
        int selectedIndex = card.transform.GetSiblingIndex() - 1;
      
        selectedCard = card;                        //set selectedCard to card
        selectedCard.ChildIndex = selectedIndex;    //set the selectedCard ChildIndex
       GetDummyCard().SetActive(true);             //activate dummy card
      GetDummyCard().transform.SetSiblingIndex(selectedIndex);    //set dummy card index
                                               //change the parent of selectedCard
        selectedCard.transform.SetParent(CardManager.instance.ParentHolder.transform);

        childCount = cardHolder.transform.childCount;

        //check if selectedIndex is less than total childCount
        if (selectedIndex + 1 < childCount)
        {
            //set the next card of the selected card
            nextCard = cardHolder.transform.GetChild(selectedIndex + 1).GetComponent<CardView>();
        }

        if (selectedIndex - 1 >= 0)
        {
            //set the previous card of selected card
            previousCard = cardHolder.transform.GetChild(selectedIndex - 1).GetComponent<CardView>();
        }

        
    }

    /// <summary>
    /// Method called on release of tap
    /// </summary>
    
    public void OnCardRelease()
    {
        //if SelectedCard is not null
        if (SelectedCard != null)
        {
            GetDummyCard().SetActive(false);        //Deactivate Dummy card
                                        //set selectedCard parent

            //set selectedCard SetSiblingIndex

            if (Mathf.Abs(selectedCard.transform.position.y - GetDummyCard().transform.position.y) >80)
            {
               if (cardHolder.transform.childCount == 10 )
                {
                    cardbool = true;
                    selectedCard.transform.SetParent(carddroppoint.transform);
                    selectedCard.GetComponent<CardView>().enabled = false;
                   selectedCard.transform.position = carddroppoint.transform.position;
                    selectedCard.tag = "catchcard";
                    GetDummyCard().transform.SetParent(CardManager.instance.ParentHolder.transform);
                    // count--;
                }
               else
                {
                    if (cardHolder.transform.childCount <= 10)
                    {
                        //  if ()
                        selectedCard.tag = "playingCard";
                        cardbool = false;
                    selectedCard.transform.SetParent(cardHolder.transform);
                    GetDummyCard().transform.SetParent(CardManager.instance.ParentHolder.transform);
                   selectedCard.transform.SetSiblingIndex(selectedCard.ChildIndex);
                    GetDummyCard().transform.SetParent(CardManager.instance.ParentHolder.transform);
                     }
                }
            }
            
            else
            {
                if (cardHolder.transform.childCount <= 10)
                {
                    selectedCard.tag = "playingCard";
                    selectedCard.transform.SetParent(cardHolder.transform);
              selectedCard.transform.SetSiblingIndex(GetDummyCard().transform.GetSiblingIndex());
                GetDummyCard().transform.SetParent(CardManager.instance.ParentHolder.transform);
                }
            }

            selectedCard = null;    //make selectedCard null

        }
    }
    private void Update()
    {
        //  Debug.Log(cardHolder.transform.childCount);
        if (selectedCard.tag == "catchcard" && cardHolder.transform.childCount > 10)
        {
            GetDummyCard().SetActive(false);
            selectedCard.transform.position = carddroppoint.transform.position;
        }
        
    }
    /// <summary>
    /// Method called on drag of card
    /// </summary>
    /// <param name="position"></param>
    public void MoveSelectedCard(Vector2 position)
    {
        if (selectedCard != null)                           //if SelectedCard is not null
        {
            selectedCard.transform.position = position;     //set selectedCard position
           
            CheckWithNextCard();                            //check for next card
            CheckWithPreviousCard();                        //check for previous card
                                                            //selectedCard.MoveCard();


        }
    }

    void CheckWithNextCard()
    {
        if (nextCard != null)
        {
            if (selectedCard.transform.position.x > nextCard.transform.position.x)
            {
                int index = nextCard.transform.GetSiblingIndex();
                nextCard.transform.SetSiblingIndex(dummyCardObj.transform.GetSiblingIndex());
                dummyCardObj.transform.SetSiblingIndex(index);

                previousCard = nextCard;
                if (index + 1 < childCount)
                {
                    nextCard = cardHolder.transform.GetChild(index + 1).GetComponent<CardView>();
                }
                else
                {
                    nextCard = null;
                }
            }
        }
    }
    
    void CheckWithPreviousCard()
    {
        if (previousCard != null)
        {
            if (selectedCard.transform.position.x < previousCard.transform.position.x)
            {
                int index = previousCard.transform.GetSiblingIndex();
                previousCard.transform.SetSiblingIndex(dummyCardObj.transform.GetSiblingIndex());
                dummyCardObj.transform.SetSiblingIndex(index);

                nextCard = previousCard;
                if (index - 1 >= 0)
                {
                    previousCard = cardHolder.transform.GetChild(index - 1).GetComponent<CardView>();
                }
                else
                {
                    previousCard = null;
                }
            }
        }
    }
   
    bool once = false;
    bool once2 = false;
    bool once3 = false;
    bool once4 = false;

    public void CardComparison()
    {
        //score = 0;
        once4 = false;
        once3 = false;
        once2 = false;
        once = false;
        for (int i = 0; i <= cardHolder.transform.childCount; i++)
        {
           if (!once2) {
                score = 0;
               
                once2 = true;
            }
            
            if (!once)
            {
                score = 0;

                once = true;
            }
            if (!once4)
            {
                //scorecount -= 10;
                score = scorecount;

                once4 = true;
            }

            //Debug.Log(cardHolder.transform.GetChild(i).GetComponent<CardView>().CardNum);
            if (cardHolder.transform.GetChild(i).GetComponent<CardView>().CardNum == cardHolder.transform.GetChild(i + 1).GetComponent<CardView>().CardNum && cardHolder.transform.GetChild(i + 1).GetComponent<CardView>().CardNum == cardHolder.transform.GetChild(i + 2).GetComponent<CardView>().CardNum)
            {
                //scorecount -= 10;
                score += 10;
               /// score = scorecount;
               
               //score += scorecount;

                ScoreText.text = "Score :" + score.ToString();
               
                if (cardHolder.transform.GetChild(i + 2).GetComponent<CardView>().CardNum == cardHolder.transform.GetChild(i + 3).GetComponent<CardView>().CardNum)
                {
                    score -= 10;
                    ScoreText.text = "Score :" + score.ToString();
                    Debug.Log("Match found");
                    Debug.Log(cardHolder.transform.GetChild(i));
                }
                else
                {

                    // score -= 10;
                    ScoreText.text = "Score :" + score.ToString();
                }

            }
            else  if (cardHolder.transform.GetChild(i).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 1).GetComponent<CardView>().Carddec && cardHolder.transform.GetChild(i + 1).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 2).GetComponent<CardView>().Carddec)
            { 
                 if (cardHolder.transform.GetChild(i).GetComponent<CardView>().CardNum == cardHolder.transform.GetChild(i + 1).GetComponent<CardView>().CardNum - 1 && cardHolder.transform.GetChild(i +1).GetComponent<CardView>().CardNum - 1 == cardHolder.transform.GetChild(i + 2).GetComponent<CardView>().CardNum - 2)
                 {
                    score += 10;
                   // score = scorecount;
                   
                    ScoreText.text = "Score :" + score.ToString();
                    Debug.Log("card sequence found");
                    if (cardHolder.transform.GetChild(i + 2).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 3).GetComponent<CardView>().Carddec &&
                           ( cardHolder.transform.GetChild(i + 3).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 4).GetComponent<CardView>().Carddec) &&
                           cardHolder.transform.GetChild(i + 4).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 5).GetComponent<CardView>().Carddec &&
                           cardHolder.transform.GetChild(i + 5).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 6).GetComponent<CardView>().Carddec &&
                           cardHolder.transform.GetChild(i + 6).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 7).GetComponent<CardView>().Carddec &&
                           cardHolder.transform.GetChild(i + 7).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 8).GetComponent<CardView>().Carddec &&
                           cardHolder.transform.GetChild(i + 8).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 9).GetComponent<CardView>().Carddec)
                    {
                        if(cardHolder.transform.GetChild(i + 2).GetComponent<CardView>().CardNum - 2 == cardHolder.transform.GetChild(i + 3).GetComponent<CardView>().CardNum - 3 &&
                            cardHolder.transform.GetChild(i + 3).GetComponent<CardView>().CardNum - 3 == cardHolder.transform.GetChild(i + 4).GetComponent<CardView>().CardNum - 4 &&
                            cardHolder.transform.GetChild(i + 4).GetComponent<CardView>().CardNum - 4 == cardHolder.transform.GetChild(i + 5).GetComponent<CardView>().CardNum - 5 &&
                            cardHolder.transform.GetChild(i + 5).GetComponent<CardView>().CardNum - 5 == cardHolder.transform.GetChild(i + 6).GetComponent<CardView>().CardNum - 6 &&
                            cardHolder.transform.GetChild(i + 6).GetComponent<CardView>().CardNum - 6 == cardHolder.transform.GetChild(i + 7).GetComponent<CardView>().CardNum - 7 &&
                            cardHolder.transform.GetChild(i + 7).GetComponent<CardView>().CardNum - 7 == cardHolder.transform.GetChild(i + 8).GetComponent<CardView>().CardNum - 8 &&
                            cardHolder.transform.GetChild(i + 8).GetComponent<CardView>().CardNum - 8 == cardHolder.transform.GetChild(i + 9).GetComponent<CardView>().CardNum - 9)
                        {
                            score -= 10;
                            ScoreText.text = "Score :" + score.ToString();
                            Debug.Log(" full card sequence found");
                        }

                    }
                    else
                    {
                       
                        //  score -= 10;
                        ScoreText.text = "Score :" + score.ToString();
                    }
                    
                 }
            }
            else
            {
                

                ScoreText.text = "Score :" + score.ToString();
            }
            
        }
      
    }
 /* public void CardSequenceComparison()
    {
        once2 = false;
        for (int i = 0; i <= cardHolder.transform.childCount; i++)
        {
            if (!once2) { score = 0; once2 = true; }
            if (cardHolder.transform.GetChild(i).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 1).GetComponent<CardView>().Carddec && cardHolder.transform.GetChild(i + 1).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 2).GetComponent<CardView>().Carddec)
            {
                if (cardHolder.transform.GetChild(i).GetComponent<CardView>().CardNum == cardHolder.transform.GetChild(i + 1).GetComponent<CardView>().CardNum - 1 && cardHolder.transform.GetChild(i + 1).GetComponent<CardView>().CardNum - 1 == cardHolder.transform.GetChild(i + 2).GetComponent<CardView>().CardNum - 2)
                {

                    // score = 0;
                    score += 10;
                    ScoreText.text = "Score :" + score.ToString();
                    Debug.Log("card sequence found");
                    if (cardHolder.transform.GetChild(i + 2).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 3).GetComponent<CardView>().Carddec ||
                           (cardHolder.transform.GetChild(i + 3).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 4).GetComponent<CardView>().Carddec) ||
                           cardHolder.transform.GetChild(i + 4).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 5).GetComponent<CardView>().Carddec ||
                           cardHolder.transform.GetChild(i + 5).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 6).GetComponent<CardView>().Carddec ||
                           cardHolder.transform.GetChild(i + 6).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 7).GetComponent<CardView>().Carddec ||
                           cardHolder.transform.GetChild(i + 7).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 8).GetComponent<CardView>().Carddec ||
                           cardHolder.transform.GetChild(i + 8).GetComponent<CardView>().Carddec == cardHolder.transform.GetChild(i + 9).GetComponent<CardView>().Carddec)
                    {
                        if (cardHolder.transform.GetChild(i + 2).GetComponent<CardView>().CardNum - 2 == cardHolder.transform.GetChild(i + 3).GetComponent<CardView>().CardNum - 3 ||
                            cardHolder.transform.GetChild(i + 3).GetComponent<CardView>().CardNum - 3 == cardHolder.transform.GetChild(i + 4).GetComponent<CardView>().CardNum - 4 ||
                            cardHolder.transform.GetChild(i + 4).GetComponent<CardView>().CardNum - 4 == cardHolder.transform.GetChild(i + 5).GetComponent<CardView>().CardNum - 5 ||
                            cardHolder.transform.GetChild(i + 5).GetComponent<CardView>().CardNum - 5 == cardHolder.transform.GetChild(i + 6).GetComponent<CardView>().CardNum - 6 ||
                            cardHolder.transform.GetChild(i + 6).GetComponent<CardView>().CardNum - 6 == cardHolder.transform.GetChild(i + 7).GetComponent<CardView>().CardNum - 7 ||
                            cardHolder.transform.GetChild(i + 7).GetComponent<CardView>().CardNum - 7 == cardHolder.transform.GetChild(i + 8).GetComponent<CardView>().CardNum - 8 ||
                            cardHolder.transform.GetChild(i + 8).GetComponent<CardView>().CardNum - 8 == cardHolder.transform.GetChild(i + 9).GetComponent<CardView>().CardNum - 9)
                        {
                            score -= 10;
                            ScoreText.text = "Score :" + score.ToString();
                            Debug.Log(" full card sequence found");
                        }

                    }
                    else
                    {

                        //  score -= 10;
                        ScoreText.text = "Score :" + score.ToString();
                    }

                }
            }
            else
            {

                //score -= 10;
                ScoreText.text = "Score :" + score.ToString();
            }
        }
    }*/
    public void CardButton()
    {
        // if (count == 9)
         
            if (cardHolder.transform.childCount == 9)
           {
              SpawnCard();
            Invoke("GettingInstCardNum", 0.2f);
            
            // k++;
            count++;
             
             }
     }
    public void GettingInstCardNum()
    {
        instCardNumber = cardObj.GetComponent<CardView>().CardNum;
        score = score - instCardNumber;
        scorecount = score;
        Debug.Log(instCardNumber);
        ScoreText.text = "Score :" + score.ToString();
    }
    #region Spawn Logic
    void SpawnCard()
    {
        GameObject card = Instantiate(cardPrefab);
        int i = Random.Range(0, 53);
        // card.name = "Card " + k;
        cardObj = card;
        card.name = cardImages[i].name;
        card.tag = "playingCard";
        card.transform.SetParent(cardHolder.transform);
        card.GetComponent<CardView>().SetCardImg(cardImages[i]);
       
       // cardnames[i].text = cardImages[i].name;
      // Debug.Log(cardnames[i]);

    }
    
    GameObject GetDummyCard()
    {
        if (dummyCardObj == null)
        {
            dummyCardObj = Instantiate(dummyCardPrefab);
            dummyCardObj.name = "DummyCard";
            dummyCardObj.transform.SetParent(cardHolder.transform);
        }
        else
        {
            if (dummyCardObj.transform.parent != cardHolder.transform)
            {
                dummyCardObj.transform.SetParent(cardHolder.transform);
            }
            return dummyCardObj;
        }

        return dummyCardObj;
    }
    #endregion
}
