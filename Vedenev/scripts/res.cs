 //##################################################################################################
    /// <summary>
    /// ��������� ��������� 58 f(x) = arctg(x) � arcctg(1/x)
    /// </summary>
    /// <param name="x">������������� �����</param>
    /// <returns>0</returns>
    [OpaqueFunction()]
    [FunctionName("L00_58_1_arctg_arcctg", "f(x) = arctg(x) � arcctg(1/x)")]
    [EquivalentIntConstant(0)]
    public static class CL00_58_1_arctg_arcctg
    {
        public static double Body(double x)
        {            
            double  X, Y1, Y2;
            Y1 = Math.Atan(x);
Y2 = Math.PI/2 - Math.Atan(1/x);
X = Y1 - Y2;
            return X;
        }
    }

    /// <summary>
    /// ��������� ��������� 58 f(x) = arctg(x) � arcctg(1/x),
    /// ����������� ������� �������� ����� ����� X - ��������� ��������� ����� ����� ��������� ���� �� ���� ������� ���,
    /// ������� ������ �������� <paramref name="count"/>.
    /// </summary>
    /// <param name="x">������������� �����</param>
    /// <param name="count">���������� ��������� ������������</param>
    /// <returns>0</returns>
    [OpaqueFunction()]
    [FunctionName("L00_58_2_arctg_arcctg", "f(x) = arctg(x) � arcctg(1/x)")]
    [EquivalentIntConstant(0)]
    public static class CL00_58_2_arctg_arcctg
    {
        public static double Body(double x, int count)
        {
            double  X, Y1, Y2;
            for (int i = 0; i < count; ++i) {
                Y1 = Math.Atan(x);
Y2 = Math.PI/2 - Math.Atan(1/x);
X = Y1 - Y2;
            }
            return X;
        }
    }

    /// <summary>
    /// ���������� ������� ����������� ��������� 58 f(x) = arctg(x) � arcctg(1/x)
    /// </summary>    
    /// <returns>(0, w)</returns>
    [OpaqueFunction()]
    [FunctionName("L00_58_1_arctg_arcctg_in", "(0, w)")]
    public static class CL00_58_1_arctg_arcctg_in
    {
        public static string Body()
        {
            return "(0, w)";
        }
        