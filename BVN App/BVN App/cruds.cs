using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Bvn_App
{
    public partial class cruds : Form
    {
        public cruds()
        {
            InitializeComponent();
        }

        ClassDao dao = new ClassDao();

        void LimparCampos()
        {
            //txtNome_Cli.Clear();
            //txtDDD.Clear();
            //txtPreço.Clear();
            //txtMin.Clear();
            //txtCelular.Clear();
            //lblID.Text = dao.NumRegistro().ToString(); // insere no label o número do próximo registro a cadastrar
        }

        void ExibirDados()
        {
            //lblID.Text = dao.campos.id.ToString();
            //txtNome_Cli.Text = dao.campos.Nome_Cli;
            //txtDDD.Text = dao.campos.DDD;
            //txtPreço.Text = dao.campos.Telefone.ToString();
            //txtCelular.Text = dao.campos.Celular.ToString();
            //txtMin.Text = dao.campos.Email.ToString();
            //btnAlterar.Enabled = true;
            //btnDeletar.Enabled = true;
            //btnSalvar.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // estabelecer conexão com o BD
            try
            {
                dao.Conecte("freedbtech_Teste", "TabCadastro");
                dao.PreencheTabela(dataGridView1);
            }
            catch
            {
                dao.Conecte("freedbtech_Teste", "TabCadastro");
                dao.PreencheTabela(dataGridView1);
            }
            LimparCampos();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            //inserir
            //if (txtNome_Cli.Text=="" || txtDDD.Text=="" || txtPreço.Text == "" || txtCelular.Text == "")
            //{
            //    MessageBox.Show("Campos em branco", "AVISO");
            //}
            
            //else
            //{
            //    dao.Insere(txtNome_Cli.Text, txtDDD.Text, txtPreço.Text, int.Parse(txtCelular.Text), int.Parse(txtMin.Text));
            //    MessageBox.Show("Registro gravado com sucesso", "Informação do Sistema");
            //    LimparCampos();
            //    dao.PreencheTabela(dataGridView1);
            //    btnSalvar.Enabled = false;
            //}
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            ////consultar por Nome_Cli
            //if (txtNome_Cli.Text != "")
            //{
            //    dao.Consulta(txtNome_Cli.Text);
            //    ExibirDados();
            //}
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            //// alterar
            //dao.Atualiza(txtNome_Cli.Text, txtDDD.Text, txtPreço.Text, int.Parse(txtCelular.Text), int.Parse(lblID.Text), int.Parse(txtMin.Text));

            //dao.PreencheTabela(dataGridView1);
            //MessageBox.Show("Registro alterado com sucesso", "AVISO");
        }

        private void BtnDeletar_Click(object sender, EventArgs e)
        {
            // deletar
            //if (MessageBox.Show("Deseja mesmo excluir esse registro?", "AVISO DE EXCLUSÃO!!!",
            //                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    dao.Deleta(int.Parse(lblID.Text));
            //    MessageBox.Show("Registro excluído com sucesso");
            //    dao.PreencheTabela(dataGridView1);
            //    LimparCampos();
            //    btnAlterar.Enabled = false;
            //    btnDeletar.Enabled = false;
            //}
            //else
            //{
            //    MessageBox.Show("Registro mantido");
            //}
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dao.Abrir();
            int id;
            string cnpj2;
            int numLinha = e.RowIndex;
            if (numLinha >= 0)
            {
                cnpj2 = dataGridView1.Rows[numLinha].Cells[1].Value.ToString();

                MySqlCommand comando = new MySqlCommand("select * from TabCadastro where cnpj= '" + cnpj2 + "'", dao.minhaConexao);
                MySqlDataReader dtReader = comando.ExecuteReader();
                if (dtReader.Read())
                {
                    id = int.Parse(dtReader["ID"].ToString());
                    Ficha ficha = new Ficha(id);
                    ficha.Show();
                }
            }
            dao.Fechar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != ""  && comboBox1.Text != "")
            {
            MySqlDataAdapter meuAdapter = new MySqlDataAdapter("SELECT Nome_Cli, Cnpj, DDD, Estado FROM TabCadastro WHERE " + comboBox1.Text + " LIKE '%" + textBox1.Text + "%'", dao.minhaConexao);
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.Clear();
            meuAdapter.Fill(dataSet, "TabCadastro");
            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = "TabCadastro";
            }

        }

        private void cadastroPorCnpjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            using (Consulta_Cnpj form2 = new Consulta_Cnpj())
                form2.ShowDialog();
            dao.PreencheTabela(dataGridView1);
            Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}