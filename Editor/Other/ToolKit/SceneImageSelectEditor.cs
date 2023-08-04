/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN
* All rights reserved.
* FileName:         Editors.Tool
* Author:           XiNan
* Version:          0.1
* UnityVersion:     2019.3.13f1
* Date:             2020-06-05
* Time:             06:59:00
* E-Mail:           1398581458@qq.com
* Description:
* History:
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace AIO.UEditor
{
    using UnityEditor;

    using UnityEngine;
    using UnityEngine.UI;

    /// <summary> ͼƬֱ�Ӹ�ֵ </summary>
    public static class SceneImageSelectEditor
    {
        //private static Object LastSelectObj = null;//������¼�ϴ�ѡ�е�GameObject��ֻ��������Image���ʱ�Ű�ͼƬ��ֵ����
        //private static Object CurSelectObj = null;

        //[InitializeOnLoadMethod]
        //private static void Init()
        //{
        //    Selection.selectionChanged += OnSelectChange;
        //}

        //private static void OnSelectChange()
        //{
        //    LastSelectObj = CurSelectObj;
        //    CurSelectObj = Selection.activeObject;
        //    //���Ҫ����Ŀ¼���޸�ΪSelectionMode.DeepAssets
        //    Object[] arr = Selection.GetFiltered(typeof(Object), SelectionMode.TopLevel);
        //    if (arr != null && arr.Length > 0)
        //    {
        //        GameObject selectObj = LastSelectObj as GameObject;
        //        if (selectObj != null && (arr[0] is Sprite || arr[0] is Texture2D))
        //        {
        //            string assetPath = AssetDatabase.GetAssetPath(arr[0]);//�õ�����Դ��·��
        //            Image image = selectObj.GetComponent<Image>();
        //            bool isImgWidget = false;
        //            if (image != null)
        //            {
        //                isImgWidget = true;
        //                Object newImg = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Sprite));
        //                Undo.RecordObject(image, "Change Image");//�������ſ����� ctrl + z �����˸�ֵ����
        //                image.sprite = newImg as Sprite;
        //                //image.SetNativeSize();        //�Ƿ�ʹ��ͼƬԭʼ��С
        //                EditorUtility.SetDirty(image);
        //            }
        //            if (isImgWidget)
        //            {   //����ͼ��ѽ��㻹��Image�ڵ�
        //                EditorApplication.delayCall = delegate { Selection.activeGameObject = LastSelectObj as GameObject; };
        //            }
        //        }
        //    }
        //}
    }
}
