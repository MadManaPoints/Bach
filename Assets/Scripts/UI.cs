using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
   [SerializeField] private Slider  _thrusterSlider;
   [SerializeField] private Slider _sensitivitySlider;
    PlayerMovement player;
    public float initValue, sensitivityBar;

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

        sensitivityBar = player.loudnessSensibility;
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

        if (_sensitivitySlider.value>=sensitivityBar){
            Debug.Log (sensitivityBar);
            _sensitivitySlider.value = sensitivityBar;
        }
        if (_sensitivitySlider.value<sensitivityBar){
            sensitivityBar=_sensitivitySlider.value;

        }
            _sensitivitySlider.value = sensitivityBar;
        
    }
}
