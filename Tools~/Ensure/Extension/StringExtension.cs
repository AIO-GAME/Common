using System.Linq;

namespace AIO
{
    internal static class StringExtension
    {
        /// <summary>
        /// ��ָ�������ж�Ӧ������ַ�����ʾ��ʽ�滻Ϊָ���ַ����еĸ�ʽ������ַ�����ʽʹ��ռλ����{0}��{1}�ȡ�
        /// </summary>
        /// <param name="format">�����ַ�����ʽ��</param>
        /// <param name="formattingArgs">����Ҫ��ʽ����ע�뵽�ַ����е�ֵ�Ķ������顣</param>
        /// <returns>����ʽ���滻Ϊ��Ӧ������ַ�����ʾ��ʽ����ַ���������</returns>
        internal static string Inject(this string format, params object[] formattingArgs)
        {
            return string.Format(format, formattingArgs);
        }

        /// <summary>
        /// ��ָ�������ж�Ӧ������ַ�����ʾ��ʽ�滻Ϊָ���ַ����еĸ�ʽ������ַ�����ʽʹ��ռλ����{0}��{1}�ȡ�
        /// �˷�������һ�����������أ���������һ���ַ�����������Ƕ������顣
        /// </summary>
        /// <param name="format">�����ַ�����ʽ��</param>
        /// <param name="formattingArgs">����Ҫ��ʽ����ע�뵽�ַ����е�ֵ���ַ������顣</param>
        /// <returns>����ʽ���滻Ϊ��Ӧ������ַ�����ʾ��ʽ����ַ���������</returns>
        internal static string Inject(this string format, params string[] formattingArgs)
        {
            return string.Format(format, formattingArgs.Select(a => a as object).ToArray());
        }
    }
}
