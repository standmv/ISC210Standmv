using System;
using System.Linq;
namespace EjercicioUno
{
    public class Juego
    {
        #region "Enums"
        public enum eEstadojuego
        {
            MostrandoMenu,
            EjecutandoJuego,
            Animando,
            JuegoFinalizado
        }
        #endregion

        #region "Atributos"

        public eEstadojuego Estado { get; set; }

        public bool TurnoPrimerJugador { get; set; }
        public string SiguienteAccion { get; set; }
        public int SiguienteColumna { get; set; }
        public const float GRAVEDAD = 9.8f;
        public bool?[,] Tablero { get; set; }
        public int ProximaFila { get; set; }
        public const string SPRITE1 = "O";
        public const string SPRITE2 = "0";
        #endregion

        #region "Comportamientos"

        public int ProximaFilaDisponible(int numeroColumna)
        {
            int filaActual = Tablero.GetLength(0);
            while (filaActual > 0 && Tablero[filaActual - 1, numeroColumna] != null)
            {
                filaActual--;
            }
            return filaActual - 1;
        }

        public bool VerificarFinalPartida(int f, int c)
        {
            int iMax, iMin, jMin, jMax, kMax, kMin, i, j, k;
            int[] consecutivo = new int[4];
            iMin = c - 3 < 0 ? 0 : c - 3;
            iMax = c + 3 >= Tablero.GetLength(0) ? Tablero.GetLength(0) - 1 : c + 3;

            jMin = f - 3 < 0 ? 0 : f - 3;
            jMax = f + 3 >= Tablero.GetLength(1) ? Tablero.GetLength(1)-1 : f + 3;

            kMax = f + 3 >= Tablero.GetLength(1) ? Tablero.GetLength(1)-1 : f + 3;
            kMin = f - 3 < 0 ? 0 : f - 3;
        
            for(i=iMin, j = jMin, k=kMax; i<=iMax || j<=jMax || k>=kMin; i++, j++, k--)
            {
                //Horizontales
                if (j<=jMax && Tablero[f, j] == TurnoPrimerJugador)
                    consecutivo[0]++;
                //Verticales
                if (i <= iMax && Tablero[i, c] == TurnoPrimerJugador)
                    consecutivo[1]++;

                //Este metodo de recuperacion de datos es llamado LINQ, permite al programador leer datos desde el mismo codigo fuente
                /*var linqTmp = (from contador in consecutivo
                               where contador == 4
                               select contador).Any();
                return linqTmp;*/

                if (consecutivo.Any(contador => contador == 4))
                    return true;
            }

            for (i = iMin, j = jMin, k = kMax; i <= iMax || j <= jMax || k >= kMin; i++, j++, k--)
            {
                //diagonal 1
                if (i <= iMax && j <= jMax && Tablero[i, j] == TurnoPrimerJugador)
                    consecutivo[2]++;

                if (consecutivo.Any(contador => contador == 4))
                    return true;
            }

            for (i = iMin, j = jMin, k = kMax; i <= iMax || j <= jMax || k >= kMin; i++, j++, k--)
            {
                //diagonal 2
                if (i <= iMax && k >= kMin && Tablero[i, k] == TurnoPrimerJugador)
                    consecutivo[3]++;

                if (consecutivo.Any(contador => contador == 4))
                    return true;
            }

            return false;
        }
        #endregion
    }
}
