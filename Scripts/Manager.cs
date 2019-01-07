using UnityEngine;

public class Manager : MonoBehaviour
{
    private LoadRole loadRole;
    private RandomPosition randomPosition;
    private DataSaver dataSaver;

    public Camera roleCamera;
    public float rotateAngle;
    public string[] sameName;

    public int roleID = 0;

    private int index;
    private int rotateTimes;
    private Vector3 position;
    private string path;

    private void Start()
    {
        dataSaver = new DataSaver();
        // path = Application.dataPath;
        path = "/Users/xiaoxiaosun/person-im";
        //path = "/Volumes/SXX^_^/person-im";
        loadRole = GetComponent<LoadRole>();
        loadRole.stringLength = sameName.Length;
        randomPosition = GetComponent<RandomPosition>();

        rotateTimes = Mathf.RoundToInt(360 / rotateAngle);
        loadRole.GeneratePeople(sameName, roleID);
        dataSaver.CreateDirectory(path, loadRole.currentRole[0].name);
    }

    private void Update()
    {
        if (!loadRole.isCreatingData)
        {
            //dataSaver.CloseStream();
            return;
        }
        if (index < rotateTimes)
        {

            //保存数据
            StartCoroutine(dataSaver.SavePicture(roleCamera, path, loadRole.currentRole[0].name, index));
            dataSaver.SavePositionData(path, roleID, loadRole.currentRole[0].transform.position, Camera.main.transform.position);

            //旋转模型
            loadRole.currentRole[0].transform.Rotate(Vector3.up, -rotateAngle);
            loadRole.currentRole[1].transform.Rotate(Vector3.up, -rotateAngle);
            //随机位置
            if (randomPosition)
            {
                position = randomPosition.GetRandomPosition(loadRole.currentRole[0].transform.localPosition);
                loadRole.currentRole[0].transform.localPosition = position;
                loadRole.currentRole[1].transform.localPosition = position;
            }

            index++;
        }
        else
        {
            //销毁模型
            Destroy(loadRole.currentRole[0]);
            Destroy(loadRole.currentRole[1]);

            //加载下一个模型，然后初始化
            loadRole.GeneratePeople(sameName, ++roleID);

            if (loadRole.isCreatingData)
            {
                //dataSaver.CloseStream();
                dataSaver.CreateDirectory(path, loadRole.currentRole[0].name);
            }

            index = 0;
        }
    }
}