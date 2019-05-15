using System;
namespace EjercicioUno
{
    public class Juego
    {
        #region "Enums"
        public enum eEstadojuego
        {
            MostrandoMenu,
            EjecutandoJuego,
            JuegoFinalizado
        }
        #endregion

        #region "Atributos"

        public eEstadojuego Estado { get; set; }

        #endregion
    }
}
