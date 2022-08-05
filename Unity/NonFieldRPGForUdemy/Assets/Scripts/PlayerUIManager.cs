using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Text hpText;
    public Text atText;

    public void UpdateUI(PlayerManager player)
    {
        hpText.text = string.Format("HP：{0}", player.hp);
    }
}
