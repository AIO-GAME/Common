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

using UnityEditor;

namespace UnityEditor
{
    /// <summary>
    /// 资源管理界面
    /// </summary>
    public class AssestManagerGraphWindow : GraphicWindow
    {
        private EditorWindow CScriptWindow;
        private EditorWindow TextureWindow;

        protected override void OnEnable()
        {
        }

        protected override void OnGUI()
        {
            GTLayout.BE.Vertical(() =>
            {
                GTLayout.AC.Button("Texture", OpenTexture);
                GTLayout.AC.Button("CScript", OpenCScript);
            });
        }

        /// <summary>
        /// 打开 纹理图集 管理
        /// </summary>
        private void OpenTexture()
        {
            if (TextureWindow == null)
                TextureWindow = UtilsEditor.Window.Open<BuiltInTextureGraphWindow>("Texture Manager", true, true);
            TextureWindow.Show(true);
        }

        /// <summary>
        /// 打开 纹理图集 管理
        /// </summary>
        private void OpenCScript()
        {
            if (CScriptWindow == null)
                CScriptWindow = UtilsEditor.Window.Open<CScriptManagerGraphWindow>("CScript Manager", true, true);
            CScriptWindow.Show(true);
        }
    }
}