using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public void SelectCharacter(Character character)
    {
        var obj = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (character.isBought & obj.selectedCharacter==null)
        {
            var tempOBJ = character.GetComponent<Image>().color;
            tempOBJ.a = 0.25f;
            character.GetComponent<Image>().color = tempOBJ;
            obj.ChangeCharacter(character);
        }
        else if (!character.isBought)
        {


        }
        else if (obj.selectedCharacter != null) {
            UnSelectCharacter(obj.selectedCharacter);
            obj.selectedCharacter = null;
            SelectCharacter(character);
        }
    }
    public void UnSelectCharacter(Character character){
        var tempOBJ = character.GetComponent<Image>().color;
        tempOBJ.a = 0;
        character.GetComponent<Image>().color = tempOBJ;
    }
    public void BuyCharacter()
    {


    }
}
