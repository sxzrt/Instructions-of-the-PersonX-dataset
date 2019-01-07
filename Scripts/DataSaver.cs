using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    private Texture2D texture;
    private Texture2D cameraTexture;
    private string path;
    private string cameraTexPath;
    private Vector3 position;

    private bool isCreateStream;

    //private FileStream fileStream;
    private StreamWriter streamWriter;

    public DataSaver()
    {
        //创建一个空纹理（图片大小为屏幕的宽高）
        texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        cameraTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        position = Vector3.zero;
    }

    public IEnumerator SavePicture(Camera m_Camera, string filePath, string roleName, int index)
    {
        //等待所有的摄像机和GUI被渲染完成。
        yield return new WaitForEndOfFrame();
        Capture(filePath, roleName, index);
        GetCameraTexture(m_Camera, filePath, roleName, index);
    }

    private void Capture(string filePath, string roleName, int index)
    {
        path = filePath + "/Data/" + roleName + "/" + index.ToString() + ".png";

        //只能在帧渲染完毕之后调用（从屏幕左下角开始绘制，绘制大小为屏幕的宽高，宽高的偏移量都为0）
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

        //图片应用（此时图片已经绘制完成）
        texture.Apply();

        //将图片装换成jpg的二进制格式，保存在byte数组中（计算机是以二进制的方式存储数据）
        byte[] bytes = texture.EncodeToPNG();

        //文件保存，创建一个新文件，在其中写入指定的字节数组（要写入的文件的路径，要写入文件的字节。）
        File.WriteAllBytes(path, bytes);
    }

    private void GetCameraTexture(Camera m_Camera, string filePath, string roleName, int index)
    {
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 16);
        m_Camera.targetTexture = rt;
        m_Camera.Render();

        RenderTexture.active = rt;
        cameraTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        cameraTexture.Apply();

        cameraTexPath = filePath + "/Data/" + roleName + "_r/" + index.ToString() + ".png";
        System.IO.File.WriteAllBytes(cameraTexPath, cameraTexture.EncodeToPNG());
    }

    public void SavePositionData(string filePath, int roleID, Vector3 rolePosition, Vector3 cameraPosition)
    {
        string path = filePath + "/Data/PositionData/" + roleID.ToString() + ".txt";

        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                position = rolePosition - cameraPosition;

                sw.WriteLine(position.x.ToString("#0.000") + ","
               + position.y.ToString("#0.000") + ","
               + position.z.ToString("#0.000"));

                sw.Flush();
            }
        }
        else
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                position = rolePosition - cameraPosition;

                sw.WriteLine(position.x.ToString("#0.000") + ","
               + position.y.ToString("#0.000") + ","
               + position.z.ToString("#0.000"));

                sw.Flush();
            }
        }
    }

    public void CreateDirectory(string filePath, string roleName)
    {
        if (!Directory.Exists(filePath + "/Data/" + roleName))
        {
            Directory.CreateDirectory(filePath + "/Data/" + roleName);
            Directory.CreateDirectory(filePath + "/Data/" + roleName + "_r");
        }
        if (!Directory.Exists(filePath + "/Data/PositionData"))
        {
            Directory.CreateDirectory(filePath + "/Data/PositionData");
        }
    }
}