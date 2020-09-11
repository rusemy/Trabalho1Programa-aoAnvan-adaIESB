using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static InputManager instance = null;
    public static InputManager Instance {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();
                if(instance == null)
                {
                    GameObject inputManagerGameObject = new GameObject("InputManager", typeof(InputManager));
                    instance = inputManagerGameObject.GetComponent<InputManager>();
                    DontDestroyOnLoad(inputManagerGameObject);
                }
            }
            return instance;
        }
    }

    private void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static KeyCode moveFoward = KeyCode.W;
    public static KeyCode moveBack = KeyCode.S;
    public static KeyCode rotateLeft = KeyCode.D;
    public static KeyCode RotateRight = KeyCode.A;


}
