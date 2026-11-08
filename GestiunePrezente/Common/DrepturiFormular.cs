namespace GestiunePrezente.Common
{
    public static class DrepturiFormular
    {
        /// <summary>
        /// Verifica daca utilizatorul curent are drept de folosinta pe intrarea ascunsa
        /// din MB_Meniu identificata prin <paramref name="mbmeNume"/>.
        /// Apeleaza inainte de a instantia orice FrmGestiune*.
        /// </summary>
        public static bool PoateDeschide(CManagerDeProiect mm, string mbmeNume)
        {
            const string sql = @"
                SELECT COUNT(1)
                  FROM MB_Meniu M
                 INNER JOIN MB_DrepturiMeniu D ON D.MBDM_MBME_Id = M.MBME_Id
                 WHERE M.MBME_Ascuns          = 1
                   AND M.MBME_Nume            = @nume
                   AND D.MBDM_MBUT_Id         = @uid
                   AND D.MBDM_DreptFolosinta  = 1
                   AND D.MBDM_MBDV_Id         = @div";
            var q = new CInterogareSQL(mm.GetConnection());
            return q.ExecuteToIntP(sql,
                CInterogareSQL.P("@nume", mbmeNume),
                CInterogareSQL.P("@uid",  mm.GetUserId()),
                CInterogareSQL.P("@div",  mm.GetDiviziune())) > 0;
        }
    }
}
