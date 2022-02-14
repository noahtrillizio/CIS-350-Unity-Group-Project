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

    public MoleMove moleMoveScript;
    
    public ScoreManager scoreManObj;
    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //if mole is clicked on, resets mole & raise score
            if (moleMoveScript.isUp && hit.collider.CompareTag("Mole"))
            {

                scoreManObj.score++;
                moleMoveScript.transform.Translate(Vector3.down * moleMoveScript.speed * Time.deltaTime);
                moleMoveScript.isUp = false;
            }




        }
    }
}   
