namespace GestiunePrezente.Common
{
    public static class CGeneratorNumarContract
    {
        // Called inside an open transaction (Serializable).
        // UPDLOCK + HOLDLOCK prevent two concurrent transactions from reading the same MAX.
        public static int Next(CInterogareSQL q)
        {
            const string sql = @"
SELECT ISNULL(MAX(CUCO_NrContract), 0) + 1
  FROM CU_Contracte WITH (UPDLOCK, HOLDLOCK);";
            return q.ExecuteToIntP(sql);
        }
    }
}
