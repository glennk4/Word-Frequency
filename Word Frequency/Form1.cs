using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; 

namespace Word_Frequency
{
    public partial class Form1 : Form
    {
        Dictionary<string, int> words = new Dictionary<string,int>(); 

        public Form1()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            ListOutput.Items.Clear(); 

            StreamReader inputFile;
            openFileDialog1.InitialDirectory = @"C:\Users\";

            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    List<string> original = new List<string>();
                    char delim = ' ';
                    string[] tokens;
                    
                    inputFile = File.OpenText(openFileDialog1.FileName);

                    while (!inputFile.EndOfStream)
                    {
                        string line = inputFile.ReadLine();
                        tokens = line.Split(delim);

                        foreach (string token in tokens)
                        {
                            original.Add(token); 
                        }
                    }

                    inputFile.Close();

                    PopulateDictionary(original); 
                    DisplayResults();
                }
                else
                {
                    MessageBox.Show("Operation Cancelled");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }


        private void PopulateDictionary(List<string> original)
        {
            List<int> occurances = new List<int>();

            //Count the occurances 
            for (int i = 0; i < original.Count; i++)
            {
                int counter = 1;
                for (int check = i + 1; check < original.Count; check++)
                {
                    if (original[i].ToLower().Equals(original[check].ToLower()))
                    {
                        counter++;
                    }
                }
                occurances.Add(counter);
            }

            //Remove duplicates and add to dictionary
            for (int i = 0; i < original.Count; i++)
            {
                for (int position = i + 1; position < original.Count; position++)
                {
                    if (original[i].ToLower().Equals(original[position].ToLower()))
                    {
                        original.RemoveAt(position);
                        occurances.RemoveAt(position);
                    }
                }
                words.Add(original[i], occurances[i]);
            }
  
        }


        private void DisplayResults()
        {
            foreach (KeyValuePair<string, int> element in words)
            {
                if (element.Value > 1)
                {
                    ListOutput.Items.Add("The word " + element.Key + " appears " + element.Value + " times"); 
                }
            }
        }
        
    }
}
