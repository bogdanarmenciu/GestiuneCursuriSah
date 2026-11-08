using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuConfigurare
{
    public partial class FrmGestiuneDrepturiMeniu : Form
    {
        readonly FrmMeniuPrincipal FrmParent;
        readonly CManagerDeProiect ManagerDeProiect;

        int m_lUtilizator = 0;
        int m_lDiviziune  = 0;
        bool m_bIgnoraEvenimente = false;
        bool m_bHeaderAcces = false;

        public FrmGestiuneDrepturiMeniu(FrmMeniuPrincipal formParent, CManagerDeProiect mm)
        {
            FrmParent = formParent;
            ManagerDeProiect = mm;
            InitializeComponent();
        }

        private void FrmGestiuneDrepturiMeniu_Load(object sender, EventArgs e)
        {
            ConfigureazaGriduri();
            ConfigureazaGrilaVoci();
            IncarcaDiviziuni();
        }

        // =========================================================
        //                   CONFIGURARE GRIDURI
        // =========================================================
        private void ConfigureazaGriduri()
        {
            // Grid utilizatori
            m_gUtilizatori.ReadOnly = false;
            m_gUtilizatori.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUtId", HeaderText = "Id", Visible = false
            });
            m_gUtilizatori.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colActiv", HeaderText = "Activ", Width = 46, ReadOnly = false
            });
            m_gUtilizatori.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNume", HeaderText = "Utilizator",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true
            });
            m_gUtilizatori.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            m_gUtilizatori.CellValueChanged += m_gUtilizatori_CellValueChanged;
            m_gUtilizatori.CurrentCellDirtyStateChanged += m_gUtilizatori_CurrentCellDirtyStateChanged;

            // Grid drepturi
            m_gDrepturi.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colAcces", HeaderText = "Acces", Width = 55, ReadOnly = false
            });
            m_gDrepturi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNume", HeaderText = "Voce meniu", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            m_gDrepturi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMenuId", HeaderText = "Id", Visible = false
            });
        }

        // =========================================================
        //                      INCARCARE DATE
        // =========================================================
        private void IncarcaDiviziuni()
        {
            m_cbDiviziune.Items.Clear();
            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP("SELECT MBDV_Id, MBDV_Diviziune FROM MB_Diviziune ORDER BY MBDV_Diviziune");
            while (q.NextRow())
                m_cbDiviziune.Items.Add(new DiviziuneItem(q.GetIntValue(0), q.GetStringValue(1) ?? ""));
            q.Close();

            // Selectează diviziunea curentă
            int divCurenta = ManagerDeProiect.GetDiviziune();
            for (int i = 0; i < m_cbDiviziune.Items.Count; i++)
            {
                if (m_cbDiviziune.Items[i] is DiviziuneItem d && d.Id == divCurenta)
                {
                    m_cbDiviziune.SelectedIndex = i;
                    return;
                }
            }
            if (m_cbDiviziune.Items.Count > 0)
                m_cbDiviziune.SelectedIndex = 0;
        }

        private void IncarcaUtilizatori()
        {
            m_gUtilizatori.Rows.Clear();
            m_gDrepturi.Rows.Clear();
            m_lUtilizator = 0;
            m_tsbSalveaza.Enabled = false;

            if (m_lDiviziune == 0) return;

            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(@"SELECT MBUT_Id, MBUT_InUz, ISNULL(MBUT_NumePrenume, MBUT_Login)
                        FROM MB_Utilizatori
                       ORDER BY MBUT_NumePrenume");
            while (q.NextRow())
            {
                bool activ = q.GetBoolValue(1);
                int  idx   = m_gUtilizatori.Rows.Add(q.GetIntValue(0), activ, q.GetStringValue(2));
                if (!activ)
                    m_gUtilizatori.Rows[idx].DefaultCellStyle.ForeColor = Color.Silver;
            }
            q.Close();
        }

        private void IncarcaDrepturi()
        {
            m_bIgnoraEvenimente = true;
            m_gDrepturi.Rows.Clear();
            m_tsbSalveaza.Enabled = false;

            if (m_lUtilizator == 0 || m_lDiviziune == 0)
            {
                m_bIgnoraEvenimente = false;
                return;
            }

            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(@"SELECT M.MBME_Id, M.MBME_Nume, ISNULL(M.MBME_MBME_Id, 0),
                             ISNULL(D.MBDM_DreptFolosinta, 0), ISNULL(M.MBME_Ascuns, 0),
                             CASE WHEN M.MBME_MBME_Id = 0 THEN 0
                                  WHEN P.MBME_MBME_Id  = 0 THEN 1
                                  ELSE 2
                             END AS Nivel
                        FROM MB_Meniu M
                        LEFT JOIN MB_Meniu P ON P.MBME_Id = M.MBME_MBME_Id
                        OUTER APPLY (
                            SELECT TOP 1 MBDM_DreptFolosinta
                              FROM MB_DrepturiMeniu
                             WHERE MBDM_MBME_Id = M.MBME_Id
                               AND MBDM_MBUT_Id = @user
                               AND (MBDM_MBDV_Id = @div OR MBDM_MBDV_Id IS NULL)
                             ORDER BY CASE WHEN MBDM_MBDV_Id = @div THEN 0 ELSE 1 END
                        ) D
                       ORDER BY M.MBME_Aria,
                                CASE WHEN M.MBME_MBME_Id = 0 THEN M.MBME_OrdineAfisare
                                     WHEN P.MBME_MBME_Id = 0 THEN P.MBME_OrdineAfisare
                                     ELSE ISNULL((SELECT MBME_OrdineAfisare FROM MB_Meniu WHERE MBME_Id = P.MBME_MBME_Id), 0)
                                END,
                                CASE WHEN M.MBME_MBME_Id = 0 THEN 0
                                     WHEN P.MBME_MBME_Id = 0 THEN M.MBME_OrdineAfisare
                                     ELSE P.MBME_OrdineAfisare
                                END,
                                M.MBME_OrdineAfisare",
                CInterogareSQL.P("@user", m_lUtilizator),
                CInterogareSQL.P("@div",  m_lDiviziune));
            while (q.NextRow())
            {
                int    menuId = q.GetIntValue(0);
                string nume   = q.GetStringValue(1) ?? "";
                int    parent = q.GetIntValue(2);
                bool   drept  = q.GetBoolValue(3);
                bool   ascuns = q.GetBoolValue(4);
                int    nivel  = q.GetIntValue(5);
                string label  = nivel switch { 1 => "    " + nume, 2 => "        " + nume, _ => nume };
                int    idx    = m_gDrepturi.Rows.Add(drept, label, menuId);
                if (ascuns)
                    m_gDrepturi.Rows[idx].DefaultCellStyle.ForeColor = Color.Silver;
            }
            q.Close();

            // stilizare: nivel 0 = antet principal (albastru), nivel 1 = sub-grup (gri deschis)
            foreach (DataGridViewRow row in m_gDrepturi.Rows)
            {
                string label = row.Cells["colNume"].Value as string ?? "";
                if (row.DefaultCellStyle.ForeColor == Color.Silver) continue;
                if (!label.StartsWith("    "))
                    row.DefaultCellStyle.BackColor = Color.FromArgb(220, 232, 255);
                else if (!label.StartsWith("        "))
                    row.DefaultCellStyle.BackColor = Color.FromArgb(240, 244, 255);
            }

            // Sincronizează checkbox-ul din header cu starea curentă
            int bifate = m_gDrepturi.Rows.Cast<DataGridViewRow>()
                .Count(r => !r.IsNewRow && Convert.ToBoolean(r.Cells["colAcces"].Value ?? false));
            m_bHeaderAcces = bifate == m_gDrepturi.Rows.Count;
            if (m_gDrepturi.Columns["colAcces"] is DataGridViewColumn col)
                m_gDrepturi.InvalidateCell(col.Index, -1);

            m_bIgnoraEvenimente = false;
        }

        private void SetToateAccesele(bool valoare)
        {
            if (m_gDrepturi.Rows.Count == 0) return;
            m_bIgnoraEvenimente = true;
            foreach (DataGridViewRow row in m_gDrepturi.Rows)
                if (!row.IsNewRow)
                    row.Cells["colAcces"].Value = valoare;
            m_bIgnoraEvenimente = false;
            m_tsbSalveaza.Enabled = true;
        }

        // =========================================================
        //                         SALVARE
        // =========================================================
        private void m_tsbSalveaza_Click(object sender, EventArgs e)
        {
            if (m_lUtilizator == 0) return;

            // Forțează commit pe celula activă înainte de a citi valorile
            m_gDrepturi.EndEdit();

            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            try
            {
                foreach (DataGridViewRow row in m_gDrepturi.Rows)
                {
                    if (row.IsNewRow) continue;
                    int  menuId = Convert.ToInt32(row.Cells["colMenuId"].Value ?? 0);
                    bool drept  = Convert.ToBoolean(row.Cells["colAcces"].Value ?? false);

                    q.ExecuteNonQueryPThrow(@"
IF EXISTS (SELECT 1 FROM MB_DrepturiMeniu
            WHERE MBDM_MBME_Id = @menu AND MBDM_MBUT_Id = @user
              AND (MBDM_MBDV_Id = @div OR MBDM_MBDV_Id IS NULL))
    UPDATE MB_DrepturiMeniu SET MBDM_DreptFolosinta = @val, MBDM_MBDV_Id = @div
     WHERE MBDM_MBME_Id = @menu AND MBDM_MBUT_Id = @user
       AND (MBDM_MBDV_Id = @div OR MBDM_MBDV_Id IS NULL);
ELSE
    INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_MBDV_Id, MBDM_DreptFolosinta)
    VALUES (@menu, @user, @div, @val);",
                        CInterogareSQL.P("@menu", menuId),
                        CInterogareSQL.P("@user", m_lUtilizator),
                        CInterogareSQL.P("@div",  m_lDiviziune),
                        CInterogareSQL.P("@val",  drept));
                }
                q.Close();
                m_tsbSalveaza.Enabled = false;
                MessageBox.Show("Drepturile au fost salvate.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                q.Close();
                MessageBox.Show("Eroare la salvare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================================
        //                       EVENIMENTE
        // =========================================================
        private void m_cbDiviziune_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_lDiviziune = (m_cbDiviziune.SelectedItem as DiviziuneItem)?.Id ?? 0;
            IncarcaUtilizatori();
        }

        private void m_gUtilizatori_SelectionChanged(object sender, EventArgs e)
        {
            if (m_gUtilizatori.CurrentRow == null) return;
            m_lUtilizator = Convert.ToInt32(m_gUtilizatori.CurrentRow.Cells["colUtId"].Value ?? 0);
            IncarcaDrepturi();
        }

        private void m_gUtilizatori_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (m_gUtilizatori.IsCurrentCellDirty)
                m_gUtilizatori.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void m_gUtilizatori_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (m_bIgnoraEvenimente) return;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != m_gUtilizatori.Columns["colActiv"]?.Index) return;

            var row   = m_gUtilizatori.Rows[e.RowIndex];
            int utId  = Convert.ToInt32(row.Cells["colUtId"].Value ?? 0);
            bool activ = Convert.ToBoolean(row.Cells["colActiv"].Value ?? false);

            try
            {
                using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
                q.ExecuteNonQueryPThrow(
                    "UPDATE MB_Utilizatori SET MBUT_InUz = @val WHERE MBUT_Id = @id",
                    CInterogareSQL.P("@val", activ),
                    CInterogareSQL.P("@id",  utId));
                q.Close();
                row.DefaultCellStyle.ForeColor = activ ? Color.Empty : Color.Silver;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la actualizare utilizator: " + ex.Message, "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_gDrepturi_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (m_gDrepturi.IsCurrentCellDirty)
                m_gDrepturi.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void m_gDrepturi_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex != -1 || e.ColumnIndex != m_gDrepturi.Columns["colAcces"]?.Index) return;

            e.PaintBackground(e.ClipBounds, true);

            var state = m_bHeaderAcces
                ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal
                : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;

            var cbSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, state);
            var cbLoc  = new Point(
                e.CellBounds.X + (e.CellBounds.Width  - cbSize.Width)  / 2,
                e.CellBounds.Y + (e.CellBounds.Height - cbSize.Height) / 2);
            CheckBoxRenderer.DrawCheckBox(e.Graphics, cbLoc, state);
            e.Handled = true;
        }

        private void m_gDrepturi_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != m_gDrepturi.Columns["colAcces"]?.Index) return;
            m_bHeaderAcces = !m_bHeaderAcces;
            SetToateAccesele(m_bHeaderAcces);
            m_gDrepturi.InvalidateCell(e.ColumnIndex, -1);
        }

        private void m_tsbUtilizatorNou_Click(object sender, EventArgs e)
        {
            if (!FrmGestiuneUtilizator.PoateDeschide(ManagerDeProiect)) return;
            var frm = new FrmGestiuneUtilizator(ManagerDeProiect);
            frm.ShowDialog(this);
            IncarcaUtilizatori();
        }

        private void m_gUtilizatori_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int utId = Convert.ToInt32(m_gUtilizatori.Rows[e.RowIndex].Cells["colUtId"].Value ?? 0);
            if (utId == 0) return;
            if (!FrmGestiuneUtilizator.PoateDeschide(ManagerDeProiect)) return;
            var frm = new FrmGestiuneUtilizator(ManagerDeProiect, utId);
            frm.ShowDialog(this);
            IncarcaUtilizatori();
        }

        private void m_gDrepturi_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (m_bIgnoraEvenimente) return;
            if (e.ColumnIndex == m_gDrepturi.Columns["colAcces"]?.Index)
                m_tsbSalveaza.Enabled = true;
        }

        // =========================================================
        //                  TAB VOCI MENIU
        // =========================================================
        private static readonly (string Cod, string Denumire)[] Arii =
        {
            ("1CU", "Meniu cursuri"),
            ("0CG", "Setări / Configurare"),
        };

        private void ConfigureazaGrilaVoci()
        {
            m_gVoci.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colVMId", HeaderText = "Id", Visible = false
            });

            var colAscuns = new DataGridViewCheckBoxColumn
            {
                Name = "colVMAscuns", HeaderText = "Ascuns", Width = 65
            };
            m_gVoci.Columns.Add(colAscuns);

            m_gVoci.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colVMNume", HeaderText = "Nume voce",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, MinimumWidth = 180
            });

            var colAria = new DataGridViewComboBoxColumn
            {
                Name = "colVMAria", HeaderText = "Arie", Width = 160,
                DisplayStyleForCurrentCellOnly = true
            };
            foreach (var (cod, den) in Arii) colAria.Items.Add($"{cod}|{den}");
            m_gVoci.Columns.Add(colAria);

            m_gVoci.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colVMParinteId", HeaderText = "Id parinte", Visible = false
            });
            m_gVoci.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colVMParinteNume", HeaderText = "Parinte", Width = 180, ReadOnly = true
            });
            m_gVoci.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colVMOrdine", HeaderText = "Ordine", Width = 70
            });
        }

        private void m_tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_tabs.SelectedTab == m_tabVoci && m_gVoci.Rows.Count == 0)
                IncarcaVociMeniu();
        }

        private void IncarcaVociMeniu()
        {
            m_gVoci.Rows.Clear();
            var parinti = new Dictionary<int, string>();

            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            // Încarc mai întâi vocile parinte pentru a le folosi la afișare
            q.OpenP(@"SELECT MBME_Id, MBME_Nume FROM MB_Meniu WHERE MBME_MBME_Id = 0 ORDER BY MBME_Aria, MBME_OrdineAfisare");
            while (q.NextRow())
                parinti[q.GetIntValue(0)] = q.GetStringValue(1) ?? "";
            q.Close();

            q.OpenP(@"SELECT MBME_Id, MBME_Nume, MBME_Aria, MBME_MBME_Id,
                             MBME_OrdineAfisare, MBME_Ascuns
                        FROM MB_Meniu
                       ORDER BY MBME_Aria, MBME_MBME_Id, MBME_OrdineAfisare");
            while (q.NextRow())
            {
                int    id     = q.GetIntValue(0);
                string nume   = q.GetStringValue(1) ?? "";
                string aria   = q.GetStringValue(2) ?? "";
                int    parId  = q.GetIntValue(3);
                int    ordine = q.GetIntValue(4);
                bool   ascuns = q.GetBoolValue(5);

                string ariaItem = Arii.FirstOrDefault(a => a.Cod == aria) is var found && found.Cod != null
                    ? $"{found.Cod}|{found.Denumire}" : aria;
                string parNume = parId > 0 ? (parinti.TryGetValue(parId, out var pn) ? pn : $"Id {parId}") : "(nivel top)";

                int idx = m_gVoci.Rows.Add(id, ascuns, parId > 0 ? "    " + nume : nume,
                                            ariaItem, parId, parNume, ordine);
                if (ascuns)
                    m_gVoci.Rows[idx].DefaultCellStyle.ForeColor = Color.Silver;
            }
            q.Close();
            m_tsbSalveazaVoci.Enabled = false;
        }

        private void m_tsbVoceNoua_Click(object sender, EventArgs e)
        {
            string ariaDefault = Arii.Length > 0 ? $"{Arii[0].Cod}|{Arii[0].Denumire}" : "";
            int idx = m_gVoci.Rows.Add(0, false, "Voce nouă", ariaDefault, 0, "(nivel top)", 0);
            m_gVoci.ClearSelection();
            m_gVoci.Rows[idx].Selected = true;
            m_gVoci.CurrentCell = m_gVoci.Rows[idx].Cells["colVMNume"];
            m_gVoci.BeginEdit(true);
            m_tsbSalveazaVoci.Enabled = true;
        }

        private void m_tsbSalveazaVoci_Click(object sender, EventArgs e)
        {
            m_gVoci.EndEdit();
            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            try
            {
                foreach (DataGridViewRow row in m_gVoci.Rows)
                {
                    if (row.IsNewRow) continue;
                    int    id     = Convert.ToInt32(row.Cells["colVMId"].Value ?? 0);
                    string nume   = (row.Cells["colVMNume"].Value as string ?? "").Trim().TrimStart();
                    string ariaIt = row.Cells["colVMAria"].Value as string ?? "";
                    string aria   = ariaIt.Contains('|') ? ariaIt.Split('|')[0] : ariaIt;
                    int    parId  = Convert.ToInt32(row.Cells["colVMParinteId"].Value ?? 0);
                    int    ordine = Convert.ToInt32(row.Cells["colVMOrdine"].Value ?? 0);
                    bool   ascuns = Convert.ToBoolean(row.Cells["colVMAscuns"].Value ?? false);

                    if (string.IsNullOrWhiteSpace(nume)) continue;

                    if (id == 0)
                    {
                        int newId = q.ExecuteToIntPThrow(@"
INSERT INTO MB_Meniu (MBME_Nume, MBME_MBME_Id, MBME_Aria, MBME_OrdineAfisare, MBME_Ascuns)
OUTPUT INSERTED.MBME_Id
VALUES (@nome, @par, @aria, @ord, @asc);",
                            CInterogareSQL.P("@nome", nume),
                            CInterogareSQL.P("@par",  parId),
                            CInterogareSQL.P("@aria", aria),
                            CInterogareSQL.P("@ord",  ordine),
                            CInterogareSQL.P("@asc",  ascuns));
                        row.Cells["colVMId"].Value = newId;
                    }
                    else
                    {
                        q.ExecuteNonQueryPThrow(@"
UPDATE MB_Meniu SET MBME_Nume = @nome, MBME_MBME_Id = @par, MBME_Aria = @aria,
                    MBME_OrdineAfisare = @ord, MBME_Ascuns = @asc
 WHERE MBME_Id = @id;",
                            CInterogareSQL.P("@nome", nume),
                            CInterogareSQL.P("@par",  parId),
                            CInterogareSQL.P("@aria", aria),
                            CInterogareSQL.P("@ord",  ordine),
                            CInterogareSQL.P("@asc",  ascuns),
                            CInterogareSQL.P("@id",   id));
                    }
                    row.DefaultCellStyle.ForeColor = ascuns ? Color.Silver : Color.Empty;
                }
                q.Close();
                m_tsbSalveazaVoci.Enabled = false;
                // Reîncarcă grida de drepturi dacă era vizibilă
                if (m_lUtilizator > 0) IncarcaDrepturi();
            }
            catch (Exception ex)
            {
                q.Close();
                MessageBox.Show("Eroare la salvare voci: " + ex.Message, "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_gVoci_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (m_gVoci.IsCurrentCellDirty)
                m_gVoci.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void m_gVoci_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) m_tsbSalveazaVoci.Enabled = true;
        }

        // =========================================================
        //                     INNER CLASSES
        // =========================================================
        private sealed class DiviziuneItem
        {
            public int Id { get; init; }
            private string Display { get; init; }
            public DiviziuneItem(int id, string display) { Id = id; Display = display; }
            public override string ToString() => Display;
        }
    }
}
