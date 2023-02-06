namespace Cryptography
{
    public partial class Form1 : Form
    {
        


        public const string Alphabet = "אבגדהו¸זחטיךכלםמןנסעףפץצקרשתüû‎‏ÿ_,.";
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.Indigo;
            button1.Text = "Çארטפנמגאעü";
            button2.Text = "Ðאסרטפנמגאעü";
            radioButton1.ForeColor = Color.White;
            radioButton2.ForeColor = Color.White;
            radioButton3.ForeColor = Color.White;
            radioButton1.Text = "Àההטעטגםûי רטפנ";
            radioButton2.Text = "Ìףכüעטןכטךאעטגםûי רטפנ";
            radioButton3.Text = "Øטפנ Ïכויפונא";
            richTextBox2.ReadOnly = true;
            textBox2.ReadOnly = true;
        }

        private String CaesarEncrypt(String text, int K)
        {
            String coded = "";
            int index = 0;
            for (int i = 0; i < text.Length; i++)
            {
                index = (Alphabet.IndexOf(text[i]) + K) % 36;
                //MessageBox.Show(index.ToString());
                coded = coded + Alphabet[index];
            }
            return coded;
        }

        private void FindPair(int K)
        {
            for (int i = 0; i < 36; i++)
            {
                if ((K * i) % 36 == 1)
                {
                    textBox2.Text = i.ToString();
                    return;
                }
            }
        }

        private String MultiplicativeCipher(String text, int K)
        {
            FindPair(K);
            string coded = "";
            int index = 0;
            for (int i = 0; i < text.Length; i++)
            {
                index = (Alphabet.IndexOf(text[i]) * K) % 36;
                coded += Alphabet[index];
            }
            return coded;
        }


        private String PlayfairCipher(String text)
        {
            return text;
        }

        private Dictionary<char, float> SymbolNChance(String text)
        {
            var characterCount = new Dictionary<char, float>();
            foreach (var c in text)
            {
                if (characterCount.ContainsKey(c))
                    characterCount[c]++;
                else
                    characterCount[c] = 1;
            }
            foreach (var c in characterCount.Keys)
            {
                characterCount[c] = characterCount[c] / 36;
            }
            return characterCount;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String text = richTextBox1.Text;
            int K = Convert.ToInt32(textBox1.Text);
            if (radioButton1.Checked)
            {
                var dict = SymbolNChance(text);
                dataGridView1.DataSource = dict.ToArray();
                dataGridView1.Refresh();
                String coded = CaesarEncrypt(text, K);
                richTextBox2.Text = coded;
                return;
            }
            if (radioButton2.Checked)
            {
                var dict = SymbolNChance(text);
                dataGridView1.DataSource = dict.ToArray();
                dataGridView1.Refresh();
                String coded = MultiplicativeCipher(text, K);
                richTextBox2.Text = coded;
                return;
            }
            if (radioButton3.Checked)
            {
                PlayfairCipher(text);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String text = richTextBox1.Text;
            int K = Convert.ToInt32(textBox1.Text);
            if (radioButton1.Checked)
            {
                K = 36 - Math.Abs(K);
                String coded = CaesarEncrypt(text, K);
                richTextBox2.Text = coded;
                return;
            }
            if (radioButton2.Checked)
            {
                MultiplicativeCipher(text, K);
                return;
            }
            if (radioButton3.Checked)
            {
                PlayfairCipher(text);
            }
        }
    }
}