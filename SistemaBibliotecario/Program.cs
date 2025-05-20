using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaBibliotecario.UI;

namespace SistemaBibliotecario
{
    /// <summary>
    /// Classe principal do sistema bibliotecário.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// Abre o formulário inicial do sistema.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormIndex());
        }
    }
}
