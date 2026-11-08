using System;
using System.Collections.Generic;
using System.Text;

namespace GestiunePrezente.Common
{
    public static class CGeneratorNumarIntern
    {
        /// <summary>
        /// Rezervă următorul număr CONSECUTIV GLOBAL.
        /// Apelezi DUPĂ q.BeginTransaction() și ÎNAINTE de INSERT.
        /// Dacă tranzacția face rollback, contorul revine — fără goluri.
        /// </summary>
        public static int Next(CInterogareSQL q)
        {
            const string sql = @"
                UPDATE AD_SecventaNumarIntern WITH (ROWLOCK, HOLDLOCK)
                   SET ADSN_UltimulNr = ADSN_UltimulNr + 1
                OUTPUT INSERTED.ADSN_UltimulNr
                 WHERE ADSN_Id = 1;";
            return q.ExecuteToIntP(sql);
        }
    }
}