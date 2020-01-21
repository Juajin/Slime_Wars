using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public void SelectCharacter(Character character)
    {
        var obj = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (character.isBought & obj.selectedCharacter == null)
        {
            var tempOBJ = character.GetComponent<Image>().color;
            tempOBJ.a = 0.25f;
            character.GetComponent<Image>().color = tempOBJ;
            obj.ChangeCharacter(character);
        }
        else if (!character.isBought)
        {
            BuyCharacter();
        }
        else if (obj.selectedCharacter != null)
        {
            UnSelectCharacter(obj.selectedCharacter);
            obj.selectedCharacter = null;
            SelectCharacter(character);
        }
    }
    public void UnSelectCharacter(Character character)
    {
        var tempOBJ = character.GetComponent<Image>().color;
        tempOBJ.a = 0;
        character.GetComponent<Image>().color = tempOBJ;
    }
    public void BuyCharacter()
    {
        Debug.Log("IM HERE");
    }
    public void ContinueWithAd()
    {
        AdController.Instance.PlayRewardedVideoAD();
    }
    public void ContinueWithJewel()
    {
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isGetLifeBefore&&GameObject.FindGameObjectWithTag("Jewel").GetComponent<Jewel>().Spend(1))
        {
            GameManagement.Instance.ContinueGame();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isGetLifeBefore = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
}
