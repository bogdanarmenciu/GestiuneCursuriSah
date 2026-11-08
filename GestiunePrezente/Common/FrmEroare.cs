namespace GestiunePrezente.Common
{
    public partial class FrmEroare : Form
    {
        public FrmEroare(string titlu, string mesaj)
        {
            InitializeComponent();
            Text           = titlu;
            m_tbMesaj.Text = mesaj;
        }

        private void m_btnCopie_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(m_tbMesaj.Text);
            m_btnCopie.Text = "✓ Copiat!";
        }

        private void m_btnInchide_Click(object sender, EventArgs e) => Close();
    }

    /// <summary>
    /// Apel rapid din orice formă: CEroare.Arata("mesaj") sau CEroare.Arata("titlu", "mesaj")
    /// </summary>
    public static class CEroare
    {
        public static void Arata(string mesaj)
            => Arata("Eroare", mesaj);

        public static void Arata(string titlu, string mesaj)
        {
            using var frm = new FrmEroare(titlu, mesaj);
            frm.ShowDialog();
        }
    }
}
