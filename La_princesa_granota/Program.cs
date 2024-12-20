using Heirloom;
using Heirloom.Desktop;

namespace MovimentImatge;

class Program
{
    const int GranotesInicials = 4;
    private static Window _finestra = null!;
    private static Cavaller _cavaller = null!;
    private static List<Granota> _granotes = new();
    private static 
    
    static void Main()
    {
        Application.Run(() =>
        {
            _finestra = new Window("La finestra", (800, 600));
            _finestra.MoveToCenter();
            _cavaller = new Cavaller(10,10);
            bool princesa = true;
            for (int i = 0; i < GranotesInicials; i++)
            {
                _granotes.Add(new Granota(princesa));
                princesa = false;
            }
            
            var loop = GameLoop.Create(_finestra.Graphics, OnUpdate);
            loop.Start();
        });
    }
    private static void OnUpdate(GraphicsContext gfx, float dt)
    {
        var rectangleFinestra = new Rectangle(0, 0, _finestra.Width, _finestra.Height);
        var rectangleGranota = new Rectangle(0, 0, _finestra.Width, _finestra.Height);
        _cavaller.Mou(rectangleFinestra);
        Granota eliminar = null;

        foreach (var granota in _granotes)
        {
            if (_cavaller.HaCapturatUnaGranota(granota))
            {
                if (granota.EsPrincesa)
                {
                    gfx.Clear();
                }
                else
                {
                    eliminar = granota
                }
            }
            
        }
        if (eliminar!= null)
        {
            _granotes.Remove(eliminar);
        }
        gfx.Clear(Color.Blue);

        _cavaller.Pinta(gfx);
        foreach (var granota in _granotes)
        {
            granota.Pinta(gfx);
        }
        
    }
}