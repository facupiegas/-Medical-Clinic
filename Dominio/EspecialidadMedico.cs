using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class EspecialidadMedico
    {
        #region Atributos y Properties
        public Especialidad Especialidad { set; get; }
        public DateTime FechaRecibimiento { set; get; }
        #endregion

        #region Constructor
        public EspecialidadMedico(Especialidad unaEspecialidad, DateTime unaFechaRecibimiento)
        {
            this.Especialidad = unaEspecialidad;
            this.FechaRecibimiento = unaFechaRecibimiento;
        }
        #endregion
    }
}
