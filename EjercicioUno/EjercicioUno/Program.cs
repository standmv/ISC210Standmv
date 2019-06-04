using System;
using System.Threading;

namespace EjercicioUno
{
    class Program
    {
        //TODO:
        //Se esta llamando tres veces la funcion ProximaFilaDisponible, es conveniente evitar eso.
        //inicializando el juego
        static Juego juegoActual = new Juego();

        //
        static int[] posicionesColumnas = new int[7] { 1, 3, 5, 7, 9, 11, 13 };
        //definiendo hilo para capturar la entrada de usuario
        static Thread hiloCaptura;

        //bloque que se utiliza para inicializar todo lo del juego
        static void InicializarJuego()
        {
            //adecuando juego listo para trabajar
            juegoActual.Estado = Juego.eEstadojuego.MostrandoMenu;
            juegoActual.TurnoPrimerJugador = true;
            juegoActual.Tablero = new bool?[6, 7];
            juegoActual.SiguienteColumna = -1;
            Console.CursorVisible = false;
            hiloCaptura = new Thread(new ThreadStart(CapturarTeclado));
            //se debe inicializar el hilo obligatoriamente para que trabaje
            hiloCaptura.Start();
        }

        static void Main(string[] args)
        {
            InicializarJuego();
            while(juegoActual.Estado != Juego.eEstadojuego.JuegoFinalizado)
            {

                //primero: verificar entrada del usuario.
                if(juegoActual.Estado == Juego.eEstadojuego.EjecutandoJuego && juegoActual.SiguienteColumna >= 0)
                {
                    juegoActual.Estado = Juego.eEstadojuego.Animando;

                    juegoActual.ProximaFila = juegoActual.ProximaFilaDisponible(juegoActual.SiguienteColumna);

                    Thread nuevoHilo = new Thread(new ThreadStart(AnimacionMoneda));
                    nuevoHilo.Start();
                    nuevoHilo.Join();

                    juegoActual.Tablero[juegoActual.ProximaFila, juegoActual.SiguienteColumna] = juegoActual.TurnoPrimerJugador;
                    if(juegoActual.VerificarFinalPartida(juegoActual.ProximaFila, juegoActual.SiguienteColumna))
                    {
                        juegoActual.Estado = Juego.eEstadojuego.JuegoFinalizado;
                        Console.WriteLine("Ha ganado el jugador: " + (juegoActual.TurnoPrimerJugador ? Juego.SPRITE1 : Juego.SPRITE2));
                    }
                    juegoActual.SiguienteColumna = -1;
                    juegoActual.TurnoPrimerJugador = !juegoActual.TurnoPrimerJugador;
                }
                //segundo: actualizar valor en funcion del estado actual.

                //Tercero; renderizar el juego.
                Renderizar();
                //20fps
                Thread.Sleep(50); 
            }

            Console.WriteLine("Gracias por jugar");
        }

        static void Renderizar()
        {
            switch (juegoActual.Estado)
            {
                case Juego.eEstadojuego.MostrandoMenu:
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Seleccione una opcion del menu: ");
                    Console.WriteLine("\n\t1: Jugar.");
                    Console.WriteLine("\n\t2: Salir.");
                    break;
                case Juego.eEstadojuego.EjecutandoJuego:
                    ImprimirEscena();
                    ImprimirTablero();
                    break;
            }
        }

        //Funcion
        static void CapturarTeclado()
        {
            string capturaActual;
            while (true)
            {
                //variable temporal para capturar la columna donde juega el jugador
                int _tmp;
                //esta linea hace que el hilo se detenga a esperar la entrada de usuario
                capturaActual = Console.ReadLine();
                switch (juegoActual.Estado)
                {
                    case Juego.eEstadojuego.MostrandoMenu:
                        if (capturaActual == "2")
                        {
                            juegoActual.Estado = Juego.eEstadojuego.JuegoFinalizado;
                            Thread.CurrentThread.Join();
                            Thread.CurrentThread.Abort();
                        }
                     
                        else if(capturaActual == "1")
                        {
                            juegoActual.Estado = Juego.eEstadojuego.EjecutandoJuego;
                        }
                        break;
                    case Juego.eEstadojuego.EjecutandoJuego:
                        if(Int32.TryParse(capturaActual, out _tmp) && _tmp >= 1 && _tmp <= 7)
                        {
                            //Selecciono una columna
                            juegoActual.SiguienteColumna = _tmp-1;
                        }
                        break;
                }
            }
        }

        static void ImprimirEscena()
        {
            Console.Clear();
            Console.WriteLine("CONNECT-4");
            Console.WriteLine("Turno Actual: " + (juegoActual.TurnoPrimerJugador ? "Primer Jugador" : "Segundo Jugador"));
            Console.WriteLine("\nSeleccione una columna [1-7]");
            Console.WriteLine(" 1 2 3 4 5 6 7");
            Console.WriteLine("---------------");
            Console.WriteLine("| | | | | | | |");
            Console.WriteLine("| | | | | | | |");
            Console.WriteLine("| | | | | | | |");
            Console.WriteLine("| | | | | | | |");
            Console.WriteLine("| | | | | | | |");
            Console.WriteLine("| | | | | | | |");
            Console.WriteLine("---------------");

        }

        static void ImprimirTablero()
        {
            short posicionInicial = 6;
            for(int i = 0; i <juegoActual.Tablero.GetLength(0); i++)
            {
                for(int j = 0; j < juegoActual.Tablero.GetLength(1); j++)
                {
                    if (juegoActual.Tablero[i, j] == null)
                        continue;
                    Console.SetCursorPosition(posicionesColumnas[j], posicionInicial + i);
                    Console.Write(juegoActual.Tablero[i,j] == true ? Juego.SPRITE1 : Juego.SPRITE2);
                }
            }
        }

        private static void AnimacionMoneda()
        {
            TimeSpan horaInicio = new TimeSpan(DateTime.Now.Ticks);
            short posicionAnterior, posicionInicial = 5, posicionFinal, posicionActual;
            posicionAnterior = posicionInicial;
            posicionFinal = (short)(juegoActual.ProximaFila +1);
            if (posicionFinal < 1)
                return;
            do
            {
                posicionActual = Convert.ToInt16(posicionInicial + Juego.GRAVEDAD * Math.Pow(new TimeSpan(DateTime.Now.Ticks).Subtract(horaInicio).TotalSeconds,2) / 2);
                Console.SetCursorPosition(0,posicionAnterior);
                Console.SetCursorPosition(posicionesColumnas[juegoActual.SiguienteColumna], posicionAnterior);
                Console.Write(" "); //Eliminamos la moneda anterior.

                Console.SetCursorPosition(posicionesColumnas[juegoActual.SiguienteColumna], posicionActual);
                Console.Write(juegoActual.TurnoPrimerJugador ? Juego.SPRITE1 : Juego.SPRITE2);
                posicionAnterior = posicionActual;
            }
            while (posicionActual <= (posicionInicial + posicionFinal));
            juegoActual.Estado = Juego.eEstadojuego.EjecutandoJuego;
            return;
        }
    }
}
