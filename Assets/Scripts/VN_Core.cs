using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VN_Core : MonoBehaviour {
    //Public
    public string[] names; // Who speak
    public string[] messages; // What speak
    public Text ui_dialbox;
    public Text ui_namebox;
    public Image Background;
    public float textspeed;
    //Private
    private int step;
    private bool IsTypewriterActive;
    private float yVelocity = 0.0f;

    private void Start()
    {
        textspeed = 0.03f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            if (IsTypewriterActive == false)
                NextStep();
            else
                DoneThisStep();
    }

    private void NextStep()
    {
        //Debug.Log("Current Step: " + step);

        switch (step)
        {
            case 0:
                Say(false);
                Background.GetComponent<Animator>().Play("show");
                break;
            case 1:
                Say(false);
                break;
            case 2:
                Say(true);
                break;
            case 3:
                step = 0;
                Background.GetComponent<Animator>().Play("hide");
                break;
            default:
                break;
        }
    }

    private void DoneThisStep()
    {
        StopAllCoroutines();
        ui_dialbox.text = messages[step];
        step++;
        IsTypewriterActive = false;
    }

    private void Say(bool IsDialogue)
    {
        //StopAllCoroutines();
        StartCoroutine(TypeWriter());

        if (IsDialogue)
            ui_namebox.text = names[step];
        else
            ui_namebox.text = string.Empty;
    }

    IEnumerator TypeWriter()
    {
        IsTypewriterActive = true;
        Debug.Log("Current Step: " + step);
        for (int i = 0; i <= messages[step].Length; i++)
        {
            ui_dialbox.text = messages[step].Substring(0, i);
            yield return new WaitForSeconds(textspeed);
        }
        IsTypewriterActive = false;
        step++;
    }

}
