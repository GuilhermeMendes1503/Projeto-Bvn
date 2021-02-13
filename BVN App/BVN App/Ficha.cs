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

namespace Bvn_App
{
    public partial class Ficha : Form
    {
        ClassDao dao = new ClassDao();
        int idficha;

        void ExibirDados()
        {
            Nome.Text = "Nome: " + dao.campos.Nome_Cli.ToString();
            DDD.Text = "DDD: (" + dao.campos.DDD + ")";
            Telefone.Text = "Telefone: " + dao.campos.Telefone.ToString();
            Celular.Text = "Celular: " + dao.campos.Celular.ToString();
            Email.Text = "Email: " + dao.campos.Email.ToString();
            Endereço.Text = "Endereço: " + dao.campos.endereco.ToString();
            Cep.Text = "Cep: " + dao.campos.cep.ToString();
            Obs.Text = dao.campos.obs.ToString();
        }

        public Ficha(int id)
        {
            InitializeComponent();
            dao.Conecte("freedbtech_Teste", "TabCadastro");
            dao.Consulta(id);
            ExibirDados();
            this.Text = "[Ficha] " + dao.campos.Nome_Cli + id;
            idficha = id;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Obs_TextChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id.Text = "asd";
        }

        private void Ficha_Load(object sender, EventArgs e)
        {
            dao.PreencheTabelaVenda(dataGridView1);
        }

        private void Ficha_FormClosing(object sender, FormClosingEventArgs e)
        {
            dao.Abrir();
            MySqlCommand comando = new MySqlCommand("update TabCadastro set obs= '" + Obs.Text + "' where id='" + idficha + "'", dao.minhaConexao);
            comando.ExecuteNonQuery();
            dao.Fechar();
        }

        private void Confirmar_Click(object sender, EventArgs e)
        {
            if (op4.Text == "" || op5.Text == "" || op6.Text == "" || TipoDePagamento.Text == "")
            {
                MessageBox.Show("Campos em branco", "AVISO");
            }

            else
            {
                dao.InsereVenda(int.Parse(op4.Text), int.Parse(op5.Text), int.Parse(Frete.Text), TipoDePagamento.Text, TipoDoEnvio.Text, StatusDoPedido.Text, Frete.Text, int.Parse(ValorNotaFiscal.Text), N_NotaFiscal.Text, int.Parse(Desconto.Text));
                MessageBox.Show("Registro gravado com sucesso", "Informação do Sistema");
                dao.PreencheTabelaVenda(dataGridView1);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            id.Text = "asd";
        }
    }
}
