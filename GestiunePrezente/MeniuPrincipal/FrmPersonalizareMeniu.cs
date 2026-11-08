using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestiunePrezente.MeniuPrincipal
{
    internal class FrmPersonalizareMeniu : Form
    {
        private readonly FrmMeniuPrincipal m_owner;
        private Color m_accent;
        private float m_fontSize;

        // Preset culori accent
        private static readonly (string Label, Color Value)[] Presets =
        [
            ("Albastru",   Color.FromArgb(37,  99,  235)),
            ("Indigo",     Color.FromArgb(99,  102, 241)),
            ("Violet",     Color.FromArgb(139,  92, 246)),
            ("Roz",        Color.FromArgb(236,  72, 153)),
            ("Roșu",       Color.FromArgb(220,  38,  38)),
            ("Portocaliu", Color.FromArgb(234,  88,  12)),
            ("Galben",     Color.FromArgb(202, 138,   4)),
            ("Verde",      Color.FromArgb(22,  163,  74)),
            ("Cyan",       Color.FromArgb(8,   145, 178)),
            ("Gri",        Color.FromArgb(107, 114, 128)),
        ];

        public FrmPersonalizareMeniu(FrmMeniuPrincipal owner, Color currentAccent, float currentFontSize)
        {
            m_owner   = owner;
            m_accent  = currentAccent;
            m_fontSize = currentFontSize;
            BuildUI();
        }

        private void BuildUI()
        {
            Text            = "Personalizare meniu";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox     = false;
            MinimizeBox     = false;
            StartPosition   = FormStartPosition.CenterParent;
            ClientSize      = new Size(360, 280);
            BackColor       = Color.White;
            Font            = new Font("Segoe UI", 9.5F);

            // ── Titlu culoare accent ─────────────────────────────────
            var lblCuloare = new Label
            {
                Text     = "Culoare accent",
                Location = new Point(16, 16),
                AutoSize = true,
                Font     = new Font("Segoe UI Semibold", 9.5F)
            };
            Controls.Add(lblCuloare);

            // Grid preset-uri
            var presetPanel = new FlowLayoutPanel
            {
                Location    = new Point(16, 38),
                Size        = new Size(328, 86),
                AutoSize    = false,
                WrapContents = true,
                Padding     = new Padding(0),
            };
            foreach (var (lbl, clr) in Presets)
            {
                var swatch = new Button
                {
                    Size        = new Size(28, 28),
                    BackColor   = clr,
                    FlatStyle   = FlatStyle.Flat,
                    Margin      = new Padding(3),
                    Cursor      = Cursors.Hand,
                    UseVisualStyleBackColor = false,
                    Tag         = clr,
                    AccessibleDescription = lbl
                };
                swatch.FlatAppearance.BorderSize        = 2;
                swatch.FlatAppearance.BorderColor       = clr == m_accent ? Color.Black : clr;
                swatch.Click += (_, _) =>
                {
                    m_accent = (Color)swatch.Tag!;
                    foreach (Button sw in presetPanel.Controls)
                        sw.FlatAppearance.BorderColor = (Color)sw.Tag! == m_accent ? Color.Black : (Color)sw.Tag!;
                };
                presetPanel.Controls.Add(swatch);
            }
            Controls.Add(presetPanel);

            // Buton culoare personalizată
            var btnCustom = new Button
            {
                Text      = "Altă culoare...",
                Location  = new Point(16, 130),
                Size      = new Size(140, 28),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = Color.FromArgb(55, 65, 81),
                Cursor    = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btnCustom.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnCustom.Click += (_, _) =>
            {
                using var cd = new ColorDialog { Color = m_accent, FullOpen = true };
                if (cd.ShowDialog(this) == DialogResult.OK)
                {
                    m_accent = cd.Color;
                    foreach (Button sw in presetPanel.Controls)
                        sw.FlatAppearance.BorderColor = (Color)sw.Tag! == m_accent ? Color.Black : (Color)sw.Tag!;
                }
            };
            Controls.Add(btnCustom);

            // ── Dimensiune font ──────────────────────────────────────
            var lblFont = new Label
            {
                Text     = "Dimensiune font meniu",
                Location = new Point(16, 172),
                AutoSize = true,
                Font     = new Font("Segoe UI Semibold", 9.5F)
            };
            Controls.Add(lblFont);

            var nudFont = new NumericUpDown
            {
                Location      = new Point(16, 194),
                Size          = new Size(80, 26),
                Minimum       = 8,
                Maximum       = 20,
                DecimalPlaces = 1,
                Increment     = 0.5m,
                Value         = (decimal)m_fontSize
            };
            nudFont.ValueChanged += (_, _) => m_fontSize = (float)nudFont.Value;
            Controls.Add(nudFont);

            var lblPt = new Label
            {
                Text     = "pt",
                Location = new Point(100, 198),
                AutoSize = true,
                ForeColor = Color.FromArgb(107, 114, 128)
            };
            Controls.Add(lblPt);

            // ── Butoane OK / Anulare ─────────────────────────────────
            var btnOk = new Button
            {
                Text         = "Salvează",
                DialogResult = DialogResult.OK,
                Location     = new Point(ClientSize.Width - 200, ClientSize.Height - 44),
                Size         = new Size(90, 32),
                BackColor    = Color.FromArgb(37, 99, 235),
                ForeColor    = Color.White,
                FlatStyle    = FlatStyle.Flat,
                Cursor       = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Click += (_, _) =>
            {
                m_owner.SalveazaPreferinteUtilizator(m_accent, m_fontSize);
                DialogResult = DialogResult.OK;
                Close();
            };
            Controls.Add(btnOk);

            var btnCancel = new Button
            {
                Text         = "Anulează",
                DialogResult = DialogResult.Cancel,
                Location     = new Point(ClientSize.Width - 104, ClientSize.Height - 44),
                Size         = new Size(88, 32),
                BackColor    = Color.FromArgb(248, 250, 252),
                ForeColor    = Color.FromArgb(55, 65, 81),
                FlatStyle    = FlatStyle.Flat,
                Cursor       = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            Controls.Add(btnCancel);

            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }
    }
}
