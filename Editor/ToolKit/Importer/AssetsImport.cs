﻿/*=================================================================================================|*|
↓  Copyright(C) 2022 by DefaultCompany            |*| ╭╩╮╮╮╔════╗╔═══════╗╔════╗╔═══════╗╔═══════╗  ↩
↓  All Rights Reserved By Author lihongliu.       |*|╭╯L ╭╠╣ No ╠╣ Pains ╠╣ No ╠╣ Gains ╠╣ XNSKY ╟  ↩
↓  Author:      |*| XiNan                         |*|╰◎═◎╯╯╚◎══◎╝╚◎═════◎╝╚◎══◎╝╚◎═════◎╝╚◎═════◎╝  ↩
↓  Email:       |*| 1398581458@qq.com                                                               ↩
↓  Version:     |*| 1.0                           |*| ╭╩╮╮╮╔════╗╔═══╗╔═══╗╔═══════╗╔════╗╔══════╗  ↩
↓  UnityVersion:|*| 2021.2.13f1c1                 |*|╭╯H ╭╠╣Only╠╣You╠╣Can╠╣Cantrol╠╣Your╠╣Future╟  ↩
↓  Date:        |*| 2022-03-08                    |*|╰◎═◎╯╯╚◎══◎╝╚◎═◎╝╚◎═◎╝╚◎═════◎╝╚◎══◎╝╚◎════◎╝  ↩
↓  URL:         |*| www.XiNansky.com                                                                ↩
↓  Nowtime:     |*| 15:15:40                      |*| ╭╩╮╮╮╔═════╗╔════╗╔══════╗╔═══╗╔══════╗╔═══╗  ↩
↓  Description: |*| |U_U|                         |*|╭╯L ╭╠╣There╠╣ Is ╠╣Always╠╣ A ╠╣Better╠╣Way╟  ↩
↓  History:     |*| |>"<|                         |*|╰◎═◎╯╯╚◎═══◎╝╚◎══◎╝╚◎════◎╝╚◎═◎╝╚◎════◎╝╚◎═◎╝  ↩
↓===================================================================================================*/

using UnityEngine;

namespace UnityEditor
{
    public class AssetsImport : AssetPostprocessor
    {
        // /// <summary>
        // /// 在导入纹理贴图之后调用
        // /// </summary>
        // private void OnPreprocessTexture()
        // {
        //     Debug.Log("OnPreprocessTexture:" + assetPath);
        //     var importer = this.assetImporter as TextureImporter;
        //     importer.textureType = TextureImporterType.Sprite;
        //     importer.maxTextureSize = 512;
        //     importer.mipmapEnabled = false;
        // }
        //
        // /// <summary>
        // /// 在导入纹理贴图之后调用
        // /// </summary>
        // public void OnPostprocessTexture(Texture2D tex)
        // {
        //     Debug.Log("OnPostprocessTexture:" + assetPath);
        // }
        //
        // /// <summary>
        // /// 在导入模型之前调用
        // /// </summary>
        // public void OnPreprocessModel()
        // {
        //     Debug.Log("OnPreprocessModel:" + assetPath);
        // }
        //
        // /// <summary>
        // /// 在导入音频之前调用
        // /// </summary>
        // public void OnPreprocessAudio()
        // {
        //     Debug.Log("OnPreprocessAudio:" + assetPath);
        // }
        //
        // /// <summary>
        // /// 在导入模型之后调用
        // /// </summary>
        // /// <param name="g"></param>
        // public void OnPostprocessModel(GameObject g)
        // {
        //     Debug.Log("OnPostprocessModel:" + assetPath);
        // }
        //
        // /// <summary>
        // /// 在导入音频之后调用
        // /// </summary>
        // /// <param name="arg"></param>
        // public void OnPostprocessAudio(AudioClip arg)
        // {
        //     Debug.Log("OnPostprocessAudio:" + assetPath);
        // }
        //
        // /// <summary>
        // /// 所有资源的导入，删除，移动操作都会调用该方法
        // /// </summary>
        // /// <param name="importedAssets"></param>
        // /// <param name="deletedAssets"></param>
        // /// <param name="movedAssets"></param>
        // /// <param name="movedFromAssetPaths"></param>
        // public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        // {
        //     Debug.Log("OnPostprocessAllAssets:");
        // }
    }
}