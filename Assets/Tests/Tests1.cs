using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

public class Tests1: StandaloneInputModule
{
    public void ClickAt(Vector2 pos, bool pressed)
    {
        Input.simulateMouseWithTouches = true;
        var pointerData = GetTouchPointerEventData(new Touch()
        {
            position = pos,
        }, out bool b, out bool bb);

        ProcessTouchPress(pointerData, pressed, !pressed);
    }
    Mouse mouse;
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("StartScreen");
    }
    
    public void clickSomething(GameObject thingToClick)
    {
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Debug.Log("Camera: " + camera.name);
        Vector3 screenPos = camera.WorldToScreenPoint(thingToClick.transform.position);
        Debug.Log("Screen pos: " + screenPos.ToString());
        Vector2 clickPos = new Vector2(screenPos.x, screenPos.y);
        ClickAt(clickPos, true);
    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    /*[UnityTest]
    public IEnumerator TestStartButtonClick()
    {
        GameObject playButton = GameObject.Find("Canvas/StartButton");
        string sceneName = SceneManager.GetActiveScene().name;
        Assert.That(sceneName, Is.EqualTo("StartScreen"));

        clickSomething(playButton);
        yield return new WaitForSeconds(5f);

        sceneName = SceneManager.GetActiveScene().name;
        Assert.That(sceneName, Is.EqualTo("Level"));
    }*/

    [UnityTest]
    public IEnumerator SampleTest()
    {
        yield return new WaitForSeconds(5f);
        Assert.That(1, Is.EqualTo(1));
    }
}
