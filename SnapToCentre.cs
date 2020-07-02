using UnityEngine;
using UnityEngine.UI;
public class SnapToCentre : MonoBehaviour
{
    //Father object of the images. Dont forget to assign.
    public RectTransform container;
    //Place where images gonna snap to
    public RectTransform centre;
    //Private 
    private float[] distance;
    private bool dragging = false;
    private int buttonDistance;
    private int minButtonNum;
    private Button[] buttons;

    //if another image added during workign it gonna stop calculate until adding new image is done
    private bool isChanging = false;

    private void Start(){
        if (!container)
        {
            Debug.LogError("Container is missing");
            return;
        }

        HandleButtonsArray();
        
        buttons = new Button[container.childCount];
        for (int i = 0; i < container.childCount; i++)
        {
            buttons[i] = container.GetChild(i).gameObject.GetComponent<Button>();
        }
        buttonDistance = (int)Mathf.Abs(buttons[1].GetComponent<RectTransform>().anchoredPosition.x - buttons[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    //It get the buttons/images from the panel's children and assign them to buttons array
    public void HandleButtonsArray()
    {
        if (!container)
        {
            Debug.LogError("Container is missing");
            return;
        }

        isChanging = true;
        buttons = new Button[container.childCount];
        
        for (int i = 0; i < container.childCount; i++)
        {
            buttons[i] = container.GetChild(i).gameObject.GetComponent<Button>();
        }

        int buttonLenght = buttons.Length;
        distance = new float[buttonLenght];

        isChanging = false;
    }

    private void Update(){

        if (!container)
            return;
        

        if (isChanging)
            return;

        Calculate();

        if (!dragging)
            LerpToButton(minButtonNum * -buttonDistance);
    }

    //Find closer image to snap.
    private void Calculate()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            distance[i] = Mathf.Abs(centre.transform.position.x - buttons[i].transform.position.x);
        }
        float minDistance = Mathf.Min(distance);

        for (int a = 0; a < buttons.Length; a++)
        {
            if (minDistance == distance[a])
            {
                minButtonNum = a;
            }
        }
    }


    //Put the closer image to center after drag end
    private void LerpToButton(int _position){
        float newX = Mathf.Lerp(container.anchoredPosition.x, _position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, container.anchoredPosition.y);
        container.anchoredPosition = newPosition;
    }

    //Called from Scroll Rect's Even Trigger (Begin drag and End drag)
    public void StartDrag(){
        dragging = true;
    }
    public void EndDrag(){
        dragging = false;
    }

}
