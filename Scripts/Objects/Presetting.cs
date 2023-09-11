using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Presetting : MonoBehaviour
{
    private CharactersManagement charactersManagement;
    private GridField gridField; 
    private List<Image> paletteColors;

    private void Start()
    {
        
    }

    private void Initialization()
    {
        
    }

    public void SetPaletteColor(Image _image)
    {
        paletteColors.Add(_image);
        charactersManagement.characters[0].color = _image.color;
    }
    
}
