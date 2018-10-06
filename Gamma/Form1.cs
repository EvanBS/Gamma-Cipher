using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gamma
{
    public partial class GammaClass : Form
    {
        private int alphSize = 0;
        private string alphaUpper = null;
        private string alphaLower = null;
        private string sourceString = null;
        private string gamma = null;
        private string result = null;

        private string symbols = "0123456789!#$%^&*()+=-_'?.,| /`~№:;@[]{}";

        private static Random random = new Random();
        public string RandomString(int length)
        {
            string chars = alphaLower + alphaUpper + symbols;
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public GammaClass()
        {
            InitializeComponent();
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Init()
        {
            if (comboBox1.Text.ToString().Equals("Eng"))
            {
                alphaUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                alphaLower = "abcdefghijklmnopqrstuvwxyz";
            }
            else
            {
                alphaUpper = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
                alphaLower = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            }

            gamma = textBox1.Text.Length == 0 ? null : textBox1.Text.ToString();
            sourceString = textBox2.Text.Length == 0 ? null : textBox2.Text.ToString();
            alphSize = alphaLower.Length;

        }

        private int getIndex(char Letter)
        {
            int index = 0;
            if (Char.IsLower(Letter))
            {
                index = alphaLower.IndexOf(Letter);
            }
            else if (Char.IsUpper(Letter))
            {
                index = alphaUpper.IndexOf(Letter);
            }
            else
            {
                index = symbols.IndexOf(Letter);
            }

            return index;
        }

        private void Decrypt()
        {
            result = null;

            for (var i = 0; i < sourceString.Length; i++)
            {

                char sourceLetter = sourceString[i];
                char gammaLetter = gamma[i % gamma.Length];


                int indexSource = getIndex(sourceLetter);

                int indexGamma = getIndex(gammaLetter);


                if (Char.IsLower(sourceLetter))
                {
                    result += alphaLower[((indexSource + alphSize - (indexGamma % alphSize))) % alphSize];
                }
                else if (Char.IsUpper(sourceLetter))
                {
                    result += alphaUpper[((indexSource + alphSize - (indexGamma % alphSize))) % alphSize];
                }
                else
                {
                    result += symbols[((indexSource + symbols.Length - (indexGamma % symbols.Length))) % symbols.Length];
                }
            }


            textBox3.Text = null;
            textBox3.Text = result;
        }
        private void Encrypt()
        {
            result = null;



            for (var i = 0; i < sourceString.Length; i++)
            {

                char sourceLetter = sourceString[i];
                char gammaLetter = gamma[i % gamma.Length];

                int indexSource = getIndex(sourceLetter);
                int indexGamma = getIndex(gammaLetter);


                //MessageBox.Show(indexSource.ToString());

                //indexGamma = alphaLower.IndexOf(gamma[i]);

                //MessageBox.Show(((indexSource ^ indexGamma) % alphSize).ToString());


                if (Char.IsLower(sourceLetter))
                {
                    result += alphaLower[(indexSource + indexGamma) % alphSize];
                }
                else if (Char.IsUpper(sourceLetter))
                {
                    result += alphaUpper[(indexSource + indexGamma) % alphSize];
                }
                else
                {
                    result += symbols[(indexSource + indexGamma) % symbols.Length];
                }

                //result += alphaLower[(indexSource ^ indexGamma) % alphSize];

            }

            MessageBox.Show(RandomString(7));

            textBox3.Text = null;
            textBox3.Text = result;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Init();
            Encrypt();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Init();
            Decrypt();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
