/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XiNan Indie Developer
* All rights reserved.
* FileName:         Editors.Note
* Author:           XiNan
* Version:          0.1
* UnityVersion:     2019.2.18f1
* Date:             2020-04-22
* Time:             15:28:09
* E-Mail:           1398581458@qq.com
* Description:
* History:
* * * * * * * * * * * * * * * * * * * * * * * * */

#region �����Ի��Ʒ���

/*
 * bool    EditorGUILayout.BeginFadeGroup(float) : ����������ƻ����Ƿ����ػ���ʾ 0Ϊ��ȫ���� 1Ϊ��ʾ ������1�򲻿���������������
 * as :    bool = EditorGUILayout.BeginFadeGroup(float);
 *         EditorGUILayout.EndFadeGroup();

 * Rect    EditorGUILayout.BeginHorizontal(GUIStyle,GUILayoutOption); ������ �Զ������ �������
 * as :    EditorGUILayout.BeginHorizontal();
 *         EditorGUILayout.EndHorizontal();

 * Vector2 EditorGUILayout.BeginScrollView(Vector2,bool,bool,GUIStyle,GUIStyle,GUIStyle,GUILayoutOption) : �����������¹��� ���ҹ���
 * as :    Vector2 = EditorGUILayout.BeginScrollView(Vector2,true,true,GUILayout.Height(100),GUILayout.Width(400)); GUIStyle���������������ò���none
 *         EditorGUILayout.EndScrollView(); trips:GUILayoutOption�Ŀ������ʹ��Screen.height,Screen.Width
*/

#endregion

#region һ���Ի��Ʒ���

/*
 * EditorGUILayout.Slider : ������, ("����",�м����,��Сֵ,���ֵ)
 * as : float = EditorGUILayout.Slider("Slider",float,0,200)

 * EditorGUILayout.Vector2Field <== ��ά���� ==> EditorGUILayout.Vector2IntField
 * EditorGUILayout.Vector3Field <== ��ά���� ==> EditorGUILayout.Vector3IntField
 * EditorGUILayout.Vector4Field <== ��ά���� ==> ("����",�м����,���,�Զ������)
 * as : Vector2 = EditorGUILayout.Vector2Field("Vector2Field",Vector2,GUIStyle,GUILayoutOption);

 * EditorGUILayout.ColorField : ��ɫѡ��,("����",��ɫ,��������,����͸����,����HDR,���,�Զ������)
 * as : Color = EditorGUILayout.ColorField(GUIContent,Color,����,͸����,HDR,GUIStyle,GUILayoutOption);
*/

#endregion

#region �������
/* GUILayout.BeginHorizontal : ��ʼ������� (GUI���,ϸ�ڲ��� �磻�߶ȿ���)
 * as : GUILayout.BeginHorizontal(GUIStyle,GUILayoutOption)

 * GUILayout.EndHorizontal : �����������
 * as : GUILayout.EndHorizontal() */
#endregion

#region �������
/* GUILayout.BeginArea : ��ʼ������� (Rect,GUIContent,GUI���,)
 * as : GUILayout.BeginArea(Rect,GUIContent,GUIStyle);

 * GUILayout.EndArea : �����������
 * as : GUILayout.EndArea(); */
#endregion

#region �������
/* GUILayout.BeginScrollView : ��ʼ�������
 * as :GUILayout.BeginScrollView(new Vector2(200,200),true,true,GUIStyle.none,GUIStyle.none,gUILayoutOption);

 * GUILayout.EndScrollView : �������ƹ���
 * as :GUILayout.EndScrollView() */
#endregion

