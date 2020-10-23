﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeitonSystem.src.controller;
using System.Text.RegularExpressions;
using SeitonSystem.src.dto;

namespace SeitonSystem.src.view
{
    public partial class FinancasCadastrarView : Form
    {
        FinançasController finançasController;
        public FinancasCadastrarView()
        {
            InitializeComponent();
        

                try
                {
                    finançasController = new FinançasController();

                }
                catch (Exception e)
                {
                    enviaMsg(e.Message, "erro");
                }

            }
                                
            
         

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            try
                {
                    Finanças finanças= new Finanças
                    {
                        
                        Titulo = txt_titulo.Text,
                        Valor = double.Parse(txt_valor.Text),
                        Descricao = txt_descricao.Text
                    };
                    if (!Regex.Match(txt_valor.Text, "^[0-9]{0,4}[,]{0,1}[0-9]{1,}$").Success)
                    {
                        enviaMsg(" Informe o Valor  corretamente!", "aviso");
                    }
                    else if (finanças.Valor <= 0.00)
                    {
                        enviaMsg("Informe o Valor !", "aviso");
                    }

                    else if (!Regex.Match(txt_titulo.Text, "^[A-Za-zàáâãéèíóôúçÁÀÉÈÍÔÓÕÚÇ ]{1,80}$").Success)
                    {
                        enviaMsg("Informe o Título da atividade corretamente!", "aviso");
                    }


                    else
                    {
                        finançasController.InserirAtividade(finanças);
                        enviaMsg("Atividade Cadastrada com Sucesso", "check");
                        LimparForm();
                    }

                }
                catch (Exception)
                {
                    enviaMsg("Preencha todos os dados corretamente", "erro");
                }

            }

       

        private void enviaMsg(String msg, String tipo)
        {
            MensagensView message = new MensagensView(msg, tipo);
            message.ShowDialog();
        }
        public void LimparForm()
        {

            txt_valor.Clear();
            txt_titulo.Clear();
            txt_descricao.Clear();
                       
        }
        private void btn_limpar_Click_1(object sender, EventArgs e)
        {
            LimparForm();
        }

        
    }
}
