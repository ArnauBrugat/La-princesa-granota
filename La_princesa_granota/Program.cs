using Heirloom;
using Heirloom.Desktop;

namespace MovimentImatge;

class Program
{
    const int GranotesInicials = 4;
    private static Window _finestra = null!;
    private static Cavaller _cavaller = null!;
    private static List<Granota> _granotes = new();
    private static Image PilotaOr = new Image("BalonOro.jpg");
    static void Main()
    {
        Application.Run(() =>
        {
            _finestra = new Window("La finestra", (800, 600));
            _finestra.MoveToCenter();
            _cavaller = new Cavaller(10, 10);

            bool princesa = true;
            for (int i = 0; i < GranotesInicials; i++)
            {
                _granotes.Add(new Granota(princesa));
                princesa = false;
            }
            bool gameOver = false;
            var loop = GameLoop.Create(_finestra.Graphics, (gfx, dt) =>
            {
                var rectangleFinestra = new Rectangle(0, 0, _finestra.Width, _finestra.Height);
                _cavaller.Mou(rectangleFinestra);
                Granota eliminar = null;
                if (gameOver)
                {
                    gfx.DrawImage(PilotaOr, rectangleFinestra);
                    return;
                }
                foreach (var granota in _granotes)
                {
                    if (_cavaller.HaCapturatUnaGranota(granota))
                    {
                        if (granota.EsPrincesa)
                        {
                            gameOver = true;
                        }
                        else
                        {
                            eliminar = granota;
                        }
                    }
                }
                if (eliminar != null)
                {
                    _granotes.Remove(eliminar);
                    _granotes.Add(new Granota(false));
                    _granotes.Add(new Granota(false));
                }
                gfx.Clear(Color.LightGray);
                _cavaller.Pinta(gfx);
                foreach (var granota in _granotes)
                {
                    granota.Pinta(gfx);
                }
                
            });

            loop.Start();
            
        });
        
    }
}