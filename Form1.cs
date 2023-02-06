namespace Cryptography
{
    public partial class Form1 : Form
    {
        


        public const string Alphabet = "абвгдеёжзийклмнопрстуфхцчшщъьыэюя_,.";
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.Indigo;
            button1.Text = "Зашифровать";
            button2.Text = "Расшифровать";
            radioButton1.ForeColor = Color.White;
            radioButton2.ForeColor = Color.White;
            radioButton3.ForeColor = Color.White;
            radioButton1.Text = "Аддитивный шифр";
            radioButton2.Text = "Мультипликативный шифр";
            radioButton3.Text = "Шифр Плейфера";
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
            if (textBox2.Text == "")
            {
                MessageBox.Show("Выберите другое число, пары к этому не найдено!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return coded;
            }
            int index = 0;
            for (int i = 0; i < text.Length; i++)
            {
                index = (Alphabet.IndexOf(text[i]) * K) % 36;
                coded += Alphabet[index];
            }
            return coded;
        }
        public void Print(char[,] matrix)
        {
            string Message = "";
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Message = Message + matrix[i, j] + "  ";
                }
                Message += "\n";
            }
            MessageBox.Show(Message);
        }

        private bool IsExist(char[,] matrix, char elem)
        {
            for (int a = 0; a < 6; a++)
                for (int b = 0; b < 6; b++)
                {
                    if (matrix[a, b] == elem)
                        return true;
                }
            return false;

        }

        private void InitMatrix(char[,] matrix)
        {
            String word = textBox3.Text;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    matrix[i, j] = '-';
            int l = 0;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    matrix[i, j] = word[l];
                    l = l + 1;
                    if (l == word.Length - 1)
                    {
                        goto next;
                    }
                }

            next:  int index = 0;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    check:  if (matrix[i, j] == '-')
                    {
                        if (IsExist(matrix, Alphabet[index]))
                        {
                            index += 1;
                            goto check;
                        }
                        else
                        {
                            matrix[i, j] = Alphabet[index];
                        }
                        index += 1;
                    }
                }
            Print(matrix);

        }

        private String PlayfairCipher(String text)
        {
            String coded = "";
            char[,] matrix = new char[6, 6];
            InitMatrix(matrix); // Инициализирует матрицу
            return coded;
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
            label5.Show();
            dataGridView1.Show();
            if (radioButton1.Checked)
            {
                int K = Convert.ToInt32(textBox1.Text);
                var dict = SymbolNChance(text);
                dataGridView1.DataSource = dict.ToArray();
                dataGridView1.Refresh();
                String coded = CaesarEncrypt(text, K);
                richTextBox2.Text = coded;
                return;
            }
            if (radioButton2.Checked)
            {
                int K = Convert.ToInt32(textBox1.Text);
                var dict = SymbolNChance(text);
                dataGridView1.DataSource = dict.ToArray();
                dataGridView1.Refresh();
                String coded = MultiplicativeCipher(text, K);
                richTextBox2.Text = coded;
                return;
            }
            if (radioButton3.Checked)
            {
                label5.Hide();
                dataGridView1.Hide();
                PlayfairCipher(text);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String text = richTextBox1.Text;
            label5.Show();
            dataGridView1.Show();
            if (radioButton1.Checked)
            {
                int K = Convert.ToInt32(textBox1.Text);
                K = 36 - Math.Abs(K);
                String coded = CaesarEncrypt(text, K);
                richTextBox2.Text = coded;
                return;
            }
            if (radioButton2.Checked)
            {
                int K = Convert.ToInt32(textBox1.Text);
                MultiplicativeCipher(text, K);
                return;
            }
            if (radioButton3.Checked)
            {
                label5.Hide();
                dataGridView1.Hide();
                PlayfairCipher(text);
            }
        }
    }
}