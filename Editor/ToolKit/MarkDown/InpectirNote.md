/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XiNan Indie Developer
* All rights reserved.
* FileName:         IHStudy.Note
* Author:           XiNan
* Version:          0.1
* UnityVersion:     2019.2.18f1
* Date:             2020-04-11
* Time:             18:22:40
* E-Mail:           1398581458@qq.com
* Description:
* History:
* * * * * * * * * * * * * * * * * * * * * * * * */

/* BeginFadeGroup                 ��ʼһ�����Ա�����/��ʾ����,�������ת�����ᱻ����.
 * BeginHorizontal                ��ʼһ��ˮƽ���鲢�õ����ľ���.
 * BeginScrollView                ����һ���Զ����ֵ�scrollview.
 * BeginToggleGroup               ��һ����������һ����ֱ����,ʹ���еĿؼ�ͬʱ���û����.
 * BeginVertical                  ��ʼһ����ֱ���鲢�õ����ľ���.
 * BoundsField                    ʹ���ĺ����������һ������.
 * BoundsIntField                 Ϊ����һ���е��Ե���������λ�úʹ�С��.
 * ColorField                     Ϊѡ��һ����ɫ��һ���ֶ�.
 * CurveField                     ����һ�����ڱ༭�������ߵ�����.
 * DelayedDoubleField             ����һ���ӳٵ��ı��ֶ�,��������˫������.
 * DelayedFloatField              Ϊ���븡������һ���ӳٵ��ı��ֶ�.
 * DelayedIntField                Ϊ������������һ���ӳٵ��ı��ֶ�.
 * DelayedTextField               ����һ���ӳٵ��ı��ֶ�.
 *
 * DoubleField                    Ϊ����˫ֵ����һ���ı��ֶ�.
 * DropdownButton                 ����һ����ť,�����������Ӧ,��ʾ���Լ�����������.
 * EndFadeGroup                   �ر�һ���ɿ�ʼ�鿪ʼ����.
 * EndHorizontal                  �ر�һ���Կ�ʼˮƽ��ʼ��С��.
 * EndScrollView                  ����һ��scrollview,��ʼ����BeginScrollView.
 * EndToggleGroup                 �ر�һ��С��,�ӿ�ʼ��С�鿪ʼ.
 * EndVertical                    �ر�һ���Կ�ʼ��ֱ��ʼ��С��.
 * EnumFlagsField                 ������ʱ,��ʾһ������ö������ֵ��ѡ��Ĳ˵�.ֵΪ0��ѡ��"û��",��ֵ0(������λԪ��)��ѡ��������ʾ�ڲ˵��Ķ���.ֵ0��0�����ƿ���ͨ����enum�����ж�����Щֵ��������ʹ��
 * EnumPopup                      ����һ��enum����ѡ���ֶ�.
 * FloatField                     Ϊ���븡��ֵ����һ���ı��ֶ�.
 * Foldout                        �����������һ���۵���ͷ��һ����ǩ.
 * GetControlRect                 ��ȡһ���༭���ؼ���rect.
 * HelpBox                        ���û��ṩһ������Ϣ�İ�����.
 * InspectorTitlebar              ��һ�������ڼ�鴰�ڵı�����.
 * IntField                       Ϊ������������һ���ı���.
 * IntPopup                       ����һ����������ѡ���ֶ�.
 * IntSlider                      ����һ��������,�û������϶����ı�һ����Сֵ�����ֵ֮�������ֵ.
 * LabelField                     ��һ����ǩ�ֶ�.(������ʾֻ����Ϣ.)
 * LayerField                     ��һ��ͼ��ѡ���ֶ�.
 * LongField                      Ϊ���볤��������һ���ı��ֶ�.
 * MaskField                      ����һ�����ֵĳ���.

 * MinMaxSlider                   ����һ������Ļ���,�û�����ʹ������ָ����Сֵ�����ֵ֮��ķ�Χ.
 * ObjectField                    ����һ���ֶ��������κζ�������.
 * PasswordField                  ����һ���ı��ֶ�,�û�������������.
 * Popup                          ����һ��ͨ�õĵ���ѡ���ֶ�.
 * PrefixLabel                    ��һЩ���Ƶ�ǰ����һ����ǩ.
 * PropertyField                  Ϊ���л������Դ���һ���ֶ�.
 * RectField                      ����һ��X,Y,W��H�ֶ�������һ��Rect.
 * RectIntField                   ��һ��X,Y,W,H��,����һ��������ɫ.
 * SelectableLabel                ����һ����ѡ��ı�ǩ�ֶ�.(������ʾ���Ը���ճ����ֻ����Ϣ).
 * Slider                         ����һ��������,�û������϶����ı�һ����Сֵ�����ֵ֮���ֵ
 * Space                          ��ǰ��Ŀؼ�������Ŀؼ�֮������һ��С�ռ�.
 * TagField                       ��һ�����ѡ���ֶ�.
 * TextArea                       ����һ���ı�����.
 * TextField                      ����һ���ı���.
 * Toggle                         �л�.
 * ToggleLeft                     ����һ���л�����,���еĿ��������,��ǩ�����������ұ�.
 * Vector2Field                   ����һ��X,Y�ֶ�,��������ʸ��2.
 * Vector2IntField                ����һ��X,Y�����ֶ�������Vector2Int.
 * Vector3Field                   ����һ��X,Y,Z�ֶ�������һ��ʸ��.
 * Vector3IntField                ����һ��X,Y,Z�����ֶ�������һ��Vector3Int.
 * Vector4Field                   ��һ��X,Y,Z,W�ֶ�,����һ��ʸ��ͼ.
 */
