﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace WordRelay
{
    public partial class Form1 : Form
    {
        string url=null;
        string apikey=null;
        string type = null;
        List<string> history = new List<string>();

        XmlDocument xml = new XmlDocument();

        public Form1()
        {
            InitializeComponent();
            url = "https://krdict.korean.go.kr/api/search?key=";
            apikey = "EBB6D3290D88C645CF1452F7DA3229D0";
            type = "&part=word&pos=1&q=";
        }

        public bool searchWord(string search)
        {
            // Query문 만들기
            string query = url + apikey + type + search;
            // Request문 보내기.
            WebRequest request = WebRequest.Create(query);
            request.Method = "GET";

            // Response 받기.
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string result = reader.ReadToEnd();

            // XML로 만들어 주기.
            xml.LoadXml(result);
            stream.Close();


            XmlNodeList xnlist = xml.GetElementsByTagName("item");
            XmlNodeList word_count = xml.GetElementsByTagName("total");
            int count = int.Parse(word_count[0].InnerText);

            if (count == 0)
            {
                wordlist.Text += "존재하지 않습니다\r\n";
                return false;
            }
            else
            {
                string mean = xnlist[0]["sense"]["definition"].InnerText;
                wordlist.Text += mean+"\r\n";
                return true;
            }
        }

        public bool checkLastWord(string word)
        {
            if (history.Count == 0)
                return true;
            else
            {
                string lastword = history.Last();
                if (lastword[lastword.Length - 1] == word[0])
                    return true;
                else
                    return false;
            }
        }

        public bool checkDuplicate(string word)
        {
            if (history.Contains(word))
                return true;
            else
                return false;
        }


        private void wordInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string word = wordInput.Text;
                wordlist.Text += word+"\r\n";
                if (checkLastWord(word)==false)
                {
                    wordlist.Text += "끝 단어와 일치하지 않습니다.\r\n";
                    wordInput.Clear();
                }
                else
                {
                    if (checkDuplicate(word)==true)
                    {
                        wordlist.Text += "사용된 단어 입니다.\r\n";
                        wordInput.Clear();
                    }
                    else // 진짜 성공했을 때.
                    {
                        if (searchWord(word) == true)
                        {
                            history.Add(wordInput.Text);
                            textBox1.Text = "5";
                        }
                       
                        wordInput.Clear();
                    }
                }
                
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int count = int.Parse(textBox1.Text);
            
            if (--count == 0)
            {
                timer1.Stop();
                wordlist.Text += "------Game Over-----\r\n";
                wordInput.Enabled = false;
            }
            textBox1.Text = count.ToString();
            
        }
    }
}
