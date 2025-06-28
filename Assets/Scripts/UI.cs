using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
   [SerializeField] private Slider  _thrusterSlider;
    PlayerMovement player;
    public float initValue;

    public void UpdateInputBar (float initValue){
        if (initValue < 0) {
            Debug.Log("No Voice Input");
            _thrusterSlider.value = 0;
        } else 

        _thrusterSlider.value = initValue;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = PlayerMovement.player;
        initValue = player.loudness;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (initValue>1){
            
            _thrusterSlider.value = initValue/100.0f;
        }

        if (Input.GetKeyDown("space"))
        {
            _thrusterSlider.value +=10;
            Debug.Log("Space Pressed");
        }
    }
}
