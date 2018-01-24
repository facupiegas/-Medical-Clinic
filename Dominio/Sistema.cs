using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class Sistema
    {
        #region Atributos y Properties
        public List<Usuario> ListaUsuarios { set; get; }
        public List<Medico> ListaMedicos { set; get; }
        public List<Paciente> ListaPacientes { set; get; }
        public List<Especialidad> ListaEspecialidades { set; get; }
        public List<Reserva> ListaReservas { set; get; }
        private static Sistema instancia;
        public static Sistema Instancia
        {
            get
            {
                if (instancia == null)
                {
                    if (File.Exists("D:/datos.dat"))
                    {
                        instancia = Deserializar();
                    }
                    else
                    {
                        instancia = new Sistema();
                        Instancia.CargaDatosPrueba();
                    }
                    CargaValoresAtributosDeClase();
                }
                return instancia;
            }
        }
        #endregion

        #region Constructor
        private Sistema()
        {
            this.ListaUsuarios = new List<Usuario>();
            this.ListaMedicos = new List<Medico>();
            this.ListaPacientes = new List<Paciente>();
            this.ListaEspecialidades = new List<Especialidad>();
            this.ListaReservas = new List<Reserva>();
        }
        #endregion

        #region Busqueda y Validaciones de Objetos
        public Medico BuscarMedico(int unNumeroLicencia)
        {
            Medico retorno = null;
            foreach (Medico tmpMedico in ListaMedicos)
            { //Recorro lista de Medicos y busco alguno con el Numero de Licencia ingresado por parametro
                if (tmpMedico.NumLicencia == unNumeroLicencia)
                {
                    retorno = tmpMedico; //si encuentro uno lo guardo en la variable
                }

            }
            return retorno; //devuelvo el objeto encontrado o en caso contrario null
        }

        public Reserva BuscarReservaPorId(int unId)
        {
            Reserva retorno = null;
            foreach (Reserva tmpReserva in ListaReservas)
            {//Recorro lista de Reservas y busco alguna con el id ingresado por parametro
                if (tmpReserva.IdReserva == unId)
                {
                    retorno = tmpReserva;//si encuentro uno lo guardo en la variable
                }

            }
            return retorno;//devuelvo el objeto encontrado o en caso contrario null
        }

        public bool YaExisteReserva(DateTime unaFecha, Medico unMedico)
        {
            bool retorno = false;
            foreach (Reserva tmpReserva in ListaReservas)
            {//Recorro lista de Reservas y busco alguna con la fecha y medico ingresados por parametro
                if (tmpReserva.Medico.NumLicencia == unMedico.NumLicencia)
                {
                    int unaHoraMenos = tmpReserva.Fecha.Hour - 1;
                    int unaHoraMas = tmpReserva.Fecha.Hour + 1;
                    //valido que el medico no este ocupado una hora antes ni una hora despues(las consultas duran 1 hora) para evitar solapamientos, por ejemplo una consulta a las 15:00 y otra a las 15:20
                    bool estaEnUnHorarioOcupado = unaFecha.Hour > unaHoraMenos && unaFecha.Hour < unaHoraMas;

                    if (unaFecha.Date == tmpReserva.Fecha.Date && estaEnUnHorarioOcupado && !tmpReserva.fueCancelada) {
                        retorno = true; //si la reserva existe devuelvo true
                    }
                }
            }
            return retorno;
        }

        public Paciente BuscarPaciente(string unaCedula)
        {
            Paciente retorno = null;
            foreach (Paciente tmpPaciente in ListaPacientes)//recorro la lista de pacientes
            {
                if (tmpPaciente.Cedula == unaCedula)//si el paciente tiene la misma cedula a la ingresada por parametro
                {
                    retorno = tmpPaciente; //guardo el objeto
                }
            }
            return retorno;
        }

        public Usuario ValidarUsuario(string unNombreUsuario,string unPassword)
        {
            Usuario retorno = null;
            foreach (Usuario tmpUsuario in ListaUsuarios)//recorro la lista de usuarios
            {
                if (tmpUsuario.NombreUsuario == unNombreUsuario && tmpUsuario.contrasena == unPassword)//si el usuario tiene mismo nombre y pass a los ingresados por parametro
                {
                    retorno = tmpUsuario; //guardo el objeto
                }
            }
            return retorno;
        }

        public Especialidad BuscarEspecialidad(int unId)
        {
            Especialidad retorno = null;
            foreach (Especialidad tmpEspecialidad in ListaEspecialidades)//recorro la lista de especialidades
            {
                if (tmpEspecialidad.IdEspecialidad == unId)//si la especilaidad tiene el mismo id al ingresado por parametro
                {
                    retorno = tmpEspecialidad;//guardo el objeto
                }
            }
            return retorno;
        }

        public Usuario BuscarUsuario(string unNombreUsuario)
        {
            Usuario retorno = null;
            foreach (Usuario tmpUsuario in ListaUsuarios)//recorro la lista de usuarios
            {
                if (tmpUsuario.NombreUsuario == unNombreUsuario)//si el usuario tiene el mismo nombre al ingresado por parametro
                {
                    retorno = tmpUsuario;//guardo el objeto
                }
            }
            return retorno;
        }

        public Paciente BuscarPacientePorUsuario(Usuario unUsuario)
        {
            Paciente retorno = null;
            foreach (Paciente p in ListaPacientes)//recorro la lista de pacientes
            {
                if(p.Usuario.NombreUsuario == unUsuario.NombreUsuario)//si el nombre de usuario del paciente es igual al nombre de usuario del objeto usuario ingresado por parametro
                {
                    retorno = p;//guardo el objeto
                }
            }
            return retorno;
        }

        public Medico BuscarMedicoPorUsuario(Usuario unUsuario)
        {
            Medico retorno = null;
            foreach (Medico m in ListaMedicos)//recorro la lista de medicos
            {
                if (m.Usuario.NombreUsuario == unUsuario.NombreUsuario)//si el nombre de usuario del medico es igual al nombre de usuario del objeto usuario ingresado por parametro
                {
                    retorno = m;//guardo el objeto
                }
            }
            return retorno;
        }
        #endregion

        #region Altas y Bajas de objetos
        public void AltaEspecialidadMedico(Especialidad unaEspecialidad, DateTime unaFechaRecibimiento,Especialista unEspecialista)
        {
            EspecialidadMedico tmpEspecialidadMedico = new EspecialidadMedico(unaEspecialidad, unaFechaRecibimiento);
            unEspecialista.ListaEspecialidades.Add(tmpEspecialidadMedico);
            Serializar();
        }
        
        public void AltaEspecialidad(string unNombreEspecialidad, double unValorHora)
        {
            Especialidad tmpEspecialidad = new Especialidad(unNombreEspecialidad, unValorHora);
            ListaEspecialidades.Add(tmpEspecialidad);
            Serializar();
        }

        public bool AltaPaciente(string unNombreCompleto, string unEmail, string unTelefono, string unaCedula, Usuario unUsuario)
        {
            bool retorno = false;
            if (this.BuscarPaciente(unaCedula) == null)
            { //Compruebo que no exista ningun Paciente con la cedula ingresada por parametro
                Paciente tmpPaciente = new Paciente(unNombreCompleto, unEmail, unTelefono, unaCedula, unUsuario);
                ListaPacientes.Add(tmpPaciente);
                retorno = true;
                Serializar();
            }
            return retorno;
        }

        public bool AltaMedicoGeneral(DateTime unaFechaRecibimiento, int unNumLicencia, string unNombreCompleto, string unaDireccion, string unTelefono, bool unEsExclusivo, Usuario unUsuario)
        {
            bool retorno = false;
            if (this.BuscarMedico(unNumLicencia) == null)
            { //Compruebo que no exista ningun Medico con ese numero de licencia
                Medico tmpMedico = new General(unaFechaRecibimiento, unNumLicencia, unNombreCompleto, unaDireccion, unTelefono, unEsExclusivo,unUsuario);
                ListaMedicos.Add(tmpMedico);
                retorno = true;
                Serializar();
            }
            return retorno;
        }

        public bool AltaMedicoEspecialista(int unNumLicencia, string unNombreCompleto, string unaDireccion, string unTelefono, bool unEsExclusivo, Usuario unUsuario)
        {
            bool retorno = false;
            if (this.BuscarMedico(unNumLicencia) == null)
            { //Compruebo que no exista ningun Medico con ese numero de licencia
                Medico tmpMedico = new Especialista(unNumLicencia, unNombreCompleto, unaDireccion, unTelefono, unEsExclusivo, unUsuario);
                ListaMedicos.Add(tmpMedico);
                retorno = true;
                Serializar();
            }

            return retorno;
        }

        public bool AltaReservaEspecialista(DateTime unaFecha, Medico unMedico, Paciente unPaciente, Especialidad unaEspecialidad)
        {
            bool retorno = false;
            if (!this.YaExisteReserva(unaFecha,unMedico) && unaFecha > DateTime.Now)
            { //Compruebo que no exista ningun Medico con ese numero de licencia
                Reserva tmpReserva = new Reserva(unaFecha, unMedico,unPaciente,unaEspecialidad);
                ListaReservas.Add(tmpReserva);
                unPaciente.ListaReservasPaciente.Add(tmpReserva);
                retorno = true;
                Serializar();
            }

            return retorno;
        }

        public bool AltaReservaGeneral(DateTime unaFecha, Medico unMedico, Paciente unPaciente)
        {
            bool retorno = false;
            if (!this.YaExisteReserva(unaFecha, unMedico) && unaFecha > DateTime.Now)
            { //Compruebo que no exista ningun Medico con ese numero de licencia
                Especialidad tmpEspecialidad = new Especialidad("N/A",1);
                Reserva tmpReserva = new Reserva(unaFecha, unMedico, unPaciente, tmpEspecialidad);
                ListaReservas.Add(tmpReserva);
                unPaciente.ListaReservasPaciente.Add(tmpReserva);
                retorno = true;
                Serializar();
            }

            return retorno;
        }

        public bool BajaReserva(Reserva unaReserva)
        {
            bool retorno = true;
            if (unaReserva.Fecha < DateTime.Now) //Si la fecha de la reserva es menor a la del dia actual no se puede dar de baja la reserva
            {
                retorno = false;
            }
            else
            {
                unaReserva.fueCancelada = true;
                Serializar();
            }
            return retorno;
        }
        #endregion

        #region Otros Metodos

        public void CargaDatosPrueba() //Datos de prueba para testeo
        {
            //Usuarios para Medicos
            Usuario medico = new Usuario(Usuario.EnumRol.Medico, "med", "123");
            Usuario medico2 = new Usuario(Usuario.EnumRol.Medico, "med2", "123");
            Usuario medico3 = new Usuario(Usuario.EnumRol.Medico, "med3", "123");
            Usuario medico4 = new Usuario(Usuario.EnumRol.Medico, "med4", "123");
            Usuario medico5 = new Usuario(Usuario.EnumRol.Medico, "med5", "123");
            Usuario medico6 = new Usuario(Usuario.EnumRol.Medico, "med5", "123");
            //Medico General
            AltaMedicoGeneral(DateTime.Now, 1, "Carlos General", "Yaguaron 1414", "099999", false, medico);
            AltaMedicoGeneral(DateTime.Now, 2, "Martin General", "Republica 1211", "099459", true, medico2);
            AltaMedicoGeneral(DateTime.Now, 3, "Alberto General", "Miguelete 3214", "0996579", false, medico3);
            //Medico Especialista
            AltaMedicoEspecialista(4, "Roberto Especialista", "Paysandu 1828", "099123", false, medico4);
            AltaMedicoEspecialista(5, "Juan Especialista", "Colonia 1165", "099923", true, medico5);
            AltaMedicoEspecialista(6, "Pedro Especialista", "Mercedes 1256", "099874", true, medico6);
            ListaUsuarios.Add(medico);
            ListaUsuarios.Add(medico2);
            ListaUsuarios.Add(medico3);
            ListaUsuarios.Add(medico4);
            ListaUsuarios.Add(medico5);
            ListaUsuarios.Add(medico6);
            //Usuario administrador
            Usuario administrador = new Usuario(Usuario.EnumRol.Administrador, "admin", "123");
            ListaUsuarios.Add(administrador);
            //Usuarios para Paciente
            Usuario paciente = new Usuario(Usuario.EnumRol.Paciente, "pac", "123");
            Usuario paciente2 = new Usuario(Usuario.EnumRol.Paciente, "pac2", "123");
            Usuario paciente3 = new Usuario(Usuario.EnumRol.Paciente, "pac3", "123");
            Usuario paciente4 = new Usuario(Usuario.EnumRol.Paciente, "pac4", "123");
            Usuario paciente5 = new Usuario(Usuario.EnumRol.Paciente, "pac5", "123");
            Usuario paciente6 = new Usuario(Usuario.EnumRol.Paciente, "pac6", "123");
            ListaUsuarios.Add(paciente);
            ListaUsuarios.Add(paciente2);
            ListaUsuarios.Add(paciente3);
            ListaUsuarios.Add(paciente4);
            ListaUsuarios.Add(paciente5);
            ListaUsuarios.Add(paciente6);
            //Paciente
            AltaPaciente("Franco Paciente", "franco@gmail.com", "099654", "12345", paciente);
            AltaPaciente("Ricardo Paciente", "riky@hotmail.com", "099124", "54321", paciente2);
            AltaPaciente("Pedro Paciente", "pedrito@gmail.com", "099098", "56789", paciente3);
            AltaPaciente("Alberto Paciente", "beto@gmail.com", "099987", "98765", paciente4);
            AltaPaciente("Gloria Paciente", "gloria@gmail.com", "098754", "32164", paciente5);
            AltaPaciente("Chingolo Paciente", "chingolo@gmail.com", "099592", "76234", paciente6);
            //Especialidad
            AltaEspecialidad("Cirujano",100);
            AltaEspecialidad("Orejologo", 50);
            AltaEspecialidad("Ojologo", 80);
            AltaEspecialidad("Currar", 150);
            AltaEspecialidad("Patologo", 40);
            AltaEspecialidad("Corazonologo", 180);
            //Reservas
            AltaReservaEspecialista(DateTime.Now.AddMonths(5),ListaMedicos[3],ListaPacientes[0],ListaEspecialidades[0]);
            AltaReservaEspecialista(DateTime.Now.AddMonths(6), ListaMedicos[3], ListaPacientes[0], ListaEspecialidades[0]);
            AltaReservaEspecialista(DateTime.Now.AddMonths(8), ListaMedicos[4], ListaPacientes[1], ListaEspecialidades[1]);
            AltaReservaEspecialista(DateTime.Now.AddMonths(10), ListaMedicos[5], ListaPacientes[2], ListaEspecialidades[2]);
            AltaReservaGeneral(DateTime.Now.AddMonths(1), ListaMedicos[0], ListaPacientes[3]);
            AltaReservaGeneral(DateTime.Now.AddDays(2), ListaMedicos[1], ListaPacientes[4]);
            AltaReservaGeneral(DateTime.Now.AddDays(56), ListaMedicos[2], ListaPacientes[5]);
            //EspecialidadMedico
            DateTime fecha = new DateTime(2015, 8, 12);
            DateTime fecha2 = new DateTime(2012, 12, 4);
            DateTime fecha3 = new DateTime(2011, 2, 5);
            DateTime fecha4 = new DateTime(2016, 8, 24);
            DateTime fecha5 = new DateTime(2010, 2, 15);
            DateTime fecha6 = new DateTime(2003, 7, 19);

            AltaEspecialidadMedico(ListaEspecialidades[0],fecha,(Especialista)ListaMedicos[3]);
            AltaEspecialidadMedico(ListaEspecialidades[1], fecha2, (Especialista)ListaMedicos[3]);
            AltaEspecialidadMedico(ListaEspecialidades[2], fecha3, (Especialista)ListaMedicos[3]);
            AltaEspecialidadMedico(ListaEspecialidades[3], fecha4, (Especialista)ListaMedicos[3]);

            AltaEspecialidadMedico(ListaEspecialidades[3], fecha, (Especialista)ListaMedicos[4]);
            AltaEspecialidadMedico(ListaEspecialidades[4], fecha5, (Especialista)ListaMedicos[4]);
            AltaEspecialidadMedico(ListaEspecialidades[5], fecha4, (Especialista)ListaMedicos[4]);
            AltaEspecialidadMedico(ListaEspecialidades[2], fecha2, (Especialista)ListaMedicos[4]);

            AltaEspecialidadMedico(ListaEspecialidades[3], fecha3, (Especialista)ListaMedicos[5]);
            AltaEspecialidadMedico(ListaEspecialidades[5], fecha2, (Especialista)ListaMedicos[5]);
            AltaEspecialidadMedico(ListaEspecialidades[0], fecha, (Especialista)ListaMedicos[5]);
            AltaEspecialidadMedico(ListaEspecialidades[1], fecha6, (Especialista)ListaMedicos[5]);

            //Historias Clinicas Pacientes
            ListaMedicos[0].AgregarDetalleHistoriaClinica(ListaPacientes[0],"Una historia de clinica como nunca antes vista");
            ListaMedicos[0].AgregarDetalleHistoriaClinica(ListaPacientes[0], "Caso extraordinario de resfrio agudo");
            ListaMedicos[0].AgregarDetalleHistoriaClinica(ListaPacientes[0], "Dolor de cabeza inexplicable");
            ListaMedicos[0].AgregarDetalleHistoriaClinica(ListaPacientes[0], "Golpe severo del dedo chiquito del pie derecho contra la cama");
        }

        public List<Reserva> DevolverListaReservasMedico(int unNumLicencia)
        {
            List<Reserva> listaReservasMedicos = new List<Reserva>();
            foreach (Reserva tmpReserva in ListaReservas)
            {
                if (tmpReserva.Medico.NumLicencia == unNumLicencia) // si el numero de licencia ingresado por parametros es igual al del medico de la reserva la agrego a la lista para devolver
                {
                    listaReservasMedicos.Add(tmpReserva);
                }
            }
            return listaReservasMedicos;
        }

        public List<Reserva> DevolverListaReservasPaciente(int unNumeroMatricula)
        {
            List<Reserva> listaReservasPaciente = new List<Reserva>();
            foreach (Reserva tmpReserva in ListaReservas)
            {
                if (tmpReserva.Paciente.NumeroMatricula == unNumeroMatricula) // si el numero de licencia ingresado por parametros es igual al del medico de la reserva la agrego a la lista para devolver
                {
                    listaReservasPaciente.Add(tmpReserva);
                }
            }
            return listaReservasPaciente;
        }

        public List<Reserva> DevolverListaReservasPacienteSinCancelar(int unNumeroMatricula)
        {
            List<Reserva> listaReservasPaciente = new List<Reserva>();
            foreach (Reserva tmpReserva in ListaReservas)
            {
                if (tmpReserva.Paciente.NumeroMatricula == unNumeroMatricula && !tmpReserva.fueCancelada) // si el numero de licencia ingresado por parametros es igual al del medico de la reserva la agrego a la lista para devolver
                {
                    listaReservasPaciente.Add(tmpReserva);
                }
            }
            return listaReservasPaciente;
        }

        public bool StringEsSoloNumeros(string unString)
        {
            foreach (char c in unString)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }

        public List<Medico> MedicosOrdenadosPorSueldo()
        {
            ListaMedicos.Sort();//ordeno por sueldo
            ListaMedicos.Reverse();//invierto el orden para obtener el orden descendente
            return ListaMedicos;
        }

        public List<Paciente> DevolverPacientesMedicoPorFecha(Medico unMedico,DateTime unaFecha)
        {
            List<Paciente> retorno = new List<Paciente>();
            foreach (Reserva tmpReserva in ListaReservas)//recorro la lista de reservas
            {
                if (tmpReserva.Medico.NumLicencia == unMedico.NumLicencia && !retorno.Contains(tmpReserva.Paciente)) //si el medico en la reserva es el mismo ingresado por parametro y el paciente no se encuentra en la lista
                {
                    if (tmpReserva.Fecha.Date == unaFecha.Date)
                    {
                        retorno.Add(tmpReserva.Paciente); //agrego el paciente a la lista a retornar
                    }
                }
            }
            return retorno;
        }

        public List<Paciente> PacientesConMasCancelaciones()
        {
            List<Paciente> retorno = new List<Paciente>();
            int maxCancelacion = 0;
            
            foreach (Paciente tmpPaciente in ListaPacientes)//recorro lista de Pacientes
            {
                int contador = 0;
                foreach (Reserva tmpReserva in tmpPaciente.ListaReservasPaciente)//recorro lista de reservas de cada Paciente
                {
                    if (tmpReserva.fueCancelada)//si la reserva fue cancelada sumo 1 al contador
                    {
                        contador++;
                    }
                }
                if (contador > maxCancelacion)//si el contador es mayor a la cantidad de reservas canceladas
                {
                    maxCancelacion = contador; 
                    retorno.Clear(); //limpio la lista
                    retorno.Add(tmpPaciente); //agrego al paciente con la cantidad maxima de cancelaciones a la lista para devolver
                }
                else if (contador == maxCancelacion && maxCancelacion != 0) // si el paciente tiene la misma cantidad de cancelaciones que el valor maximo y el mismo no es 0 (estado incial de la variable)
                {
                    retorno.Add(tmpPaciente); //agrego el paciente a la lista para devolver
                }
            }
            return retorno;
        }

        public List<string> DevolverHistoriaClinicaPaciente(Paciente unPaciente)
        {
            return unPaciente.HistoriaClinica;
        } 

        public List<Reserva> ListaReservasPacienteGeneral(Paciente unPaciente)
        {
            List<Reserva> retorno = new List<Reserva>();
            foreach (Reserva tmpReserva in unPaciente.ListaReservasPaciente)//recorro la lista de reservas
            {
                if (tmpReserva.Medico is General)//si el medico de la reserva es de tipo general
                {
                    retorno.Add(tmpReserva);//agrego la reserva a la lista
                }
            }
            return retorno;
        }

        public List<Reserva> ListaReservasPacientePorEspecialidad(Paciente unPaciente, Especialidad unaEspecialidad)
        {
            List<Reserva> retorno = new List<Reserva>();
            foreach (Reserva tmpReserva in unPaciente.ListaReservasPaciente)//recorro la lista de reservas del paciente
            {
                if (tmpReserva.Especialidad.IdEspecialidad == unaEspecialidad.IdEspecialidad)//si la reserva tiene la misma especialidad a la ingresada por parametro
                {
                    retorno.Add(tmpReserva); //agrego la reserva a la lista
                }
            }
            
            return retorno;
        }

        public List<Reserva> ListaReservasPacientePorFecha(Paciente unPaciente, DateTime unaFecha)
        {
            List<Reserva> retorno = new List<Reserva>();
            foreach (Reserva tmpReserva in unPaciente.ListaReservasPaciente)//recorro la lista de reservas del paciente ingresado por parametro
            {
                if (tmpReserva.Fecha.Date == unaFecha.Date)//si la reserva tiene la misma fecha a la ingresada por parametro
                {
                    retorno.Add(tmpReserva);//agrego la reserva a la lista
                }
            }

            return retorno;
        }

        public List<Medico> DevolverListaMedicosGeneralesDisponibles(DateTime unaFecha)
        {
            List<Medico> retorno = new List<Medico>();
            foreach (Medico tmpMedico in ListaMedicos)
            {
                if (!this.YaExisteReserva(unaFecha, tmpMedico) && tmpMedico is General) // si el medico esta disponible en esa fecha y hora lo agrego a la lista
                {
                    retorno.Add(tmpMedico);
                }
            }
            return retorno;
        }

        public List<Medico> DevolverListMedicosEspecialistaDisponibles(DateTime unaFecha,Especialidad unaEspecialidad)
        {
            List<Medico> retorno = new List<Medico>();
            foreach (Medico tmpMedico in ListaMedicos)//recorro la lista de medicos
            {
                if (tmpMedico is Especialista)//si el medico es de tipo especialista
                {
                    Especialista auxEspecialista = (Especialista)tmpMedico;
                    foreach (EspecialidadMedico tmpEspecialidadMedico in auxEspecialista.ListaEspecialidades)//recorro lista de espcialidades del medico
                    {
                        if (!this.YaExisteReserva(unaFecha, auxEspecialista) && tmpEspecialidadMedico.Especialidad.IdEspecialidad == unaEspecialidad.IdEspecialidad) // si el medico esta disponible en esa fecha y hora lo agrego a la lista
                        {
                            retorno.Add(tmpMedico); //lo agrego a la lista
                        }
                    }
                }
            }

            return retorno; 
        }

        private static void Serializar()
        {
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory+"datos.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, instancia);
            fs.Close();
        }

        private static Sistema Deserializar()
        {
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "datos.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            Sistema inst = bf.Deserialize(fs) as Sistema;
            fs.Close();
            return inst;
        }

        private static void CargaValoresAtributosDeClase()
        {
            StreamReader sr = null;
            try
            {
                sr = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "parametros.txt");
            }
            catch (Exception)
            {
                //Console.WriteLine("UPS!");
            }
            if (sr != null)
            {
                bool quedanLineas = true;
                while (quedanLineas)
                {
                    string linea = sr.ReadLine();
                    if (linea == null)
                    {
                        quedanLineas = false;
                    }
                    else
                    {
                        string[] claveValor = linea.Split(':');
                        string clave = claveValor[0];
                        double valor = double.Parse(claveValor[1]);
                        if (clave == "ficto")
                        {
                            Especialista.FictoEspecialista = valor;
                        }
                        else
                        {
                            foreach (Especialidad esp in Sistema.instancia.ListaEspecialidades)
                            {
                                if (esp.NombreEspecialidad == clave)
                                {
                                    esp.ValorHora = valor;
                                }
                            }
                        }

                    }
                }
            }
        }
        #endregion
    }
}
