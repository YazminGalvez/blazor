namespace blazor.Components.Data
{
    public class ServicioJuegos
    {
        private List<Juego> juegos = new List<Juego>
        {
            new Juego{Identificador=1,Nombre="Ravel", Jugado=false},
            new Juego{Identificador=2,Nombre="Carcassonne", Jugado=true}
        };
        public Task<List<Juego>> ObtenerJuegos() => Task.FromResult(juegos);

        public Task AgregarJuego(Juego juego)
        {
            juegos.Add(juego);
            return Task.CompletedTask;
        }
        public void CambiarEstadoJugado(Juego juego)
        {
            var juegoExistente = juegos.FirstOrDefault(j => j.Identificador == juego.Identificador);
            if (juegoExistente != null)
            {
                juegoExistente.Jugado = !juegoExistente.Jugado;
            }
        }
    }
}