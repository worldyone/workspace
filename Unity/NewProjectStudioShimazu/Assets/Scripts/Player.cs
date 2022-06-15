using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text HPText;
    public Slider HPSlider;

    // Start is called before the first frame update
    void Start()
    {
        HPText.text = "50";
        HPSlider.value = 50;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1f, 0f, 0f);
    }
}
