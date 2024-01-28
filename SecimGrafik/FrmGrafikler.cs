using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SecimGrafik
{
	public partial class FrmGrafikler : Form
	{
		public FrmGrafikler()
		{
			InitializeComponent();
		}


		SqlConnection baglanti = new SqlConnection(@"Data Source = msyc; Initial Catalog = DBSECIMPROJE; Integrated Security = True");

		private void FrmGrafikler_Load(object sender, EventArgs e)
		{
			// İlçe Adlarını kombo Boxa Çekme
			baglanti.Open();

			SqlCommand komut = new SqlCommand("Select ILCEAD from TBLILCE", baglanti);
			SqlDataReader dr = komut.ExecuteReader();

			while (dr.Read())
			{
				comboBox1.Items.Add(dr[0]);
			}

			baglanti.Close();

			// Grafige Toplam Sonuçları Getirme

			baglanti.Open();

			SqlCommand komut2 = new SqlCommand("Select SUM(APARTI),SUM(BPARTI),SUM(CPARTI),SUM(DPARTI),SUM(EPARTI) From TBLILCE", baglanti);
			SqlDataReader dr2 = komut2.ExecuteReader();

			while (dr2.Read())
			{
				chart1.Series["Partiler"].Points.AddXY("A Parti", dr2[0]);
				chart1.Series["Partiler"].Points.AddXY("B Parti", dr2[1]);
				chart1.Series["Partiler"].Points.AddXY("C Parti", dr2[2]);
				chart1.Series["Partiler"].Points.AddXY("D Parti", dr2[3]);
				chart1.Series["Partiler"].Points.AddXY("E Parti", dr2[4]);
			}
			baglanti.Close();
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			baglanti.Open();

			SqlCommand komut3 = new SqlCommand("Select * From TBLILCE Where ILCEAD=@P1",baglanti);
			komut3.Parameters.AddWithValue("@P1", comboBox1.Text);

			SqlDataReader dr3 = komut3.ExecuteReader();
			while (dr3.Read())
			{
				progressBar1.Value = int.Parse(dr3[2].ToString());
				progressBar2.Value = int.Parse(dr3[3].ToString());
				progressBar3.Value = int.Parse(dr3[4].ToString());
				progressBar4.Value = int.Parse(dr3[5].ToString());
				progressBar5.Value = int.Parse(dr3[6].ToString());


				LblA.Text = dr3[2].ToString();
				LblB.Text = dr3[3].ToString();
				LblC.Text = dr3[4].ToString();
				LblD.Text = dr3[5].ToString();
				LblE.Text = dr3[6].ToString();

			}
			baglanti.Close();




		}
	}
}
