using UnityEngine;

public class InputController : MonoBehaviour
{
    public SnakeController snakeController;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        snakeController.SetTurnDirection(horizontal);

        if (Input.GetKeyDown(KeyCode.O))
        {
           // snakeController.AddSegment();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //snakeController.RemoveSegment();
        }
    }
}
