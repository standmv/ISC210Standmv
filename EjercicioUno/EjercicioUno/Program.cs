using System;
using System.Threading;

namespace EjercicioUno
{
    class Program
    {
        //inicializando el juego
        static Juego juegoActual = new Juego();

        //definiendo hilo para capturar la entrada de usuario
        static Thread hiloCaptura;

        //bloque que se utiliza para inicializar todo lo del juego
        static void InicializarJuego()
        {
            //adecuando juego listo para trabajar
            juegoActual.Estado = Juego.eEstadojuego.MostrandoMenu;
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

                //segundo: actualizar valor en funcion del estado actual.

                //Tercero; renderizar el juego.
                Renderizar();
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
            }
        }

        //Funcion
        static void CapturarTeclado()
        {
            string capturaActual;
            while (true)
            {
                //esta linea hace que el hilo se detenga a esperar la entrada de usuario
                capturaActual = Console.ReadLine();
                switch (juegoActual.Estado)
                {
                    case Juego.eEstadojuego.MostrandoMenu:
                        if (capturaActual == "2")
                            juegoActual.Estado = Juego.eEstadojuego.JuegoFinalizado;
                        Thread.CurrentThread.Join();
                        Thread.CurrentThread.Abort();
                        break;
                }
            }
        }
    }
}
