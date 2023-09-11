using UnityEngine;
using UnityEngine.UI;

public class QuantityCellsPanel : MonoBehaviour
{ 
    public byte index;
    [HideInInspector] public RectTransform rectTransform;
    
    public Image image;
    public Text quantityCells;
    
    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
