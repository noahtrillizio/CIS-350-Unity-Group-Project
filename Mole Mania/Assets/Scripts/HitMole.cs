/*
 * Jacob Zydorowicz
 * Project 2 Mole Mania
 * Detects if hammer hits mole and raises score
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMole : MonoBehaviour
{
    public int score;
    public MoleMove moleMoveScript;


    public ScoreManager scoreManObj;
    private void OnMouseDown()
    {
        //object click detection based on https://answers.unity.com/questions/1128405/how-do-i-detect-a-click-on-an-object-with-a-partic.html
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //if mole is clicked on, resets mole & raise score
            if (moleMoveScript.isUp && hit.collider.CompareTag("Mole"))
            {
                score++;
                //scoreManObj.score++; //as it is now, you cannot attach the score manager to the prefabs, which is what's breaking the spawn manager 
                                     //it only works on the test mole.
                                     //if i delete this line and the reference to the score manager, it works but no longer counts score. - anna

                //moleMoveScript.transform.Translate(Vector3.down * moleMoveScript.speed * Time.deltaTime); //<<<commented this out so we can still go back to it if we need to
                //moleMoveScript.isUp = false;

                //just going to test another method here so i can see if this will make my spawn manager work - anna
                Destroy(gameObject);
            }




        }
    }
}   
