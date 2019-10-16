using System.Collections;
using UnityEngine;

public class LoadRole : MonoBehaviour
{
    private Manager manager;
    private Outline outLine;

    public Outline.Mode OutLineMode = Outline.Mode.OutlineAll;
    public Color OutLineColor = Color.red;

    [Range(0, 10)]
    public float OutLineWidth = 5;

    [HideInInspector]
    public GameObject[] currentRole = new GameObject[2];

    [HideInInspector]
    public bool isCreatingData = true;

    private GameObject targetPeople;
    private string targetName;
    private int nameIndex;

    public int stringLength
    {
        set; get;
    }

    public void GeneratePeople(string[] sameName, int roleID)
    {
        //加载人物模型
        GameObject obj = FindRole(sameName, roleID);
        if (obj != null)
        {
//            if (currentRole.Length != 2)
//           {
                currentRole = new GameObject[2];
//          }
            targetPeople = GameObject.Instantiate(obj, this.transform.position, Quaternion.Euler(0, 0, 0));
            currentRole[0] = targetPeople;
            ChangeLayer(currentRole[0]);
            currentRole[0].transform.SetParent(this.transform);
            //currentRole[0].layer=


            //targetPeople = GameObject.Instantiate(obj, this.transform.position, Quaternion.Euler(0, 0, 0));
            //currentRole[1] = targetPeople;
            //currentRole[1].transform.SetParent(this.transform);
            //SetOutLine(currentRole[1]);
        }
        else
        {
            isCreatingData = false;
            print("所有模型数据生成结束！");
            return;
        }
    }

    private void ChangeLayer(GameObject trans)
    {
        Transform[] transforms = trans.GetComponentsInChildren<Transform>();
        for (int i = 0; i < transforms.Length; i++)
        {
            transforms[i].gameObject.layer = LayerMask.NameToLayer("role");
        }
    }

    private void SetOutLine(GameObject obj)
    {
        outLine = obj.GetComponent<Outline>();
        if (outLine == null)
            outLine = obj.AddComponent<Outline>();
        if (outLine.enabled == false)
            outLine.enabled = true;
        outLine.OutlineMode = OutLineMode;
        outLine.OutlineColor = OutLineColor;
        outLine.OutlineWidth = OutLineWidth;
    }

    private GameObject FindRole(string[] sameName, int roleID)
    {
        targetName = sameName[nameIndex] + roleID.ToString();
        GameObject obj = Resources.Load(targetName) as GameObject;
        if (obj == null)
        {
            nameIndex++;
            if (nameIndex < stringLength)
            {
                ResetRoleID();
                FindRole(sameName, 1);
            }
            else
            {
                return null;
            }
        }
        return obj;
    }

    private void ResetRoleID()
    {
        if (manager == null)
            manager = GetComponent<Manager>();
        manager.roleID = 1;
    }
}