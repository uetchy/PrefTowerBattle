using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;

public class GameManager : MonoBehaviour
{

    private bool isObjectMoving = false;
    private GameObject movingObject;
    private int numberOfObjects = 0;
    private float spawnOffset = 3.0f;
    public Camera mainCamera;
    public GameObject rotateButton;
    Object[] prefabs;

    void OnEnable()
    {
        Debug.Log("Enabled");
        enableTouchEvent();
    }

    void OnDisable()
    {
        disableTouchEvent();
    }

    void Start()
    {
        Debug.Log("Start");
        prefabs = Resources.LoadAll("Prefectures");
    }

    void enableTouchEvent()
    {
        GetComponent<PressGesture>().Pressed += pressedHandler;
        GetComponent<ScreenTransformGesture>().Transformed += dragTransformHandler;
        GetComponent<ScreenTransformGesture>().TransformCompleted += dragEndedHandler;
    }

    void disableTouchEvent()
    {
        GetComponent<PressGesture>().Pressed -= pressedHandler;
        GetComponent<ScreenTransformGesture>().Transformed -= dragTransformHandler;
        GetComponent<ScreenTransformGesture>().TransformCompleted -= dragEndedHandler;
    }

    // Tapped event
    private void pressedHandler(object sender, System.EventArgs e)
    {
        Debug.Log("Pressed");
        if (isObjectMoving)
        {
            return;
        }

        // Get tapped position
        var gesture = sender as PressGesture;

        // Randomly sample a prefecture from the list
        GameObject prefab = (GameObject)prefabs[Random.Range(0, prefabs.Length)];

        // Create new prefecture on top of screen
        movingObject = Instantiate(prefab, new Vector2(mainCamera.ScreenToWorldPoint(gesture.ScreenPosition).x, spawnOffset), Quaternion.identity);
        movingObject.GetComponent<Rigidbody2D>().isKinematic = true;
        isObjectMoving = true;
    }

    // Drag event
    private void dragTransformHandler(object sender, System.EventArgs e)
    {
        if (!isObjectMoving)
        {
            return;
        }

        Debug.Log("Drag");
        var gesture = sender as ScreenTransformGesture;
        Debug.Log(gesture.ScreenPosition.x);

        Vector2 offset = new Vector2(mainCamera.ScreenToWorldPoint(gesture.ScreenPosition).x, spawnOffset);
        movingObject.transform.position = offset;
    }

    void dragEndedHandler(object sender, System.EventArgs e)
    {
        Debug.Log("Drag Ended");
        var gesture = sender as ScreenTransformGesture;
        Vector2 offset = new Vector2(mainCamera.ScreenToWorldPoint(gesture.ScreenPosition).x, spawnOffset);
        movingObject.transform.position = offset;
        movingObject.GetComponent<Rigidbody2D>().isKinematic = false;
        movingObject = null;
        numberOfObjects += 1;
        isObjectMoving = false;
    }

    public void RotateObject()
    {
        movingObject.transform.Rotate(0, 0, -30);
    }
}
