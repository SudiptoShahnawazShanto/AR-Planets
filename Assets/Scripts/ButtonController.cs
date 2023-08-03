using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    public GameObject definedButton;
    public UnityEvent OnClick = new UnityEvent();
    public Color activeColor;
    public Color passiveColor;

    private bool buttonActive = false;

    private void Start()
    {
        definedButton = this.gameObject;
    }

    private void ChangeButtonColor(Color color)
    {
        var meshRenderer = definedButton.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.material.color = color;
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                    {
                        buttonActive = true;
                        ChangeButtonColor(activeColor);
                    }
                    else
                    {
                        buttonActive = false;
                        ChangeButtonColor(passiveColor);
                    }
                    break;

                case TouchPhase.Ended:
                    if (buttonActive && Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                    {
                        OnClick.Invoke();
                    }
                    buttonActive = false;
                    ChangeButtonColor(passiveColor);
                    break;
            }
        }
    }
}