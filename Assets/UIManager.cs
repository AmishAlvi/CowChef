using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] VerticalLayoutGroup TicksHolder, OrderIconsHolder, OrderTextHolder;
    [SerializeField] GameObject IconPrefab, TickPrefab, OrderTextPrefab;
    private List<Food> ingredients;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateUI(List<Food> ingredients)
    {
        foreach(Food ingredient in ingredients)
        {
            GameObject Tick = Instantiate(TickPrefab, TicksHolder.transform);

            GameObject Icon = Instantiate(IconPrefab, OrderIconsHolder.transform);
            Icon.GetComponent<Image>().sprite = ingredient.GetComponent<SpriteRenderer>().sprite;

            GameObject OrderText = Instantiate(OrderTextPrefab, OrderTextHolder.transform);
            OrderText.GetComponent<Text>().text = ingredient.GetName();

            ingredient.SetUI(OrderText.GetComponent<Text>(), Icon.GetComponent<Image>(), Tick.GetComponent<Image>());

            Tick.SetActive(true);
            Icon.SetActive(true);
            OrderText.SetActive(true);

        }
    }
}
