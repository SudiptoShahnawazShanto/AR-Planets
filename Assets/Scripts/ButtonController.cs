using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    public GameObject definedButton;
    public UnityEvent OnClick = new UnityEvent();
    public Color color;

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
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out Hit) && Hit.collider.gameObject == gameObject)
            {
                ChangeButtonColor(color);
                OnClick.Invoke();
            }
        }
    }
}
