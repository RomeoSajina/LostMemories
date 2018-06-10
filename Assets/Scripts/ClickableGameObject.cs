using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickableGameObject : MonoBehaviour {

    [Tooltip("Akcija koje će se pozvati kad se klikne button")]
    public Button.ButtonClickedEvent OnClick;// = new Button.ButtonClickedEvent();

    private void OnMouseUpAsButton(){
        OnClick.Invoke();
    }
}
