using UnityEngine;

public class Character : MonoBehaviour
{
    public byte index;
    public byte quantityMovements;
    public byte quantityCells;
    [Space] 
    public Color32 color;
    [Space] 
    [SerializeField] private QuantityCellsPanel quantityCellsPanelPrefab;
                     private RectTransform quantityCellsPanelParent;
                     
    public QuantityCellsPanel quantityCellsPanel;
    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        color = new Color32(  
            (byte) Random.Range(0,255),  
            (byte) Random.Range(0,255),  
            (byte) Random.Range(0,255),  
            (byte) 255);

        quantityCellsPanelParent = 
            GameObject.Find("Characters Cells Panels").GetComponent<RectTransform>();
        
        quantityCellsPanel = Instantiate(quantityCellsPanelPrefab);
        quantityCellsPanel.transform.parent = quantityCellsPanelParent;

        quantityCellsPanel.image.color = color;
    }

    public void QuantityCellsUpdateText()
    {
        quantityCellsPanel.quantityCells.text = "" + quantityCells;
    }
}