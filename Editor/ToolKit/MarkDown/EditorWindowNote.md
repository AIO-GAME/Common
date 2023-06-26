/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XiNan Indie Developer
* All rights reserved.
* FileName:         Editors.Tool
* Author:           XiNan
* Version:          0.1
* UnityVersion:     2019.2.18f1
* Date:             2020-04-22
* Time:             15:25:46
* E-Mail:           1398581458@qq.com
* Description:
* History:
* * * * * * * * * * * * * * * * * * * * * * * * */

//MenuItem : ��Ҫ�� void ǰ���� static �ֶ�,�ڷ�����Ϊ���Ӳ˵�����ť,�������� void ����.
//as : [MenuItem("GenerateFolder/Open My Window")]

//AssetDatabase.LoadAssetAtPath : ��Ŀ¼ָ�� Assets ��һ��
//as : AssetDatabase.LoadAssetAtPath<Sprite>("Assets/UI/activity.png)

#region GetWindow GetWindowWithRect

/* �Ƽ����� (ǿ��ת��)GetWindow<����>("�Ƿ���Ҫ���ڸ���","������","�Ƿ���Ҫ�Զ���ʾ�򿪴������");
 * system.Type t : The type of the window. Must derive from EditorWindow.
 * system.Type t : typeof(����)��
 * utility : Set this to true, to create a floating utility window, false to create a normal window.
 * utility : ����һ�������� false �������game���Ⱥϲ� true���� ������һ������ Ϊ������һ�����
 * title : If GetWindow creates a new window, it will get this title. If this value is null, use the class name as title.
 * title : ���� ���ֵΪ�� ����Ϊ����
 * focus : Whether to give the window focus, if it already exists. (If GetWindow creates a new window, it will always get focus).
 * focus : ����Ѿ����� �Ƿ�����ڽ��� ���Ѵ��ڵĴ��� ֵΪtrue ���ý��� false �򲻻�������ʾ����
*/

#endregion

#region EditorWindow

/* ֵ
 * Vector2 = window.maxSize                    �������Ŀ���
 * Vector2 = window.minSize                    ������С�Ŀ���
 * bool    = focusedWindow                     ��ǰ���м��̽���ı༭������
 * bool    = mouseOverWindow                   ��ǰ�����ָ���µı༭������

 * bool    = window.maximized                  �Ƿ����
 * Rect    = window.position                   ��������Ļ����ʾ��λ��
 * bool    = window.autoRepaintOnSceneChange   �������з����ı�ʱ �Ƿ��Զ�����ˢ�´���
 * bool    = window.wantsMouseMove             �����������е�GUI�Ƿ���յ�MouseMove�¼���
 * bool    = window.wantsMouseEnterLeaveWindow �������༭�������е�GUI�Ƿ���յ���MouseEnterWindow��MouseLeaveWindow�¼���
 * int     = window.depthBufferBits            ��Ȼ����
 * HideFlags window.hideFlags;                 HideFlags. unity�༭��ǩ

 * ���Ϊwindow.titleContent = GUIContent.none ��ò�ʹ�� Ŀǰû��ʵ��Ч��
 * window.titleContent = GUIContent            ���ڻ��Ʊ༭�����ڱ����GUIContent��������
 * window.titleContent.tooltip                 ������Ҫ��ʾ�ı�ע ��Ҫ�������ʾ ��ò�ʹ��
 * window.titleContent.image                   ������Ҫ��ʾ��ͼƬ �Զ���ʾ ��ò�ʹ��
 * window.titleContent.text                    ������Ҫ��ʾ������ ������Ļ���� ��ò�ʹ��
*/

/* ����
 * window.Focus();                             �����̽����Ƶ���һ���༭������
 * window.RemoveNotification();                �Ƴ���ʾ����
 * window.SendEvent();                         �����¼�������

 * window.BeginWindows();                      ���ϼ������п�ʼ���ƴ���
 * window.EndWindows();                        ���ϼ������н������ƴ���

 * window.Show();                              ��ʾ����
 * window.ShowTab();                           ��ʾ����
 * window.ShowPopup();                         ��ʾ����
 * window.ShowAsDropDown(Rect,Vector2);        ����ʽ����
 * window.ShowAuxWindow();                     �ڸ�����������ʾ�༭�����ڡ�
 * window.ShowUtility();                       ���򻯴���
 * window.ShowNotification(GUIContent);        ֪ͨ

 * window.Repaint();                           ���»��ƴ���
*/

/* ��̬����
 * EditorWindow.FocusWindowIfItsOpen<����>();                 ���ָ�����͵ĵ�һ���ҵ��ı༭�������Ǵ򿪵ģ����иñ༭�����ڡ�
 * EditorWindow.CreateInstance<����>();                       �����ɱ�д�ű��Ķ����ʵ����
 * EditorWindow.GetWindow<>()                                 ����һ������ ��������Ŵ���С
 * EditorWindow.GetWindowWithRect<>(Rect,utility,title,focus);����һ������ ����������Ŵ���С �̶����ڴ�С
*/

#endregion
