/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;
using TMPro;

public class EnemyFactory : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;

    public GameObject buttonPanel;
    public GameObject buttonPrefab;

    List<EnemyAI> enemies;
    // Start is called before the first frame update
    void Start()
    {
        var enemyTypes = Assembly.GetAssembly(typeof(EnemyAI)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(EnemyAI)));

        enemies = new List<EnemyAI>();

       

        foreach(var type in enemyTypes)
        {
            var tempType = Activator.CreateInstance(type) as EnemyAI;
            enemies.Add(tempType);
        }

        //manually adding for testing purposes
        EnemyAI testEnemy;
        enemies.Add(testEnemy);

        ButtonPanel();
    }

    public EnemyAI GetEnemy(string enemyType)
    {
        foreach(EnemyAI enemy in enemies)
        {
           //theres only one anyways
            var target = Activator.CreateInstance(enemy.GetType()) as EnemyAI;
            return target;
        }
        return null;
    }
    void ButtonPanel()
    {
        foreach(EnemyAI enemy in enemies)
        {
            var button = Instantiate(buttonPrefab);
            button.transform.SetParent(buttonPanel.transform);
            button.gameObject.name = "Enemy" + " Button";
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy";
        }
    }
}
 */