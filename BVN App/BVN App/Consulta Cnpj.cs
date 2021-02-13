using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using HtmlAgilityPack;
using System.Net;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Bvn_App
{
    public partial class Consulta_Cnpj : Form
    {
        ClassDao dao = new ClassDao();

        public Consulta_Cnpj()
        {
            InitializeComponent();
            dao.Conecte("freedbtech_Teste", "TabCadastro");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var web = new WebClient();
            web.Encoding = Encoding.UTF8;



            string code = textBox1.Text;

            string[] rem = new string[] { ".", "/", ".", "-" };

            foreach (string pon in rem)
            {
                code = code.Replace(pon, string.Empty);
            }

            if (textBox1.Text != string.Empty)
            {
                string cnpj = web.DownloadString("https://www.receitaws.com.br/v1/cnpj/" + code);
                var document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(cnpj);
                Post post = JsonConvert.DeserializeObject<Post>(cnpj);

                // formating phone number
                if (post.status != "ERROR")
                {
                    if (post.situacao != "BAIXADA")
                    {
                        string DDD = string.Empty;
                        int test = post.telefone.IndexOf("(");
                        if (test >= 0)
                        {
                            string[] telefones = post.telefone.Split('/');
                            for (int i = 0; i < telefones.Length; i++)
                            {
                                telefones[i].Replace(" ", "");
                                telefones[i] = telefones[i].Remove(post.telefone.IndexOf("("), 5);
                            }
                            label5.Text = "Telefone: " + string.Join(" | ", telefones);

                            DDD = post.telefone.Substring(post.telefone.IndexOf("("), 3);
                            DDD = DDD.Replace("(", "");
                        }

                        string endereco = post.logradouro + " - " + post.numero;

                        label1.Text = "Nome: " + post.nome;
                        label2.Text = "Email: " + post.email;
                        label3.Text = "Cnpj: " + post.cnpj;
                        label4.Text = "Cep: " + post.cep;
                        label6.Text = "Abertura: " + post.abertura;
                        label7.Text = "Tipo: " + post.tipo;
                        label8.Text = "Estado: " + post.uf;
                        label9.Text = "Municipio: " + post.municipio;
                        label10.Text = "Endereco: " + endereco;
                        label11.Text = "DDD: (" + DDD + ")";


                        if (DialogResult.Yes == MessageBox.Show("Deseja salvar", "Cadastrar Cliente", MessageBoxButtons.YesNo))
                        {
                            dao.Insere(post.nome, post.email, post.cnpj, post.cep, post.telefone, post.abertura, post.uf, post.municipio, endereco, DDD);
                            MessageBox.Show("Registro gravado com sucesso", "Informação do Sistema");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Empresa Baixada");
                    }
                }
                else
                {
                    MessageBox.Show("Cnpj invalido");
                }
            }
        }

        private void Consulta_Cnpj_Load(object sender, EventArgs e)
        {

        }
    }
}

