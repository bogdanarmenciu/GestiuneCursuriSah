using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuConfigurare
{
    public partial class FrmCautareSocietate : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        string? m_sSql;

        public FrmCautareSocietate(FrmMeniuPrincipal FormParent, CManagerDeProiect MM)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;

            InitializeComponent();

            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            m_gSocietate.ReadOnly = true;

            Cauta();
        }

        private void m_tsbCautare_Click(object sender, EventArgs e)
        {
            Cauta();
        }

        private void Cauta()
        {
            m_sSql = @"SELECT MBSO_Id, MBSO_Descriere, MBSO_CUI, MBSO_J, MBSO_Strada, MBSO_NumarStrada, MBSO_Bloc, MBSO_Scara, MBSO_Apartament, 
                MBSO_CodPostal, MBSO_Oras, MBSO_Judet, MBSO_Tara, MBSO_Banca, MBSO_Cont, MBSO_Reprezentant, MBSO_TelReprezentant, 
                MBSO_EmailReprezentant, MBSO_FunctieReprezentant, MBSO_CISerieReprezentant, MBSO_CINumarReprezentant, MBSO_CNPReprezentant, 
                MBSO_EmailSocietate, MBSO_TelefonSocietate, MBSO_ResponsabilDiplome, MBSO_ResponsabilChitante, MBSO_ServerIP
                FROM MB_Societate";

            m_gSocietate.SetSql(m_sSql);

            m_gSocietate.FormatColumn("Id", 50, bVisible: false);
            m_gSocietate.FormatColumn("Societate", 200);
            m_gSocietate.FormatColumn("CUI", 100);
            m_gSocietate.FormatColumn("J", 100);
            m_gSocietate.FormatColumn("Strada", 100);
            m_gSocietate.FormatColumn("Numar strada", 100);
            m_gSocietate.FormatColumn("Bloc", 100);
            m_gSocietate.FormatColumn("Scara", 100);
            m_gSocietate.FormatColumn("Ap.", 100);
            m_gSocietate.FormatColumn("Cod Postal", 100);
            m_gSocietate.FormatColumn("Oras", 100);
            m_gSocietate.FormatColumn("Judet", 100);
            m_gSocietate.FormatColumn("Tara", 100);
            m_gSocietate.FormatColumn("Banca", 100);
            m_gSocietate.FormatColumn("Cont", 100);
            m_gSocietate.FormatColumn("Reprezentant", 100);
            m_gSocietate.FormatColumn("Telefon Reprezentant", 100);
            m_gSocietate.FormatColumn("Email Reprezentant", 100);
            m_gSocietate.FormatColumn("Functie Reprezentant", 100);
            m_gSocietate.FormatColumn("CISerie Reprezentant", 100);
            m_gSocietate.FormatColumn("CINumar Reprezentant", 100);
            m_gSocietate.FormatColumn("CNP Reprezentant", 100);
            m_gSocietate.FormatColumn("Email Societate", 100);
            m_gSocietate.FormatColumn("Telefon Societate", 100);
            m_gSocietate.FormatColumn("Director diplome", 100);
            m_gSocietate.FormatColumn("Casier chitante", 100);
            m_gSocietate.FormatColumn("IP Server", 100);

            m_gSocietate.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }


        private void m_gSocietate_DoubleClick(object sender, EventArgs e)
        {
            int lMBSO;
            object value = m_gSocietate.GetColumnData("MBSO_Id")!;
            if (value != null)
            {
                lMBSO = Convert.ToInt32(value);
                if (!FrmGestiuneSocietate.PoateDeschide(ManagerDeProiect)) return;
                FrmGestiuneSocietate frm = new(FrmParent, ManagerDeProiect, lMBSO)
                {
                    MdiParent = FrmParent
                };
                frm.Show();
            }
        }
    }
}
