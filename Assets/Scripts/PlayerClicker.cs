using System.Diagnostics;
using UnityEngine;

public class PlayerClicker : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                DialogManager dialogManager = hit.collider.GetComponent<DialogManager>();

                if (dialogManager != null)
                    // empieza el dialogo con esta linea
                    dialogManager.TriggerDialogue(this.gameObject);
            }
        }
    }
}
