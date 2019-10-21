using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public Animator anim;
    private Queue<string> sentence;
    // Start is called before the first frame update
    void Start()
    {
        sentence = new Queue<string>();
    }


    public void StartDialog(Dialog dialog)
    {
        anim.SetBool("isOpen", true);
        nameText.text = dialog.name;
        sentence.Clear();

        foreach(string sentences in dialog.sentence)
        {
            sentence.Enqueue(sentences);
        }
        DisplayNextSentence();

    }
    public void DisplayNextSentence()
    {
      
            if (sentence.Count == 0)
            {
               EndDialog();
                return;
            }
            string sentences = sentence.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentences));
 
    }
    IEnumerator TypeSentence (string sentence)
    {
        dialogText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }
   void EndDialog()
    {
        anim.SetBool("isOpen", false);
    }
   
}
