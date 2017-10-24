using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VN_Core : MonoBehaviour {
    //Public
    public string[] names; // Who speak
    public string[] messages; // What speak
    public Text ui_dialbox; // Dialogue 
    public Text ui_namebox; // Name 
    public Image ui_background;
    public Sprite[] Backgrounds;
    public float textspeed; // Typewriter speed text
    //Private
    private int step;
    private bool IsTypewriterActive;

    enum bg_type { clouds_1, street_1 };

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
                DoneThisStep(); // Show all text
    }

    private void NextStep()
    {
        //Debug.Log("Current Step: " + step);

        switch (step)
        {
            case 0:
                ChangeBG(bg_type.clouds_1);
                PlayMusic();
                ui_background.GetComponent<Animator>().Play("show");
                Say(false);
                break;
            case 2:
                ui_background.GetComponent<Animator>().Play("hide");
                Say(false);
                break;
            case 3:
                ChangeBG(bg_type.street_1);
                Say(false);
                break;
            case 9:
                step = 0;
                ui_background.GetComponent<Animator>().Play("hide");
                break;
            default:
                Say(false);
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


    private void ChangeBG(bg_type bg)
    {
        switch(bg)
        {
            case bg_type.street_1:
                ui_background.sprite = Backgrounds[1];
                break;
            case bg_type.clouds_1:
                ui_background.sprite = Backgrounds[0];
                break;
        }
        ui_background.GetComponent<Animator>().Play("show");
    }

    private void PlayMusic()
    {
        gameObject.GetComponent<AudioSource>().Play();
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
