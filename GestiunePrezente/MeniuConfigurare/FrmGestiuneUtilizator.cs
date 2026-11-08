using GestiunePrezente.Common;

namespace GestiunePrezente.MeniuConfigurare
{
    public partial class FrmGestiuneUtilizator : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        int m_lMBUT = 0;
        bool m_bIgnora = false;

        public const string MBME_NUME = "Gestiune Utilizator";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmGestiuneUtilizator(CManagerDeProiect mm, int lMBUT = 0)
        {
            ManagerDeProiect = mm;
            m_lMBUT = lMBUT;
            InitializeComponent();
        }

        private void FrmGestiuneUtilizator_Load(object sender, EventArgs e)
        {
            Text = m_lMBUT == 0 ? "Utilizator nou" : "Editare utilizator";
            ConfigureazaGrila();
            IncarcaDiviziuni();
            if (m_lMBUT > 0)
                IncarcaDate();
            else
            {
                m_ckActiv.Checked = true;
                m_lblParola.Text = "Parolă: *";
            }
            m_tsbSalveaza.Enabled = false;
        }

        // =========================================================
        //                   CONFIGURARE GRILĂ
        // =========================================================
        private void ConfigureazaGrila()
        {
            m_gRoluri.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDivId", HeaderText = "Id", Visible = false
            });
            m_gRoluri.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDivNume", HeaderText = "Diviziune",
                ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            m_gRoluri.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colAdmin", HeaderText = "Administrator", Width = 110
            });
            m_gRoluri.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colDirector", HeaderText = "Director", Width = 80
            });
            m_gRoluri.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colSecretar", HeaderText = "Secretariat", Width = 90
            });
        }

        private void IncarcaDiviziuni()
        {
            m_bIgnora = true;

            // Combo conectare — 0 = oricare
            m_cbConectare.Items.Clear();
            m_cbConectare.Items.Add(new DivItem(0, "(oricare diviziune)"));

            // Combo div. implicită — 0 = niciuna
            m_cbDivImplicita.Items.Clear();
            m_cbDivImplicita.Items.Add(new DivItem(0, "(niciuna)"));

            m_gRoluri.Rows.Clear();
            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP("SELECT MBDV_Id, MBDV_Diviziune FROM MB_Diviziune ORDER BY MBDV_Diviziune");
            while (q.NextRow())
            {
                int id = q.GetIntValue(0); string den = q.GetStringValue(1) ?? "";
                m_cbConectare.Items.Add(new DivItem(id, den));
                m_cbDivImplicita.Items.Add(new DivItem(id, den));
                m_gRoluri.Rows.Add(id, den, false, false, false);
            }
            q.Close();

            m_cbConectare.SelectedIndex   = 0;
            m_cbDivImplicita.SelectedIndex = 0;
            m_bIgnora = false;
        }

        private sealed class DivItem
        {
            public int Id { get; }
            readonly string _display;
            public DivItem(int id, string display) { Id = id; _display = display; }
            public override string ToString() => _display;
        }

        private void SelecteazaComboDiv(ComboBox cb, int id)
        {
            for (int i = 0; i < cb.Items.Count; i++)
                if (cb.Items[i] is DivItem d && d.Id == id) { cb.SelectedIndex = i; return; }
            cb.SelectedIndex = 0;
        }

        // =========================================================
        //                     ÎNCĂRCARE DATE
        // =========================================================
        private void IncarcaDate()
        {
            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());

            // Date utilizator
            q.OpenP(@"SELECT MBUT_Login, ISNULL(MBUT_NumePrenume,''), ISNULL(MBUT_Email,''),
                             MBUT_InUz, ISNULL(MBUT_ConectareLaDiviziune,0), ISNULL(MBUT_MBDV_Id,0)
                        FROM MB_Utilizatori WHERE MBUT_Id = @id",
                CInterogareSQL.P("@id", m_lMBUT));
            if (q.NextRow())
            {
                m_bIgnora = true;
                m_ebLogin.Text       = q.GetStringValue(0) ?? "";
                m_ebNumePrenume.Text = q.GetStringValue(1) ?? "";
                m_ebEmail.Text       = q.GetStringValue(2) ?? "";
                m_ckActiv.Checked    = q.GetBoolValue(3);
                m_ebParola.PlaceholderText = "(lasă gol pentru a nu schimba)";
                SelecteazaComboDiv(m_cbConectare,    q.GetIntValue(4));
                SelecteazaComboDiv(m_cbDivImplicita, q.GetIntValue(5));
                m_bIgnora = false;
            }
            q.Close();

            // Roluri
            m_bIgnora = true;
            q.OpenP(@"SELECT MBRU_MBDV_Id, MBRU_Administrator, MBRU_FlagDirector, MBRU_FlagSecretariat
                        FROM MB_RoluriUtilizatori
                       WHERE MBRU_MBUT_Id = @id",
                CInterogareSQL.P("@id", m_lMBUT));
            var roluri = new List<(int div, bool admin, bool dir, bool sec)>();
            while (q.NextRow())
                roluri.Add((q.GetIntValue(0), q.GetBoolValue(1), q.GetBoolValue(2), q.GetBoolValue(3)));
            q.Close();

            foreach (DataGridViewRow row in m_gRoluri.Rows)
            {
                int divId = Convert.ToInt32(row.Cells["colDivId"].Value ?? 0);
                var rol = roluri.FirstOrDefault(r => r.div == divId);
                row.Cells["colAdmin"].Value    = rol.admin;
                row.Cells["colDirector"].Value = rol.dir;
                row.Cells["colSecretar"].Value = rol.sec;
            }
            m_bIgnora = false;
        }

        // =========================================================
        //                         SALVARE
        // =========================================================
        private void m_tsbSalveaza_Click(object sender, EventArgs e)
        {
            if (!Valideaza()) return;
            if (Salveaza())
            {
                m_tsbSalveaza.Enabled = false;
                if (m_lMBUT == 0)
                    MessageBox.Show("Utilizatorul a fost creat.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool Valideaza()
        {
            if (string.IsNullOrWhiteSpace(m_ebLogin.Text))
            {
                MessageBox.Show("Login-ul este obligatoriu.", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_ebLogin.Focus();
                return false;
            }
            if (m_lMBUT == 0 && string.IsNullOrWhiteSpace(m_ebParola.Text))
            {
                MessageBox.Show("Parola este obligatorie la crearea unui utilizator nou.", "Validare",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_ebParola.Focus();
                return false;
            }
            return true;
        }

        private bool Salveaza()
        {
            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            try
            {
                bool schimbaParola = !string.IsNullOrWhiteSpace(m_ebParola.Text);

                int conectareDiv = (m_cbConectare.SelectedItem  as DivItem)?.Id ?? 0;
                int divImplicita = (m_cbDivImplicita.SelectedItem as DivItem)?.Id ?? 0;

                if (m_lMBUT == 0)
                {
                    // INSERT utilizator nou
                    m_lMBUT = q.ExecuteToIntPThrow(@"
INSERT INTO MB_Utilizatori (MBUT_MBGR_Id, MBUT_Login, MBUT_Pass, MBUT_InUz,
                             MBUT_NumePrenume, MBUT_Email,
                             MBUT_ConectareLaDiviziune, MBUT_MBDV_Id, MBUT_MBSO_Id)
OUTPUT INSERTED.MBUT_Id
VALUES (1, @login, @pass, @activ, @nume, @email,
        NULLIF(@conectare,0), NULLIF(@divImpl,0),
        (SELECT TOP 1 MBSO_Id FROM MB_Societate));",
                        CInterogareSQL.P("@login",    m_ebLogin.Text.Trim()),
                        CInterogareSQL.P("@pass",     m_ebParola.Text),
                        CInterogareSQL.P("@activ",    m_ckActiv.Checked),
                        CInterogareSQL.P("@nume",     Nul(m_ebNumePrenume.Text)),
                        CInterogareSQL.P("@email",    Nul(m_ebEmail.Text)),
                        CInterogareSQL.P("@conectare", conectareDiv),
                        CInterogareSQL.P("@divImpl",   divImplicita));
                }
                else
                {
                    // UPDATE utilizator existent
                    var pars = new List<Microsoft.Data.SqlClient.SqlParameter>
                    {
                        CInterogareSQL.P("@login",    m_ebLogin.Text.Trim()),
                        CInterogareSQL.P("@activ",    m_ckActiv.Checked),
                        CInterogareSQL.P("@nume",     Nul(m_ebNumePrenume.Text)),
                        CInterogareSQL.P("@email",    Nul(m_ebEmail.Text)),
                        CInterogareSQL.P("@conectare", conectareDiv),
                        CInterogareSQL.P("@divImpl",   divImplicita),
                        CInterogareSQL.P("@id",       m_lMBUT),
                    };
                    string sqlPass = "";
                    if (schimbaParola)
                    {
                        sqlPass = ", MBUT_Pass = @pass";
                        pars.Add(CInterogareSQL.P("@pass", m_ebParola.Text));
                    }
                    q.ExecuteNonQueryPThrow($@"
UPDATE MB_Utilizatori SET
  MBUT_Login                = @login{sqlPass},
  MBUT_InUz                 = @activ,
  MBUT_NumePrenume          = @nume,
  MBUT_Email                = @email,
  MBUT_ConectareLaDiviziune = NULLIF(@conectare,0),
  MBUT_MBDV_Id              = NULLIF(@divImpl,0)
 WHERE MBUT_Id = @id;",
                        pars.ToArray());
                }

                // Salvare roluri per diviziune
                foreach (DataGridViewRow row in m_gRoluri.Rows)
                {
                    if (row.IsNewRow) continue;
                    int  divId  = Convert.ToInt32(row.Cells["colDivId"].Value ?? 0);
                    bool admin  = Convert.ToBoolean(row.Cells["colAdmin"].Value ?? false);
                    bool dir    = Convert.ToBoolean(row.Cells["colDirector"].Value ?? false);
                    bool sec    = Convert.ToBoolean(row.Cells["colSecretar"].Value ?? false);

                    q.ExecuteNonQueryPThrow(@"
IF EXISTS (SELECT 1 FROM MB_RoluriUtilizatori WHERE MBRU_MBUT_Id = @u AND MBRU_MBDV_Id = @div)
    UPDATE MB_RoluriUtilizatori SET
      MBRU_Administrator   = @admin,
      MBRU_FlagDirector    = @dir,
      MBRU_FlagSecretariat = @sec
     WHERE MBRU_MBUT_Id = @u AND MBRU_MBDV_Id = @div;
ELSE
    INSERT INTO MB_RoluriUtilizatori (MBRU_MBUT_Id, MBRU_MBDV_Id, MBRU_Administrator, MBRU_FlagDirector, MBRU_FlagSecretariat)
    VALUES (@u, @div, @admin, @dir, @sec);",
                        CInterogareSQL.P("@u",     m_lMBUT),
                        CInterogareSQL.P("@div",   divId),
                        CInterogareSQL.P("@admin", admin),
                        CInterogareSQL.P("@dir",   dir),
                        CInterogareSQL.P("@sec",   sec));
                }

                q.Close();
                Text = "Editare utilizator";
                return true;
            }
            catch (Exception ex)
            {
                q.Close();
                MessageBox.Show("Eroare la salvare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static object Nul(string s)
            => string.IsNullOrWhiteSpace(s) ? DBNull.Value : s.Trim();

        // =========================================================
        //                       EVENIMENTE
        // =========================================================
        private void m_eb_Changed(object sender, EventArgs e)
        {
            if (!m_bIgnora) m_tsbSalveaza.Enabled = true;
        }

        private void m_gRoluri_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (m_gRoluri.IsCurrentCellDirty)
                m_gRoluri.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void m_gRoluri_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!m_bIgnora && e.RowIndex >= 0) m_tsbSalveaza.Enabled = true;
        }
    }
}
