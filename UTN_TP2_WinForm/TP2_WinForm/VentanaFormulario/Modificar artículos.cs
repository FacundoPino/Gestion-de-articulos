﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP2_WinForm.Negocio;

namespace TP2_WinForm.VentanaFormulario
{
    public partial class Modificar_artículos : Form
    {
        private List<Articulos> listaArticulos;

        public Modificar_artículos()
        {
            InitializeComponent();
        }

        private void Modificar_artículos_Load(object sender, EventArgs e)
        {
            Globales.DiseñoDtv(ref dgvArticulos);

            cargarArticulos();
        }

        public void cargarArticulos()
        {
            ArticulosNegocio negocio = new ArticulosNegocio();
            listaArticulos = negocio.ListarArticulos();
            dgvArticulos.DataSource = negocio.ListarArticulos();
            Globales.DiseñoDtv(ref dgvArticulos);
            dgvArticulos.Columns["IdArticulo"].Visible = false;
            dgvArticulos.Columns["Imagen"].Visible = false;
            AgregariconoPc.Load(listaArticulos[0].Imagen);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cargarImagen(string imagen)
        {
            try
            {
                AgregariconoPc.Load(imagen);
            }
            catch (Exception)
            {
                AgregariconoPc.Load("https://motomagdperu.com/img/productos/no-image.png");
            }
        }

        private void dgvArticulos_Leave(object sender, EventArgs e)
        {

        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulos articulo = new Articulos();
            articulo = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;

            CategoriasNegocios categoriasNegocios = new CategoriasNegocios();
            MarcasNegocio marcasNegocio = new MarcasNegocio();

            try
            {
                cboCategoria.DataSource = categoriasNegocios.listarCategorias();
                cboCategoria.ValueMember = "IdCategoria";
                cboCategoria.DisplayMember = "Descripcion";
                cboMarca.DataSource = marcasNegocio.listarMarcas();
                cboMarca.ValueMember = "IdMarca";
                cboMarca.DisplayMember = "Descripcion";

                txtCodArticulo.Text = articulo.CodArticulo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtPrecio.Text = articulo.Precio.ToString();
                cargarImagen(articulo.Imagen);
                txtImagen.Text = articulo.Imagen.ToString();

                cboMarca.SelectedText = articulo.Marcas.Descripcion;
                cboCategoria.SelectedText = articulo.Categorias.Descripcion;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ArticulosNegocio articulosNegocio = new ArticulosNegocio();
            Articulos articulo = new Articulos();

            if(dgvArticulos.CurrentRow != null)
            {

             articulo = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;

             try
             {

                 articulo.Nombre = txtNombre.Text;
                 articulo.Descripcion = txtDescripcion.Text;
                 articulo.Marcas = (Marcas)cboMarca.SelectedItem;
                 articulo.Categorias = (Categorias)cboCategoria.SelectedItem;
                 articulo.Precio = decimal.Parse(txtPrecio.Text);
                 articulo.Imagen = txtImagen.Text;

                 if (articulo.CodArticulo != "" && articulo.Nombre != "" && articulo.Descripcion != "" && txtPrecio.Text != "")
                 {
                     articulosNegocio.ModificarArticulo(articulo);
                     articulosNegocio.ModificarMarca(articulo);
                     articulosNegocio.ModificarImagen(articulo);
                     articulosNegocio.ModificarCategoria(articulo);

                     MessageBox.Show("Modificado exitosamente :)");
                 }
                 else
                 {
                     MessageBox.Show("Complete todos los campos mi estimado/a");
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Complete todos los campos mi estimado/a");
             }
            }
            else
            {
                MessageBox.Show("Porfavor seleccione una fila");
            }
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Globales.decimales(txtPrecio, e);
        }
    }
}