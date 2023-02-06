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
            String text = textBox3.Text;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    matrix[i, j] = '-';
            int l = 0;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    matrix[i, j] = text[l];
                    l = l + 1;
                    if (l == text.Length - 1)
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

        private void IndexesOf(char elem, char[,] matrix, ref int index_i, ref int index_j)
        {
            for (int k = 0; k < 6; k++)
                for (int f = 0; f < 6; f++)
                {
                    if (matrix[k, f] == elem)
                    {
                        index_i = k;
                        index_j = f;
                        return;
                    }
                }
        }

        private String PlayfairCipher(String text)
        {
            String coded = "";
            char[,] matrix = new char[6, 6];
            InitMatrix(matrix); // Инициализирует матрицу


            int index_i_1 = 0, index_j_1 = 0, index_i_2 = 0, index_j_2 = 0;
            int i = 0;
            while (i < text.Length)
            {
                IndexesOf(text[i], matrix, ref index_i_1, ref index_j_1);
                if (i + 1 == text.Length || text[i] == text[i + 1])
                {
                    IndexesOf(text[i], matrix, ref index_i_2, ref index_j_2);
                }
                else
                {
                    IndexesOf(text[i + 1], matrix, ref index_i_2, ref index_j_2);
                }

                if (index_i_1 == index_i_2)
                {
                    if (index_j_1 == 5)
                    {
                        index_j_1 = 0;
                        index_j_2 += 1;
                    }
                    else if (index_j_2 == 5)
                    {
                        index_j_1 += 1;
                        index_j_2 = 0;
                    }
                    else
                    {
                        index_j_1 += 1;
                        index_j_2 += 1;
                    }
                }
                else if (index_j_1 == index_j_2)
                {
                    if (index_i_1 == 5)
                    {
                        index_i_1 = 0;
                        index_i_2 += 1;
                    }
                    else if (index_i_2 == 5)
                    {
                        index_i_1 += 1;
                        index_i_2 = 0;
                    }
                    else
                    {
                        index_i_1 += 1;
                        index_i_2 += 1;
                    }
                }
                else
                {
                    int b = index_i_1;
                    index_i_1 = index_i_2;
                    index_i_2 = b;
                }
                coded = coded + matrix[index_i_1, index_j_1] + matrix[index_i_2, index_j_2];
                i += 2;
            }
            return coded;

        }

        string PlayfairDecipher(string text)
        {
            String coded = "";
            char[,] matrix = new char[6, 6];
            InitMatrix(matrix); // Инициализирует матрицу

            int index_i_1 = 0, index_j_1 = 0, index_i_2 = 0, index_j_2 = 0;
            int i = 0;
            while (i < text.Length)
            {
                IndexesOf(text[i], matrix, ref index_i_1, ref index_j_1);
                IndexesOf(text[i + 1], matrix, ref index_i_2, ref index_j_2);

                if (index_i_1 == index_i_2)
                {
                    if (index_j_1 == 0)
                    {
                        index_j_1 = 5;
                        index_j_2 -= 1;
                    }
                    else if (index_j_2 == 0)
                    {
                        index_j_1 -= 1;
                        index_j_2 = 5;
                    }
                    else
                    {
                        index_j_1 -= 1;
                        index_j_2 -= 1;
                    }
                }
                else if (index_j_1 == index_j_2)
                {
                    if (index_i_1 == 0)
                    {
                        index_i_1 = 5;
                        index_i_2 -= 1;
                    }
                    else if (index_i_2 == 0)
                    {
                        index_i_1 -= 1;
                        index_i_2 = 5;
                    }
                    else
                    {
                        index_i_1 -= 1;
                        index_i_2 -= 1;
                    }
                }
                else
                {
                    int b = index_i_1;
                    index_i_1 = index_i_2;
                    index_i_2 = b;
                }
                if (matrix[index_i_1, index_j_1] != ',')
                    coded = coded + matrix[index_i_1, index_j_1];
                if (matrix[index_i_2, index_j_2] != '.')
                    coded = coded + matrix[index_i_2, index_j_2];

                i += 2;
            }
            return coded;
        }


        private Dictionary<char, float> SymbolNChance(String text)
        {
            var characterCount = new Dictionary<char, float>();
            foreach (var coded in text)
            {
                if (characterCount.ContainsKey(coded))
                    characterCount[coded]++;
                else
                    characterCount[coded] = 1;
            }
            foreach (var coded in characterCount.Keys)
            {
                characterCount[coded] = characterCount[coded] / 36;
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
                String coded = PlayfairCipher(text);
                richTextBox2.Text = coded;
                return;
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
                String coded = PlayfairDecipher(text);
                String newcoded = coded.Remove(coded.Length - 1, 1);
                richTextBox2.Text = newcoded;
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String text = richTextBox1.Text;
            String newtext = text.Replace(" ", "_").ToLower();
            richTextBox1.Text = newtext;
        }
    }
}