/*=================================================================================================|*|
↓  Copyright(C) 2022 by DefaultCompany            |*| ╭╩╮╮╮╔════╗╔═══════╗╔════╗╔═══════╗╔═══════╗  ↩
↓  All Rights Reserved By Author lihongliu.       |*|╭╯L ╭╠╣ No ╠╣ Pains ╠╣ No ╠╣ Gains ╠╣ XNSKY ╟  ↩
↓  Author:      |*| XiNan                         |*|╰◎═◎╯╯╚◎══◎╝╚◎═════◎╝╚◎══◎╝╚◎═════◎╝╚◎═════◎╝  ↩
↓  Email:       |*| 1398581458@qq.com                                                               ↩
↓  Version:     |*| 1.0                           |*| ╭╩╮╮╮╔════╗╔═══╗╔═══╗╔═══════╗╔════╗╔══════╗  ↩
↓  UnityVersion:|*| 2021.2.13f1c1                 |*|╭╯H ╭╠╣Only╠╣You╠╣Can╠╣Cantrol╠╣Your╠╣Future╟  ↩
↓  Date:        |*| 2022-03-09                    |*|╰◎═◎╯╯╚◎══◎╝╚◎═◎╝╚◎═◎╝╚◎═════◎╝╚◎══◎╝╚◎════◎╝  ↩
↓  URL:         |*| www.XiNansky.com                                                                ↩
↓  Nowtime:     |*| 09:30:36                      |*| ╭╩╮╮╮╔═════╗╔════╗╔══════╗╔═══╗╔══════╗╔═══╗  ↩
↓  Description: |*| |U_U|                         |*|╭╯L ╭╠╣There╠╣ Is ╠╣Always╠╣ A ╠╣Better╠╣Way╟  ↩
↓  History:     |*| |>"<|                         |*|╰◎═◎╯╯╚◎═══◎╝╚◎══◎╝╚◎════◎╝╚◎═◎╝╚◎════◎╝╚◎═◎╝  ↩
↓===================================================================================================*/

using AIO.UEditor;
using UnityEditor;

namespace AIO.UEditor
{
    /// <summary>
    /// 资源管理界面
    /// </summary>
    [GWindow("资源管理器", "Assets Manager", Group = "Tools",
        MinSizeWidth = 600, MinSizeHeight = 600
    )]
    public class AssetManagerGraphWindow : GraphicWindow
    {
        private EditorWindow CScriptWindow;
        private EditorWindow TextureWindow;

        protected override void OnActivation()
        {
        }

        protected override void OnGUI()
        {
            GELayout.Vertical(() =>
            {
                GELayout.Button("Texture", OpenTexture);
                GELayout.Button("CScript", OpenCScript);
            });
        }

        /// <summary>
        /// 打开 纹理图集 管理
        /// </summary>
        private void OpenTexture()
        {
            if (TextureWindow == null)
                TextureWindow = EHelper.Window.Open<BuiltInTextureGraphWindow>("Texture Manager", true, true);
            TextureWindow.Show(true);
        }

        /// <summary>
        /// 打开 纹理图集 管理
        /// </summary>
        private void OpenCScript()
        {
            if (CScriptWindow == null)
                CScriptWindow = EHelper.Window.Open<CScriptManagerGraphWindow>("CScript Manager", true, true);
            CScriptWindow.Show(true);
        }
    }
}