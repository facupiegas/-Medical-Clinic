using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class Especialidad
    {
        #region Atributos y Properties
        //Atributos de Clase
        // Autonumerado
        private static int _UltimoNro = 0;
        public static int UltimoNumero
        {
            get { return _UltimoNro; }
        }
        // Atributos de Instancia
        public int IdEspecialidad { set; get; }
        public string NombreEspecialidad { set; get; }
        public double ValorHora { set; get; }
        #endregion

        #region Constructores
        public Especialidad(string unNombreEspecialidad, double unValorHora)
        {
            _UltimoNro++;
            this.IdEspecialidad = _UltimoNro;
            this.NombreEspecialidad = unNombreEspecialidad;
            this.ValorHora = unValorHora;
        }
        #endregion

        #region Otros Metodos
        #endregion
    }
}