/*����
 * GUILayout.Window : ��Ϸ�л��ƴ��ڷ��� ��Ҫ��OnGUIʹ��
 * as GUILayout.Window(windowID,Rect,GUI.WindowFunction(int),"Content");(ID,��ʾλ��,���ƴ������ݷ���,����)

 * GUILayout.Box : ����box��״�߿���
 * as GUILayout.Box(Content,GuiStyle,GUILayoutOption);("���ݣ�ͼƬ",���,�Զ������)

 * GUILayout.Button : ���ư�ť��� ����ֵΪboolean ��ťÿ���һ����ı�һ��״̬
 * as GUILayout.Button(Content,GUIStyle,GUILayoutOption);("���ݣ�ͼƬ",���,�Զ������)
 * GUILayout.RepeatButton : ���ư�ť��� ����ֵΪboolean ��ס���ţ���ť�ͻ᷵��true

 * GUILayout.PasswordField : ����һ������� ����ֵΪstring
 * as string = GUILayout.PasswordField(string,'*'(char),MaxLength,GUIStyle,GUILayoutOption);("��������","�����ַ�",���볤��,���,�Զ������)

 * GUILayout.FlexibleSpace : ����һ�пհ�Ԫ��
 * GUILayout.Space : �ڵ�ǰ���ֲ���ָ���߶ȿհ�

 * GUILayout.SelectionGrid : ���ƿ���ѡ��ĸ��Ӱ�ť �������ж�����ݿɵ�� ����int ����ҵ���ĸ����±�
 * as int = GUILayout.SelectionGrid(int,GUIContent[](),xCount(int),GUIStyle,GUILayoutOption);(�±�,���ݼ���,ÿ������ʾN������,���,�Զ������)

 * GUILayout.HorizontalScrollbar : ���ƺ���� ����ֵfloat
 * as float = GUILayout.HorizontalScrollbar(float(value),size,left,right,GUILayoutOption);(λ��,�����С,���������,�������ұ�,���,�Զ������)
 * GUILayout.VerticalScrollbar : �����ݹ��� ����ֵfloat ͬ��

 * GUILayout.HorizontalSlider : �Ử���� ����ֵfloat
 * asfloat = GUILayout.HorizontalSlider(float(value),left,right,gUILayoutOption);(λ��,���������,�������ұ�,���,�Զ������)
 * GUILayout.VerticalSlider  : �ݻ����� ����ֵfloat ͬ��

 * GUILayout.Label : ���� �����������Ϸ�
 * as GUILayout.Label("Content",GUIStyle,GUILayoutOption);("����",���,�Զ������)

 * GUILayout.TextArea <<= �ַ������ ����ֵΪstring �û�������ַ� =>> GUILayout.TextField
 * as string = GUILayout.TextArea(string,maxLenght,GUIStyle,GUILayoutOption);("����",������󳤶�,���,�Զ������)
 * as string = GUILayout.TextField(string,maxLenght,GUIStyle,GUILayoutOption);ͬ�� Ч��΢����΢

 * GUILayout.Toggle : ����һ����ť ����ֵ boolean �û�ÿ���һ����ı�һ��״̬
 * as bool = GUILayout.Toggle(bool,GUIContent,GUIStyle,GUILayoutOption);(״̬bool,����,���,�Զ������)

 * GUILayout.Toolbar : ����һ������bar��ť ����ֵ int �����û�ѡ���bar�±�
 * as int = GUILayout.Toolbar(int,GUIContent[],GUIStyle,GUILayoutOption);(ѡ�е��±�,��������(ͼƬ),���,�Զ������)

*/

/* ��������Ϊ GUILayoutOption
 * GUILayout.MaxHeight : ���ݸ��ؼ����߶�����
 * GUILayout.MinHeight : ���ݸ��ؼ���С�߶�����
 * GUILayout.MaxWidth  : ���ݸ��ؼ�����������
 * GUILayout.MinWidth  : ���ݸ��ؼ���С��������
 * GUILayout.ExpandHeight : ���ݸ��ռ�������������ֱ��չ�ؼ�
 * GUILayout.ExpandWidth  : ���ݸ��ռ�����������������չ�ؼ�
 * GUILayout.Width  : ���ݸ��ռ����
 * GUILayout.Height : ���ݸ��ռ�߶�
*/
