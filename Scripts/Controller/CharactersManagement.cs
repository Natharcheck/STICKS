using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharactersManagement : MonoBehaviour
{
    [SerializeField] private byte qauntityCharacters;
    [SerializeField] private byte qauntityCells;
    [Space]
    [SerializeField] public  List<Character> characters;
    [SerializeField] private Character prefabCharacter;
                     private Character currentCharacter;
    [Space] 
    [SerializeField] private RectTransform selectQuantityPanel;
    [Space]
    [SerializeField] private PhysicsRaycaster raycaster;
                     private List<RaycastResult> resultsRaycast;
                     
                     private List<EventTrigger> eventTriggers;
                     private GridField gridField;
    private void Start()
    {
        Initialization();
    }
    
    private void Initialization()
    {
        characters = new List<Character>(qauntityCharacters);
        gridField = FindObjectOfType<GridField>();
    }
    
    private void OnPointerClick(PointerEventData eventData)
    {
        resultsRaycast = new List<RaycastResult>(0);
        raycaster.Raycast(eventData, resultsRaycast);

        GetOnClickStick();
    }
    private void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void Create() //rename
    {
        for (byte i = 0; i < qauntityCharacters; i++)
            characters.Add(Instantiate(prefabCharacter));
        currentCharacter = characters[0];

        ListUpdateCharacterIndex();

        GetEventTriggersPointerClick();
        GetEventTriggersPointerDown();
    }
    private void GetOnClickStick()
    {
        Stick stick = resultsRaycast[0].gameObject.GetComponent<Stick>();
        Cell cell = stick.GetComponentInParent<Cell>();

        gridField.SetExaminationPositionStick(stick, cell);
        stick.SetEnable(currentCharacter.color);
        
        currentCharacter.quantityMovements = gridField.tempQuantityMovements;

        if (currentCharacter.quantityMovements == 1)
        {
            gridField.tempQuantityCells = 0;
            GetNextCurrentCharacter();
        }
        else
        {
            currentCharacter.quantityCells += gridField.tempQuantityCells;
            currentCharacter.QuantityCellsUpdateText();
        }
    }
    private void GetNextCurrentCharacter()
    {
        if (currentCharacter.index + 1 < characters.Count)
        {
            currentCharacter = characters[currentCharacter.index + 1];
        }
        else if (currentCharacter.index + 1 >= characters.Count)
                currentCharacter = characters[0];
        
        selectQuantityPanel.position = currentCharacter.quantityCellsPanel.rectTransform.position;
    }

    private void ListUpdateCharacterIndex()
    {
        for (byte i = 0; i < characters.Count; i++)
            characters[i].index += i;
    }
    
    public void SetQuantityCharacters(Slider _slider)
    {
        qauntityCharacters = (byte)_slider.value;
    }
    
    private void GetEventTriggersPointerClick()
    {
        eventTriggers = new List<EventTrigger>(gridField.STICKS_COUNT);
        gridField.GetEventStick(eventTriggers);
        
        for (int i = 0; i < gridField.STICKS_COUNT; i++)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
        
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => 
                { OnPointerClick((PointerEventData)data); });
            
            eventTriggers[i].triggers.Add(entry);
        }
    }
    
    private void GetEventTriggersPointerDown()
    {
        for (int i = 0; i < gridField.STICKS_COUNT; i++)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
        
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) => 
                { OnPointerDown((PointerEventData)data); });
            
            eventTriggers[i].triggers.Add(entry);
        }
    }

    public void SetLoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}