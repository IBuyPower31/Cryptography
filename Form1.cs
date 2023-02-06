namespace Cryptography
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.Indigo;
            button1.Text = "Зашифровать";
            button2.Text = "Расшифровать";
            radioButton1.ForeColor = Color.White;
            radioButton2.ForeColor = Color.White;
            radioButton3.ForeColor = Color.White;
            richTextBox2.ReadOnly = true;
        }

        private String CaesarEncrypt(String text)
        {
            return text;
        }

        private String MultiplicativeCipher(String text)
        {
            return text;
        }


        private String PlayfairCipher(String text)
        {
            return text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String text = richTextBox1.Text;
            if (radioButton1.Checked)
            {
                CaesarEncrypt(text);
                return;
            }
            if (radioButton2.Checked)
            {
                MultiplicativeCipher(text);
                return;
            }
            if (radioButton3.Checked)
            {
                PlayfairCipher(text);
            }

        }

    }
}